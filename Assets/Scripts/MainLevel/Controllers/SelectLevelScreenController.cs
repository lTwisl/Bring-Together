public class SelectLevelScreenController
{
    private readonly SelectLevelScreenView _selectLevelScreenView;
    private readonly TableLevelsController _tableLevelsController;

    private readonly TableLevelsModel _tableLevelsModel;

    public SelectLevelScreenController(SelectLevelScreenView selectLevelScreenView, ScenesTree scenesTree)
    {
        _selectLevelScreenView = selectLevelScreenView;

        // Инициализация уравней
        _tableLevelsModel = TableLevelsModel.FromJson(scenesTree.GetSceneNames());
        if (_tableLevelsModel == null)
            _tableLevelsModel = new(scenesTree.GetSceneNames());

        _tableLevelsModel.TotalStarsChanged += OnTotalStarsChanged;

        // Инициализация визуала уравней
        _selectLevelScreenView.TableLevelsView.CreateLevelViews(scenesTree.GetSceneTitles());

        _tableLevelsController = new(_tableLevelsModel, _selectLevelScreenView.TableLevelsView);
    }

    public void Show()
    {
        UpdateView();
        _selectLevelScreenView.gameObject.SetActive(true);
    }
    public void Hide() => _selectLevelScreenView.gameObject.SetActive(false);

    public void SetStars(string sceneName, int countStars)
    {
        _tableLevelsController.SetStars(sceneName, countStars);
    }

    private void OnTotalStarsChanged(int totalStars)
    {
        _selectLevelScreenView.TotalStars = totalStars;
    }

    public void SaveModelToJson()
    {
        _tableLevelsModel.ToJson();
    }

    public void UpdateView()
    {
        _tableLevelsController.UpdateView();
        _selectLevelScreenView.TotalStars = _tableLevelsModel.TotalStars;
    }
}
