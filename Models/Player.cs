namespace GA_2d_shooter;

public class Player : MovingSprite
{
    public Texture2D HPTexture => Globals.Content.Load<Texture2D>("Heart50");
    public Weapon Weapon { get; set; }
    public Weapon prevWeapon { get; set; }
    public Weapon Pistol;
    public Weapon Sniper;
    public Weapon Shotgun;
    public Weapon Minigun;
    public Weapon SMG;
    public List<Weapon> WeaponList = new List<Weapon>();
    public int HP { get; private set; }
    public bool Dead { get; private set; }
    public int Experience { get; private set; }
    private float Iframe { get;  set; }
    private DateTime lastHitTime = DateTime.MinValue;

    public Player(Texture2D tex) : base(tex, GetStartPosition())
    {
        Reset();
        Speed = 200;
        Experience = SaveManager.LoadExperience();
        Iframe = 3f; //3 sec iframe
    }

    private static Vector2 GetStartPosition()
    {
        Background bg = new();
        float totalWidth = bg.mapTileSize.X * bg.tiles[0, 0].texture.Width;
        float totalHeight = bg.mapTileSize.Y * bg.tiles[0, 0].texture.Height;
        
        return new Vector2(totalWidth / 2, totalHeight / 2);
    }

    public void AddExperience(int exp)
    {
        Experience += exp;
        SaveManager.SaveExperience(Experience);
    }

    public void Reset()
    {
        if (WeaponList.Count > 0)
        {
            WeaponList.Clear();
        }
        Pistol = new Pistol();
        Sniper = new Sniper();
        Shotgun = new Shotgun();
        SMG = new SMG();
        Minigun = new Minigun();
        WeaponList.Add(Pistol);
        WeaponList.Add(Sniper);
        WeaponList.Add(Shotgun);
        WeaponList.Add(SMG);
        WeaponList.Add(Minigun);
        
        Dead = false;
        HP = 3;
        Weapon = Pistol;
        prevWeapon = Weapon;
        Position = GetStartPosition();
    }

    public void EquipSlot(int slot)
    {
        if (Weapon != null && prevWeapon != Weapon)
        {  
            prevWeapon = Weapon;
        }

        switch (slot)
        {
            case 1: Weapon = Pistol; break;
            case 2: Weapon = Sniper; break;
            case 3: Weapon = Shotgun; break;
            case 4: Weapon = SMG; break;
            case 5: Weapon = Minigun; break;
        }
        if (Weapon == prevWeapon && Weapon.Reloading)
        {
            Weapon.Reloading = false;
        }
    }

    private void CheckDeath(List<Zombie> zombies)
    {
        foreach (var z in zombies)
        {
            if (z.HP <= 0) continue;

            if ((Position - z.Position).Length() < z.HitRange)
            {
                if ((DateTime.Now - lastHitTime).TotalSeconds >= Iframe)
                {
                    HP--;
                    lastHitTime = DateTime.Now;
                    if (HP <= 0)
                    {
                        //Dead = true;
                    }
                    break;
                }
            }
        }
    }

    public void Update(List<Zombie> zombies, Camera camera)
    {
        /*if (prevWeapon != null && prevWeapon != Weapon && prevWeapon != Minigun && 
        prevWeapon.Ammo < prevWeapon.MaxAmmo && !prevWeapon.Reloading)
        {
            prevWeapon.Reload();
        }*/ //Auto reloader

        if (InputManager.Direction != Vector2.Zero)
        {
            var dir = Vector2.Normalize(InputManager.Direction);
            Position = new(
                MathHelper.Clamp(Position.X + (dir.X * Speed * Globals.TotalSeconds), 0, Globals.Bounds.X),
                MathHelper.Clamp(Position.Y + (dir.Y * Speed * Globals.TotalSeconds), 0, Globals.Bounds.Y)
            );
        }

        var toMouse = InputManager.MouseWorldPosition - Position;
        Rotation = (float)Math.Atan2(toMouse.Y, toMouse.X);

        Weapon.Update();
        if (prevWeapon != Weapon) prevWeapon?.Update();

        if (InputManager.WeaponKey.HasValue)
        {
            switch (InputManager.WeaponKey.Value)
            {
                case Keys.D1:
                    EquipSlot(1);
                    break;
                case Keys.D2:
                    EquipSlot(2);
                    break;
                case Keys.D3:
                    EquipSlot(3);
                    break;
                case Keys.D4:
                    EquipSlot(4);
                    break;
                case Keys.D5:
                    EquipSlot(5);
                    break;
            }
        }

        if (InputManager.MouseLeftDown)
        {
            Weapon.Fire(this);
        }

        CheckDeath(zombies);
    }
}
