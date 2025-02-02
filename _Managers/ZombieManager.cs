using System.Diagnostics;

namespace GA_2d_shooter;

public static class ZombieManager
{
    public static List<Zombie> Zombies { get; } = [];
    private static Texture2D _texture;
    private static float _spawnCooldown;
    private static float _spawnTime;
    private static Random _random;
    private static int _padding;
    public static int _totalZombieCount;

    public static void Init()
    {
        _texture = Globals.Content.Load<Texture2D>("zombie");
        _spawnCooldown = 5f;
        _spawnTime = _spawnCooldown;
        _random = new();
        _padding = _texture.Width / 2;
        _totalZombieCount = 0;
    }

    public static void Reset()
    {
        Zombies.Clear();
        _spawnTime = _spawnCooldown;
    }

    private static Vector2 RandomPosition()
    {
        float w = Globals.Bounds.X;
        float h = Globals.Bounds.Y;
        Vector2 pos = new();

        if (_random.NextDouble() <  w / (w + h))
        {
            pos.X = (int)(_random.NextDouble() * w);
            pos.Y = (int)(_random.NextDouble() < 0.5 ? -_padding : h + _padding);
        }
        else
        {
            pos.Y = (int)(_random.NextDouble() * h);
            pos.X = (int)(_random.NextDouble() < 0.5 ? -_padding : w + _padding);
        }

        return pos;
    }

    public static void AddZombie()
    {
        if (Zombies.Count < 50)
        {
            Zombies.Add(new(_texture, RandomPosition()));
            _totalZombieCount++;
        }
        if (_totalZombieCount % 5 == 0 && _totalZombieCount > 0 && _spawnCooldown > 0.5f)
        {
            _spawnCooldown -= 0.5f;
        }
        Debug.WriteLine($"Zombie: {_totalZombieCount}");
        Debug.WriteLine($"Cooldown: {_spawnCooldown}");
    }

    public static void Update(Player player)
    {
        _spawnTime -= Globals.TotalSeconds;
        while(_spawnTime <= 0)
        {
            _spawnTime += _spawnCooldown;
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
