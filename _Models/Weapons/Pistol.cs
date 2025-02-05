namespace GA_2d_shooter;

public class Pistol : Weapon
{
    public override Texture2D ProjectileTexture => Globals.Content.Load<Texture2D>("PistolBullet");
    public override Texture2D ProjectileTextureUI => Globals.Content.Load<Texture2D>("PistolBulletUI");
    public Pistol()
    {
        cooldown = 0.5f;
        maxAmmo = 10;
        Ammo = maxAmmo;
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
