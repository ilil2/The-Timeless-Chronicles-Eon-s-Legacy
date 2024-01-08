using System;
using System.Collections.Generic;
using System.IO;
using Godot;

namespace Lib;

public class InputControl
{
    private List<(string, Key)> _listInputControl;
    
    public InputControl()
    {
        _listInputControl = new List<(string, Key)>
        {
            ("Forward", Key.Z),         // 0
            ("Backward", Key.S),        // 1
            ("Left", Key.Q),            // 2
            ("Right", Key.D),           // 3
            ("Sprint", Key.Shift),      // 4
            ("Dash", Key.Space),        // 5
            ("Capacity 1", Key.A),      // 6
            ("Capacity 2", Key.E),      // 7
            ("Capacity 3", Key.F),      // 8
            ("Item 1", Key.Key1),       // 9
            ("Item 2", Key.Key2),       // 10
            ("Item 3", Key.Key3),       // 11
            ("Inventory", Key.Tab),     // 12
            ("Reload", Key.R),          // 13
            ("Chat", Key.T),            // 14
            ("Pause", Key.Escape)       // 15
        };
        
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
            ("Forward", Key.Z),         // 0
            ("Backward", Key.S),        // 1
            ("Left", Key.Q),            // 2
            ("Right", Key.D),           // 3
            ("Sprint", Key.Shift),      // 4
            ("Dash", Key.Space),        // 5
            ("Capacity 1", Key.A),      // 6
            ("Capacity 2", Key.E),      // 7
            ("Capacity 3", Key.F),      // 8
            ("Item 1", Key.Key1),       // 9
            ("Item 2", Key.Key2),       // 10
            ("Item 3", Key.Key3),       // 11
            ("Inventory", Key.Tab),     // 12
            ("Reload", Key.R),          // 13
            ("Chat", Key.T),            // 14
            ("Pause", Key.Escape)       // 15
        };
    }
    
}