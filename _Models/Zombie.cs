using System.Diagnostics;

namespace GA_2d_shooter;

public class Zombie : MovingSprite
{
    public int HP { get; set; }

    public Zombie(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
        Speed = 75;
        HP = 10;
    }

    public void TakeDamage(int dmg)
    {
        HP -= dmg;
        if (HP <= 0) ExperienceManager.AddExperience(Position);
    }

    public void Update(Player player)
    {
        var toPlayer = player.Position - Position;
        Rotation = (float)Math.Atan2(toPlayer.Y, toPlayer.X);

        if (toPlayer.Length() > 4)
        {
            var dir = Vector2.Normalize(toPlayer);
            Position += dir * Speed * Globals.TotalSeconds;
        }
    }
}
