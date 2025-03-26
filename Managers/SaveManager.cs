namespace GA_2d_shooter;

public static class SaveManager
{
    private static readonly string timeFile = "times.dat";

    public static void SaveTime(float time)
    {
        List<float> times = LoadTimes();
        times.Add(time);
        times.Sort();
        times.Reverse();

        using (BinaryWriter writer = new BinaryWriter(File.Open(timeFile, FileMode.Create)))
        {
            writer.Write(times.Count);
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
                    times.Add(reader.ReadSingle());
                }
            }
            catch (EndOfStreamException)
            {
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