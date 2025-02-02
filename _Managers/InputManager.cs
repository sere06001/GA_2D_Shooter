namespace GA_2d_shooter;

public static class InputManager
{
    private static MouseState _lastMouseState;
    private static KeyboardState _lastKeyboardState;
    private static Vector2 _direction;
    public static Vector2 Direction => _direction;
    public static Vector2 MousePosition => Mouse.GetState().Position.ToVector2();
    public static bool MouseClicked { get; private set; }
    //public static bool MouseRightClicked { get; private set; }
    public static bool MouseLeftDown { get; private set; }
    public static Keys? WeaponKey { get; private set; }

    public static void Update(Player player)
    {
        var keyboardState = Keyboard.GetState();
        var mouseState = Mouse.GetState();

        _direction = Vector2.Zero;
        if (keyboardState.IsKeyDown(Keys.W)) _direction.Y--;
        if (keyboardState.IsKeyDown(Keys.S)) _direction.Y++;
        if (keyboardState.IsKeyDown(Keys.A)) _direction.X--;
        if (keyboardState.IsKeyDown(Keys.D)) _direction.X++;
        if (keyboardState.IsKeyDown(Keys.R)) player.Weapon.Reload();

        MouseLeftDown = mouseState.LeftButton == ButtonState.Pressed;
        MouseClicked = MouseLeftDown && (_lastMouseState.LeftButton == ButtonState.Released);
        /*MouseRightClicked = mouseState.RightButton == ButtonState.Pressed
                            && (_lastMouseState.RightButton == ButtonState.Released);*/

        if (_lastKeyboardState.IsKeyUp(Keys.D1) && keyboardState.IsKeyDown(Keys.D1))
            WeaponKey = Keys.D1;
        else if (_lastKeyboardState.IsKeyUp(Keys.D2) && keyboardState.IsKeyDown(Keys.D2))
            WeaponKey = Keys.D2;
        else if (_lastKeyboardState.IsKeyUp(Keys.D3) && keyboardState.IsKeyDown(Keys.D3))
            WeaponKey = Keys.D3;
        else
            WeaponKey = null;

        //WeaponKeys = _lastKeyboardState.IsKeyUp(Keys.D1) && keyboardState.IsKeyDown(Keys.D1);

        _lastMouseState = mouseState;
        _lastKeyboardState = keyboardState;
    }
}
