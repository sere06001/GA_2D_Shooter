using System.Diagnostics;

namespace GA_2d_shooter;

public class Zombie : MovingSprite
{
    public int HP { get; set; }
    public float HitRange { get; protected set; }
    public int XPAmountOnDeath { get; protected set; }
    private float iframeTimer;
    private float IFRAME_DURATION = 0.1f; //Duration in seconds
    private Dictionary<Projectile, bool> hitByProjectile;

    public Zombie(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
        Speed = 100;
        HP = 200;
        HitRange = tex.Width-20;
        XPAmountOnDeath = 1;
        hitByProjectile = new Dictionary<Projectile, bool>();
        iframeTimer = 0f;
    }

    public void TakeDamage(int dmg, Player player, Projectile projectile)
    {
        if (hitByProjectile.ContainsKey(projectile))
            return;

        HP -= dmg;
        hitByProjectile[projectile] = true;
        iframeTimer = IFRAME_DURATION;

        if (HP <= 0) 
            player.AddExperience(XPAmountOnDeath);
    }

    public void Update(Player player)
    {
        if (iframeTimer > 0)
        {
            iframeTimer -= Globals.TotalSeconds;
            if (iframeTimer <= 0)
            {
                hitByProjectile.Clear();
            }
        }

        var toPlayer = player.Position - Position;
        Rotation = (float)Math.Atan2(toPlayer.Y, toPlayer.X);

        if (toPlayer.Length() > 4)
        {
            var dir = Vector2.Normalize(toPlayer);
            Vector2 newPosition = Position + dir * Speed * Globals.TotalSeconds;

            bool canMove = true;
            foreach (var otherZombie in ZombieManager.Zombies)
            {
                if (this is Tank || this is Fastie)
                {
                    canMove = true;
                    break;
                }
                
                if (otherZombie != this)
                {
                    float distance = Vector2.Distance(newPosition, otherZombie.Position);
                    float minDistance = (texture.Width + otherZombie.texture.Width) / 3f;
                                        
                    if (distance < minDistance)
                    {
                        canMove = false;
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
