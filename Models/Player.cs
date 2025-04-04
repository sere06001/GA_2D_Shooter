namespace GA_2d_shooter;

public class Player : MovingSprite
{
    public Texture2D HPTexture => Globals.Content.Load<Texture2D>("Heart50");
    public Weapon Weapon { get; set; }
    public Weapon Pistol;
    public Weapon Sniper;
    public Weapon Shotgun;
    public Weapon Minigun;
    public Weapon SMG;
    public List<Weapon> WeaponList = new List<Weapon>();
    public int HP { get; private set; }
    public bool Dead { get; private set; }
    public int Experience { get; private set; }
    public int ScoreToDisplay { get; private set; }
    private float Iframe { get; set; }
    private DateTime lastHitTime = DateTime.MinValue;

    public Player(Texture2D tex) : base(tex, GetStartPosition())
    {
        Reset();
        Speed = 200;
        Iframe = 3f; // 3 seconds iframe
    }

    private static Vector2 GetStartPosition()
    {
        Background bg = new();
        return bg.GetCenterPosition();
    }

    public void AddExperience(int exp)
    {
        Experience += exp;
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
        Position = GetStartPosition();
        ScoreToDisplay = Experience;
        Experience = 0;
    }

    public void EquipSlot(int slot)
    {
        switch (slot)
        {
            case 1: Weapon = Pistol; break;
            case 2: Weapon = Sniper; break;
            case 3: Weapon = Shotgun; break;
            case 4: Weapon = SMG; break;
            case 5: Weapon = Minigun; break;
        }
    }

    private void CheckDeath(List<Zombie> zombies, float gameTimer)
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
                        Dead = true;
                        SaveManager.SaveTime(gameTimer, Experience);
                    }
                    break;
                }
            }
        }
    }

    public void Update(List<Zombie> zombies, Camera camera, float gameTimer)
    {
        if (InputManager.Direction != Vector2.Zero)
        {
            var dir = Vector2.Normalize(InputManager.Direction);
            Position = new(
                MathHelper.Clamp(Position.X + (dir.X * Speed * Globals.TotalSeconds), 0, Globals.MapBounds.X),
                MathHelper.Clamp(Position.Y + (dir.Y * Speed * Globals.TotalSeconds), 0, Globals.MapBounds.Y)
            );
        }

        var toMouse = InputManager.MouseWorldPosition - Position;
        Rotation = (float)Math.Atan2(toMouse.Y, toMouse.X);

        foreach (var weapon in WeaponList)
        {
            weapon.Update();
        }

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

        CheckDeath(zombies, gameTimer);
    }
}