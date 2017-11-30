using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace VisowFrameWork {

    public class VersionInfo {
        public string Target;
        public string AppVersion;
        public string ResVersion;
        public string updateUrl;
        public Dictionary<string, string> fileTagDict = new Dictionary<string, string>();
    }

    public enum FileStatus {
        Change,
        Delete,
        New,
        Equal,
    }

    public class CompareInfo {
        public string fileName;
        public string oldMd5;
        public string newMd5;
        public FileStatus status;
    }

    public class Version
    {

        private Version(){}

        private static Version s_instance = null;

        public static Version GetInstance() 
        {
            if (s_instance == null)
            {
                s_instance = new Version();
            }
            return s_instance;
        }

        // Use this for initialization

        public static string AppVersion = "";
        public static string ResVersion = "";

        /// <summary>
        /// 解析版本信息
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public VersionInfo ReadVersionFile(string filePath)
        {
            VersionInfo fileContent = new VersionInfo();
            if (!File.Exists(filePath))
                return null;
            string[] allLine = File.ReadAllLines(filePath);
            for (int i = 0; i < allLine.Length; i++)
            {
                if (i == 0)
                    ReadVersion(allLine[i], ref fileContent);
                else {
                    string[] md5Info = allLine[i].Split('|');
                    if (!fileContent.fileTagDict.ContainsKey(md5Info[0]))
                        fileContent.fileTagDict.Add(md5Info[0], md5Info[1]);
                }
            }
            return fileContent;
        }

        public VersionInfo ReadVersionText(string text)
        {
            VersionInfo fileContent = new VersionInfo();
            string[] allLine = text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < allLine.Length; i++)
            {
                if (i == 0)
                    ReadVersion(allLine[i], ref fileContent);
                else
                {
                    string[] md5Info = allLine[i].Split('|');
                    if (!fileContent.fileTagDict.ContainsKey(md5Info[0]))
                        fileContent.fileTagDict.Add(md5Info[0], md5Info[1]);
                }
            }
            return fileContent;
        }

        public void WriteVersion(VersionInfo fileContent, string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            FileStream fs = new FileStream(filePath, FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("Target=" + fileContent.Target + "|" + "AppVersion=" +  fileContent.AppVersion + "|" + "ResVersion=" + fileContent.ResVersion + "|" + "UpdateUrl=" + fileContent.updateUrl);

            foreach (var item in fileContent.fileTagDict)
            {
                sw.WriteLine(item.Key + "|" + item.Value);
            }
            sw.Close(); fs.Close();
        }

        void ReadVersion(string text, ref VersionInfo fileContent)
        { 
            string[] info = text.Split('|');
            if (info.Length != 4) return;
            fileContent.Target = info[0].Replace("Target=", "");
            fileContent.AppVersion = info[1].Replace("AppVersion=", "");
            fileContent.ResVersion = info[2].Replace("ResVersion=", "");
            fileContent.updateUrl = info[3].Replace("UpdateUrl=", "");
        }

        /// <summary>
        /// 比较版本信息
        /// </summary>
        /// <param name="oldInfo"></param>
        /// <param name="newInfo"></param>
        /// <returns></returns>
        public List<CompareInfo> CompareVersion(VersionInfo oldInfo, VersionInfo newInfo)
        {
            List<CompareInfo> compareList = new List<CompareInfo>();

            List<string> allFileList = new List<string>();

            foreach (var item in oldInfo.fileTagDict)
            {
                if (!allFileList.Contains(item.Key))
                {
                    allFileList.Add(item.Key);
                }
            }

            foreach (var item in newInfo.fileTagDict)
            {
                if (!allFileList.Contains(item.Key))
                {
                    allFileList.Add(item.Key);
                }
            }

            foreach (var fileName in allFileList)
            {
                string oldMd5;
                oldInfo.fileTagDict.TryGetValue(fileName, out oldMd5);
                string newMd5;
                newInfo.fileTagDict.TryGetValue(fileName, out newMd5);
                CompareInfo compareInfo = new CompareInfo();
                compareInfo.fileName = fileName;
                compareInfo.oldMd5 = oldMd5;
                compareInfo.newMd5 = newMd5;
                if (oldMd5 == null)
                {
                    compareInfo.status = FileStatus.New;

                }
                else if (newMd5 == null)
                {
                    compareInfo.status = FileStatus.Delete;
                }
                else if (oldMd5.Equals(newMd5))
                {
                    compareInfo.status = FileStatus.Equal;

                }
                else if (!oldMd5.Equals(newMd5))
                {
                    compareInfo.status = FileStatus.Change;
                }
                compareList.Add(compareInfo);
            }
            return compareList;
        }

        /// <summary>
        /// 编译版本信息文件
        /// </summary>
        /// <param name="appVersion"></param>
        /// <param name="resVersion"></param>
        /// <param name="url"></param>
        /// <param name="assetPath"></param>
        /// <param name="outPath"></param>
        public void BuildVersionFile(string target, string appVersion, string resVersion, string url, string assetPath, string outPath)
        {
            if (!Directory.Exists(assetPath))
                return;
            if (outPath == null)
            {
                outPath = assetPath;
            }
            if (appVersion == null || appVersion.Equals(""))
            {
                appVersion = "0";
            }
            if (resVersion == null || resVersion.Equals(""))
            {
                resVersion = "";
            }
            if (url == null)
            {
                url = "";
            }
            string newFilePath = outPath + CoreConst.VersionFile;
            if (File.Exists(newFilePath))
                File.Delete(newFilePath);
            List<string> files = new List<string>();
            List<string> paths = new List<string>();
            Recursive(assetPath, ref files, ref paths);

            FileStream fs = new FileStream(newFilePath, FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("Target="+ target + "|" + "AppVersion=" + appVersion + "|" + "ResVersion=" + resVersion + "|" + "UpdateUrl=" + url);
            for (int i = 0; i < files.Count; i++)
            {
                string file = files[i];
                string ext = Path.GetExtension(file);
                if (file.EndsWith(".meta") || file.Contains(".DS_Store")) continue;

                string md5 = Util.md5file(file);
                string value = file.Replace(assetPath, string.Empty);
                sw.WriteLine(value + "|" + md5);
            }
            sw.Close(); fs.Close();
        }

        /// <summary>
        /// 循环遍历,获取所有文件和文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <param name="files"></param>
        /// <param name="paths"></param>
        public void Recursive(string path, ref List<string> files, ref List<string> paths)
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
                Recursive(dir, ref files, ref paths);
            }
        }
    }
}


