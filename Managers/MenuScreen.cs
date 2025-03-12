namespace GA_2d_shooter;
public class MenuScreen
{
    private readonly Game1 game;
    private readonly string[] menuItems = { "Start Game", "Options", "Exit" };
    private int selectedIndex;
    private readonly Vector2[] menuPositions;
    private readonly Color normalColor = Color.White;
    private readonly Color selectedColor = Color.Yellow;
    private KeyboardState prevKeyState;
    public MenuScreen(Game1 game)
    {
        this.game = game;
        menuPositions = new Vector2[menuItems.Length];

        float centerX = Globals.Bounds.X / 2;
        float centerY = Globals.Bounds.Y / 2;

        // Calculate text height and add some padding
        float textHeight = Globals.Font.MeasureString(menuItems[0]).Y;
        float spacing = textHeight * 2f; // Double the text height for spacing

        // Find middle item index
        int middleIndex = menuItems.Length / 2;

        // Calculate positions relative to middle
        for (int i = 0; i < menuItems.Length; i++)
        {
            menuPositions[i] = new Vector2(
                centerX,
                centerY + (i - middleIndex) * spacing
            );
        }
    }
    public void Settings()
    {
        
    }
    public void Update(GameTime gameTime)
    {
        KeyboardState currentKeyState = Keyboard.GetState();
        if (currentKeyState.IsKeyDown(Keys.Down) && !prevKeyState.IsKeyDown(Keys.Down))
            selectedIndex = (selectedIndex + 1) % menuItems.Length;
        if (currentKeyState.IsKeyDown(Keys.Up) && !prevKeyState.IsKeyDown(Keys.Up))
            selectedIndex = (selectedIndex - 1 + menuItems.Length) % menuItems.Length;
        if (currentKeyState.IsKeyDown(Keys.Enter) && !prevKeyState.IsKeyDown(Keys.Enter))
        {
            if (game.isFirstTimeStartingGame)
            {
                switch (selectedIndex)
                {
                    case 0: //Start new game
                        game.isInMenu = false;
                        break;
                    case 1: //Settings
                        Settings();
                        break;
                    case 2: //Exit
                        game.Exit();
                        break;
                }
                game.isFirstTimeStartingGame = false;
            }
            else
            {
                switch (selectedIndex)
                {
                    case 0: //Start new game
                        game.isInMenu = false;
                        game.Restart();
                        break;
                    case 1: //Resume
                        game.isInMenu = false;
                        break;
                    case 2: //Exit
                        game.Exit();
                        break;
                }
            }
        }
        prevKeyState = currentKeyState;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        Vector2 textMiddlePoint;
        for (int i = 0; i < menuItems.Length; i++)
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