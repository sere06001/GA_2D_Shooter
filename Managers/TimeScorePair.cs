namespace GA_2d_shooter;
public class TimeScorePair
{
    public float Time { get; set; }
    public int Score { get; set; }

    public TimeScorePair(float time, int score)
    {
        Time = time;
        Score = score;
    }
}