namespace GA_2d_shooter;

public class Sniper : Weapon
{
    public override Texture2D ProjectileTexture => Globals.Content.Load<Texture2D>("SniperBullet25");
    public override Texture2D WeaponIcon => Globals.Content.Load<Texture2D>("SniperIcon");
    public Sniper()
    {
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
            Damage = 200
        };

        ProjectileManager.AddProjectile(pd, this);
    }
}
