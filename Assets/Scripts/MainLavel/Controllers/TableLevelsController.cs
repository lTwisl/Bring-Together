
using System.Linq;
using UnityEngine;

public class TableLevelsController
{
    private TableLevelsModel _model;
    private TableLevelsView _view;

    private LevelController[] _levelControllers;

    public TableLevelsController(TableLevelsModel model, TableLevelsView view)
    {
        _model = model;
        _view = view;

        _levelControllers = new LevelController[_model.Levels.Length];
        for (int i = 0; i < _model.Levels.Length; ++i)
        {
            _levelControllers[i] = new LevelController(_model.Levels[i], _view.CreateLevelView());
        }

        //_model.TotalStarsChanged += (int totalStars) => _view.TotalStars = _model.TotalStars;
    }

    public void SetStars(string levelName, int stars)
    {
        _levelControllers.First(x => x.LevelName == levelName).SetStars(stars);
    }

    public void UpdateView()
    {
        for (int i = 0; i < _levelControllers.Length; ++i)
        {
            _levelControllers[i].UpdateView();
        }
        //_model.CalculateTotalStars();
    }
}

