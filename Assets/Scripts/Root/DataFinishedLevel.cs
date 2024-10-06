public class DataFinishedLevel
{
    public string SceneName { get; set; } = "";
    public int Score { get; set; } = 0;
    public int Stars { get; set; } = 0;

    public void Clear()
    {
        SceneName = "";
        Score = 0;
        Stars = 0;
    }
}
