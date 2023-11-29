using System;
using System.Collections.Generic;
using System.IO;
using Godot;

namespace Lib;

public class InputControl
{
    private Dictionary<string, Key> _dictInputControl = new Dictionary<string, Key>();

    public InputControl()
    {
        StreamReader ControlFile = new StreamReader("../Save/Control.txt");
        string? line;
        while ((line = ControlFile.ReadLine()) != null)
        {
            int separator = line.IndexOf(";", StringComparison.Ordinal);
            string input = line.Substring(0, separator - 1);
            string key = line.Substring(separator + 1);

            _dictInputControl[input] = (Key) Conversions.AtoI(key);
        }
        ControlFile.Close();
    }

    public void Save()
    {
        string save = "";
        foreach (var (input,key) in _dictInputControl)
        {
            save += $"{input};{key}\n";
        }

        save = save.Substring(0, save.Length - 1);
        
        StreamWriter ControlFile = new StreamWriter("../Save/Control.txt");
        ControlFile.Write(save);
        ControlFile.Close();
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