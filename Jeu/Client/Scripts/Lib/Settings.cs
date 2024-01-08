using System;
using System.Collections.Generic;
using System.IO;

namespace Lib;

public class Settings
{
    private Dictionary<string, int> _dictSettings = new Dictionary<string, int>
    {
        {"language", 0},
        {"mouseSensibility", 10},
        {"chatSize", 1},
    };

    public Settings()
    {
        string[] lines = File.ReadAllLines("Scripts/Save/Settings.txt");
        foreach (var line in lines)
        {
            int separator = line.IndexOf(";", StringComparison.Ordinal);
            string name = line.Substring(0, separator - 1);
            string value = line.Substring(separator + 1);

            _dictSettings[name] = Conversions.AtoI(value);
        }
    }
    
    public void Save()
    {
        string save = "";
        foreach (var (name,value) in _dictSettings)
        {
            save += $"{name};{value}\n";
        }

        save = save.Substring(0, save.Length - 1);
        
        File.WriteAllText("Scripts/Save/Settings.txt", save);
    }
}