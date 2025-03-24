namespace GA_2d_shooter;

public class LeaderboardScreen
{
    private readonly Game1 game;
    private List<float> times;
    private List<Vector2> leaderboardPositions;
    private readonly Color textColor = Color.White;
    private int amountOfTimesToShow = 10;

    public LeaderboardScreen(Game1 game)
    {
        this.game = game;
        times = SaveManager.LoadTimes();
        UpdateLeaderboardPositions();
    }

    public void Update(GameTime gameTime)
    {
        times = SaveManager.LoadTimes();
        UpdateLeaderboardPositions();

        KeyboardState currentKeyState = Keyboard.GetState();

        if (currentKeyState.IsKeyDown(Keys.Escape))
        {
            game.isInMenu = true;
            game.isInLeaderboard = false;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < amountOfTimesToShow; i++)
        {
            int minutes;
            int seconds;
            float hundredths;
            string timeString;

            if (i < times.Count)
            {
                minutes = (int)times[i] / 60;
                seconds = (int)times[i] % 60;
                hundredths = times[i] % 1 * 100;
                timeString = $"{i + 1}:  {minutes:D2}:{seconds:D2}.{hundredths:00}";
            }
            else if (i+1 >= 10)
            {
                timeString = $"{i + 1}:  00:00.00";
            }
            else
            {
                timeString = $"{i + 1}:   00:00.00";
            }

            spriteBatch.DrawString(
                Globals.Font,
                timeString,
                leaderboardPositions[i],
                textColor,
                0f,
                Globals.Font.MeasureString(timeString) / 2,
                1.5f,
                SpriteEffects.None,
                0f
            );
        }
    }

    private void UpdateLeaderboardPositions()
    {
        leaderboardPositions = new List<Vector2>();

        float centerX = Globals.Bounds.X / 2;
        float centerY = Globals.Bounds.Y / 2;
        float spacing = (Globals.Bounds.Y-200)/amountOfTimesToShow;

        for (int i = 0; i < amountOfTimesToShow; i++)
        {
            float offset = (i - 4.5f) * spacing;
            leaderboardPositions.Add(new Vector2(centerX, centerY + offset));
        }
    }
}