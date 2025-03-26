namespace GA_2d_shooter;

public class Background
{
    private readonly Sprite mapSprite;
    public readonly Vector2 MapSize;

    public Background()
    {
        Texture2D mapTexture = Globals.Content.Load<Texture2D>("Map");
        MapSize = new Vector2(mapTexture.Width, mapTexture.Height);

        // Center the map
        Vector2 mapPosition = MapSize / 2;
        mapSprite = new Sprite(mapTexture, mapPosition);

        // Set the map bounds for player movement and other calculations
        Globals.MapBounds = new Point((int)MapSize.X, (int)MapSize.Y);
    }

    public Vector2 GetCenterPosition()
    {
        return MapSize / 2;
    }

    public void Draw()
    {
        mapSprite.Draw();
    }
}