using System.Diagnostics;

namespace GA_2d_shooter;

public class Fastie : Zombie
{

    public Fastie(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
        Speed = 175;
        HP = 100;
        HitRange = tex.Width/1.75f;
        XPAmountOnDeath = 2;
    }
}
