using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace VisowFrameWork {
    public class VersionCompareCtrl : EditorWindow
    {
        VersionInfo versionInfo1;
        VersionInfo versionInfo2;
        string version1Path;
        string version2Path;

        Vector2 scrollPos;
        

        void Awake()
        {
            this.titleContent = new GUIContent("版本比对工具");
        }



        void OnGUI()
        {
            GUILayout.BeginHorizontal();
            version1Path = EditorGUILayout.TextField("Version1路径:", version1Path);
            if (GUILayout.Button("select", GUILayout.Width(100)))
            {
                OpenFileDialog fb = new OpenFileDialog();   //创建控件并实例化
                fb.Filter = "版本文件|*.ver";
                if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    version1Path = fb.FileName;
                    versionInfo1 = Version.GetInstance().ReadVersionFile(version1Path);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            version2Path = EditorGUILayout.TextField("Version2路径:", version2Path);
            if (GUILayout.Button("select", GUILayout.Width(100)))
            {
                OpenFileDialog verfd2 = new OpenFileDialog();   //创建控件并实例化
                verfd2.Filter = "版本文件|*.ver";
                if (verfd2.ShowDialog() == System.Windows.Forms.DialogResult.OK )
                {
                    version2Path = verfd2.FileName;
                    versionInfo2 = Version.GetInstance().ReadVersionFile(version2Path);
                }
            }
            GUILayout.EndHorizontal();
                
            if( versionInfo1 != null && versionInfo2 != null)
            {
                DoCompare();
            }
            
        }

        void OnInspectorUpdate()
        {
            this.Repaint();  //重新画窗口  
        }

        void DoCompare()
        {
            List<CompareInfo> compareInfoList = Version.GetInstance().CompareVersion(versionInfo1, versionInfo2);
            if (compareInfoList.Count == 0) return;
            string titleVersion1 = versionInfo1.AppVersion + "_" + versionInfo1.ResVersion;
            string tieleVersion2 = versionInfo2.AppVersion + "_" + versionInfo2.ResVersion;
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("文件名");
            EditorGUILayout.LabelField(titleVersion1 + " MD5");
            EditorGUILayout.LabelField(tieleVersion2 + " MD5");
            EditorGUILayout.LabelField("文件状态");
            EditorGUILayout.EndHorizontal();

            foreach (var item in compareInfoList)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(item.fileName);
                EditorGUILayout.LabelField(item.oldMd5);
                EditorGUILayout.LabelField(item.newMd5);
                EditorGUILayout.LabelField(System.Enum.GetName(typeof(FileStatus), item.status));
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();

            if (GUILayout.Button("生成更新包"))
            {
                List<string> updateResList = new List<string>();
                foreach (var item in compareInfoList)
                {
                    if (item.status != FileStatus.Equal && item.status != FileStatus.Delete)
                    {
                        updateResList.Add(item.fileName);
                    }
                }
                if (updateResList.Count == 0)
                {
                    this.ShowNotification(new GUIContent("没有更新的资源"));
                    return;
                }
                string zipName = tieleVersion2 + ".zip";
            }
        }
    }
}


