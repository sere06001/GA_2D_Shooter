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
        menuItems = new List<string> { "Resume", "Start new game", "Local leaderboard", $"Fullscreen: {(game._graphics.IsFullScreen ? "ON" : "OFF")}", "Exit" };
        menuPositions = new List<Vector2>();
        UpdateMenuPositions();
    }
        private void UpdateMenuPositions()
    {
        menuPositions.Clear();
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

    public void FullscreenMode()
    {
        if (game._graphics.IsFullScreen)
        {
            Globals.WindowModeOffset = 10;
            game._graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - Globals.WindowModeOffset;
            game._graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - Globals.WindowModeOffset;
            game._graphics.IsFullScreen = false;
            game._graphics.ApplyChanges();
        }
        else
        {
            game._graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            game._graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            game._graphics.IsFullScreen = true;
            game._graphics.ApplyChanges();
            Globals.WindowModeOffset = 0;
        }
        Globals.Bounds = new(game._graphics.PreferredBackBufferWidth, game._graphics.PreferredBackBufferHeight);
        menuItems[3] = $"Fullscreen: {(game._graphics.IsFullScreen ? "ON" : "OFF")}";
        UpdateMenuPositions();
        UIManager.UpdateUIBounds();
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
            switch (selectedIndex)
            {
                case 0: //Resume
                    game.isInMenu = false;
                    if (game.isFirstTimeInMenu)
                    {
                        game.isFirstTimeInMenu = false;
                        menuItems[1] = "Restart";
                    }
                    break;
                case 1: //Start new game or Restart
                    game.isInMenu = false;
                    game.Restart();
                    if (game.isFirstTimeInMenu)
                    {
                        game.isFirstTimeInMenu = false;
                        menuItems[1] = "Restart";
                    }
                    break;
                case 2: //Local leaderboard
                    game.isInMenu = false;
                    game.isInLeaderboard = true;
                    break;
                case 3: //Fullscreen toggle
                    FullscreenMode();
                    break;
                case 4: //Exit
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