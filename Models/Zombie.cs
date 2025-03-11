using System.Diagnostics;

namespace GA_2d_shooter;

public class Zombie : MovingSprite
{
    public int HP { get; set; }
    public float HitRange { get; protected set; }

    public Zombie(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
        Speed = 100;
        HP = 200;
        HitRange = tex.Width;
    }

    public void TakeDamage(int dmg, Player player)
    {
        HP -= dmg;
        if (HP <= 0) player.AddExperience(1);
    }

    public void Update(Player player)
    {
        var toPlayer = player.Position - Position;
        Rotation = (float)Math.Atan2(toPlayer.Y, toPlayer.X);

        if (toPlayer.Length() > 4)
        {
            var dir = Vector2.Normalize(toPlayer);
            Vector2 newPosition = Position + dir * Speed * Globals.TotalSeconds;

            // Check collision with other zombies
            bool canMove = true;
            foreach (var otherZombie in ZombieManager.Zombies)
            {
                if (otherZombie != this)
                {
                    float distance = Vector2.Distance(newPosition, otherZombie.Position);
                    float minDistance = (texture.Width + otherZombie.texture.Width) / 3f; // Adjust divisor for tighter/looser packing
                    if (otherZombie is Fastie)
                    {
                        canMove = true;
                    }
                                        
                    if (distance < minDistance)
                    {
                        canMove = false;
                        // Add slight repulsion to prevent stacking
                        Vector2 awayFromOther = Vector2.Normalize(Position - otherZombie.Position);
                        Position += awayFromOther * Speed * Globals.TotalSeconds * 0.1f;
                        break;
                    }
                }
            }

            if (canMove)
            {
                Position = newPosition;
            }
        }
    }
}
