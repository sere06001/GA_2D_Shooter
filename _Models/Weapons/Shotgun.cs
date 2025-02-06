namespace GA_2d_shooter;

public class Shotgun : Weapon
{
    public override Texture2D ProjectileTexture => Globals.Content.Load<Texture2D>("zombie");
    public override Texture2D ProjectileTextureUI => Globals.Content.Load<Texture2D>("zombie");
    private const float ANGLE_STEP = (float)(Math.PI / 16);

    public Shotgun()
    {
        cooldown = 0.75f;
        MaxAmmo = 8;
        Ammo = MaxAmmo;
        reloadTime = 3f;
    }

    protected override void CreateProjectiles(Player player)
    {
        ProjectileData pd = new()
        {
            Position = player.Position,
            Rotation = player.Rotation - (3 * ANGLE_STEP),
            Lifespan = 0.5f,
            Speed = 500,
            Damage = 100
        };

        for (int i = 0; i < 5; i++)
        {
            pd.Rotation += ANGLE_STEP;
            ProjectileManager.AddProjectile(pd, this);
        }
    }
}
