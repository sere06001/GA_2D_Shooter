namespace GA_2d_shooter;

public class Minigun : Weapon
{
    public override Texture2D ProjectileTexture => Globals.Content.Load<Texture2D>("bullet");
    public override Texture2D ProjectileTextureUI => Globals.Content.Load<Texture2D>("bullet");
    public Minigun()
    {
        cooldown = 0.07f;
        MaxAmmo = 100;
        Ammo = MaxAmmo;
        reloadTime = 0f;
    }

    protected override void CreateProjectiles(Player player)
    {
        ProjectileData pd = new()
        {
            Position = player.Position,
            Rotation = player.Rotation,
            Lifespan = 20f,
            Speed = 1000,
            Damage = 20
        };

        ProjectileManager.AddProjectile(pd, this);
    }
    public override string GetAmmo()
    {
        return $"∞";
    }
}
