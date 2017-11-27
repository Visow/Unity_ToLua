using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


namespace VisowFrameWork {
    [CustomEditor(typeof(LuaComponentGroup))]
    public class LuaComponentGroupInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            // LuaCOmponenetGroup实例
            LuaComponentGroup luaGroup = target as LuaComponentGroup;

            base.OnInspectorGUI();
            GUI.color = Color.red;
            GUILayout.Box("绑定区域:Lua脚本组件", GUILayout.ExpandWidth(true), GUILayout.Height(50));
            GUI.color = Color.white;
            //如果鼠标正在拖拽中或拖拽结束时，并且鼠标所在位置在文本输入框内

            EventType eventType = Event.current.type;
            if (eventType == EventType.DragUpdated || eventType == EventType.DragPerform)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (eventType == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();
                    //改变鼠标的外表  
                    string filePath = DragAndDrop.paths[0];
                    filePath = Application.dataPath + filePath.Replace("Assets", "");
                    Debug.Log("BindLuaComponent:" + filePath);
                    if (File.Exists(filePath))
                    {
                        string bandName = filePath.Replace(Application.dataPath + "/Game/Lua/", "");
                        bandName = bandName.Replace('/', '.');
                        bandName = bandName.Replace(".lua", "");

                        if (!luaGroup.luaComponenetName.Contains(bandName))
                        {
                            luaGroup.luaComponenetName.Add(bandName);
                        }
                    }
                }
                Event.current.Use();
            }
        }
    }
}


