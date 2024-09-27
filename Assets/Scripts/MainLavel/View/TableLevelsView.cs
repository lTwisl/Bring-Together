using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TableLevelsView : MonoBehaviour
{
    [SerializeField] private LevelView _levelViewPrefab;

    public LevelView CreateLevelView()
    {
        return Instantiate(_levelViewPrefab, transform);
    }
}
