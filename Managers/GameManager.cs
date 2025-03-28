namespace GA_2d_shooter;

public class GameManager
{
    public readonly Player player;
    private readonly Background bg;
    private readonly Game1 game;
    public Camera camera;

    public GameManager(Game1 game)
    {
        this.game = game;
        bg = new();
        ExperienceManager.Init(Globals.Content.Load<Texture2D>("exp"));

        player = new(Globals.Content.Load<Texture2D>("player"));
        ZombieManager.Init();
        camera = new Camera(game.GraphicsDevice.Viewport);
        InputManager.Init(camera);
    }

    public void Restart()
    {
        ProjectileManager.Reset();
        ZombieManager.Reset();
        player.Reset();
        game.gameTimer = 0f;
    }

    public void Update()
    {
        InputManager.Update(player);
        ExperienceManager.Update(player);
        player.Update(ZombieManager.Zombies, camera, game.gameTimer);
        ZombieManager.Update(player);
        ProjectileManager.Update(ZombieManager.Zombies, player);

        if (player.Dead)
        {
            game.deathScreen = new DeathScreen(game, game.gameTimer);
            game.isInDeathScreen = true;
            Restart();
        }
    }

    public void Draw()
    {
        bg.Draw();
        ExperienceManager.Draw();
        ProjectileManager.Draw();
        player.Draw();
        ZombieManager.Draw();
        UIManager.Draw(player, game);
    }
}