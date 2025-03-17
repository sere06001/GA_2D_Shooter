namespace GA_2d_shooter;

public class Background
{
    public readonly Point mapTileSize = new(6, 4);
    public readonly Sprite[,] tiles;

    public Background()
    {
        tiles = new Sprite[mapTileSize.X, mapTileSize.Y];

        List<Texture2D> textures = new(5);
        for (int i = 1; i < 6; i++) textures.Add(Globals.Content.Load<Texture2D>($"tile{i}"));

        Point TileSize = new(textures[0].Width, textures[0].Height);
        Random random = new();
        for (int y = 0; y < mapTileSize.Y; y++)
        {
            for (int x = 0; x < mapTileSize.X; x++)
            {
                int r = random.Next(0, textures.Count);
                tiles[x, y] = new(textures[r], new((x + 0.5f) * TileSize.X, (y + 0.5f) * TileSize.Y));
            }
        }
    }

    public void Draw()
    {
        for (int y = 0; y < mapTileSize.Y; y++)
        {
            for (int x = 0; x < mapTileSize.X; x++) tiles[x, y].Draw();
        }
    }
}
