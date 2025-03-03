namespace GA_2d_shooter;

public static class UIManager
{
    private static Texture2D bulletTexture; //Default bullet texture if none is assigned in weapon subclass
    private static Texture2D weaponTexture;
    private static Texture2D middleWeapon;

    public static void Init(Texture2D tex)
    {
        bulletTexture = tex;
    }

    public static void Draw(Player player)
    {
        //Texture2D bulletTexture = player.Weapon.ProjectileTextureUI;
        Color c = player.Weapon.Reloading ? Color.Red : Color.White;

        /*for (int i = 0; i < player.Weapon.Ammo; i++)
        {
            Vector2 pos = new(0, i * bulletTexture.Height * 2);
            Globals.SpriteBatch.Draw(bulletTexture, pos, null, c * 0.75f, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
        }*/

        Vector2 pos = new (0,0);

        
        for (int i = 0; i < player.HP; i++)
        {
            pos = new(i*50+5, 5);
            Globals.SpriteBatch.Draw(player.HPTexture, pos, Color.White);
        }



        int halfX = Globals.Bounds.X / 2;
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

            int middleX = halfX - (middleWeaponWidth / 2);

            int currentX = middleX;
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





        if (player.Weapon != null)
        {
            pos = new(Globals.Bounds.X*0.85f, Globals.Bounds.Y*0.8f);
            Globals.SpriteBatch.DrawString(Globals.Font, player.Weapon.GetAmmo(), pos, c);
        }
    }
}
