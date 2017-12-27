using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace VisowFrameWork {
    public class UIRootManager : ManagerBase
    {
        public Vector2 ScreenSize {
            get {
                return uiRoot.GetComponent<RectTransform>().sizeDelta;
            }
        }
        GameObject uiRoot;
        public GameObject UIRoot {
            get {
                uiRoot = GameObject.Find("2DUICanvas");
                if (uiRoot == null)
                {
                    GameObject prefab = Util.ResManager().GetPrefab("Prefabs/Core/2DUICanvas.prefab");
                    if (prefab)
                    {
                        uiRoot = GameObject.Instantiate(prefab) as GameObject;
                        if (uiRoot)
                        {
                            //DontDestroyOnLoad(uiRoot);
                            uiRoot.name = "2DUICanvas";
                        }
                    }
                }
                return uiRoot;
            }
        }

        void Awake() 
        {

        }

        // Use this for initialization
        void Start()
        {

        }

        public override void Close()
        {
            base.Close();
            if (uiRoot != null)
            {
                Destroy(uiRoot);
                uiRoot = null;
            }
        }

    }

}
