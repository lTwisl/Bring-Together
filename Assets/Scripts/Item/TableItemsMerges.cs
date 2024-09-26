using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Table Items Merges", menuName = "Create Table Items Merges")]
public class TableItemsMerges : ScriptableObject
{
    // Структура параметров предмета
    [System.Serializable]
    public struct ItemParameters
    {
        public string itemName;
        public GameObject prefab;
        public int pointsForMerging;
        //public ParticleSystem particleSystem;
    }

    // Структура класса предметов
    [System.Serializable]
    public struct ItemClasses
    {
        public string className;
        public ItemsTypes typeSpawnItems;
        public int indexSmallestSpawnItem;
        public int indexBiggestSpawnItem;
        public ItemParameters[] ItemParameters;
    }

    public ItemClasses[] ItemClass;
}
