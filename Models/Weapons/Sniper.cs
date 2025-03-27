namespace GA_2d_shooter;

public class Sniper : Weapon
{
    public override Texture2D ProjectileTexture => Globals.Content.Load<Texture2D>("SniperBullet25");
    public override Texture2D WeaponIcon => Globals.Content.Load<Texture2D>("SniperIconKey2");
    public override Texture2D WeaponIconSelected => Globals.Content.Load<Texture2D>("SniperIconKey");
    public override Texture2D WeaponIconLocked => Globals.Content.Load<Texture2D>("SniperIconLocked");
    public Sniper()
    {
        XPforUnlock = 0;
        cooldown = 1.5f;
        MaxAmmo = 3;
        Ammo = MaxAmmo;
        reloadTime = 2f;
    }

    protected override void CreateProjectiles(Player player)
    {
        ProjectileData pd = new()
        {
            Position = player.Position,
            Rotation = player.Rotation,
            Lifespan = 30f,
            Speed = 1250,
            Damage = 200,
            Pierce = 5
        };

        ProjectileManager.AddProjectile(pd, this);
    }
}
