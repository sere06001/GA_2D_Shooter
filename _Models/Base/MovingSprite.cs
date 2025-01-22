namespace GA_2d_shooter;

public class MovingSprite(Texture2D tex, Vector2 pos) : Sprite(tex, pos)
{
    public int Speed { get; set; } = 300;
}
