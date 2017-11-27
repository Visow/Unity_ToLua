using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace VisowFrameWork {
    public class Packager
    {
        public static string platform = string.Empty;
        static List<string> paths = new List<string>();
        static List<string> files = new List<string>();
        static List<AssetBundleBuild> maps = new List<AssetBundleBuild>();

        ///-----------------------------------------------------------
        static string[] exts = { ".txt", ".xml", ".lua", ".assetbundle", ".json" };
        static bool CanCopy(string ext)
        {   //能不能复制
            foreach (string e in exts)
            {
                if (ext.Equals(e)) return true;
            }
            return false;
        }

        [MenuItem("Visow工具箱/资源/资源打包", false, 101)]
        static void ResPackager()
        {
            ResToolsWindow curWindow = (ResToolsWindow)EditorWindow.GetWindow(typeof(ResToolsWindow));
        }

        [MenuItem("Visow工具箱/资源/版本比对工具", false, 102)]
        static void VersionCompare()
        {
            VersionCompareCtrl curWindow = (VersionCompareCtrl)EditorWindow.GetWindow(typeof(VersionCompareCtrl));
        }

        [MenuItem("Visow工具箱/资源/test", false, 103)]
        static void test()
        {
            
        }


        /// <summary>
        /// 生成绑定素材
        /// </summary>
        public static void BuildAssetResource(BuildTarget target)
        {
            if (Directory.Exists(Util.DataPath))
            {
                Directory.Delete(Util.DataPath, true);
            }
            string streamPath = Application.streamingAssetsPath;
            if (Directory.Exists(streamPath))
            {
                Directory.Delete(streamPath, true);
            }
            Directory.CreateDirectory(streamPath);

            maps.Clear();

            HandleLuaFile();
            HandleResuorcesBundle();
            BuildPipeline.BuildAssetBundles(AppDataPath + "/StreamingAssets/Resources", maps.ToArray(), BuildAssetBundleOptions.None, target);
            string streamDir = Application.dataPath + "/" + CoreConst.LuaTempDir;
            if (Directory.Exists(streamDir)) Directory.Delete(streamDir, true);
            AssetDatabase.Refresh();
        }



        static void AddBuildMap(string bundleName, string pattern, string path)
        {
            string[] files = Directory.GetFiles(path, pattern);
            if (files.Length == 0) return;

            for (int i = 0; i < files.Length; i++)
            {
                files[i] = files[i].Replace('\\', '/');
            }
            AssetBundleBuild build = new AssetBundleBuild();
            string[] split = bundleName.Split('/');
            string fileName = "";
            string writeBundleName = "";
            for (int i = 0; i < split.Length; i++)
            {
                if (i > 0)
                {
                    fileName += ('_' + split[i]);
                    if (i < split.Length - 1)
                    {
                        writeBundleName += ('/' + split[i]);
                    }
                }
                else
                {
                    fileName += split[i];
                    writeBundleName += split[i];
                }
            }

            build.assetBundleName = writeBundleName + "/" + fileName;
            build.assetNames = files;
            maps.Add(build);
        }

        /// <summary>
        /// 处理Lua代码包
        /// </summary>
        static void HandleLuaBundle()
        {
            string streamDir = Application.dataPath + "/" + CoreConst.LuaTempDir;
            if (!Directory.Exists(streamDir)) Directory.CreateDirectory(streamDir);

            string[] srcDirs = { CustomSettings.luaDir };
            for (int i = 0; i < srcDirs.Length; i++)
            {
                ToLuaMenu.CopyLuaBytesFiles(srcDirs[i], streamDir);
            }
            string[] dirs = Directory.GetDirectories(streamDir, "*", SearchOption.AllDirectories);
            for (int i = 0; i < dirs.Length; i++)
            {
                string name = dirs[i].Replace(streamDir, string.Empty);
                name = name.Replace('\\', '_').Replace('/', '_');
                name = "lua/lua_" + name.ToLower() + CoreConst.ExtName;

                string path = "Assets" + dirs[i].Replace(Application.dataPath, "");
                AddBuildMap(name, "*.bytes", path);
            }
            AddBuildMap("lua/lua" + CoreConst.ExtName, "*.bytes", "Assets/" + CoreConst.LuaTempDir);

            //-------------------------------处理非Lua文件----------------------------------
            string luaPath = AppDataPath + "/StreamingAssets/lua/";
            for (int i = 0; i < srcDirs.Length; i++)
            {
                paths.Clear(); files.Clear();
                string luaDataPath = srcDirs[i].ToLower();
                Recursive(luaDataPath);
                foreach (string f in files)
                {
                    if (f.EndsWith(".meta") || f.EndsWith(".lua")) continue;
                    string newfile = f.Replace(luaDataPath, "");
                    string path = Path.GetDirectoryName(luaPath + newfile);
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    string destfile = path + "/" + Path.GetFileName(f);
                    File.Copy(f, destfile, true);
                }
            }
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 处理Lua文件
        /// </summary>
        static void HandleLuaFile()
        {
            string resPath = AppDataPath + "/StreamingAssets/";
            string luaPath = resPath + "/lua/";

            //----------复制Lua文件----------------
            if (!Directory.Exists(luaPath))
            {
                Directory.CreateDirectory(luaPath);
            }
            string[] luaPaths = { CoreConst.GameRoot + "/Lua" };

            for (int i = 0; i < luaPaths.Length; i++)
            {
                paths.Clear(); files.Clear();
                string luaDataPath = luaPaths[i].ToLower();
                Recursive(luaDataPath);
                int n = 0;
                foreach (string f in files)
                {
                    if (f.EndsWith(".meta")) continue;
                    string newfile = f.Replace(luaDataPath, "");
                    string newpath = luaPath + newfile;
                    string path = Path.GetDirectoryName(newpath);
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    if (File.Exists(newpath))
                    {
                        File.Delete(newpath);
                    }

                    File.Copy(f, newpath, true);

                    UpdateProgress(n++, files.Count, newpath);
                }
            }
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }

        static void HandleResuorcesBundle()
        {
            string resPath = AppDataPath + "/StreamingAssets/Resources";
            if (!Directory.Exists(resPath))
            {
                Directory.CreateDirectory(resPath);
            }
            string workResPath = CoreConst.GameRoot + "/Resources";

            paths.Clear(); files.Clear();
            Recursive(workResPath);

            foreach (string dir in paths)
            {
                string name = dir.Replace(workResPath + "/", "");
                string[] curFiles = Directory.GetFiles(dir);
                if (curFiles.Length > 0)
                {
                    AddBuildMap(name + "/png" + CoreConst.ExtName, "*.png", "Assets/" + dir.Substring(AppDataPath.Length + 1));
                    AddBuildMap(name + "/prefab" + CoreConst.ExtName, "*.prefab", "Assets/" + dir.Substring(AppDataPath.Length + 1));
                    AddBuildMap(name + "/anim" + CoreConst.ExtName, "*.anim", "Assets/" + dir.Substring(AppDataPath.Length + 1));
                }
            }
        }

        public static void BuildFileIndex(string target, string appVer, string resVer, string url)
        {
            string resPath = AppDataPath + "/StreamingAssets/";
            Version.GetInstance().BuildVersionFile(target, appVer, resVer, url, resPath, resPath);
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 数据目录
        /// </summary>
        static string AppDataPath
        {
            get { return Application.dataPath.ToLower(); }
        }

        /// <summary>
        /// 遍历目录及其子目录
        /// </summary>
        public static void Recursive(string path)
        {
            string[] names = Directory.GetFiles(path);
            string[] dirs = Directory.GetDirectories(path);
            foreach (string filename in names)
            {
                string ext = Path.GetExtension(filename);
                if (ext.Equals(".meta")) continue;
                files.Add(filename.Replace('\\', '/'));
            }
            foreach (string dir in dirs)
            {
                paths.Add(dir.Replace('\\', '/'));
                Recursive(dir);
            }
        }

        static void UpdateProgress(int progress, int progressMax, string desc)
        {
            string title = "Processing...[" + progress + " - " + progressMax + "]";
            float value = (float)progress / (float)progressMax;
            EditorUtility.DisplayProgressBar(title, desc, value);
        }

        public static void EncodeLuaFile(string srcFile, string outFile)
        {
            if (!srcFile.ToLower().EndsWith(".lua"))
            {
                File.Copy(srcFile, outFile, true);
                return;
            }
            bool isWin = true;
            string luaexe = string.Empty;
            string args = string.Empty;
            string exedir = string.Empty;
            string currDir = Directory.GetCurrentDirectory();
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                isWin = true;
                luaexe = "luajit.exe";
                args = "-b " + srcFile + " " + outFile;
                exedir = AppDataPath.Replace("assets", "") + "LuaEncoder/luajit/";
            }
            else if (Application.platform == RuntimePlatform.OSXEditor)
            {
                isWin = false;
                luaexe = "./luajit";
                args = "-b " + srcFile + " " + outFile;
                exedir = AppDataPath.Replace("assets", "") + "LuaEncoder/luajit_mac/";
            }
            Directory.SetCurrentDirectory(exedir);
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = luaexe;
            info.Arguments = args;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.UseShellExecute = isWin;
            info.ErrorDialog = true;
            Util.Log(info.FileName + " " + info.Arguments);

            Process pro = Process.Start(info);
            pro.WaitForExit();
            Directory.SetCurrentDirectory(currDir);
        }

        public static void SaveOldVersionInfo()
        {
            string filePath = Application.streamingAssetsPath + "/" + CoreConst.VersionFile;
            if(!File.Exists(filePath)) return;
            VersionInfo versionInfo = Version.GetInstance().ReadVersionFile(filePath);
            string historyPath = Application.dataPath.Replace("/Assets", "/VersionHistory");
            if (!Directory.Exists(historyPath))
                Directory.CreateDirectory(historyPath);
            historyPath += "/" + versionInfo.Target;
            if (!Directory.Exists(historyPath))
                Directory.CreateDirectory(historyPath);
            historyPath += ("/" + versionInfo.AppVersion + "_" + versionInfo.ResVersion + ".ver");
            if (File.Exists(historyPath))
            {
                // 如果当前版本记录文件存在
                if (EditorUtility.DisplayDialog("版本控制", "当前版本文件已经存在,点击确定覆盖!", "确定", "取消"))
                {
                    Version.GetInstance().WriteVersion(versionInfo, historyPath);
                    ResToolsWindow curWindow = (ResToolsWindow)EditorWindow.GetWindow(typeof(ResToolsWindow));
                    curWindow.ShowNotification(new GUIContent("添加成功"));
                }
            }
            else
            {
                Version.GetInstance().WriteVersion(versionInfo, historyPath);
                ResToolsWindow curWindow = (ResToolsWindow)EditorWindow.GetWindow(typeof(ResToolsWindow));
                curWindow.ShowNotification(new GUIContent("添加成功"));
            }
            
        }
    }
}

