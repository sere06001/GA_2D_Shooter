using System.Diagnostics;

namespace GA_2d_shooter;

public static class ZombieManager
{
    public static List<Zombie> Zombies { get; } = [];
    private static Texture2D textureZombie;
    private static Texture2D textureTank;
    private static Texture2D textureFastie;
    private static float spawnCooldown;
    private static float spawnTime;
    private static Random random;
    private static int padding;
    public static int totalZombieCount;

    public static void Init()
    {
        textureZombie = Globals.Content.Load<Texture2D>("zombie");
        textureTank = Globals.Content.Load<Texture2D>("zombie");
        textureFastie = Globals.Content.Load<Texture2D>("zombie");
        spawnCooldown = 5f;
        spawnTime = spawnCooldown;
        random = new();
        padding = textureZombie.Width / 2;
        totalZombieCount = 0;
    }

    public static void Reset()
    {
        Zombies.Clear();
        spawnTime = spawnCooldown;
    }

    private static Vector2 RandomPosition()
    {
        float w = Globals.Bounds.X;
        float h = Globals.Bounds.Y;
        Vector2 pos = new();

        if (random.NextDouble() <  w / (w + h))
        {
            pos.X = (int)(random.NextDouble() * w);
            pos.Y = (int)(random.NextDouble() < 0.5 ? -padding : h + padding);
        }
        else
        {
            pos.Y = (int)(random.NextDouble() * h);
            pos.X = (int)(random.NextDouble() < 0.5 ? -padding : w + padding);
        }
        
        return pos;
    }

    public static void AddZombie()
    {
        if (Zombies.Count < 50) //Limit max zombies on screen to 50
        {
            if (totalZombieCount % 10 == 0 && totalZombieCount > 0)
            {
                Zombies.Add(new Tank(textureTank, RandomPosition()));
            }
            if (totalZombieCount % 5 == 0 && totalZombieCount > 0)
            {
                Zombies.Add(new Fastie(textureFastie, RandomPosition()));
            }

            //Spawn regular zombies even if other zombie type spawns
            Zombies.Add(new(textureZombie, RandomPosition()));
            


            totalZombieCount++;
        }
        if (totalZombieCount % 5 == 0 && totalZombieCount > 0 && spawnCooldown > 0.5f)
        {
            spawnCooldown -= 0.5f;
        }
    }

    public static void Update(Player player)
    {
        spawnTime -= Globals.TotalSeconds;
        while(spawnTime <= 0)
        {
            spawnTime += spawnCooldown;
            AddZombie();
        }

        foreach (var z in Zombies)
        {
            z.Update(player);
        }
        Zombies.RemoveAll((z) => z.HP <= 0);
    }

    public static void Draw()
    {
        foreach (var z in Zombies)
        {
            z.Draw();
        }
    }
}
