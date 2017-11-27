
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using LuaInterface;

namespace VisowFrameWork {
    public class LoadingManager : ManagerBase
    {
        protected AsyncOperation _op;
        protected int _displayProgress;
        protected int _destProgress;

        public int GetDisplayProgress()
        {
            return _displayProgress;
        }

        public virtual void LoadSceneByName(string name, LuaFunction update = null, LuaFunction complete = null, LuaFunction nextLoad = null)
        {
            _displayProgress = 0;
            _destProgress = 0;
            StartCoroutine(IELoadSceneByName(name, LoadSceneMode.Single, update, complete, nextLoad));
        }

        protected IEnumerator IELoadSceneByName(string name, LoadSceneMode mode, LuaFunction update = null, LuaFunction complete = null, LuaFunction nextLoad = null)
        {
            _op = SceneManager.LoadSceneAsync(name, mode);
            if (_op == null)
            {
                yield return null;
            }
            _op.allowSceneActivation = false;
            while (_op.isDone == false && _op.progress < 0.9f)
            {
                _destProgress = Mathf.FloorToInt(_op.progress * 100);
                if (_destProgress == 0)
                {
                    yield return new WaitForEndOfFrame();
                }
                while (_displayProgress < _destProgress)
                {
                    ++_displayProgress;
                    yield return new WaitForEndOfFrame();
                }
            }

            _destProgress = 100;
            while (_displayProgress < _destProgress)
            {
                ++_displayProgress;
                if (update != null)
                {
                    update.Call(_displayProgress);
                }
                yield return new WaitForEndOfFrame();
            }
            _op.allowSceneActivation = true;

            if (complete != null)
            {
                complete.Call(name);
            }
            yield return new WaitForEndOfFrame();
            if (nextLoad != null)
            {
                nextLoad.Call(name);
            }
        }

        public void LoadSceneAddtiveByName(string name)
        {
            _displayProgress = 0;
            _destProgress = 0;
            StartCoroutine(IELoadSceneByName(name, LoadSceneMode.Additive));
        }

        public void UnloadSceneByName(string name)
        {
            SceneManager.UnloadSceneAsync(name);
        }

    }
}

