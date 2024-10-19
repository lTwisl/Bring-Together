using System;
using System.IO;
using System.Linq;
using UnityEngine;


[Serializable]
public class TableLevelsModel
{
    public event Action<int> TotalStarsChanged;

    [field: SerializeField, HideInInspector] public LevelModel[] Levels { get; private set; }

    [SerializeField, HideInInspector] private int _totalStars;
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

    public void ToJson()
    {
        string jsonText = JsonUtility.ToJson(this, true);
        File.WriteAllText(GameManager.FILE_SAVE, jsonText);
    }

    public static TableLevelsModel FromJson(string[] sceneNames = null)
    {
        TableLevelsModel model;
        try
        {
            model = JsonUtility.FromJson<TableLevelsModel>(File.ReadAllText(GameManager.FILE_SAVE));
        }
        catch
        {
            return null;
        }

        if (sceneNames != null)
        {
            if (sceneNames.Length != model.Levels.Length)
            {
                Debug.LogWarning($"Количество сцен в сохранённом файле не совпадают с количеством в ScenesTree. " +
                    $"{model.Levels.Length} и {sceneNames.Length}");
            }
            else
            {
                for (int i = 0; i < sceneNames.Length; i++)
                {
                    if (sceneNames[i] != model.Levels[i].LevelName)
                    {
                        Debug.LogWarning($"Не совпадают имена сцен {i}: {model.Levels[i].LevelName} и {sceneNames[i]}");
                        break;
                    }
                }
            }
        }

        for (int i = 0; i < model.Levels.Length; i++)
        {
            model.Levels[i].StarsChanged += model.OnStarsChanged;
        }
        return model;
    }
}

