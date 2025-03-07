namespace GA_2d_shooter;

public static class UIManager
{
    private static Texture2D weaponTexture;
    private static Texture2D middleWeapon;
    private static Vector2 pos;
    public static void Draw(Player player, Camera camera)
    {
        int x = 0;
        int y = 0;

        Color c = player.Weapon.Reloading ? Color.Red : Color.White;
        
        for (int i = 0; i < player.HP; i++)
        {
            pos = new(i*50+5, 5);
            Globals.SpriteBatch.Draw(player.HPTexture, pos, Color.White);
        }

        float halfX = player.Position.X / 2;
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
                Globals.SpriteBatch.Draw(weaponTexture, new Vector2(currentX, 5), Color.White);
            }

            Globals.SpriteBatch.Draw(middleWeapon, new Vector2(middleX, 5), Color.White);

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
                Globals.SpriteBatch.Draw(weaponTexture, new Vector2(currentX, 5), Color.White);
                currentX += weaponTexture.Width + spacing;
            }
        }

        string timeString = $"{Globals.Minutes:D2}:{Globals.Seconds:D2}.{Globals.Hundredths:00}";
        pos = new(player.Position.X*0.89f, player.Position.Y*0f);
        Globals.SpriteBatch.DrawString(Globals.Font, $"{timeString}", pos, Color.White);

        if (player.Weapon != null)
        {
            pos = new(player.Position.X+200, player.Position.Y+200);
            Globals.SpriteBatch.DrawString(Globals.Font, player.Weapon.GetAmmo(), pos, c);
        }
    }
}
