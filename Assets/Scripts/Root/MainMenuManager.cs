using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance { get; private set; }

    [SerializeField] private StartMenuScreenView _startMenuScreen;
    [SerializeField] private SelectLevelScreenView _selectLevelScreen;
    //[SerializeField] public string[] _levelNames;
    private ScenesTree scenesTree;

    private SelectLevelScreenController _selectLevelScreenController;


    public virtual void Awake()
    {
        Instance = this;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        scenesTree = Resources.Load("Scenes Tree") as ScenesTree;
    }

    public void Run(DataFinishedLevel dataFinishedLevel)
    {
        _selectLevelScreenController = new SelectLevelScreenController(_selectLevelScreen, scenesTree);

        if (dataFinishedLevel.SceneName != null && dataFinishedLevel.SceneName != "")
            _selectLevelScreenController.SetStars(dataFinishedLevel.SceneName, dataFinishedLevel.Stars);

        _selectLevelScreenController.Show();
        _selectLevelScreenController.SaveModelToJson();
    }
}
