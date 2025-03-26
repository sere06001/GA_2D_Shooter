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
    private static Game1 game;
    public static void Init(Game1 game1)
    {
        game = game1;
    }

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
        pos = new(x, y - 200);
        Globals.SpriteBatch.DrawString(Globals.Font, $"Pos: {player.Position}", pos, Color.White);
        pos = new(x, y - 100);
        Globals.SpriteBatch.DrawString(Globals.Font, $"Rot: {player.Rotation}", pos, Color.White);
        pos = new(x - 100, y);
        Globals.SpriteBatch.DrawString(Globals.Font, $"Mouse Pos: {InputManager.MouseWorldPosition}", pos, Color.White);
        pos = new(x - 100, y + 100);
        Globals.SpriteBatch.DrawString(Globals.Font, $"WeaponList Count: {player.WeaponList.Count}", pos, Color.White);
    }
    public static void UpdateUIBounds()
    {
        windowWidth = Globals.Bounds.X;
        windowHeight = Globals.Bounds.Y;
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
                pos = new(currentX + playerX / 2, playerY - windowHeight * 0.95f / 2);
                Globals.SpriteBatch.Draw(weaponTexture, pos, Color.White);
            }
            pos = new(middleX + playerX / 2, playerY - windowHeight * 0.95f / 2);
            Globals.SpriteBatch.Draw(middleWeapon, pos, Color.White);

            currentX = middleX + middleWeaponWidth + spacing;
            for (int i = middleIndex + 1; i < totalWeapons; i++)
            {
                GetNonMiddleWepUI(player, i);
                pos = new(currentX + playerX / 2, playerY - windowHeight * 0.95f / 2);
                Globals.SpriteBatch.Draw(weaponTexture, pos, Color.White);
                currentX += weaponTexture.Width + spacing;
            }
        }
    }
    public static void DrawWeaponTimer(Player player)
    {
        int totalWeapons = player.WeaponList.Count;
        if (totalWeapons > 0)
        {
            int middleIndex = totalWeapons / 2;
            int middleWeaponWidth = middleWeapon.Width;
            float halfX = playerX / 2;
            float middleX = halfX - (middleWeaponWidth / 2);
            int spacing = 25;
            int offset = 20;

            float currentX = middleX;
            for (int i = middleIndex - 1; i >= 0; i--)
            {
                GetNonMiddleWepUI(player, i);
                currentX -= weaponTexture.Width + spacing;
                if (player.WeaponList[i].Reloading)
                {
                    string timerText = player.WeaponList[i].GetReloadProgress();
                    Vector2 timerPos = new(
                        currentX + playerX / 2 + weaponTexture.Width / 2,
                        playerY - windowHeight * 0.95f / 2 + weaponTexture.Height + offset
                    );
                    Globals.SpriteBatch.DrawString(
                        Globals.Font,
                        timerText,
                        timerPos,
                        Color.Red,
                        0f,
                        Globals.Font.MeasureString(timerText) / 2,
                        0.75f,
                        SpriteEffects.None,
                        0f
                    );
                }
            }

            if (player.WeaponList[middleIndex].Reloading)
            {
                string timerText = player.WeaponList[middleIndex].GetReloadProgress();
                Vector2 timerPos = new(
                    middleX + playerX / 2 + middleWeaponWidth / 2,
                    playerY - windowHeight * 0.95f / 2 + middleWeapon.Height + offset
                );
                Globals.SpriteBatch.DrawString(
                    Globals.Font,
                    timerText,
                    timerPos,
                    Color.Red,
                    0f,
                    Globals.Font.MeasureString(timerText) / 2,
                    0.75f,
                    SpriteEffects.None,
                    0f
                );
            }

            currentX = middleX + middleWeaponWidth + spacing;
            for (int i = middleIndex + 1; i < totalWeapons; i++)
            {
                if (player.WeaponList[i].Reloading)
                {
                    string timerText = player.WeaponList[i].GetReloadProgress();
                    Vector2 timerPos = new(
                        currentX + playerX / 2 + weaponTexture.Width / 2,
                        playerY - windowHeight * 0.95f / 2 + weaponTexture.Height + offset
                    );
                    Globals.SpriteBatch.DrawString(
                        Globals.Font,
                        timerText,
                        timerPos,
                        Color.Red,
                        0f,
                        Globals.Font.MeasureString(timerText) / 2,
                        0.75f,
                        SpriteEffects.None,
                        0f
                    );
                }
                currentX += weaponTexture.Width + spacing;
            }
        }
    }

    public static void DrawHP(Player player)
    {
        for (int i = 0; i < player.HP; i++)
        {
            pos = new(i * 50 + 5 + playerX - windowWidth / 2, playerY - windowHeight / 2 + 5);
            Globals.SpriteBatch.Draw(player.HPTexture, pos, Color.White);
        }
    }

    public static void DrawGameTimer(float gameTimer)
    {
        int minutes = (int)(gameTimer / 60);
        int seconds = (int)(gameTimer % 60);
        float hundredths = gameTimer % 1 * 100;
        string timerText = $"{minutes:D2}:{seconds:D2}.{hundredths:00}";
        float adjustedX = playerX + (Globals.Bounds.X - (game._graphics.IsFullScreen ? 0 : Globals.WindowModeOffset)) / 2 
            - Globals.Font.MeasureString(timerText).X - 10;
        pos = new(adjustedX, playerY - windowHeight / 2 + 10);
        Globals.SpriteBatch.DrawString(Globals.Font, timerText, pos, Color.White);
    }

    public static void DrawAmmo(Player player)
    {
        Color c = player.Weapon.Reloading ? Color.Red : Color.White;
        string ammo = player.Weapon.GetAmmo();
        Vector2 ammoSize = Globals.Font.MeasureString(ammo);
        float adjustedX = playerX + (Globals.Bounds.X - (game._graphics.IsFullScreen ? 0 : Globals.WindowModeOffset)) / 2 
            - ammoSize.X - 10 - windowWidth/40;
        float adjustedY = playerY + (Globals.Bounds.Y - (game._graphics.IsFullScreen ? 0 : Globals.WindowModeOffset)) / 2 
            - ammoSize.Y - windowHeight/20;
        pos = new(adjustedX, adjustedY);
        Globals.SpriteBatch.DrawString(Globals.Font, ammo, pos, c);
    }

    public static void Draw(Player player, Game1 game)
    {
        playerX = player.Position.X;
        playerY = player.Position.Y;

        DrawHP(player);
        DrawWeapons(player);
        DrawWeaponTimer(player);
        //DebugUI(player, playerX, playerY);

        if (player.Weapon != null)
        {
            DrawAmmo(player);
        }
    }
}