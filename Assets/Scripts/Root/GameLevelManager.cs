using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager Instance { get; private set; }

    public event Action<int> ChanchedScore;
    public event Action<int> LevelComplited;

    [field: SerializeField] public ItemsContainer ItemsContainer {get; private set;}
    [SerializeField] private int ScoreForComplited;

    private int _score;
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            ChanchedScore?.Invoke(_score);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void LoadSceneGame(string scene)
    {
        GameManager.Instance.LoadScene(scene);
    }

    public void Run(DataFinishedLevel data)
    {
        ChanchedScore += (int score) =>
        {
            if (Score >= ScoreForComplited)
                LevelComplited?.Invoke(Score);
        };

        LevelComplited += (int score) =>
        {
            Debug.Log($"Score = {score}");

            data.Score = score;
            data.SceneName = SceneManager.GetActiveScene().name;
            LoadSceneGame(ScenesName.MAIN);
        };

        Item.Merged += OnMerged;
    }


    private void OnDestroy()
    {
        Item.Merged -= OnMerged;
    }

    private void OnMerged(Item newItem)
    {
        Score += ItemsContainer[newItem.Index].pointsForMerging;
        Debug.Log($"Score = {Score}");
    }
}
