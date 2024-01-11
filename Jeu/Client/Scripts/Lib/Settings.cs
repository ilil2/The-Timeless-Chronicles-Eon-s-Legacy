using System;
using System.Collections.Generic;
using System.IO;
using Godot;

namespace Lib;

public class Settings
{
    private Dictionary<string, int> _dictSettings;

    public Settings()
    {
        ResetSettings();
        
        string[] lines = File.ReadAllLines("Scripts/Save/Settings.txt");
        foreach (var line in lines)
        {
            int separator = line.IndexOf(";", StringComparison.Ordinal);
            string name = line.Substring(0, separator);
            string value = line.Substring(separator + 1);
            
            _dictSettings[name] = Conversions.AtoI(value);
        }
        
    }
    
    public void SaveSettings()
    {
        string save = "";
        foreach (var (name,value) in _dictSettings)
        {
            save += $"{name};{value}\n";
        }
        
        save = save.Substring(0, save.Length - 1);
        
        File.WriteAllText("Scripts/Save/Settings.txt", save);
    }
    
    public Dictionary<string, int> GetAllSettings()
    {
        return _dictSettings;
    }
    
    public void ResetSettings()
    {
        _dictSettings = new Dictionary<string, int>
        {
            {"language", 0},
            {"mouseSensibility", 10},
            {"chatSize", 1},
        };
    }
}