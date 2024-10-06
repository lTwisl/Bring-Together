using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TableItemsMerges))]
public class TableItemsMergesInpector : Editor
{
    public override void OnInspectorGUI()
    {
        TableItemsMerges tableItemsMerges = (TableItemsMerges)target;
        //DrawDefaultInspector();
        if (DrawDefaultInspector())
        {
            if (GUI.changed)
            {
                Undo.RecordObject(this, "Editor Modify Scriptable Object");
                for (int i = 0; i < tableItemsMerges.ItemClass.Length; i++)
                {
                    tableItemsMerges.ItemClass[i].className = tableItemsMerges.ItemClass[i].typeSpawnItems.ToString();

                    for (int j = 0; j < tableItemsMerges.ItemClass[i].ItemParameters.Length; j++)
                    {
                        if (tableItemsMerges.ItemClass[i].ItemParameters[j].prefab != null)
                        {
                            tableItemsMerges.ItemClass[i].ItemParameters[j].itemName = tableItemsMerges.ItemClass[i].ItemParameters[j].prefab.name;
                            Undo.RecordObject(tableItemsMerges.ItemClass[i].ItemParameters[j].prefab, "Editor Modify Scriptable Object");
                            //tableItemsMerges.ItemClass[i].ItemParameters[j].prefab.GetComponent<Item>().Index = j;
                            EditorUtility.SetDirty(tableItemsMerges.ItemClass[i].ItemParameters[j].prefab);
                        }
                    }
                }
                EditorUtility.SetDirty(this);
            }
        }

        //if (GUILayout.Button("Проставить индексы обьектов"))
        //{
        //    for (int i = 0; i < tableItemsMerges.ItemClass.Length; i++)
        //    {
        //        for (int j = 0; j < tableItemsMerges.ItemClass[i].ItemParameters.Length; j++)
        //        {
        //            if (tableItemsMerges.ItemClass[i].ItemParameters[j].prefab != null)
        //            {
        //                tableItemsMerges.ItemClass[i].ItemParameters[j].prefab.GetComponent<Item>().itemIndexInClass = j;
        //            }
        //        }
        //    }
        //}
    }
}
