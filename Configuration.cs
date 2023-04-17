using Newtonsoft.Json;
using System.IO;

public class Configuration
{
    public const string filePath = "config.json";

    public static DataContainer GetConfig()
    {
        if (GetRawFile(out string jsonText))
            return JsonConvert.DeserializeObject<DataContainer>(jsonText);

        return new DataContainer();
    }

    static bool GetRawFile(out string jsonText)
    {
        if (File.Exists(filePath))
        {
            jsonText = File.ReadAllText(filePath);
            return true;
        }

        jsonText = null;
        return false;
    }

    public class DataContainer
    {
        public string startHotkey;
        public string stopHotkey;
        public DataItem[] steps;
    }

    public class DataItem
    {
        public string type;

        public int x;
        public int y;

        public int preDelay;
    }
}