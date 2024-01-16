using Godot;
using System;

public partial class OtherScientistScript : OtherClassScript
{
    public override void _Ready()
    {
        InitOtherPlayer();
    }
    
    public override void _Process(double delta)
    {
        PseudoManager();

        if (GameManager.InfoAutreJoueur[$"attack{Id}"] != "" && GameManager.InfoAutreJoueur[$"attack{Id}"] != "stop")
        {
            PackedScene laserScene = GD.Load<PackedScene>("res://Scenes/EntityScenes/Laser.tscn");
            Node3D laser = laserScene.Instantiate<Node3D>();
            
            string[] laserInfo = GameManager.InfoAutreJoueur[$"attack{Id}"].Split(";");
            
            laser.Position = new Vector3(laserInfo[0].ToFloat(), laserInfo[1].ToFloat(), laserInfo[2].ToFloat());
            laser.Rotation = new Vector3(laserInfo[3].ToFloat(), laserInfo[4].ToFloat(), laserInfo[5].ToFloat());

            ((Laser)laser).SetLaserID(Id);
            GetTree().Root.AddChild(laser);

            GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
        }
    }
    
    public override void _PhysicsProcess(double delta)
    {
        SetPosition();
        SetRotation();
    }
}
