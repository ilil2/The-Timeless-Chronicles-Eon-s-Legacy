using System;
using System.Collections.Generic;
using System.IO;
using Godot;

namespace Lib;

public class InputControl
{
    private List<(string, Key)> _listInputControl = new()
    {
        ("forward", Key.Z),         // 0
        ("backward", Key.S),        // 1
        ("left", Key.Q),            // 2
        ("right", Key.D),           // 3
        ("sprint", Key.Shift),      // 4
        ("dash", Key.Space),        // 5
        ("capacity1", Key.A),       // 6
        ("capacity2", Key.E),       // 7
        ("capacity3", Key.F),       // 8
        ("item1", Key.Key1),        // 9
        ("item2", Key.Key2),        // 10
        ("item3", Key.Key3),        // 11
        ("inventory", Key.Tab),     // 12
        ("reload", Key.R),          // 13
        ("tchat", Key.T),           // 14
        ("pause", Key.Escape)       // 15
    };
    

    public InputControl()
    {
        string[] lines = File.ReadAllLines("Scripts/Save/Control.txt");
        int index = 0;
        foreach (var line in lines)
        {
            string[] elements = line.Split(";");
            _listInputControl[index] = (elements[0], (Key)Conversions.AtoI(elements[1]));
            index += 1;
        }
    }

    public void SaveControl()
    {
        string save = "";
        for (int i = 0; i < _listInputControl.Count; i++)
        {
            save += $"{_listInputControl[i].Item1};{_listInputControl[i].Item2.GetHashCode()}\n";
        }

        save = save.Substring(0, save.Length - 1);
        
        File.WriteAllText("Scripts/Save/Control.txt", save);
    }

    public List<(string, Key)> GetAllControl()
    {
        return _listInputControl;
    }

    public void SetControl(int input, Key key)
    {
        if (_listInputControl.Count > input)
        {
            _listInputControl[input] = (_listInputControl[input].Item1, key);
        }
    }

    public void ResetControl()
    {
        _listInputControl = new()
        {
            ("forward", Key.Z),         // 0
            ("backward", Key.S),        // 1
            ("left", Key.Q),            // 2
            ("right", Key.D),           // 3
            ("sprint", Key.Shift),      // 4
            ("dash", Key.Space),        // 5
            ("capacity1", Key.A),       // 6
            ("capacity2", Key.E),       // 7
            ("capacity3", Key.F),       // 8
            ("item1", Key.Key1),        // 9
            ("item2", Key.Key2),        // 10
            ("item3", Key.Key3),        // 11
            ("inventory", Key.Tab),     // 12
            ("reload", Key.R),          // 13
            ("tchat", Key.T),           // 14
            ("pause", Key.Escape)       // 15
        };
    }
    
}