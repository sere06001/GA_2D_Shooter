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
    public static SpriteFont Font { get; set; }
    public static void Update(GameTime gt)
    {
        float totalTime = (float)gt.TotalGameTime.TotalSeconds;
        Minutes = (int)(totalTime / 60);   // Get minutes
        Seconds = (int)(totalTime % 60);   // Get remaining seconds
        Hundredths = (totalTime % 1) * 100; // Get hundredths of a second
        TotalSeconds = (float)gt.ElapsedGameTime.TotalSeconds;
    }
}
