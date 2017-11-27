using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestZip : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ZipManager zipManager = gameObject.AddComponent<ZipManager>();
        zipManager.uncompless(Application.streamingAssetsPath + "/StreamingAssets.zip", Application.streamingAssetsPath, endCallBack, errCallback);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void endCallBack(object obj)
    {
        UnityEngine.Debug.Log(obj);
    }

    public void errCallback(System.Exception e)
    {
        UnityEngine.Debug.Log(e);
    }
}
