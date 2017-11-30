using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace VisowFrameWork {
    public class FileUtil
    {
        /// <summary>
        /// 取得数据存放目录(可写目录)
        /// </summary>
        public static string DataPath
        {
            get
            {
                string game = CoreConst.AppName.ToLower();
                Debug.Log(Application.dataPath);
                if (Application.isMobilePlatform)
                {
                    return Application.persistentDataPath + "/" + game + "/";
                }
                int i = Application.dataPath.LastIndexOf('/');
                return Application.dataPath.Substring(0, i + 1) + game + "/";
            }
        }

        /// <summary>
        /// 应用程序内容路径
        /// </summary>
        public static string AppContentPath()
        {
            string path = string.Empty;
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    path = "jar:file://" + Application.dataPath + "!/assets/";
                    break;
                case RuntimePlatform.IPhonePlayer:
                    path = Application.dataPath + "/Raw/";
                    break;
                default:
                    path = StreamAssetsPath;
                    break;
            }
            return path;
        }

        public static string StreamAssetsPath {
            get {
                return EditRoot + '/' + CoreConst.AssetDir +  '/';
            }
        }

        public static string StreamAssetsLuaPath
        {
            get {
                return (StreamAssetsPath + "lua/");
            }
        }

        public static string StreamResourcesPath
        {
            get {
                return (StreamAssetsPath + "Resources/");
            }
        }

        public static string FrameWorksPath
        {
            get {
                return EditRoot + "/FrameWorks/";
            }
        }

        public static string GameRoot
        {
            get {
                return EditRoot + "/Game/";
            }
        }

        public static string EditRoot {
            get {
                return Application.dataPath;
            }
        }

        public static string DevLuaPath
        {
            get {
                return GameRoot + "Lua/";
            }
        }

        public static string DevResourcesPath
        {
            get {
                return GameRoot + "Resources/";
            }
        }


        /// <summary>
        /// 获取相对路径
        /// </summary>
        /// <returns>The relative path.</returns>
        public static string GetRelativePath()
        {
            if (Application.isEditor)
                return "file://" + System.Environment.CurrentDirectory.Replace("\\", "/") + "/Assets/" + CoreConst.AssetDir + "/";
            else if (Application.isMobilePlatform || Application.isConsolePlatform)
                return "file:///" + DataPath;
            else // For standalone player.
                return "file://" + Application.streamingAssetsPath + "/";
        }

        public static string getFilePath(string fileName)
        {
            if (CoreConst.DebugMode == true)
            {
                return GameRoot + "/Resources/" + fileName;
            }
            if (File.Exists(DataPath + "/Resources/" + fileName))
                return DataPath + "/Resources/" + fileName;
            return AppContentPath() + "/Resources/" + fileName;
        }

    }
}

