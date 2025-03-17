namespace GA_2d_shooter;
public static class SaveManager
{
    private static string saveFile = "save.dat";
    private static string timeFile = "times.dat";

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

    public static void SaveTime(float time)
    {
        List<float> times = LoadTimes();
        times.Add(time);
        
        using (BinaryWriter writer = new BinaryWriter(File.Open(timeFile, FileMode.Create)))
        {
            writer.Write(times.Count);  // Write number of times first
            foreach (float t in times)
            {
                writer.Write(t);
            }
        }
    }

    public static List<float> LoadTimes()
    {
        List<float> times = new List<float>();
        
        if (!File.Exists(timeFile))
        {
            return times;
        }

        using (BinaryReader reader = new BinaryReader(File.Open(timeFile, FileMode.Open)))
        {
            try
            {
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    times.Add(reader.ReadSingle());  // ReadSingle for float
                }
            }
            catch (EndOfStreamException)
            {
                // Handle corrupted file
                return new List<float>();
            }
        }
        times.Sort();
        times.Reverse();
        return times;
    }

    public static void ResetTimes()
    {
        if (File.Exists(timeFile))
        {
            File.Delete(timeFile);
        }
    }
}