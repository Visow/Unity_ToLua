using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibServer.Common {
    public abstract class BaseThread {
        protected Thread thread = null;
        private bool isRunning = false;
        public bool IsRuning
        {
           get {
            return isRunning;
           }
            set {
                if (value)
                {
                    Start();
                    return;
                }
                isRunning = value;
                if(thread != null && value == false)
                {
                    try
                    {
                        thread.Abort();
                    }
                    catch(Exception ) { }
                    
                    thread = null;
                }
            }

        }

        abstract public void run();

        

        public void Start() {
            if (isRunning == true)
            {
                return;
            }
            isRunning = true;
            thread = new Thread(run);
            thread.Start();
        }
    }
}
