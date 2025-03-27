namespace GA_2d_shooter;

public class SMG : Weapon
{
    public override Texture2D ProjectileTexture => Globals.Content.Load<Texture2D>("PistolBulletNew25");
    public override Texture2D WeaponIcon => Globals.Content.Load<Texture2D>("SMGIconKey2");
    public override Texture2D WeaponIconSelected => Globals.Content.Load<Texture2D>("SMGIconKey");
    public override Texture2D WeaponIconLocked => Globals.Content.Load<Texture2D>("SMGIconLocked");
    public SMG()
    {
        XPforUnlock = 1;
        cooldown = 0.08f;
        MaxAmmo = 30;
        Ammo = MaxAmmo;
        reloadTime = 2f;
        Pierce = 2;
    }

    protected override void CreateProjectiles(Player player)
    {
        ProjectileData pd = new()
        {
            Position = player.Position,
            Rotation = player.Rotation,
            Lifespan = 2f,
            Speed = 750,
            Damage = 75
        };

        ProjectileManager.AddProjectile(pd, this);
    }
}
