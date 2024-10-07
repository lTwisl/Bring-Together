public class SelectLevelScreenController
{
    private readonly SelectLevelScreenView _selectLevelScreenView;

    private readonly TableLevelsController _tableLevelsController;

    //public SelectLevelScreenController(SelectLevelScreenView selectLevelScreenView, string[] levels)
    //{
    //    _selectLevelScreenView = selectLevelScreenView;

    //    TableLevelsModel model = new(levels);
    //    _tableLevelsController = new(model, _selectLevelScreenView.TableLevelsView);

    //    model.TotalStarsChanged += OnTotalStarsChanged;

    //    _tableLevelsController.UpdateView();
    //}

    public SelectLevelScreenController(SelectLevelScreenView selectLevelScreenView, ScenesTree scenesTree)
    {
        _selectLevelScreenView = selectLevelScreenView;

        TableLevelsModel model = new(scenesTree);
        _tableLevelsController = new(model, _selectLevelScreenView.TableLevelsView);

        model.TotalStarsChanged += OnTotalStarsChanged;

        _tableLevelsController.UpdateView();
    }

    public void Show() => _selectLevelScreenView.gameObject.SetActive(true);
    public void Hide() => _selectLevelScreenView.gameObject.SetActive(false);

    public void SetStars(string levelName, int stars)
    {
        _tableLevelsController.SetStars(levelName, stars);
    }

    private void OnTotalStarsChanged(int totalStars)
    {
        _selectLevelScreenView.TotalStars = totalStars;
    }
}
