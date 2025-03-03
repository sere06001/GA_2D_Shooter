namespace GA_2d_shooter;

public static class ExperienceManager
{
    private static Texture2D texture;
    public static List<Experience> Experience { get; } = [];
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

    public static void Reset()
    {
        Experience.Clear();
    }

    public static void AddExperience(Vector2 pos)
    {
        Experience.Add(new(texture, pos));
    }

    public static void Update(Player player)
    {
        foreach (var e in Experience)
        {
            e.Update();

            if ((e.Position - player.Position).Length() < 50)
            {
                e.Collect();
                player.GetExperience(1);
            }
        }

        Experience.RemoveAll((e) => e.Lifespan <= 0);

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
        Globals.SpriteBatch.Draw(texture, position, null, Color.White * 0.75f, 0f, Vector2.Zero, 2f, SpriteEffects.None, 1f);
        Globals.SpriteBatch.DrawString(font, playerExp, textPosition, Color.White);

        foreach (var e in Experience)
        {
            e.Draw();
        }
    }
}
