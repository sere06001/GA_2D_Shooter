using System.Diagnostics;

namespace GA_2d_shooter;

public class Tank : Zombie
{

    public Tank(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
        Speed = 75;
        HP = 1000;
    }
}
