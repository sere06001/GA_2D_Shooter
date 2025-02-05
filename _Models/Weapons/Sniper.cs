namespace GA_2d_shooter;

public class Sniper : Weapon
{
    public Sniper()
    {
        cooldown = 1.5f;
        maxAmmo = 10;
        Ammo = maxAmmo;
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

        ProjectileManager.AddProjectile(pd);
    }
}
