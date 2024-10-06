using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager Instance { get; private set; }

   
    public event Action<int> ChanchedScore;
    public event Action<int> LevelComplited;

    [field: SerializeField] public ItemsContainer ItemsContainer { get; private set; }
    [SerializeField] private int _countStars;

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

    private DataFinishedLevel _dataFinishedLevel;


    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        Item.Merged += OnMerged;
    }

    private void OnDisable()
    {
        Item.Merged -= OnMerged;
    }

    public void Run(DataFinishedLevel dataFinishedLevel)
    {
        _dataFinishedLevel = dataFinishedLevel;
        _dataFinishedLevel.Clear();

        _dataFinishedLevel.SceneName = SceneManager.GetActiveScene().name;
    }

    private void OnMerged(Item newItem)
    {
        Score += ItemsContainer[newItem.Index].pointsForMerging;
        _dataFinishedLevel.Score = Score;

        int countItems = ItemsContainer.Length;
        if (newItem.Index >= countItems - _countStars)
        {
            _countStars -= 1;
            _dataFinishedLevel.Stars += 1;
            Debug.Log("Pickup star!!!");


            if (_countStars <= 0)
            {
                LevelComplited?.Invoke(Score);
                Debug.Log("Level complited!!!");

                GameManager.Instance.LoadScene(ScenesName.MAIN_MENU);
            }
        }

        /*Debug.Log($"Score = {Score}");*/
    }
}
