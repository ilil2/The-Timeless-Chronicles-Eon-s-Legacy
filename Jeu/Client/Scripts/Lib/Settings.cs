using System;
using System.Collections.Generic;
using System.IO;

namespace Lib;

public class Settings
{
    private Dictionary<string, int> _dictSettings = new Dictionary<string, int>();

    public Settings()
    {
        StreamReader SettingsFile = new StreamReader("../Save/Settings.txt");
        string? line;
        while ((line = SettingsFile.ReadLine()) != null)
        {
            int separator = line.IndexOf(";", StringComparison.Ordinal);
            string name = line.Substring(0, separator - 1);
            string value = line.Substring(separator + 1);

            _dictSettings[name] = Conversions.AtoI(value);
        }
        SettingsFile.Close();
    }
    
    public void Save()
    {
        string save = "";
        foreach (var (name,value) in _dictSettings)
        {
            save += $"{name};{value}\n";
        }

        save = save.Substring(0, save.Length - 1);
        
        StreamWriter SettingsFile = new StreamWriter("../Save/Settings.txt");
        SettingsFile.Write(save);
        SettingsFile.Close();
    }
}