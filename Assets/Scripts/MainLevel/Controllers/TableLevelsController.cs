using System.Collections.Generic;
using UnityEngine;


public class TableLevelsController
{
    private readonly TableLevelsModel _tableLevelsModel;
    private readonly TableLevelsView _tableLevelsView;

    private Dictionary<string, LevelController> _levelControllersMap = new();

    public TableLevelsController(TableLevelsModel model, TableLevelsView view)
    {
        _tableLevelsModel = model;
        _tableLevelsView = view;

        for (int i = 0; i < _tableLevelsModel.Levels.Length; ++i)
        {
            _levelControllersMap.Add(_tableLevelsModel.Levels[i].LevelName, new LevelController(_tableLevelsModel.Levels[i], view.LevelViews[i]));
        }
    }

    public void SetStars(string sceneName, int countStars)
    {
        if (_levelControllersMap.ContainsKey(sceneName) == true)
            _levelControllersMap[sceneName].SetStars(countStars);
        else 
            Debug.LogWarning($"Сцены с иемене \"{sceneName}\" не существует в общем списке сцен");
    }

    public void UpdateView()
    {
        foreach (LevelController levelController in _levelControllersMap.Values)
        {
            levelController.UpdateView();
        }
    }
}

