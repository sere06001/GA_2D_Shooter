namespace GA_2d_shooter;

public static class ExperienceManager
{
    private static Texture2D texture;
    private static Vector2 position;
    private static string playerExp;
    private static Vector2 centeredPosition;

    public static void Init(Texture2D tex)
    {
        texture = tex;
        position = new(0, 0);
    }

    public static void Update(Player player)
    {
        position = new(player.Position.X, player.Position.Y);
    
        position = new(
            position.X + Globals.Bounds.X/2 - texture.Width*2,  // Account for 2x scale
            position.Y - Globals.Bounds.Y/2 + 50
        );

        playerExp = player.Experience.ToString();
    
        Vector2 textSize = Globals.Font.MeasureString(playerExp);
        
        centeredPosition = new Vector2(
            position.X + texture.Width - textSize.X/2,
            position.Y + texture.Height - textSize.Y/2
        );

        foreach (Weapon gun in player.WeaponList)
        {
            if (player.Experience >= gun.XPforUnlock)
            {
                gun.UnlockGun();
            }
        }
    }

    public static void Draw()
    {
        if (texture == null || Globals.Font == null || Globals.SpriteBatch == null || string.IsNullOrEmpty(playerExp))
            return;

        Globals.SpriteBatch.Draw(texture, position, null, Color.White * 1f, 0f, Vector2.Zero, 2f, SpriteEffects.None, 1f);
        
        Globals.SpriteBatch.DrawString(Globals.Font, playerExp, centeredPosition, Color.White);
    }
}