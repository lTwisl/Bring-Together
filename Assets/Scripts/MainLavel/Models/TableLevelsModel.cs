﻿using System;
using System.Linq;
using UnityEngine;


public class TableLevelsModel
{
    public event Action<int> TotalStarsChanged;

    [field: SerializeField] public LevelModel[] Levels { get; private set; }

    private int _totalStars;
    public int TotalStars
    {
        get => _totalStars;
        private set
        {
            _totalStars = value;
            TotalStarsChanged?.Invoke(TotalStars);
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

    public void OnStarsChanged(int stars)
    {
        CalculateTotalStars();
    }

    public void CalculateTotalStars()
    {
        TotalStars = Levels.Sum(x => x.Stars);
    }
}

