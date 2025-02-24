namespace GA_2d_shooter;

public abstract class Weapon
{
    protected float cooldown;
    protected float cooldownLeft;
    public int MaxAmmo {get; protected set; }
    public int Ammo { get; protected set; }
    protected float reloadTime;
    public bool Reloading { get; set; }
    private KeyboardState key;
    public abstract Texture2D ProjectileTexture { get; }
    public abstract Texture2D WeaponIcon { get; }

    protected Weapon()
    {
        cooldownLeft = 0f;
        Reloading = false;
    }

    public virtual void Reload()
    {
        if (Reloading || (Ammo == MaxAmmo)) return;
        cooldownLeft = reloadTime;
        key = Keyboard.GetState();
        Reloading = true;
        Ammo = MaxAmmo;
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

    public virtual string GetAmmo()
    {
        return $"{Ammo}/{MaxAmmo}";
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
