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
        ResetControl();
        
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
    
    public void SetControlName(int input, string name)
    {
        if (_listInputControl.Count > input)
        {
            _listInputControl[input] = (name, _listInputControl[input].Item2);
        }
    }

    public void ResetControl()
    {
        int language = GameManager.SettingsManager.GetAllSettings()["language"];
        Dictionary<string, string> languageDict = GameManager.LanguageManager.GetLanguage(language);
        
        _listInputControl = new()
        {
            (languageDict["settingsMenuControlsMoveForward"], Key.Z),         // 0
            (languageDict["settingsMenuControlsMoveBackward"], Key.S),        // 1
            (languageDict["settingsMenuControlsMoveLeft"], Key.Q),            // 2
            (languageDict["settingsMenuControlsMoveRight"], Key.D),           // 3
            (languageDict["settingsMenuControlsSprint"], Key.Shift),          // 4
            (languageDict["settingsMenuControlsDash"], Key.Space),            // 5
            (languageDict["settingsMenuControlsCapacity1"], Key.A),           // 6
            (languageDict["settingsMenuControlsCapacity2"], Key.E),           // 7
            (languageDict["settingsMenuControlsCapacity3"], Key.F),           // 8
            (languageDict["settingsMenuControlsItem1"], Key.Key1),            // 9
            (languageDict["settingsMenuControlsItem2"], Key.Key2),            // 10
            (languageDict["settingsMenuControlsItem3"], Key.Key3),            // 11
            (languageDict["settingsMenuControlsInventory"], Key.Tab),         // 12
            (languageDict["settingsMenuControlsReload"], Key.R),              // 13
            (languageDict["settingsMenuControlsChat"], Key.T),                // 14
            (languageDict["settingsMenuControlsPause"], Key.Escape)           // 15
        };
    }
    
}