public class LevelController
{
    private readonly LevelModel _levelModel;
    private readonly LevelView _levelView;

    public string LevelName => _levelModel.LevelName;

    public LevelController(LevelModel levelModel, LevelView levelView)
    {
        _levelModel = levelModel;
        _levelView = levelView;

        _levelView.BtnOpenLevel.onClick.AddListener(OnOpenLevel);
        _levelModel.StarsChanged += OnStarsChanged;
    }

    public void SetStars(int stars)
    {
        _levelModel.Stars = stars;
    }

    public void UpdateView()
    {
        _levelView.Stars = _levelModel.Stars;
    }

    private void OnStarsChanged(int stars)
    {
        _levelView.Stars = stars;
    }

    private void OnOpenLevel()
    {
        GameManager.Instance.LoadScene(_levelModel.LevelName);
    }
}

