namespace GA_2d_shooter
{
    public static class SaveManager
    {
        private static string saveFile = "save.dat";
        public static void SaveExperience(int experience)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(saveFile, FileMode.Create)))
            {
                writer.Write(experience);
            }
        }
        public static int LoadExperience()
        {
            if (!File.Exists(saveFile))
            {
                return 0;
            }

            using (BinaryReader reader = new BinaryReader(File.Open(saveFile, FileMode.Open)))
            {
                return reader.ReadInt32();
            }
        }
    }
}
