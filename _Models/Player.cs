namespace GA_2d_shooter;

public class Player : MovingSprite
{
    public Weapon Weapon { get; set; }
    private Weapon _weapon1;
    private Weapon _weapon2;
    private Weapon _weapon3;
    public bool Dead { get; private set; }
    public int Experience { get; private set; }

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
        _weapon1 = new MachineGun();
        _weapon2 = new Shotgun();
        _weapon3 = new Sniper();
        Dead = false;
        Weapon = _weapon1;
        Position = GetStartPosition();
        Experience = 0;
    }

    public void EquipSlot(int slot)
    {
        switch (slot)
        {
            case 1: Weapon = _weapon1;
                break;
            case 2: Weapon = _weapon2;
                break;
            case 3: Weapon = _weapon3;
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
                Dead = true;
                break;
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
            }
        }

        if (InputManager.MouseLeftDown)
        {
            Weapon.Fire(this);
        }

        if (InputManager.MouseRightClicked)
        {
            Weapon.Reload();
        }

        CheckDeath(zombies);
    }
}
