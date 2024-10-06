using System;
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

    public TableLevelsModel(LevelModel[] levels)
    {
        Levels = levels;
        foreach (LevelModel levelModel in Levels)
        {
            levelModel.StarsChanged += OnStarsChanged;
        }
    }

    public TableLevelsModel(string[] levelNames)
    {
        Levels = new LevelModel[levelNames.Length];
        for (int i = 0; i < levelNames.Length; i++)
        {
            Levels[i] = new LevelModel(0, levelNames[i]);
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

