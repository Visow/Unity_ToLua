using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VisowFrameWork {

    public class ManagerName 
    {
        public const string Lua = "LuaManager";
        public const string Resource = "ResourceManager";
        public const string UIRoot = "UIRootManager";
        public const string Update = "UpdateManager";
        public const string NetWork = "NetWorkManager";
        public const string Loading = "LoadingManager";
    }

    public class ManagerBase : MonoBehaviour {
        virtual public void Close() { }
    }

    public class Manager
    {
        static Dictionary<string, object> m_Managers = new Dictionary<string, object>();
        static GameObject m_GameManager;

        /// <summary>
        /// 添加Unity对象
        /// </summary>
        public static T Add<T>(string typeName) where T : MonoBehaviour
        {
            object result = null;
            m_Managers.TryGetValue(typeName, out result);
            if (result != null)
            {
                return (T)result;
            }
            if (m_GameManager == null)
            {
                m_GameManager = new GameObject("GameManager");
                UnityEngine.Object.DontDestroyOnLoad(m_GameManager);
            }
            Component c = m_GameManager.AddComponent<T>();
            m_Managers.Add(typeName, c);
            return (T)c;
        }

        /// <summary>
        /// 获取系统管理器
        /// </summary>
        public static T Get<T>(string typeName) where T : MonoBehaviour
        {
            if (!m_Managers.ContainsKey(typeName))
            {
                return default(T);
            }
            object manager = null;
            m_Managers.TryGetValue(typeName, out manager);
            return (T)manager;
        }

        public static void Remove<T>(string typeName) where T : ManagerBase
        {
            object manager = null;
            m_Managers.TryGetValue(typeName, out manager);
            if (manager != null)
            {
                ((T)manager).Close();
                UnityEngine.GameObject.Destroy((T)manager);
                m_Managers.Remove(typeName);
            }
        }
    }
}

