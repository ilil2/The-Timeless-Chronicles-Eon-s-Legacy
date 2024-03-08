using Godot;
using System;

public partial class OtherScientistScript : OtherClassScript
{
    public override void _Ready()
    {
        InitOtherPlayer();
        AnimationOtherTree = GetNode<AnimationTree>("AnimationOtherTree");
        AnimationOtherTree.Active = true;
    }
    
    public override void _Process(double delta)
    {
        PseudoManager();

        if (GameManager.InfoAutreJoueur[$"attack{Id}"] != "" && GameManager.InfoAutreJoueur[$"attack{Id}"] != "stop" && GameManager.InfoAutreJoueur[$"attack{Id}"] != "next")
		{
			PackedScene laserScene = GD.Load<PackedScene>("res://Scenes/EntityScenes/Laser.tscn");
			Node3D laser = laserScene.Instantiate<Node3D>();
			
			string[] laserInfo = GameManager.InfoAutreJoueur[$"attack{Id}"].Split(";");
			
			laser.Position = new Vector3(float.Parse(laserInfo[0]), float.Parse(laserInfo[1]), float.Parse(laserInfo[2]));
			laser.Rotation = new Vector3(float.Parse(laserInfo[3]), float.Parse(laserInfo[4]), float.Parse(laserInfo[5]));

			((Laser)laser).SetLaserID(Id);
			GetTree().Root.AddChild(laser);

			GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
		}
    }
    
    public override void _PhysicsProcess(double delta)
    {
        SetPosition();
        SetRotation();

		switch (GameManager.InfoAutreJoueur[$"attack{Id}"])
		{
			case "shoot":
				AnimationOtherTree.Set("parameters/conditions/WhenWalk", false);
				AnimationOtherTree.Set("parameters/conditions/WhenShoot", true);
				AnimationOtherTree.Set("parameters/conditions/Idle", false);
				GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
				break;
			case "walk":
				AnimationOtherTree.Set("parameters/conditions/WhenWalk", true);
				AnimationOtherTree.Set("parameters/conditions/WhenShoot", false);
				AnimationOtherTree.Set("parameters/conditions/Idle", false);
				AnimationOtherTree.Set("parameters/Walk/blend_position", new Vector2(0, 1));
				GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
				break;
			case "walkside":
				AnimationOtherTree.Set("parameters/conditions/WhenWalk", true);
				AnimationOtherTree.Set("parameters/conditions/WhenShoot", false);
				AnimationOtherTree.Set("parameters/conditions/Idle", false);
				AnimationOtherTree.Set("parameters/Walk/blend_position", new Vector2(1, 0));
				GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
				break;
			case "stop":
			case "idle":
				AnimationOtherTree.Set("parameters/conditions/WhenWalk", false);
				AnimationOtherTree.Set("parameters/conditions/WhenShoot", false);
				AnimationOtherTree.Set("parameters/conditions/Idle", true);
				GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
				break;
		}
    }
}
