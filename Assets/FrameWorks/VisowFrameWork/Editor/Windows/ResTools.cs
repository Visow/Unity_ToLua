using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
namespace VisowFrameWork {
    public class ResToolsWindow : EditorWindow
    {

        public enum ResBuildTarget
        { 
            Windows,
            Android,
            IOS,
        }

        string appVer = "0.0.0";
        string resVer = "0.0.0";
        string target = "";
        string url = "https://www.baidu.com";
        System.Enum buildTarget = ResBuildTarget.Windows;
        void Awake() {
            this.titleContent = new GUIContent("资源打包");
            // 加载当前在项目中的版本资源信息;
            string filePath = Application.streamingAssetsPath + "/" + CoreConst.VersionFile;
            if (File.Exists(filePath))
            {
                VersionInfo curVersionInfo = Version.GetInstance().ReadVersionFile(filePath);
                target = curVersionInfo.Target;
                if (target.Equals("Windows"))
                {
                    buildTarget = ResBuildTarget.Windows;
                }
                else if (target.Equals("Android"))
                {
                    buildTarget = ResBuildTarget.Android;
                }
                else if (target.Equals("IOS"))
                {
                    buildTarget = ResBuildTarget.IOS;
                }
                appVer = curVersionInfo.AppVersion;
                resVer = curVersionInfo.ResVersion;
                url = curVersionInfo.updateUrl;
            }
        }

        void OnGUI() {
            appVer = EditorGUILayout.TextField("App版本号:", appVer);
            resVer = EditorGUILayout.TextField("Res版本号:", resVer);
            url = EditorGUILayout.TextField("更新地址:", url);
            buildTarget = EditorGUILayout.EnumPopup("选择编译资源对应的平台:", buildTarget);
            BuildTarget curTarget = 0;
            switch ((ResBuildTarget)buildTarget)
            { 
                case ResBuildTarget.Windows:
                    curTarget = BuildTarget.StandaloneWindows;
                    break;
                case ResBuildTarget.Android:
                    curTarget = BuildTarget.Android;
                    break;
                case ResBuildTarget.IOS:
                    {
#if UNITY_5
                    curTarget = BuildTarget.iOS;
#else
                        curTarget = BuildTarget.iOS;
#endif
                        break;                 
                    }

                default:
                    break;
            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("编译资源"))
            {
                // 编译之前,保存旧的文件
                //Packager.SaveOldVersionInfo(curTarget);
                // 编译资源
                Packager.BuildAssetResource(curTarget);
                Packager.BuildFileIndex(System.Enum.GetName(typeof(ResBuildTarget), buildTarget), appVer, resVer, url);
            }
            if (GUILayout.Button("加入版本控制"))
            {
                Packager.SaveOldVersionInfo();
            }
            if (GUILayout.Button("清除版本记录"))
            {
                string dirPath = Application.dataPath.Replace("/Asset", "/VersionHistory");
                if (EditorUtility.DisplayDialog("版本控制", "请点击确定清理历史版本记录!", "确定", "取消"))
                {
                    if (Directory.Exists(dirPath))
                        Directory.Delete(dirPath);
                    this.ShowNotification(new GUIContent("已经删除历史版本记录!"));
                }

            }
            EditorGUILayout.EndHorizontal();
        }

        void OnInspectorUpdate()
        {
            this.Repaint();  //重新画窗口  
        }  
    }
}


