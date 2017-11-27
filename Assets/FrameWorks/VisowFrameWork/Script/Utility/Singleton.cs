using UnityEngine;

namespace VisowFrameWork {
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance
        {
            get
            {
                if (m_s_instance == null)
                {
                    T[] managers = Object.FindObjectsOfType(typeof(T)) as T[];
                    if (managers.Length != 0)
                    {
                        if (managers.Length == 1)
                        {
                            m_s_instance = managers[0];
                            m_s_instance.gameObject.name = typeof(T).Name;
                            return m_s_instance;
                        }
                        else
                        {
                            Debug.LogError("Class " + typeof(T).Name + " exists multiple times in violation of singleton pattern. Destroying all copies");
                            foreach (T manager in managers)
                            {
                                Destroy(manager.gameObject);
                            }
                        }
                    }
                    var go = new GameObject(typeof(T).Name, typeof(T));
                    m_s_instance = go.GetComponent<T>();
                }
                return m_s_instance;
            }
            set
            {
                m_s_instance = value as T;
            }
        }

        protected static T m_s_instance;
    }
}

