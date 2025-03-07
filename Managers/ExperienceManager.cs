namespace GA_2d_shooter;

public static class ExperienceManager
{
    private static Texture2D texture;
    private static Vector2 position;
    private static Vector2 textPosition;
    private static string playerExp;

    public static void Init(Texture2D tex)
    {
        texture = tex;
        position = new(0, 0);
    }

    public static void Update(Player player)
    {
        position = new (player.Position.X, player.Position.Y);
        position = new (position.X+Globals.Bounds.X/2-texture.Width, position.Y-Globals.Bounds.Y/2-texture.Height+50);
        playerExp = player.Experience.ToString();
        var x = Globals.Font.MeasureString(playerExp).X / 2;
        textPosition = new(position.X - texture.Width, position.Y+texture.Height/2);

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
        Globals.SpriteBatch.Draw(texture, position, null, Color.White * 1f, 1f, Vector2.Zero, 2f, SpriteEffects.None, 1f);
        Globals.SpriteBatch.DrawString(Globals.Font, playerExp, textPosition, Color.White);
    }
}
