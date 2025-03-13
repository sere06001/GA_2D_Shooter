namespace GA_2d_shooter;

public static class UIManager
{
    private static Texture2D weaponTexture;
    private static Texture2D middleWeapon;
    private static Vector2 pos;
    public static float windowWidth = Globals.Bounds.X;
    public static float windowHeight = Globals.Bounds.Y;
    public static float playerX;
    public static float playerY;

    public static void GetNonMiddleWepUI(Player player, int index)
    {
        if (player.WeaponList[index] == player.Weapon)
        {
            weaponTexture = player.WeaponList[index].WeaponIconSelected;
        }
        else
        {
            if (player.WeaponList[index].IsUnlocked)
            {
                weaponTexture = player.WeaponList[index].WeaponIcon;
            }
            else
            {
                weaponTexture = player.WeaponList[index].WeaponIconLocked;
            }
        }
    }
    public static void GetMiddleIndexWepUI(Player player, int index)
    {
        if (player.Weapon == player.WeaponList[index])
        {
            middleWeapon = player.WeaponList[index].WeaponIconSelected;
        }
        else
        {
            if (player.WeaponList[index].IsUnlocked)
            {
                middleWeapon = player.WeaponList[index].WeaponIcon;
            }
            else
            {
                middleWeapon = player.WeaponList[index].WeaponIconLocked;
            }
        }
    }
    public static void DebugUI(Player player, float x, float y)
    {
        pos = new(x, y-200);
        Globals.SpriteBatch.DrawString(Globals.Font, $"Pos: {player.Position}", pos, Color.White);
        pos = new(x, y-100);
        Globals.SpriteBatch.DrawString(Globals.Font, $"Rot: {player.Rotation}", pos, Color.White);
        pos = new(x-100, y);
        Globals.SpriteBatch.DrawString(Globals.Font, $"Mouse Pos: {InputManager.MouseWorldPosition}", pos, Color.White);
        pos = new(x-100, y+100);
        Globals.SpriteBatch.DrawString(Globals.Font, $"WeaponList Count: {player.WeaponList.Count}", pos, Color.White);
    }
    public static void DrawWeapons(Player player)
    {
        playerX = player.Position.X;
        playerY = player.Position.Y;
        float halfX = playerX / 2;
        int totalWeapons = player.WeaponList.Count;

        int spacing = 25;

        if (totalWeapons > 0)
        {
            int middleIndex = totalWeapons / 2;
            GetMiddleIndexWepUI(player, middleIndex);
            int middleWeaponWidth = middleWeapon.Width;

            float middleX = halfX - (middleWeaponWidth / 2);

            float currentX = middleX;
            for (int i = middleIndex - 1; i >= 0; i--)
            {
                GetNonMiddleWepUI(player, i);
                currentX -= weaponTexture.Width + spacing;
                pos = new (currentX + playerX/2, playerY-windowHeight*0.95f/2);
                Globals.SpriteBatch.Draw(weaponTexture, pos, Color.White);
            }
            pos = new (middleX + playerX/2, playerY-windowHeight*0.95f/2);
            Globals.SpriteBatch.Draw(middleWeapon, pos, Color.White);

            currentX = middleX + middleWeaponWidth + spacing;
            for (int i = middleIndex + 1; i < totalWeapons; i++)
            {
                GetNonMiddleWepUI(player, i);
                pos = new(currentX + playerX/2, playerY-windowHeight*0.95f/2);
                Globals.SpriteBatch.Draw(weaponTexture, pos, Color.White);
                currentX += weaponTexture.Width + spacing;
            }
        }
    }
    public static void Draw(Player player, Game1 game)
    {
        playerX = player.Position.X;
        playerY = player.Position.Y;

        Color c = player.Weapon.Reloading ? Color.Red : Color.White;
        
        for (int i = 0; i < player.HP; i++)
        {
            pos = new(i*50+5+playerX-windowWidth/2, playerY-windowHeight/2+5);
            Globals.SpriteBatch.Draw(player.HPTexture, pos, Color.White);
        }
        
        DrawWeapons(player);
        

        string timeString = $"{Globals.Minutes:D2}:{Globals.Seconds:D2}.{Globals.Hundredths:00}";
        pos = new(playerX+windowWidth/2-Globals.Font.MeasureString(timeString).X-5, playerY-windowHeight/2);
        Globals.SpriteBatch.DrawString(Globals.Font, $"{timeString}", pos, Color.White);

        DebugUI(player, playerX, playerY);

        if (player.Weapon != null)
        {
            pos = new(playerX+400, playerY+200);
            Globals.SpriteBatch.DrawString(Globals.Font, player.Weapon.GetAmmo(), pos, c);
        }
    }
}
