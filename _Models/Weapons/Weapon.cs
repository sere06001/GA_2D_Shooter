namespace GA_2d_shooter;

public abstract class Weapon
{
    protected float cooldown;
    protected float cooldownLeft;
    protected int maxAmmo;
    public int Ammo { get; protected set; }
    protected float reloadTime;
    public bool Reloading { get; protected set; }
    private KeyboardState key;
    public abstract Texture2D ProjectileTexture { get; }
    public abstract Texture2D ProjectileTextureUI { get; }

    protected Weapon()
    {
        cooldownLeft = 0f;
        Reloading = false;
    }

    public virtual void Reload()
    {
        if (Reloading || (Ammo == maxAmmo)) return;
        cooldownLeft = reloadTime;
        key = Keyboard.GetState();
        Reloading = true;
        Ammo = maxAmmo;
    }

    protected abstract void CreateProjectiles(Player player);

    public virtual void Fire(Player player)
    {
        if (cooldownLeft > 0 || Reloading) return;

        Ammo--;
        if (Ammo > 0)
        {
            cooldownLeft = cooldown;
        }
        else
        {
            Reload();
        }

        CreateProjectiles(player);
    }

    public virtual void Update()
    {
        if (cooldownLeft > 0)
        {
            cooldownLeft -= Globals.TotalSeconds;
        }
        else if (Reloading)
        {
            Reloading = false;
        }
    }
}
