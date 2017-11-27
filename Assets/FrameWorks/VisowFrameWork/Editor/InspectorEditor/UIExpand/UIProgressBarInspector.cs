using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
namespace VisowFrameWork {

    [CustomEditor(typeof(UIProgressBar))]
    public class UIProgressBarInspector : Editor
    { 

        private float percent = 0f;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            UIProgressBar progressBar = target as UIProgressBar;
            percent = progressBar.Percent;
            percent = EditorGUILayout.Slider("Value:", percent, 0f, 100f);
            progressBar.Percent = percent;
        }

        void OnInspectorUpdate()
        {
            this.Repaint(); // 刷新Inspector
        }

    }
}

