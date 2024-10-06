using System;
using UnityEngine;


public class LevelModel
{
    public event Action<int> StarsChanged;

    public string LevelName;

    private int _stars;
    public int Stars { 
        get => _stars; 
        set
        {
            if (_stars == value)
                return;

            _stars = value;
            StarsChanged?.Invoke(_stars);
        }
    }

    public LevelModel(int stars, string levelName)
    {
        Stars = stars;
        LevelName = levelName;
    }
}

