namespace GA_2d_shooter;

public static class ProjectileManager
{
    public static List<Projectile> Projectiles { get; } = [];

    public static void Reset()
    {
        Projectiles.Clear();
    }

    public static void AddProjectile(ProjectileData data, Weapon weapon)
    {
        Texture2D projectileTexture = weapon.ProjectileTexture;
        Projectiles.Add(new(projectileTexture, data));
    }

    public static void Update(List<Zombie> zombies, Player player)
    {
        foreach (var p in Projectiles)
        {
            p.Update();
            foreach (var z in zombies)
            {
                if (z.HP <= 0) continue;
                float widestDimensionTexture = 0;
                if (z.texture.Width > z.texture.Height)
                {
                    widestDimensionTexture = z.texture.Width;
                }
                else
                {
                    widestDimensionTexture = z.texture.Height;
                }
                if ((p.Position - z.Position).Length() < widestDimensionTexture/2)
                {
                    z.TakeDamage(p.Damage, player);
                    p.Pierce--;
                    if (p.Pierce <= 0)
                    {
                        p.Destroy();
                    }
                    break;
                }
            }
        }
        Projectiles.RemoveAll((p) => p.Lifespan <= 0);
    }

    public static void Draw()
    {
        foreach (var p in Projectiles)
        {
            p.Draw();
        }
    }
}
