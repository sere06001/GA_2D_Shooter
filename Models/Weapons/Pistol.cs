namespace GA_2d_shooter;

public class Pistol : Weapon
{
    public override Texture2D ProjectileTexture => Globals.Content.Load<Texture2D>("PistolBulletNew25");
    public override Texture2D WeaponIcon => Globals.Content.Load<Texture2D>("PistolIconKey2");
    public override Texture2D WeaponIconSelected => Globals.Content.Load<Texture2D>("PistolIconKey");
    public override Texture2D WeaponIconLocked => Globals.Content.Load<Texture2D>("PistolIconLocked");
    public Pistol()
    {
        XPforUnlock = 0;
        cooldown = 0.5f;
        MaxAmmo = 10;
        Ammo = MaxAmmo;
        reloadTime = 1f;
    }

    protected override void CreateProjectiles(Player player)
    {
        ANGLE_STEP = (float)(Math.PI / 16)/2;
        
        ProjectileData pd = new()
        {
            Position = player.Position,
            Rotation = player.Rotation,
            Lifespan = 1f,
            Speed = 500,
            Damage = 50
        };

        pd.Rotation = Spread(player, ANGLE_STEP);

        ProjectileManager.AddProjectile(pd, this);
    }
}
