namespace GA_2d_shooter;

public class GameManager
{
    public readonly Player player;
    private readonly Background bg;
    private readonly Game1 game;
    private readonly Camera camera;

    public GameManager(Game1 game)
    {
        this.game = game;
        bg = new();
        var texture = Globals.Content.Load<Texture2D>("bullet");
        ProjectileManager.Init(texture);
        ExperienceManager.Init(Globals.Content.Load<Texture2D>("exp"));

        player = new(Globals.Content.Load<Texture2D>("player"));
        ZombieManager.Init();
        camera = game._camera;
    }

    public void Restart()
    {
        ProjectileManager.Reset();
        ZombieManager.Reset();
        player.Reset();
    }

    public void Update()
    {
        if (game.IsActive)
        {
            InputManager.Update(player);
            ExperienceManager.Update(player);
            player.Update(ZombieManager.Zombies);
            ZombieManager.Update(player);
            ProjectileManager.Update(ZombieManager.Zombies, player);
            if (player.Dead) Restart(); //Add menu screen
        }
    }

    public void Draw()
    {
        bg.Draw();
        ExperienceManager.Draw();
        ProjectileManager.Draw();
        player.Draw();
        ZombieManager.Draw();
        UIManager.Draw(player, camera);
    }
}
