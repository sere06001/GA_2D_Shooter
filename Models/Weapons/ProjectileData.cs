namespace GA_2d_shooter;

public sealed class ProjectileData
{
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public float Lifespan { get; set; }
    public int Speed { get; set; }
    public int Damage { get; set; }
    public int Pierce { get; set; } = 0;
}
