using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager
{
    public static GameManager Instance { get; private set; }

    private Coroutines _coroutines;
    private DataFinishedLevel _dataFinishedLevel;

    private UiLoadingScreenView _uiLoadingScreen;

    public const string FILE_SAVE = ".\\UserSettings\\Save.json";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AutostartGame()
    {
        // TODO: ��������� ��������� �������.

        Instance = new GameManager();
        Instance.RunGame();
    }

    private GameManager()
    {
        _coroutines = new GameObject("Coroutines").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(_coroutines.gameObject);

        _dataFinishedLevel = new DataFinishedLevel();

        UiLoadingScreenView prefabUiLoadingScreen = Resources.Load<UiLoadingScreenView>("CanvasLoadingScreen");
        _uiLoadingScreen = Object.Instantiate(prefabUiLoadingScreen);
        Object.DontDestroyOnLoad(_uiLoadingScreen.gameObject);

    }

    private void RunGame()
    {
#if UNITY_EDITOR
        string sceneName = SceneManager.GetActiveScene().name;
        LoadScene(sceneName);
#else
        LoadScene(ScenesName.MAIN_MENU);
#endif
    }

    public void LoadScene(string sceneName)
    {
        _coroutines.StartCoroutine(LoadAndStartScene(sceneName));
    }

    private IEnumerator LoadAndStartScene(string sceneName)
    {
        _uiLoadingScreen.Show();

        yield return SceneManager.LoadSceneAsync(ScenesName.BOOT);
        yield return SceneManager.LoadSceneAsync(sceneName);

        yield return null/*new WaitForSeconds(2)*/; // ��� ������ 2 �������, ��� ������ ���������� null (������� ������ �����)

        if (sceneName == ScenesName.MAIN_MENU)
        {
            MainMenuManager manager = Object.FindObjectOfType<MainMenuManager>();
            if (manager != null)
                manager.Run(_dataFinishedLevel);
            else
                Debug.LogError("�� ������ ��� ���������");
        }
        else
        {
            GameLevelManager manager = Object.FindObjectOfType<GameLevelManager>();
            if (manager != null)
                manager.Run(_dataFinishedLevel);
            else
                Debug.LogError("�� ������ ��� ���������");
        }

        _uiLoadingScreen.Hide();
    }
}
