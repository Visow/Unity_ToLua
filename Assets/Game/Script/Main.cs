using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using VisowFrameWork;

using UnityEngine.UI;
using System.IO;

public class Main : MonoBehaviour {

	public Text m_tipsText;
	public UIProgressBar m_ProgressBar;

	// Use this for initialization
    void Awake() {
		m_tipsText.text = "正在初始化资源";
    }

	void Start () {
		CheckExtractResource ();
	}


	public void CheckExtractResource() {
        bool isExists = Directory.Exists(FileUtil.DataPath) && Directory.Exists(FileUtil.DataPath + "lua/") && File.Exists(FileUtil.DataPath + CoreConst.VersionFile);
        if (CoreConst.DebugMode == true || isExists)
        {
			StartGame ();
			return;  //文件已经解压过了，自己可添加检查文件列表逻辑
		}
		StartCoroutine(OnExtractResource());    //启动释放协成 

	}

	IEnumerator OnExtractResource() {
		string dataPath = FileUtil.DataPath; //数据目录
		string resPath = FileUtil.AppContentPath (); //游戏包资源目录

		if (Directory.Exists (dataPath))
			Directory.Delete (dataPath, true);
		Directory.CreateDirectory (dataPath);

        string infile = resPath + CoreConst.VersionFile;
        string outfile = dataPath + CoreConst.VersionFile;
		if (File.Exists (outfile))
			File.Delete (outfile);
		Debug.Log(infile);
		Debug.Log(outfile);

		if (Application.platform == RuntimePlatform.Android) {
			WWW www = new WWW(infile);
			yield return www;

			if (www.isDone) {
				File.WriteAllBytes(outfile, www.bytes);
			}
			yield return 0;
		} else File.Copy(infile, outfile, true);
		yield return new WaitForEndOfFrame();

		//释放所有文件到数据目录
		VersionInfo verSionInfo = Version.GetInstance().ReadVersionFile(outfile);

		int fileIndex = 0;
		foreach (var dic in verSionInfo.fileTagDict) {
			infile = resPath + dic.Key;  //
			outfile = dataPath + dic.Key;

			m_tipsText.text = "正在释放文件:>" + dic.Key + "(资源释放不消耗流量)";
			m_ProgressBar.Percent = (fileIndex * 100) / verSionInfo.fileTagDict.Count;
			Debug.Log("正在释放文件:>" + infile);
			fileIndex++;

			string dir = Path.GetDirectoryName(outfile);
			if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

			if (Application.platform == RuntimePlatform.Android) {
				WWW www = new WWW(infile);
				yield return www;

				if (www.isDone) {
					File.WriteAllBytes(outfile, www.bytes);
				}
				yield return 0;
			} else {
				if (File.Exists(outfile)) {
					File.Delete(outfile);
				}
				File.Copy(infile, outfile, true);
			}
			yield return new WaitForEndOfFrame();
		}
		m_tipsText.text = "资源初始化完成!!!";

		yield return new WaitForSeconds(0.1f);

		//释放完成，启动游戏流程
		StartGame();
	}

	public void StartGame() {
		GameObject tempUI = GameObject.Find ("ReleaseResPanel");
		if (tempUI != null) {
			GameObject.Destroy (tempUI);
		}
		LuaManager luaManager = Util.LuaManager();
		luaManager.InitStart();

		// 资源初始化
		ResourceManager resManager = Util.ResManager();
		resManager.Initialize();

		if (CoreConst.UpdateMode == true)
			luaManager.DoFile("PreMain.lua");
		else
			luaManager.DoFile("Main.lua");
	}
}
