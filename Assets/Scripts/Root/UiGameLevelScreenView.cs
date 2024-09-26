using UnityEngine;
using UnityEngine.UI;

public class UiGameLevelScreenView : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    private void Start()
    {
        GameLevelManager.Instance.ChanchedScore += UpdateScore;
    }

    private void OnDestroy()
    {
        GameLevelManager.Instance.ChanchedScore -= UpdateScore;
    }

    private void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}
