﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VisowFrameWork
{
    public class CoreConst
    {
		public const bool DebugMode = true;                       //调试模式-用于内部测试

        /// <summary>
        /// 如果开启更新模式，前提必须启动框架自带服务器端。
        /// 否则就需要自己将StreamingAssets里面的所有内容
        /// 复制到自己的Webserver上面，并修改下面的WebUrl。
        /// </summary>
        public const bool UpdateMode = false;                       //更新模式-默认关闭 
        public const string UpdateAssetsPath = "F:/Git/U3DTolua/Server/StreamAssets/";

        public const int TimerInterval = 1;
        public const int GameFrameRate = 30;                        //游戏帧频

        public const string AppName = "Project_1";               //应用程序名称

        public const string AppPrefix = AppName + "_";              //应用程序前缀
        public const string ExtName = ".unity3d";                   //素材扩展名
        public const string AssetDir = "StreamingAssets";           //素材目录 
        public const string VersionFile = "version.ver";            // 版本文件名
        public const string WebUrl = "http://localhost:6688/";      //测试更新地址

        public static string UserId = string.Empty;                 //用户ID
        public static int SocketPort = 0;                           //Socket服务器端口
        public static string SocketAddress = string.Empty;          //Socket服务器地址

        public static string FrameWorksRoot 
        {
            get
            {
                return FileUtil.FrameWorksPath;
            }
        }

        public static string GameRoot
        {
            get
            {
                return FileUtil.GameRoot;
            }
        }

        public static string ToLuaRoot
        {
            get
            {
                return FrameWorksRoot + "/" + "ToLua";
            }
        }

        public static string HistoryRoot
        {
            get {
                return Application.dataPath.Replace("/Assets", "/VersionHistory");
            }
        }

    }
}

