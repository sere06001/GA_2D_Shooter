namespace GA_2d_shooter;

public class Shotgun : Weapon
{
    public override Texture2D ProjectileTexture => Globals.Content.Load<Texture2D>("Pellet25");
    public override Texture2D WeaponIcon => Globals.Content.Load<Texture2D>("ShotgunIconKey2");
    public override Texture2D WeaponIconSelected => Globals.Content.Load<Texture2D>("ShotgunIconKey");
    public override Texture2D WeaponIconLocked => Globals.Content.Load<Texture2D>("ShotgunIconLocked");
    private int pelletCount;

    public Shotgun()
    {
        XPforUnlock = 0;
        cooldown = 0.75f;
        MaxAmmo = 8;
        Ammo = MaxAmmo;
        reloadTime = 3f;
        pelletCount = 5;
    }

    protected override void CreateProjectiles(Player player)
    {
        ANGLE_STEP = (float)(Math.PI / 16);
        
        float startAngle = player.Rotation - (pelletCount - 1) / 2f * ANGLE_STEP;
        ProjectileData pd = new()
        {
            Position = player.Position,
            Rotation = startAngle,
            Lifespan = 0.5f,
            Speed = 650,
            Damage = 100
        };

        for (int i = 0; i < pelletCount; i++) //Pellets
        {
            pd.Rotation = startAngle + (i * ANGLE_STEP);
            ProjectileManager.AddProjectile(pd, this);
        }
    }
}
