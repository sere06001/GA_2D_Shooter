namespace GA_2d_shooter;

public static class ProjectileManager
{
    private static Texture2D texture;
    public static List<Projectile> Projectiles { get; } = [];

    public static void Init(Texture2D tex)
    {
        texture = tex;
    }

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
                if ((p.Position - z.Position).Length() < 32)
                {
                    z.TakeDamage(p.Damage, player);
                    p.Destroy();
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
