using System.Collections.Generic;
using UnityEngine;

public class TableLevelsView : MonoBehaviour
{
    [SerializeField] private LevelView _levelViewPrefab;

    private List<LevelView> _levelViews = new();

    public LevelView CreateLevelView()
    {
        LevelView levelView = Instantiate(_levelViewPrefab, transform);
        _levelViews.Add(levelView);
        return levelView;
    }
}
