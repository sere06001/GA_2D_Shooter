namespace GA_2d_shooter;

public class SMG : Weapon
{
    public override Texture2D ProjectileTexture => Globals.Content.Load<Texture2D>("PistolBulletNew25");
    public override Texture2D WeaponIcon => Globals.Content.Load<Texture2D>("SMGIconKey2");
    public override Texture2D WeaponIconSelected => Globals.Content.Load<Texture2D>("SMGIconKey");
    public override Texture2D WeaponIconLocked => Globals.Content.Load<Texture2D>("SMGIconLocked");
    public SMG()
    {
        XPforUnlock = 30;
        cooldown = 0.2f;
        MaxAmmo = 30;
        Ammo = MaxAmmo;
        reloadTime = 2f;
    }

    protected override void CreateProjectiles(Player player)
    {
        ProjectileData pd = new()
        {
            Position = player.Position,
            Rotation = player.Rotation,
            Lifespan = 2f,
            Speed = 750,
            Damage = 50
        };

        ProjectileManager.AddProjectile(pd, this);
    }
}
