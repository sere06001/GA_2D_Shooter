namespace GA_2d_shooter;

public class Projectile : MovingSprite
{
    public Vector2 Direction { get; set; }
    public float Lifespan { get; private set; }
    public int Damage { get; }
    public int Pierce { get; set;}

    public Projectile(Texture2D tex, ProjectileData data) : base(tex, data.Position)
    {
        Speed = data.Speed;
        Rotation = data.Rotation;
        Direction = new((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
        Lifespan = data.Lifespan;
        Damage = data.Damage;
        Pierce = data.Pierce;
    }

    public void Destroy()
    {
        Lifespan = 0;
    }

    public void Update()
    {
        Position += Direction * Speed * Globals.TotalSeconds;
        Lifespan -= Globals.TotalSeconds;
    }
}
