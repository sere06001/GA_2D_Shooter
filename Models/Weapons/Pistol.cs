namespace GA_2d_shooter;

public class Pistol : Weapon
{
    public override Texture2D ProjectileTexture => Globals.Content.Load<Texture2D>("PistolBulletNew25");
    public override Texture2D WeaponIcon => Globals.Content.Load<Texture2D>("PistolIcon");
    public Pistol()
    {
        cooldown = 0.5f;
        MaxAmmo = 10;
        Ammo = MaxAmmo;
        reloadTime = 1f;
    }

    protected override void CreateProjectiles(Player player)
    {
        ProjectileData pd = new()
        {
            Position = player.Position,
            Rotation = player.Rotation,
            Lifespan = 1f,
            Speed = 500,
            Damage = 50
        };

        ProjectileManager.AddProjectile(pd, this);
    }
}
