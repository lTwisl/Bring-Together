using System.Collections.Generic;
using UnityEngine;

public class TableLevelsView : MonoBehaviour
{
    [SerializeField] private LevelView _levelViewPrefab;

    public List<LevelView> LevelViews { get; private set; } = new();

    public LevelView CreateLevelView()
    {
        LevelView levelView = Instantiate(_levelViewPrefab, transform);
        LevelViews.Add(levelView);
        return levelView;
    }

    public void CreateLevelViews(string[] sceneTitles)
    {
        for (int i = 0; i < sceneTitles.Length; ++i)
        {
            LevelView view = CreateLevelView();
            view.SetName(sceneTitles[i]);
        }
    }
}
