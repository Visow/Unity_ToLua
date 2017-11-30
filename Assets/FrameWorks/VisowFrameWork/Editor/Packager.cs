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
        public static string PackagerOutPath = FileUtil.StreamAssetsPath;
        static List<string> paths = new List<string>();
        static List<string> files = new List<string>();
        static List<AssetBundleBuild> maps = new List<AssetBundleBuild>();
        

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
        public static void BuildAssetResource(BuildTarget target, string streamPath)
        {
            if (Directory.Exists(streamPath))
            {
                Directory.Delete(streamPath, true);
            }
            Directory.CreateDirectory(streamPath);

            maps.Clear();

            HandleLuaFile(streamPath + "lua/");
            HandleResuorcesBundle(streamPath + "Resources/");
            BuildPipeline.BuildAssetBundles(streamPath + "Resources/", maps.ToArray(), BuildAssetBundleOptions.None, target);
            AssetDatabase.Refresh();
        }



        static void AddBuildMap(string bundleName, string pattern, string path)
        {
            string[] files = Directory.GetFiles(path, pattern);
            if (files.Length == 0) return;
			List<string> abFileList = new List<string> ();
            for (int i = 0; i < files.Length; i++)
            {
				if(files[i].EndsWith(".meta"))
					continue;
				abFileList.Add(files[i].Replace('\\', '/'));
            }
			if (abFileList.Count == 0)
				return;
            AssetBundleBuild build = new AssetBundleBuild();
            string[] split = bundleName.Split('/');
            string fileName = "";
            string writeBundleName = "";
			bundleName = bundleName.Replace ('/', '_');

			build.assetBundleName = bundleName;
            build.assetNames = files;
            maps.Add(build);
        }

        /// <summary>
        /// 处理Lua文件
        /// </summary>
        static void HandleLuaFile(string luaPath)
        {
            //----------复制Lua文件----------------
            if (!Directory.Exists(luaPath))
            {
                Directory.CreateDirectory(luaPath);
            }
            string[] luaPaths = { FileUtil.DevLuaPath };

            for (int i = 0; i < luaPaths.Length; i++)
            {
                paths.Clear(); files.Clear();
                string luaDataPath = luaPaths[i];
                Recursive(luaDataPath);
                int n = 0;
                foreach (string f in files)
                {
                    if (f.EndsWith(".meta")) continue;
                    if (f.IndexOf(".vscode") >= 0) continue;
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

        static void HandleResuorcesBundle(string resPath)
        {
            if (!Directory.Exists(resPath))
            {
                Directory.CreateDirectory(resPath);
            }
            string workResPath = FileUtil.DevResourcesPath;

            paths.Clear(); files.Clear();
            Recursive(workResPath);

            foreach (string dir in paths)
            {
                string name = dir.Replace(workResPath, "");
                string[] curFiles = Directory.GetFiles(dir);
				string subFilePath = "Assets/" + dir.Substring (FileUtil.EditRoot.Length + 1);
                if (curFiles.Length > 0)
                {
					AddBuildMap (name + CoreConst.ExtName, "*.*", subFilePath);
                }
            }
        }

        public static void BuildFileIndex(string target, string appVer, string resVer, string url, string resPath)
        {
            Version.GetInstance().BuildVersionFile(target, appVer, resVer, url, resPath, resPath);
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 数据目录
        /// </summary>
        static string AppDataPath
        {
            get { return FileUtil.EditRoot.ToLower(); }
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
            string filePath = FileUtil.StreamAssetsPath + "/" + CoreConst.VersionFile;
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

