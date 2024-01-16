using Godot;
using System;

public partial class OtherArcherScript : OtherClassScript
{
    public override void _Ready()
    {
        InitOtherPlayer();
    }
    
    public override void _Process(double delta)
    {
        PseudoManager();

        if (GameManager.InfoAutreJoueur[$"attack{Id}"] != "")
        {
            PackedScene arrowScene = GD.Load<PackedScene>("res://Scenes/EntityScenes/Arrow.tscn");
            RigidBody3D arrow = arrowScene.Instantiate<RigidBody3D>();
            
            string[] arrowInfo = GameManager.InfoAutreJoueur[$"attack{Id}"].Split(";");
            
            arrow.Position = new Vector3(arrowInfo[0].ToFloat(), arrowInfo[1].ToFloat(), arrowInfo[2].ToFloat());
            arrow.Rotation = new Vector3(arrowInfo[3].ToFloat(), arrowInfo[4].ToFloat(), arrowInfo[5].ToFloat());
            arrow.LinearVelocity = new Vector3(arrowInfo[6].ToFloat(), arrowInfo[7].ToFloat(), arrowInfo[8].ToFloat());
            GetTree().Root.AddChild(arrow);
            
            GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
        }
    }
    
    public override void _PhysicsProcess(double delta)
    {
        SetPosition();
        SetRotation();
    }
}
