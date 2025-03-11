namespace GA_2d_shooter;

public static class ZombieManager
{
    public static List<Zombie> Zombies { get; } = [];
    private static Texture2D textureZombie;
    private static Texture2D textureTank;
    private static Texture2D textureFastie;
    private static float spawnCooldownReset;
    private static float spawnCooldown;
    private static float spawnTime;
    private static Random random;
    public static int totalZombieCount;

    public static void Init()
    {
        textureZombie = Globals.Content.Load<Texture2D>("Zombie1");
        textureTank = Globals.Content.Load<Texture2D>("ZombieTank");
        textureFastie = Globals.Content.Load<Texture2D>("ZombieFastie1");
        spawnCooldownReset = 1f;
        spawnCooldown = spawnCooldownReset;
        spawnTime = spawnCooldown;
        random = new();
        totalZombieCount = 0;
    }

    public static void Reset()
    {
        Zombies.Clear();
        spawnCooldown = spawnCooldownReset;
        spawnTime = spawnCooldown;
    }

    private static Vector2 RandomPosition(Player player)
    {
        float screenWidth = Globals.Bounds.X;
        float screenHeight = Globals.Bounds.Y;

        Vector2 playerPos = player.Position;

        float spawnBuffer = 50f; //Extra distance
        float minX = playerPos.X - screenWidth/2 - spawnBuffer;
        float maxX = playerPos.X + screenWidth/2 + spawnBuffer;
        float minY = playerPos.Y - screenHeight/2 - spawnBuffer;
        float maxY = playerPos.Y + screenHeight/2 + spawnBuffer;

        Vector2 spawnPos;
        do
        {
            //Randomly choose edge (0=top, 1=right, 2=bottom, 3=left)
            int edge = random.Next(4);

            switch (edge)
            {
                case 0: //Top
                    spawnPos = new Vector2(
                        random.Next((int)minX, (int)maxX),
                        minY
                    );
                    break;
                case 1: //Right
                    spawnPos = new Vector2(
                        maxX,
                        random.Next((int)minY, (int)maxY)
                    );
                    break;
                case 2: //Bottom
                    spawnPos = new Vector2(
                        random.Next((int)minX, (int)maxX),
                        maxY
                    );
                    break;
                default: //Left
                    spawnPos = new Vector2(
                        minX,
                        random.Next((int)minY, (int)maxY)
                    );
                    break;
            }
        } while (IsPositionVisible(spawnPos, playerPos, screenWidth, screenHeight));

        return spawnPos;
    }

    private static bool IsPositionVisible(Vector2 position, Vector2 playerPos, float screenWidth, float screenHeight)
    {
        return position.X > playerPos.X - screenWidth/2 &&
               position.X < playerPos.X + screenWidth/2 &&
               position.Y > playerPos.Y - screenHeight/2 &&
               position.Y < playerPos.Y + screenHeight/2;
    }

    public static void RandomTexture()
    {
        textureZombie = Globals.Content.Load<Texture2D>($"Zombie{random.Next(1,5)}");
        textureTank = Globals.Content.Load<Texture2D>("ZombieTank");
        textureFastie = Globals.Content.Load<Texture2D>($"ZombieFastie{random.Next(1,3)}");
    }

    public static void AddZombie(Player player)
    {
        if (Zombies.Count < 100) //Limit max zombies on screen to 100
        {
            RandomTexture();
            
            if (totalZombieCount % 10 == 0 && totalZombieCount > 0)
            {
                Zombies.Add(new Tank(textureTank, RandomPosition(player)));
                totalZombieCount++;
            }
            if (totalZombieCount % 5 == 0 && totalZombieCount > 0)
            {
                Zombies.Add(new Fastie(textureFastie, RandomPosition(player)));
                totalZombieCount++;
            }
            //Spawn regular zombies even if other zombie type spawns
            Zombies.Add(new(textureZombie, RandomPosition(player)));
            totalZombieCount++;
        }

        if (totalZombieCount % 5 == 0 && totalZombieCount > 0 && spawnCooldown > 1f)
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
            AddZombie(player);
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
