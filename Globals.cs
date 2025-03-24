using Microsoft.Xna.Framework.Content;

namespace GA_2d_shooter;

public static class Globals
{
    public static float TotalSeconds { get; set; }
    public static int Minutes { get; private set; }
    public static int Seconds { get; private set; }
    public static float Hundredths { get; private set; }
    public static ContentManager Content { get; set; }
    public static SpriteBatch SpriteBatch { get; set; }
    public static Point Bounds { get; set; }
    public static Point MapBounds { get; set; }
    public static SpriteFont Font { get; set; }
    public static int WindowModeOffset { get; set; } = 0;
    public static void Update(GameTime gt)
    {
        float totalTime = (float)gt.TotalGameTime.TotalSeconds;
        Minutes = (int)(totalTime / 60);
        Seconds = (int)(totalTime % 60);
        Hundredths = totalTime % 1 * 100;
        TotalSeconds = (float)gt.ElapsedGameTime.TotalSeconds;
    }
}
