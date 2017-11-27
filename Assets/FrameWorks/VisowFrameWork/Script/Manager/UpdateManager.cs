using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using System.IO;
using System;

namespace VisowFrameWork {

    public class UpdateManager : ManagerBase
    {

        enum EventID { 
            LoadVersionInfo,
            CheckVersion,
            DownLoad,
            Completed,
            Error,
        }

        VersionInfo curVersionInfo;
        VersionInfo CurVersionInfo
        {
            get{ return curVersionInfo;}
        }
        VersionInfo serverVersionInfo;
        VersionInfo ServerVersionInfo
        {
            get { return serverVersionInfo; }
        }

        List<string> needDownFileList = new List<string>(); // 需要下载的文件List;
        List<string> downFailedList = new List<string>();   // 下载失败的文件List;
        List<string> downSucList = new List<string>(); // 已经成功下载的文件List;

        LuaFunction msgHandler;    // 回调事件

        public void Prepare(LuaFunction handler)
        {
            if (msgHandler != null)
            {
                msgHandler.Dispose();
                msgHandler = null;
            }
            msgHandler = handler;
        }

        public void StartGetVersionInfo()
        {
            curVersionInfo = Version.GetInstance().ReadVersionFile(Util.DataPath + "version.ver");
            StartCoroutine("GetVersionInfo");
        }

        IEnumerator GetVersionInfo()
        {
            Debug.Log(curVersionInfo.updateUrl + curVersionInfo.Target + "/" + "version.ver");
            WWW loadVersion = new WWW(curVersionInfo.updateUrl + curVersionInfo.Target + "/" + "version.ver");
            yield return loadVersion;

            if (!loadVersion.error.Equals(""))
            {
                msgHandler.BeginPCall();
                msgHandler.Push((int)EventID.Error);
                msgHandler.Push(loadVersion.error);
                msgHandler.PCall();
                msgHandler.EndPCall();
                // 通知版本信息加载失败;
                yield break;
            }
                
            serverVersionInfo = Version.GetInstance().ReadVersionText(loadVersion.text);
            if (serverVersionInfo != null)
            {
                if (msgHandler != null)
                {
                    msgHandler.BeginPCall();
                    msgHandler.Push((int)EventID.LoadVersionInfo);
                    msgHandler.PCall();
                    msgHandler.EndPCall();
                }
            }
            else
            {
                if (msgHandler != null)
                {
                    msgHandler.BeginPCall();
                    msgHandler.Push((int)EventID.Error);
                    msgHandler.Push("加载版本信息失败!");
                    msgHandler.PCall();
                    msgHandler.EndPCall();
                }
            }
        }

        public void StartCheckVersion()
        {
            StartCoroutine("CheckVersion");
        }

        IEnumerator CheckVersion()
        {
            List<CompareInfo> compareInfoList = Version.GetInstance().CompareVersion(curVersionInfo, serverVersionInfo);
            yield return compareInfoList;
            int idx = 0;
            foreach (var item in compareInfoList)
            {
                idx++;
                if (item.status == FileStatus.New || item.status == FileStatus.Change)
                {
                    needDownFileList.Add(item.fileName);
                }
                CheckVersionProgress(compareInfoList.Count, idx);
                yield return new WaitForEndOfFrame();
            }
        }

        void CheckVersionProgress(int totalFile, int curFile)
        {
            if (msgHandler != null)
            {
                msgHandler.BeginPCall();
                msgHandler.Push((int)EventID.CheckVersion);
                msgHandler.Push(totalFile);
                msgHandler.Push(curFile);
                msgHandler.PCall();
                msgHandler.EndPCall();
            }
        }

        public void StartDownLoad()
        {
            StartCoroutine("DownLoad");
        }

        IEnumerator DownLoad()
        {
            string urlTitle = curVersionInfo.updateUrl + curVersionInfo.Target + '/';
            if (needDownFileList.Count > 0)
            {
                foreach (string fileName in needDownFileList)
                {
                    WWW loadFile = new WWW(urlTitle + fileName);
                    yield return loadFile;

                    if (!loadFile.error.Equals(""))
                    {
                        msgHandler.BeginPCall();
                        msgHandler.Push((int)EventID.Error);
                        msgHandler.Push(loadFile.error);
                        msgHandler.PCall();
                        msgHandler.EndPCall();
                        // 通知版本信息加载失败;
                        downFailedList.Add(fileName);
                        continue;
                    }

                    try
                    {
                        string subPath = loadFile.url.Replace(urlTitle, "");
                        string fullPath = Util.DataPath + subPath;
                        string[] dirArr = subPath.Split('/');
                        string fullDirName = Util.DataPath;
                        for (int i = 0; i < dirArr.Length - 1; i++)
                        {
                            fullDirName += (dirArr[i] + "/");
                        }

                        if (!Directory.Exists(fullDirName))
                        {
                            Directory.CreateDirectory(fullDirName);
                        }
                        if (File.Exists(fullPath))
                        {
                            File.Delete(fullPath);
                        }
                        FileInfo fileInfo = new FileInfo(fullPath);
                        Stream stream = fileInfo.Create();
                        stream.Write(loadFile.bytes, 0, loadFile.bytes.Length);
                        stream.Close();
                        stream.Dispose();
                        Debug.Log("down load file suc ==========>" + fullPath);
                        downSucList.Add(fileName);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.ToString());
                    }
                    DownLoadProcess();
                }

                // 下载失败文件处理
            }
            else
            {
                DownLoadProcess();
            }
        }

        void DownLoadProcess()
        {
            if (msgHandler != null)
            {
                msgHandler.BeginPCall();
                msgHandler.Push((int)EventID.DownLoad);
                msgHandler.Push(needDownFileList.Count);
                msgHandler.Push(downSucList.Count);
                msgHandler.PCall();
                msgHandler.EndPCall();
            }    
        }

        public void StartGame()
        {
            StartCoroutine("OnStartGame");
        }

        IEnumerator OnStartGame()
        {
            // 移除当前UIRoot
            Manager.Remove<UIRootManager>(ManagerName.UIRoot);
            yield return new UnityEngine.WaitForEndOfFrame();

            // 清理当前AssetBundle
            ResourceManager resManager = Manager.Add<ResourceManager>(ManagerName.Resource);
            resManager.Close();
            resManager.Initialize();
            yield return new UnityEngine.WaitForEndOfFrame();

            // 重新加载Lua组件
            LuaManager luaManager = Manager.Add<LuaManager>(ManagerName.Lua);
            luaManager.Close();
            luaManager.ReStart();

            luaManager.DoFile("Main.lua");
        }

        void OnDestory()
        {
            if (msgHandler != null)
            {
                msgHandler.Dispose();
                msgHandler = null;
            }
        }
    }


}

