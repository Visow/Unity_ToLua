﻿using UnityEngine;

public static class LuaConst
{
    public static string luaDir = Application.dataPath + "/Game/Lua";
    public static string toluaDir = Application.dataPath + "/FrameWorks/ToLua/Lua";

#if UNITY_STANDALONE
    public static string osDir = "Win";
#elif UNITY_ANDROID
    public static string osDir = "Android";            
#elif UNITY_IPHONE
    public static string osDir = "iOS";        
#else
    public static string osDir = "";        
#endif

    public static string luaResDir = string.Format("{0}/{1}/Lua", Application.persistentDataPath, osDir);      //手机运行时lua文件下载目录    


    public static bool openLuaSocket = true;            //是否打开Lua Socket库
}