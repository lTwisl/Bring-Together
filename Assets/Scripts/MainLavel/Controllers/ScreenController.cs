public class ScreenController
{
    private ScreenView _view;

    public TableLevelsController TableLevelsController { get; private set; }
    private TableLevelsModel _tableLevelsModel;

    public ScreenController(ScreenView view, string[] levelNames)
    {
        _view = view;

        _tableLevelsModel = new TableLevelsModel(levelNames);
        TableLevelsController = new TableLevelsController(_tableLevelsModel, _view.TableLevelsView);

        _tableLevelsModel.TotalStarsChanged += OnTotalStarsChanged; 
    }

    void OnTotalStarsChanged(int totalStars)
    {
        _view.TotalStars = totalStars;
    }
}
