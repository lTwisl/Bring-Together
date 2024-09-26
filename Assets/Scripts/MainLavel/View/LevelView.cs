using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    public string LevelName;

    [SerializeField] private Button BtnOpenLevel;

    public GameObject[] _stars;

    private void Awake()
    {
        BtnOpenLevel.onClick.AddListener(() => GameManager.Instance.LoadScene(LevelName));
    }
}
