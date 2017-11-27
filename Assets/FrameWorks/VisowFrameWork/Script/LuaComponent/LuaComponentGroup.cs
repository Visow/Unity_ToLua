using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VisowFrameWork {
    public class LuaComponentGroup : MonoBehaviour
    {
        [SerializeField]
        public List<string> luaComponenetName = new List<string>();
        [SerializeField]
        public List<LuaComponent> luaComponenetList = new List<LuaComponent>();

        public static LuaComponent CreateLuaComponent(string modelName)
        {
            object[] objectArr = Util.LuaManager().CallFunction("CreateLuaComponent", modelName);
            if (objectArr != null)
                return objectArr[0] as LuaComponent;
            return null;
        }

        void Awake()
        {
            for (int i = 0; i < luaComponenetName.Count; i++)
            {
                LuaComponent lua = CreateLuaComponent(luaComponenetName[i]);
                if (lua != null)
                {
                    lua.modelName = luaComponenetName[i];
                    lua.content = this;
                    lua.Awake();
                    luaComponenetList.Add(lua);
                }
            }
        }

        // Use this for initialization
        void Start()
        {
            for (int i = 0; i < luaComponenetList.Count; i++)
            {
                luaComponenetList[i].Start();
            }
        }

        void OnEnable() {
            for (int i = 0; i < luaComponenetList.Count; i++)
            {
                luaComponenetList[i].OnEnable();
            }
        }

        public void Focus(bool isFocus)
        {
            for (int i = 0; i < luaComponenetList.Count; i++)
            {
                luaComponenetList[i].Focus(isFocus);
            } 
        }

       void OnDestroy() {
            for (int i = 0; i < luaComponenetList.Count; i++)
            {
                luaComponenetList[i].OnDestroy();
            }
            luaComponenetList.Clear();
        }


        // Update is called once per frame
        //void Update()
        //{
        //    for (int i = 0; i < luaComponenetList.Count; i++)
        //    {
        //        luaComponenetList[i].Update();
        //    }
        //}

#if UNITY_EDITOR

#endif
    }
}

