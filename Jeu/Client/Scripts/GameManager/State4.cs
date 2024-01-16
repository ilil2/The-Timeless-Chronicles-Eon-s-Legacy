using System;
using Godot;
using Serveur;

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
            UDP.Send(soc2,$"{InfoJoueur["id"]}/{InfoJoueur["class"]}",iep2);
        }
        else if (((MapLvl1Script)Map).MapIsReady() && !StartMap)
        {
            Map.Visible = false;
            UDP.Send(soc2,"load",iep2);
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