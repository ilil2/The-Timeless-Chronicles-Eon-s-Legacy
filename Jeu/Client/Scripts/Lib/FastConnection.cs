using System;
using System.Collections.Generic;
using System.IO;

namespace Lib;

public class FastConnection
{
    private List<string> _listConnection;
    
    FastConnection()
    {
        _listConnection = new List<string>();
        
        string[] lines = File.ReadAllLines("Scripts/Save/FastConnection.txt");
        foreach (var line in lines)
        {
            if (!line.Contains('\\') && !line.Contains('"') && !line.Contains("'"))
            {
                _listConnection.Add(line);
            }
        }
    }
    
    public void SaveConnection()
    {
        string save = "";
        foreach (var e in _listConnection)
        {
            save += $"{e}\n";
        }
        
        save = save.Substring(0, save.Length - 1);
        
        File.WriteAllText("Scripts/Save/FastConnection.txt", save);
    }
    
    public (string, string) GetLastConnection()
    {
        if (_listConnection.Count > 2)
        {
            return (_listConnection[0], _listConnection[1]);
        }
        return ("", "");
    }
}