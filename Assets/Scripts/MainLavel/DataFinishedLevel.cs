public static class DataFinishedLevel
{
    public static string SceneName { get; set; }
    public static int Score { get; set; }
    public static int Stars { get; set; }

    public static void Clear()
    {
        SceneName = "";
        Score = 0;
        Stars = 0;
    }
}
