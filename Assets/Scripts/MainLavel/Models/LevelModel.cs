using System;
using UnityEngine;


public class LevelModel
{
    public event Action<int> StarsChanged;

    [SerializeField] public string LevelName;

    [SerializeField] private int _stars;
    public int Stars { 
        get => _stars; 
        set
        {
            _stars = value;
            StarsChanged?.Invoke(_stars);
        }
    }

    public LevelModel(int stars, string levelName)
    {
        Stars = stars;
        LevelName = levelName;
    }

    public void LoadLevel()
    {
        GameManager.Instance.LoadScene(LevelName);
    }
}

