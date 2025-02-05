namespace GA_2d_shooter;

public class Player : MovingSprite
{
    public Weapon Weapon { get; set; }
    private Weapon Pistol;
    private Weapon Sniper;
    private Weapon Shotgun;
    private Weapon MachineGun;
    public int HP { get; private set; }
    public bool Dead { get; private set; }
    public int Experience { get; private set; }
    private DateTime lastHitTime = DateTime.MinValue;

    public Player(Texture2D tex) : base(tex, GetStartPosition())
    {
        Reset();
    }

    private static Vector2 GetStartPosition()
    {
        return new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2);
    }

    public void GetExperience(int exp)
    {
        Experience += exp;
    }

    public void Reset()
    {
        Pistol = new Pistol();
        Sniper = new Sniper();
        Shotgun = new Shotgun();
        MachineGun = new MachineGun();
        
        Dead = false;
        HP = 0;
        Weapon = Pistol;
        Position = GetStartPosition();
        Experience = 0;
    }

    public void EquipSlot(int slot)
    {
        switch (slot)
        {
            case 1: Weapon = Pistol;
                break;
            case 2: Weapon = Sniper;
                break;
            case 3: Weapon = Shotgun;
                break;
            case 4: Weapon = MachineGun;
                break;
        }
    }

    private void CheckDeath(List<Zombie> zombies)
    {
        foreach (var z in zombies)
        {
            if (z.HP <= 0) continue;
    
            if ((Position - z.Position).Length() < 50)
            {
                if ((DateTime.Now - lastHitTime).TotalSeconds >= 0.5) //Iframe 0.5 seconds
                {
                    HP++;
                    lastHitTime = DateTime.Now;
                    if (HP >= 3)
                    {
                        Dead = true;
                    }
                    break;
                }
            }
        }
    }

    public void Update(List<Zombie> zombies)
    {
        if (InputManager.Direction != Vector2.Zero)
        {
            var dir = Vector2.Normalize(InputManager.Direction);
            Position = new(
                MathHelper.Clamp(Position.X + (dir.X * Speed * Globals.TotalSeconds), 0, Globals.Bounds.X),
                MathHelper.Clamp(Position.Y + (dir.Y * Speed * Globals.TotalSeconds), 0, Globals.Bounds.Y)
            );
        }

        var toMouse = InputManager.MousePosition - Position;
        Rotation = (float)Math.Atan2(toMouse.Y, toMouse.X);

        Weapon.Update();

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
            }
        }

        if (InputManager.MouseLeftDown)
        {
            Weapon.Fire(this);
        }

        /*if (InputManager.MouseRightClicked)
        {
            Weapon.Reload();
        }*/

        CheckDeath(zombies);
    }
}
