namespace GA_2d_shooter;

public class Minigun : Weapon
{
    public override Texture2D ProjectileTexture => Globals.Content.Load<Texture2D>("PistolBulletNew25");
    public override Texture2D WeaponIcon => Globals.Content.Load<Texture2D>("MinigunIconKey2");
    public override Texture2D WeaponIconSelected => Globals.Content.Load<Texture2D>("MinigunIconKey");
    public override Texture2D WeaponIconLocked => Globals.Content.Load<Texture2D>("MinigunIconLocked");
    public Minigun()
    {
        XPforUnlock = 100;
        cooldown = 0.03f;
        MaxAmmo = 150;
        Ammo = MaxAmmo;
        reloadTime = 10f;
    }

    protected override void CreateProjectiles(Player player)
    {
        ANGLE_STEP = (float)(Math.PI / 16);
        
        ProjectileData pd = new()
        {
            Position = player.Position,
            Rotation = player.Rotation,
            Lifespan = 20f,
            Speed = 1000,
            Damage = 30
        };

        pd.Rotation = Spread(player, ANGLE_STEP);

        ProjectileManager.AddProjectile(pd, this);
    }
    public override string GetAmmo()
    {
        return $"{Ammo}/∞";
    }
}
