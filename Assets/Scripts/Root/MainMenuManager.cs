using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance { get; private set; }

    [SerializeField] private string[] _levelNames;

    [SerializeField] private ScreenView _screenView;
    private ScreenController _screenController;

    public virtual void Awake()
    {
        Instance = this;
        _screenController = new ScreenController(_screenView, _levelNames);
    }

    private void Start()
    {
        _screenController.TableLevelsController.SetStars("Game", 2);
    }

    public void Run()
    {

    }

    public void LoadSceneGame(string scene)
    {
        GameManager.Instance.LoadScene(scene);
    }
}
