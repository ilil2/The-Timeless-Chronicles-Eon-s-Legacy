using Godot;

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

		switch (GameManager.InfoAutreJoueur[$"animation{Id}"])
		{
			case "shoot":
				OtherAnimationSet(false, true, false);
				GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
				break;
			case "walk":
				OtherAnimationSet(true, false, false);
				AnimationOtherTree.Set("parameters/Walk/blend_position", new Vector2(0, 1));
				GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
				break;
			case "walkside":
				OtherAnimationSet(true, false, false);
				AnimationOtherTree.Set("parameters/Walk/blend_position", new Vector2(1, 0));
				GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
				break;
			case "stop":
			case "idle":
				OtherAnimationSet(false, false, true);
				GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
				break;
			case "death":
				OtherAnimationSet(false, false, false, false, true);
				GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
				isAlive = false;
				break;
			case "damage":
				OtherAnimationSet(false, false, false, true);
				GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
				break;
		}
    }
    
    private void OtherAnimationSet(bool walk, bool shoot, bool idle, bool damage = false, bool death = false)
    {
	    AnimationOtherTree.Set("parameters/conditions/WhenWalk", walk);
	    AnimationOtherTree.Set("parameters/conditions/WhenShoot", shoot);
	    AnimationOtherTree.Set("parameters/conditions/Idle", idle);
	    AnimationOtherTree.Set("parameters/conditions/Death", death);
	    AnimationOtherTree.Set("parameters/conditions/Damage", damage);
    }
}
