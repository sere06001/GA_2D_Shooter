namespace GA_2d_shooter;

public class Minigun : Weapon
{
    public override Texture2D ProjectileTexture => Globals.Content.Load<Texture2D>("PistolBulletNew25");
    public override Texture2D WeaponIcon => Globals.Content.Load<Texture2D>("MinigunIconKey2");
    public override Texture2D WeaponIconSelected => Globals.Content.Load<Texture2D>("MinigunIconKey");
    public override Texture2D WeaponIconLocked => Globals.Content.Load<Texture2D>("MinigunIconLocked");
    public Minigun()
    {
        cooldown = 0.07f;
        MaxAmmo = 100;
        Ammo = MaxAmmo;
        reloadTime = 7f;
    }

    protected override void CreateProjectiles(Player player)
    {
        ProjectileData pd = new()
        {
            Position = player.Position,
            Rotation = player.Rotation,
            Lifespan = 20f,
            Speed = 1000,
            Damage = 30
        };

        ProjectileManager.AddProjectile(pd, this);
    }
    public override string GetAmmo()
    {
        return $"{Ammo}/∞";
    }
}
