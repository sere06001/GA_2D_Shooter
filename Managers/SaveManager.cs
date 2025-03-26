using GA_2d_shooter;

public static class SaveManager
{
    private static readonly string timeFile = "times.dat";
    public static void SaveTime(float time, int score)
    {
        List<TimeScorePair> pairs = LoadTimes();
        pairs.Add(new TimeScorePair(time, score));
        
        // Sort by time (descending)
        pairs = pairs.OrderByDescending(p => p.Time).ToList();

        using (BinaryWriter writer = new BinaryWriter(File.Open(timeFile, FileMode.Create)))
        {
            writer.Write(pairs.Count);
            foreach (var pair in pairs)
            {
                writer.Write(pair.Time);
                writer.Write(pair.Score);
            }
        }
    }

    public static List<TimeScorePair> LoadTimes()
    {
        List<TimeScorePair> pairs = new List<TimeScorePair>();

        if (!File.Exists(timeFile))
        {
            return pairs;
        }

        using (BinaryReader reader = new BinaryReader(File.Open(timeFile, FileMode.Open)))
        {
            try
            {
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    float time = reader.ReadSingle();
                    int score = reader.ReadInt32();
                    pairs.Add(new TimeScorePair(time, score));
                }
            }
            catch (EndOfStreamException)
            {
                return new List<TimeScorePair>();
            }
        }

        return pairs;
    }

    public static void ResetTimes()
    {
        if (File.Exists(timeFile))
        {
            File.Delete(timeFile);
        }
    }
}