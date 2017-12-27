using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LibServer.Common
{
    public abstract class SyncPool
    {
        private long lastCheckOut;

        private static Hashtable unlocked;
        private static Hashtable locked;
        internal static long GARBAGE_INTERVAL = 90 * 10000;

        static SyncPool() {
            locked = Hashtable.Synchronized(new Hashtable());
            unlocked = Hashtable.Synchronized(new Hashtable());
        }

        internal SyncPool() {
            lastCheckOut = DateTime.Now.Ticks;

            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Enabled = true;
            aTimer.Interval = GARBAGE_INTERVAL;
            aTimer.Elapsed += new ElapsedEventHandler(CollectGarbage);

        }

        protected abstract object Create();
        protected abstract bool Validate(object o);
        protected abstract void Close(object o);

        internal object Get()
        {
            long now = DateTime.Now.Ticks;
            lastCheckOut = now;
            object o = null;
            lock(this)
            {
                try 
                {
                    foreach(DictionaryEntry entry in unlocked)
                    {
                        o = entry.Key;
                        unlocked.Remove(o);
                        if(!Validate(o))
                        {
                            locked.Add(o, now);
                            return o;
                        }
                        else
                        {
                            Close(o);
                            o = null;
                        }
                    }
                }
                catch(Exception){}
                o = Create();
                locked.Add(o, now);
            }
            return o;
        }

        internal void Reset(object o)
        {
            if (o != null)
            {
                lock (this)
                {
                    if (Validate(o))
                        Close(o);
                    locked.Remove(o);
                    unlocked.Add(o, DateTime.Now.Ticks);
                }
            }
        }

        private void CollectGarbage(object sender, ElapsedEventArgs ea)
        {
            lock (this)
            {
                object o;
                long now = DateTime.Now.Ticks;
                IDictionaryEnumerator e = unlocked.GetEnumerator();
                try
                {
                    o = e.Key;
                    if (now - (long)e.Value > GARBAGE_INTERVAL)
                    {
                        unlocked.Remove(o);
                        Close(o);
                        o = null;
                    }
                }
                catch (Exception) { }
            }
        }

        public int Count {
            get {
                return unlocked.Count + locked.Count;
            }
        }
    }
}
