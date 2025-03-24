namespace GA_2d_shooter;

public class LeaderboardScreen
{
    private readonly Game1 game;
    private readonly List<float> times;
    private readonly List<Vector2> leaderboardPositions;
    private readonly Color textColor = Color.White;

    public LeaderboardScreen(Game1 game)
    {
        this.game = game;
        times = SaveManager.LoadTimes();
        leaderboardPositions = new List<Vector2>();

        float centerX = Globals.Bounds.X / 2;
        float startY = 100;
        float spacing = 50;

        for (int i = 0; i < times.Count; i++)
        {
            leaderboardPositions.Add(new Vector2(centerX, startY + i * spacing));
        }
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState currentKeyState = Keyboard.GetState();

        if (currentKeyState.IsKeyDown(Keys.Escape))
        {
            game.isInMenu = true;
            game.isInLeaderboard = false;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < times.Count; i++)
        {
            int minutes = (int)times[i] / 60;
            int seconds = (int)times[i] % 60;
            float hundredths = times[i] % 1 * 100;
            string timeString = $"{minutes:D2}:{seconds:D2}.{hundredths:00}";

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
}