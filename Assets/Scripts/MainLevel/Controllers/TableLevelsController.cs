using System.Collections.Generic;

public class TableLevelsController
{
    private readonly TableLevelsModel _tableLevelsModel;
    private readonly TableLevelsView _tableLevelsView;

    private Dictionary<string, LevelController> _levelControllersMap = new();

    public TableLevelsController(TableLevelsModel model, TableLevelsView view)
    {
        _tableLevelsModel = model;
        _tableLevelsView = view;

        foreach (LevelModel level in _tableLevelsModel.Levels)
        {
            LevelView levelView = _tableLevelsView.CreateLevelView();
            levelView.SetName(level.LevelName);
            _levelControllersMap.Add(level.LevelName, new LevelController(level, levelView));
        }
    }

    public void SetStars(string levelName, int stars)
    {
        _levelControllersMap[levelName].SetStars(stars);
    }

    public void UpdateView()
    {
        foreach (LevelController levelController in _levelControllersMap.Values)
        {
            levelController.UpdateView();
        }
    }
}

