namespace GA_2d_shooter;

public static class UIManager
{
    private static Texture2D bulletTexture;

    public static void Init(Texture2D tex)
    {
        bulletTexture = tex;
    }

    public static void Draw(Player player)
    {
        Texture2D bulletTexture = player.Weapon.ProjectileTextureUI;
        Color c = player.Weapon.Reloading ? Color.Red : Color.White;

        /*for (int i = 0; i < player.Weapon.Ammo; i++)
        {
            Vector2 pos = new(0, i * bulletTexture.Height * 2);
            Globals.SpriteBatch.Draw(bulletTexture, pos, null, c * 0.75f, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
        }*/
        if (player.Weapon != null)
        {
            Vector2 ammoPosition = new(50, 50);
            Globals.SpriteBatch.DrawString(Globals.Font, player.Weapon.GetAmmo(), ammoPosition, c);
        }
    }
}
