using System;
using System.Collections.Generic;
using System.IO;
using Godot;

namespace Lib;

public class InputControl
{
    private Dictionary<string, Key> _dictInputControl = new Dictionary<string, Key>
    {
        {"forward", Key.Z},
        {"backward", Key.S},
        {"left", Key.Q},
        {"right", Key.D},
        {"sprint", Key.Shift},
        {"dash", Key.Space},
        {"capacity1", Key.A},
        {"capacity2", Key.E},
        {"capacity3", Key.F},
        {"item1", Key.Key1},
        {"item2", Key.Key2},
        {"item3", Key.Key3},
        {"inventory", Key.Tab},
        {"reload", Key.R},
        {"tchat", Key.T},
        {"pause", Key.Escape}
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