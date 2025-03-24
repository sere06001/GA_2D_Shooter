namespace GA_2d_shooter;

public class Game1 : Game
{
    public GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameManager gameManager;
    public Camera camera;
    private MenuScreen menuScreen;
    private LeaderboardScreen leaderboardScreen;
    public bool isInMenu = true;
    public bool isInLeaderboard = false;
    public bool isFirstTimeInMenu = true;

    private float gameTimer;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    public void Restart()
    {
        gameManager.Restart();
        gameTimer = 0f;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
        _graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;
        _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();

        Globals.Bounds = new(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

        Globals.Font = Content.Load<SpriteFont>("font");
        Globals.Content = Content;
        gameManager = new(this);

        camera = new Camera(GraphicsDevice.Viewport);

        base.Initialize();
        menuScreen = new MenuScreen(this);
        leaderboardScreen = new LeaderboardScreen(this);

        gameTimer = 0f;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Globals.SpriteBatch = _spriteBatch;
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            isInMenu = true;
            isInLeaderboard = false;
        }

        if (isInMenu)
        {
            menuScreen.Update(gameTime);
        }
        else if (isInLeaderboard)
        {
            leaderboardScreen.Update(gameTime);
        }
        else
        {
            gameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            Globals.Update(gameTime);
            gameManager.Update();

            if (gameManager.player != null)
            {
                camera.Follow(gameManager.player);
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        if (isInMenu)
        {
            _spriteBatch.Begin();
            menuScreen.Draw(_spriteBatch);
            _spriteBatch.End();
        }
        else if (isInLeaderboard)
        {
            _spriteBatch.Begin();
            leaderboardScreen.Draw(_spriteBatch);
            _spriteBatch.End();
        }
        else
        {
            _spriteBatch.Begin(transformMatrix: camera.Transform);
            gameManager.Draw();

            UIManager.DrawGameTimer(gameTimer);

            _spriteBatch.End();
        }

        base.Draw(gameTime);
    }
}