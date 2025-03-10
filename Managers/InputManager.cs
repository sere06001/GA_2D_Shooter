namespace GA_2d_shooter;

public static class InputManager
{
    private static MouseState lastMouseState;
    private static KeyboardState lastKeyboardState;
    private static Vector2 direction;
    public static Vector2 Direction => direction;
    public static Vector2 MousePosition => Mouse.GetState().Position.ToVector2();
    //public static bool MouseClicked { get; private set; }
    public static bool MouseLeftDown { get; private set; }
    public static Keys? WeaponKey { get; private set; }
    public static void Update(Player player)
    {
        var keyboardState = Keyboard.GetState();
        var mouseState = Mouse.GetState();

        direction = Vector2.Zero;
        if (keyboardState.IsKeyDown(Keys.W)) direction.Y--;
        if (keyboardState.IsKeyDown(Keys.S)) direction.Y++;
        if (keyboardState.IsKeyDown(Keys.A)) direction.X--;
        if (keyboardState.IsKeyDown(Keys.D)) direction.X++;
        if (keyboardState.IsKeyDown(Keys.R)) player.Weapon.Reload();

        MouseLeftDown = mouseState.LeftButton == ButtonState.Pressed;
        //MouseClicked = MouseLeftDown && (lastMouseState.LeftButton == ButtonState.Released);

        if (lastKeyboardState.IsKeyUp(Keys.D1) && keyboardState.IsKeyDown(Keys.D1))
            WeaponKey = Keys.D1;
        else if (lastKeyboardState.IsKeyUp(Keys.D2) && keyboardState.IsKeyDown(Keys.D2)
        && player.Sniper.IsUnlocked)
            WeaponKey = Keys.D2;
        else if (lastKeyboardState.IsKeyUp(Keys.D3) && keyboardState.IsKeyDown(Keys.D3) 
        && player.Shotgun.IsUnlocked)
            WeaponKey = Keys.D3;
        else if (lastKeyboardState.IsKeyUp(Keys.D4) && keyboardState.IsKeyDown(Keys.D4) 
        && player.SMG.IsUnlocked)
            WeaponKey = Keys.D4;
        else if (lastKeyboardState.IsKeyUp(Keys.D5) && keyboardState.IsKeyDown(Keys.D5) 
        && player.Minigun.IsUnlocked)
            WeaponKey = Keys.D5;
        else
            WeaponKey = null;

        lastMouseState = mouseState;
        lastKeyboardState = keyboardState;
    }
}
