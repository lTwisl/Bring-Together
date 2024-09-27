public class LevelController
{
    private LevelModel _levelModel;
    private LevelView _levelView;

    public string LevelName => _levelModel.LevelName;

    public LevelController(LevelModel levelModel, LevelView levelView)
    {
        _levelModel = levelModel;
        _levelView = levelView;

        levelView.BtnOpenLevel.onClick.AddListener(() => levelModel.LoadLevel());
        levelModel.StarsChanged += (int stars) => levelView.Stars = stars;
    }

    public void SetStars(int stars)
    {
        _levelModel.Stars = stars;
    }

    public void UpdateView()
    {
        _levelView.Stars = _levelModel.Stars;
    }
}

