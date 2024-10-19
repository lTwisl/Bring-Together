using System.Linq;

public class SelectLevelScreenController
{
    private readonly SelectLevelScreenView _selectLevelScreenView;

    private readonly TableLevelsController _tableLevelsController;

    public SelectLevelScreenController(SelectLevelScreenView selectLevelScreenView, ScenesTree scenesTree)
    {
        _selectLevelScreenView = selectLevelScreenView;

        TableLevelsModel model = new(scenesTree.GetSceneNames());
        _selectLevelScreenView.TableLevelsView.CreateLevelViews(scenesTree.GetSceneTitles());

        _tableLevelsController = new(model, _selectLevelScreenView.TableLevelsView);

        model.TotalStarsChanged += OnTotalStarsChanged;

        _tableLevelsController.UpdateView();
    }

    public void Show() => _selectLevelScreenView.gameObject.SetActive(true);
    public void Hide() => _selectLevelScreenView.gameObject.SetActive(false);

    public void SetStars(string sceneName, int countStars)
    {
        _tableLevelsController.SetStars(sceneName, countStars);
    }

    private void OnTotalStarsChanged(int totalStars)
    {
        _selectLevelScreenView.TotalStars = totalStars;
    }
}
