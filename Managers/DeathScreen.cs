namespace GA_2d_shooter;

public class DeathScreen
{
    private readonly Game1 game;
    private readonly float finalTime;
    private Vector2 messagePosition;
    private Vector2 timePosition;
    private Vector2 killsPosition;
    private readonly Color textColor = Color.Red;
    private readonly Color statsColor = Color.White;

    public DeathScreen(Game1 game, float time)
    {
        this.game = game;
        this.finalTime = time;
        UpdatePositions();
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState currentKeyState = Keyboard.GetState();

        if (currentKeyState.IsKeyDown(Keys.Enter) || currentKeyState.IsKeyDown(Keys.Escape))
        {
            game.isInMenu = true;
            game.isInDeathScreen = false;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(
            Globals.Font,
            "YOU DIED",
            messagePosition,
            textColor,
            0f,
            Globals.Font.MeasureString("YOU DIED") / 2,
            2.0f,
            SpriteEffects.None,
            0f
        );

        int minutes = (int)finalTime / 60;
        int seconds = (int)finalTime % 60;
        float hundredths = finalTime % 1 * 100;
        string timeString = $"Your time: {minutes:D2}:{seconds:D2}.{hundredths:00}";

        spriteBatch.DrawString(
            Globals.Font,
            timeString,
            timePosition,
            statsColor,
            0f,
            Globals.Font.MeasureString(timeString) / 2,
            1.5f,
            SpriteEffects.None,
            0f
        );

        string killsString = $"Your score: {game.gameManager.player.ScoreToDisplay}";
        
        spriteBatch.DrawString(
            Globals.Font,
            killsString,
            killsPosition,
            statsColor,
            0f,
            Globals.Font.MeasureString(killsString) / 2,
            1.5f,
            SpriteEffects.None,
            0f
        );
    }

    private void UpdatePositions()
    {
        float centerX = Globals.Bounds.X / 2;
        float centerY = Globals.Bounds.Y / 2;

        messagePosition = new Vector2(centerX, centerY - 100);
        timePosition = new Vector2(centerX, centerY);
        killsPosition = new Vector2(centerX, centerY + 100);
    }
}