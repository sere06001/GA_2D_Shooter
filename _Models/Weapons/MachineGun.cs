namespace GA_2d_shooter;

public class MachineGun : Weapon
{
    public override Texture2D ProjectileTexture => Globals.Content.Load<Texture2D>("bullet");
    public override Texture2D ProjectileTextureUI => Globals.Content.Load<Texture2D>("bullet");
    public MachineGun()
    {
        cooldown = 0.1f;
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
            Damage = 40
        };

        ProjectileManager.AddProjectile(pd, this);
    }
}
