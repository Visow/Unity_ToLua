using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;

namespace VisowFrameWork {
    public class LuaComponent
    {
        private enum Function
        {
            Awake = 0,
            Start = 1,
            OnDestroy = 2,
            Focus = 3,
            Enable = 4,
        };

        public string modelName;
        public LuaFunction lifecyleHandler;
        public LuaComponentGroup content{get;set;}

        public GameObject gameObject { get { return content.gameObject; } }

        public void Awake()
        {
            if (lifecyleHandler == null)
                return;
            lifecyleHandler.BeginPCall();
            lifecyleHandler.Push(this);
            lifecyleHandler.Push((int)Function.Awake);
            lifecyleHandler.PCall();
            lifecyleHandler.EndPCall();
        }

        // Use this for initialization
        public void Start()
        {
            if (lifecyleHandler == null)
                return;
            lifecyleHandler.BeginPCall();
            lifecyleHandler.Push(this);
            lifecyleHandler.Push((int)Function.Start);
            lifecyleHandler.PCall();
            lifecyleHandler.EndPCall();
        }


        public void OnEnable()
        {
            if (null == lifecyleHandler)
            {
                return;
            }
            lifecyleHandler.BeginPCall();
            lifecyleHandler.Push(this);
            lifecyleHandler.Push((int)Function.Enable);
            lifecyleHandler.PCall();
            lifecyleHandler.EndPCall();
        }

        public void Focus(bool isFocus)
        {
            if (null == lifecyleHandler)
            {
                return;
            }
            lifecyleHandler.BeginPCall();
            lifecyleHandler.Push(this);
            lifecyleHandler.Push((int)Function.Focus);
            lifecyleHandler.Push(isFocus);
            lifecyleHandler.PCall();
            lifecyleHandler.EndPCall();
        }

        public void OnDestroy()
        {
            if (null == lifecyleHandler)
                return;

            lifecyleHandler.BeginPCall();
            lifecyleHandler.Push(this);
            lifecyleHandler.Push((int)Function.OnDestroy);
            lifecyleHandler.PCall();
            lifecyleHandler.EndPCall();
            lifecyleHandler.Dispose();
            lifecyleHandler = null;
        }

        // Update is called once per frame
        public void Update()
        {
            if (lifecyleHandler == null)
                return;
            //lifecyleHandler.BeginPCall();
            //lifecyleHandler.Push(this);
            //lifecyleHandler.Push((int)Function.Start);
            //lifecyleHandler.PCall();
            //lifecyleHandler.EndPCall();
        }

        public void SetHandler(LuaFunction handler)
        {
            lifecyleHandler = handler;
        }
    }

}
