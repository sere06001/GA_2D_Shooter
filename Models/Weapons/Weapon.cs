using System.Globalization;

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
    public abstract Texture2D WeaponIconSelected { get;}
    public abstract Texture2D WeaponIconLocked { get;}
    public bool IsUnlocked { get; protected set; } = false;
    public int XPforUnlock { get; protected set; }
    protected int Pierce { get; set; } = 0;
    protected Random random = new Random();
    protected float ANGLE_STEP { get; set; } = 0;

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
    protected virtual float Spread(Player player, float step)
    {
        int flipDirection = random.Next(2) == 0 ? -1 : 1;
        float rotation = player.Rotation + (float)random.NextDouble() * step * flipDirection;
        return rotation;
    }

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
    
    public void UnlockGun()
    {
        IsUnlocked = true;
    }
    public virtual string GetReloadProgress()
    {
        if (!Reloading) return string.Empty;
        if (cooldownLeft < 0) return string.Empty;
        return string.Format(CultureInfo.InvariantCulture, "{0:F1}s", cooldownLeft);
    }

    public virtual void Update()
    {
        if (cooldownLeft > 0)
        {
            cooldownLeft -= Globals.TotalSeconds;
            if (cooldownLeft <= 0 && Reloading)
            {
                Reloading = false;
                cooldownLeft = 0;
            }
        }
    }
}
