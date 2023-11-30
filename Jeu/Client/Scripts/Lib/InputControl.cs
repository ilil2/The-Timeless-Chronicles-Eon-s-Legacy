using System;
using System.Collections.Generic;
using System.IO;
using Godot;

namespace Lib;

public class InputControl
{
    private Dictionary<string, Key> _dictInputControl = new Dictionary<string, Key>
    {
        {"forward", (Key)90},          // Z
        {"backward", (Key)83},         // S
        {"left", (Key)81},             // Q
        {"right", (Key)68},            // D
        {"sprint", (Key)4194325},      // Shift
        {"dash", (Key)32},             // Space
        {"capacity1", (Key)65},        // A
        {"capacity2", (Key)69},        // E
        {"capacity3", (Key)70},        // F
        {"item1", (Key)49},            // 1
        {"item2", (Key)50},            // 2
        {"item3", (Key)51},            // 3
        {"inventory", (Key)4194306},   // Tab
        {"reload", (Key)82},           // R
        {"tchat", (Key)84}             // T
    };
    

    public InputControl()
    {
        string[] lines = File.ReadAllLines("Scripts/Save/Control.txt");
        foreach (var line in lines)
        {
            int separator = line.IndexOf(";", StringComparison.Ordinal);
            string input = line.Substring(0, separator - 1);
            string key = line.Substring(separator + 1);

            _dictInputControl[input] = (Key) Conversions.AtoI(key);
        }
    }

    public void Save()
    {
        string save = "";
        foreach (var (input,key) in _dictInputControl)
        {
            save += $"{input};{key}\n";
        }

        save = save.Substring(0, save.Length - 1);
        
        File.WriteAllText("Scripts/Save/Control.txt", save);
        
    }

    public Dictionary<string, Key> GetAllControl()
    {
        return _dictInputControl;
    }

    public void SetControl(string input, Key key)
    {
        _dictInputControl[input] = key;
    }
    
}