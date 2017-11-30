using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using System;
namespace VisowFrameWork {
    public class LuaManager : ManagerBase
    {
        private LuaState lua;
        private LuaLoader loader;
        private LuaLooper loop = null;

        // Use this for initialization
        void Awake()
        {

        }

        public void InitStart()
        {
            loader = new LuaLoader();
            lua = new LuaState();
            this.OpenLibs();
            lua.LuaSetTop(0);
            LuaBinder.Bind(lua);
            LuaCoroutine.Register(lua, this);
            DelegateFactory.Init();

            InitLuaPath();
            InitLuaBundle();
            this.lua.Start();    //启动LUAVM
            this.StartLooper();
        }

        public void ReStart()
        {
            lua = new LuaState();

            this.OpenLibs();
            lua.LuaSetTop(0);

            LuaBinder.Bind(lua);
            LuaCoroutine.Register(lua, this);
            DelegateFactory.Init();

            this.lua.Start();    //启动LUAVM
            this.StartLooper();
        }

        void StartLooper()
        {
            loop = gameObject.AddComponent<LuaLooper>();
            loop.luaState = lua;
        }

        void OpenLibs()
        {
            lua.OpenLibs(LuaDLL.luaopen_pb);
//            lua.OpenLibs(LuaDLL.luaopen_sproto_core);
//            lua.OpenLibs(LuaDLL.luaopen_protobuf_c);
            lua.OpenLibs(LuaDLL.luaopen_lpeg);
            lua.OpenLibs(LuaDLL.luaopen_bit);
            lua.OpenLibs(LuaDLL.luaopen_socket_core);

            // for lua ide
            if (CoreConst.DebugMode)
                OpenLuaSocket();

            this.OpenCJson();
        }

        //cjson 比较特殊，只new了一个table，没有注册库，这里注册一下
        protected void OpenCJson()
        {
            lua.LuaGetField(LuaIndexes.LUA_REGISTRYINDEX, "_LOADED");
            lua.OpenLibs(LuaDLL.luaopen_cjson);
            lua.LuaSetField(-2, "cjson");

            lua.OpenLibs(LuaDLL.luaopen_cjson_safe);
            lua.LuaSetField(-2, "cjson.safe");
        }

        /// <summary>
        /// 初始化Lua代码加载路径
        /// </summary>
        void InitLuaPath()
        {
            if (CoreConst.DebugMode)
            {
                lua.AddSearchPath(FileUtil.DevLuaPath);
            }
            else
            {
                lua.AddSearchPath(FileUtil.DataPath + "lua");
				lua.AddSearchPath (FileUtil.DataPath + "lua/tolua");
            }
        }

        /// <summary>
        /// 初始化LuaBundle
        /// </summary>
        void InitLuaBundle()
        {

        }

        public void DoFile(string filename)
        {
            lua.DoFile(filename);
        }

        // Update is called once per frame
        public object[] CallFunction(string funcName, params object[] args)
        {
            LuaFunction func = lua.GetFunction(funcName);
            if (func != null)
            {
                return func.LazyCall(args);
            }
            return null;
        }

        public void LuaGC()
        {
            lua.LuaGC(LuaGCOptions.LUA_GCCOLLECT);
        }

        public override void Close()
        {
            if (loop != null)
            {
                loop.Destroy();
                loop = null;    
            }

            if (lua != null)
            {
                lua.Dispose();
                lua = null;
            }
            loader = null;
        }

        void OnDestroy()
        {
            Close();
            UnityEngine.Debug.Log("~LuaManager was destroy!");
        }

#region luaide 调试库添加
    //如果项目中没有luasocket 请打开
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int LuaOpen_Socket_Core(IntPtr L)
    {
        return LuaDLL.luaopen_socket_core(L);
    }

    protected void OpenLuaSocket()
    {
        LuaConst.openLuaSocket = true;
        lua.BeginPreLoad();
        lua.RegFunction("socket.core", LuaOpen_Socket_Core);
        lua.EndPreLoad();
    }
#endregion

    }
}


