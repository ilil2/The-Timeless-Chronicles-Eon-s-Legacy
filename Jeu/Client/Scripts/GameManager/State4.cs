using System;
using Godot;

public partial class State4 : GameManager
{
    public int LoadBar = 0;
    public static void State()
    {
        if (MapOnLoad)
        {
            ((ProgressBarMapLvl1)ProgressBar).Load = ((MapLvl1Script)Map).step;
        }
        if (ClassSelectUI.ClassChose != "")
        {
            InfoJoueur["class"] = ClassSelectUI.ClassChose;
            ClassSelectUI.ClassChose = "";
            tw2.WriteLine(InfoJoueur["class"]);
            tw2.Flush();
        }
        else if (((MapLvl1Script)Map).MapIsReady() && !StartMap)
        {
            Map.Visible = false;
            tw2.WriteLine("load");
            tw2.Flush();
            MapOnLoad = false;
            ((ProgressBarMapLvl1)ProgressBar).Load = 2500;
        }
        else if (StartMap)
        {
            ProgressBar.QueueFree();
            Map.Visible = true;
            state = 5;
        }
    }
}