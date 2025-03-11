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
        float startY = Globals.Bounds.Y / 3;
        for (int i = 0; i < menuItems.Length; i++)
        {
            menuPositions[i] = new Vector2(
                centerX,
                startY + i * 80
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
            switch (selectedIndex)
            {
                case 0: //Start Game
                    game.isInMenu = false;
                    break;
                case 1: //Settings
                    Settings();
                    break;
                case 2: //Exit
                    game.Exit();
                    break;
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