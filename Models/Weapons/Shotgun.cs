namespace GA_2d_shooter;

public class Shotgun : Weapon
{
    public override Texture2D ProjectileTexture => Globals.Content.Load<Texture2D>("Pellet25");
    private float ANGLE_STEP;
    private int pelletCount;

    public Shotgun()
    {
        cooldown = 0.75f;
        MaxAmmo = 8;
        Ammo = MaxAmmo;
        reloadTime = 3f;
        pelletCount = 3;
        ANGLE_STEP = (float)(Math.PI / 16);
    }

    protected override void CreateProjectiles(Player player)
    {
        float startAngle = player.Rotation - (pelletCount - 1) / 2f * ANGLE_STEP;
        ProjectileData pd = new()
        {
            Position = player.Position,
            Rotation = startAngle,
            Lifespan = 0.3f,
            Speed = 900,
            Damage = 100
        };

        for (int i = 0; i < pelletCount; i++) //Pellets
        {
            pd.Rotation = startAngle + (i * ANGLE_STEP);
            ProjectileManager.AddProjectile(pd, this);
        }
    }
}
