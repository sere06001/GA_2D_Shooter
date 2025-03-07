namespace GA_2d_shooter;

public static class ExperienceManager
{
    private static Texture2D texture;
    private static SpriteFont font;
    private static Vector2 position;
    private static Vector2 textPosition;
    private static string playerExp;

    public static void Init(Texture2D tex)
    {
        texture = tex;
        font = Globals.Content.Load<SpriteFont>("font");
        position = new(Globals.Bounds.X - (2 * texture.Width), 0);
    }

    public static void Update(Player player)
    {
        playerExp = player.Experience.ToString();
        var x = font.MeasureString(playerExp).X / 2;
        textPosition = new(Globals.Bounds.X - x - 32, 14);

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
        Globals.SpriteBatch.DrawString(font, playerExp, textPosition, Color.White);
    }
}
