namespace GA_2d_shooter;

public static class UIManager
{
    private static Texture2D weaponTexture;
    private static Texture2D middleWeapon;
    private static Vector2 pos;
    private static Vector2 aaa = new(0,0);
    public static void Draw(Player player, Camera camera)
    {
        float windowWidth = Globals.Bounds.X;
        float windowHeight = Globals.Bounds.Y;
        float x = player.Position.X;
        float y = player.Position.Y;

        Color c = player.Weapon.Reloading ? Color.Red : Color.White;
        
        for (int i = 0; i < player.HP; i++)
        {
            pos = new(i*50+5+x-windowWidth/2, y-windowHeight/2+5);
            Globals.SpriteBatch.Draw(player.HPTexture, pos, Color.White);
        }

        float halfX = x / 2;
        int totalWeapons = player.WeaponList.Count;

        int spacing = 25;

        if (totalWeapons > 0)
        {
            int middleIndex = totalWeapons / 2;
            if (player.Weapon == player.WeaponList[middleIndex])
            {
                middleWeapon = player.WeaponList[middleIndex].WeaponIconSelected;
            }
            else
            {
                if (player.WeaponList[middleIndex].IsUnlocked)
                {
                    middleWeapon = player.WeaponList[middleIndex].WeaponIcon;
                }
                else
                {
                    middleWeapon = player.WeaponList[middleIndex].WeaponIconLocked;
                }
            }
            int middleWeaponWidth = middleWeapon.Width;

            float middleX = halfX - (middleWeaponWidth / 2);

            float currentX = middleX;
            for (int i = middleIndex - 1; i >= 0; i--)
            {
                if (player.WeaponList[i] == player.Weapon)
                {
                    weaponTexture = player.WeaponList[i].WeaponIconSelected;
                }
                else
                {
                    if (player.WeaponList[i].IsUnlocked)
                    {
                        weaponTexture = player.WeaponList[i].WeaponIcon;
                    }
                    else
                    {
                        weaponTexture = player.WeaponList[i].WeaponIconLocked;
                    }
                }
                currentX -= weaponTexture.Width + spacing;
                pos = new (currentX + x/2, y-windowHeight*0.95f/2);
                Globals.SpriteBatch.Draw(weaponTexture, pos, Color.White);
            }
            pos = new (middleX + x/2, y-windowHeight*0.95f/2);
            Globals.SpriteBatch.Draw(middleWeapon, pos, Color.White);

            currentX = middleX + middleWeaponWidth + spacing;
            for (int i = middleIndex + 1; i < totalWeapons; i++)
            {
                if (player.WeaponList[i] == player.Weapon)
                {
                    weaponTexture = player.WeaponList[i].WeaponIconSelected;
                }
                else
                {
                    if (player.WeaponList[i].IsUnlocked)
                    {
                        weaponTexture = player.WeaponList[i].WeaponIcon;
                    }
                    else
                    {
                        weaponTexture = player.WeaponList[i].WeaponIconLocked;
                    }
                }
                pos = new(currentX + x/2, y-windowHeight*0.95f/2);
                Globals.SpriteBatch.Draw(weaponTexture, pos, Color.White);
                currentX += weaponTexture.Width + spacing;
            }
        }

        string timeString = $"{Globals.Minutes:D2}:{Globals.Seconds:D2}.{Globals.Hundredths:00}";
        pos = new(x+windowWidth/2-Globals.Font.MeasureString(timeString).X-5, y-windowHeight/2);
        Globals.SpriteBatch.DrawString(Globals.Font, $"{timeString}", pos, Color.White);
        pos = new(x, y-200);
        Globals.SpriteBatch.DrawString(Globals.Font, $"Pos: {player.Position}", pos, Color.White);
        pos = new(x, y-100);
        Globals.SpriteBatch.DrawString(Globals.Font, $"Rot: {player.Rotation}", pos, Color.White);
        pos = new(x-100, y);
        Globals.SpriteBatch.DrawString(Globals.Font, $"Mouse Pos: {InputManager.MousePosition}", pos, Color.White);

        if (player.Weapon != null)
        {
            pos = new(x+400, y+200);
            Globals.SpriteBatch.DrawString(Globals.Font, player.Weapon.GetAmmo(), pos, c);
        }
    }
}
