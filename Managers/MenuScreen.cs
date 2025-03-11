namespace GA_2d_shooter;
public class MenuScreen
{
    private readonly Game1 _game;
    private readonly string[] _menuItems = { "Start Game", "Options", "Exit" };
    private int _selectedIndex;
    private readonly Vector2[] _menuPositions;
    private readonly Color _normalColor = Color.White;
    private readonly Color _selectedColor = Color.Yellow;
    private KeyboardState _prevKeyState;
    public MenuScreen(Game1 game)
    {
        _game = game;
        _menuPositions = new Vector2[_menuItems.Length];
        // Calculate center positions for menu items
        float centerX = Globals.Bounds.X / 2;
        float startY = Globals.Bounds.Y / 3;
        for (int i = 0; i < _menuItems.Length; i++)
        {
            _menuPositions[i] = new Vector2(
                centerX,
                startY + i * 80
            );
        }
    }
    public void Update(GameTime gameTime)
    {
        KeyboardState currentKeyState = Keyboard.GetState();
        if (currentKeyState.IsKeyDown(Keys.Down) && !_prevKeyState.IsKeyDown(Keys.Down))
            _selectedIndex = (_selectedIndex + 1) % _menuItems.Length;
        if (currentKeyState.IsKeyDown(Keys.Up) && !_prevKeyState.IsKeyDown(Keys.Up))
            _selectedIndex = (_selectedIndex - 1 + _menuItems.Length) % _menuItems.Length;
        if (currentKeyState.IsKeyDown(Keys.Enter) && !_prevKeyState.IsKeyDown(Keys.Enter))
        {
            switch (_selectedIndex)
            {
                case 0: // Start Game
                    ((Game1)_game).isInMenu = false;
                    break;
                case 1: // Options
                    // Add options menu logic here
                    break;
                case 2: // Exit
                    _game.Exit();
                    break;
            }
        }
        _prevKeyState = currentKeyState;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        Vector2 textMiddlePoint;
        for (int i = 0; i < _menuItems.Length; i++)
        {
            Color color = (i == _selectedIndex) ? _selectedColor : _normalColor;
            textMiddlePoint = Globals.Font.MeasureString(_menuItems[i]) / 2;
            spriteBatch.DrawString(
                Globals.Font,
                _menuItems[i],
                _menuPositions[i],
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