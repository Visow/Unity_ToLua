using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using VisowFrameWork;

public class Main : MonoBehaviour {

	// Use this for initialization
    void Awake() {

    }
	void Start () {
        LuaManager luaManager = Util.LuaManager();
        luaManager.InitStart();
        
        // 初始化网络
        NetWorkManager netWorkManager = Util.NetWorkManager();

        // 资源初始化
        ResourceManager resManager = Util.ResManager();
        resManager.Initialize();

        if (CoreConst.DebugMode == false)
            luaManager.DoFile("PreMain.lua");
        else
            luaManager.DoFile("Main.lua");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
