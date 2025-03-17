namespace GA_2d_shooter;

public class MenuScreen
{
    private readonly Game1 game;
    private readonly List<string> menuItems;
    private int selectedIndex;
    private readonly List<Vector2> menuPositions;
    private readonly Color normalColor = Color.White;
    private readonly Color selectedColor = Color.Yellow;
    private KeyboardState prevKeyState;

    public MenuScreen(Game1 game)
    {
        this.game = game;
        menuItems = new List<string> { "Resume", "Start new game", "Local leaderboard", "Settings", "Exit" };
        menuPositions = new List<Vector2>();

        float centerX = Globals.Bounds.X / 2;
        float centerY = Globals.Bounds.Y / 2;

        float textHeight = Globals.Font.MeasureString(menuItems[0]).Y;
        float spacing = textHeight * 2f;

        int middleIndex = menuItems.Count / 2;

        for (int i = 0; i < menuItems.Count; i++)
        {
            menuPositions.Add(new Vector2(
                centerX,
                centerY + (i - middleIndex) * spacing
            ));
        }
    }

    public void Settings()
    {
        game._graphics.IsFullScreen = !game._graphics.IsFullScreen;
        game._graphics.ApplyChanges();
    }
    public void LocalLeaderboard()
    {
        List<float> times = SaveManager.LoadTimes();
        for (int i = 0; i < times.Count; i++)
        {
            int minutes = (int)times[i] / 60;
            int seconds = (int)times[i] % 60;
            float hundredths = times[i] % 1 * 100;
            string timeString = $"{minutes:D2}:{seconds:D2}.{hundredths:00}";
            Vector2 pos = new(Globals.Bounds.X/2, 100 + i * 50);
            Globals.SpriteBatch.DrawString(Globals.Font, $"{timeString}", pos, Color.White);
        }
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState currentKeyState = Keyboard.GetState();
        
        if (currentKeyState.IsKeyDown(Keys.Down) && !prevKeyState.IsKeyDown(Keys.Down))
            selectedIndex = (selectedIndex + 1) % menuItems.Count;
        
        if (currentKeyState.IsKeyDown(Keys.Up) && !prevKeyState.IsKeyDown(Keys.Up))
            selectedIndex = (selectedIndex - 1 + menuItems.Count) % menuItems.Count;
        
        if (currentKeyState.IsKeyDown(Keys.Enter) && !prevKeyState.IsKeyDown(Keys.Enter))
        {
            if (game.isFirstTimeInMenu)
            {
                game.isFirstTimeInMenu = false;
                menuItems[1] = "Restart";
            }
            switch (selectedIndex)
            {
                case 0: //Resume
                    game.isInMenu = false;
                    break;
                case 1: //Restart
                    game.isInMenu = false;
                    game.Restart();
                    break;
                case 2:
                    LocalLeaderboard();
                    break;
                case 3:
                    Settings();
                    break;
                case 4:
                    game.Exit();
                    break;
            }
        }
        
        prevKeyState = currentKeyState;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Vector2 textMiddlePoint;
        for (int i = 0; i < menuItems.Count; i++)
        {
            Color color = (i == selectedIndex) ? selectedColor : normalColor;
            textMiddlePoint = Globals.Font.MeasureString(menuItems[i]) / 2;
            spriteBatch.DrawString(
                Globals.Font,
                menuItems[i],
                menuPositions[i],
                color,
                0f,
                textMiddlePoint,
                1.5f,
                SpriteEffects.None,
                0f
            );
        }
    }
}