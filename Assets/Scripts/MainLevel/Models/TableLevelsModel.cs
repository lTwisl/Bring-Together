using System;
using System.Collections.Generic;
using System.Linq;


public class TableLevelsModel
{
    public event Action<int> TotalStarsChanged;

    public LevelModel[] Levels { get; private set; }

    private int _totalStars;
    public int TotalStars
    {
        get => _totalStars;
        private set
        {
            if (_totalStars == value)
                return;

            _totalStars = value;
            TotalStarsChanged?.Invoke(_totalStars);
        }
    }

    public TableLevelsModel(string[] sceneNames)
    {
        Levels = new LevelModel[sceneNames.Length];
        for (int i = 0; i < sceneNames.Length; i++)
        {
            Levels[i] = new LevelModel(0, sceneNames[i]);
            Levels[i].StarsChanged += OnStarsChanged;
        }
    }

    private void OnStarsChanged(int stars)
    {
        CalculateTotalStars();
    }

    public void CalculateTotalStars()
    {
        TotalStars = Levels.Sum(x => x.Stars);
    }
}

