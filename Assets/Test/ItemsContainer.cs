#if UNITY_EDITOR
using System.Reflection;
#endif
using UnityEditor;
using UnityEngine;
using static TableItemsMerges;


[CreateAssetMenu(fileName = "ItemsContainer", menuName = "Create Items Container")]
public class ItemsContainer : ScriptableObject
{
    public string className;
    public ItemsTypes typeSpawnItems;
    public int indexSmallestSpawnItem;
    public int indexBiggestSpawnItem;

    [SerializeField] private ItemParameters[] _itemParameters;

    public ItemParameters this[int index]
    {
        get => _itemParameters[index];
    }

    public int Length => _itemParameters.Length;


#if UNITY_EDITOR
    private void OnValidate()
    {
        for (int i = 0; i < _itemParameters.Length; ++i)
        {
            Undo.RecordObject(_itemParameters[i].prefab, "Editor Modify Scriptable Object");

            _itemParameters[i].itemName = _itemParameters[i].prefab.name;

            PropertyInfo fieldIndex = typeof(Item).GetProperty("Index");
            Item item = _itemParameters[i].prefab.GetComponent<Item>();
            fieldIndex.SetValue(item, i);

            EditorUtility.SetDirty(_itemParameters[i].prefab);
        }
        EditorUtility.SetDirty(this);
    }
#endif
}
