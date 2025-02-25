using System.Diagnostics;

namespace GA_2d_shooter;

public class Fastie : Zombie
{

    public Fastie(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
        Speed = 150;
        HP = 100;
    }
}
