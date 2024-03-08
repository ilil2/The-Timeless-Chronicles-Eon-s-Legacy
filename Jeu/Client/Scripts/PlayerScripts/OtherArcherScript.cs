using Godot;
using System;

public partial class OtherArcherScript : OtherClassScript
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

        if (GameManager.InfoAutreJoueur[$"attack{Id}"] != "" && GameManager.InfoAutreJoueur[$"attack{Id}"] != "next")
        {
            PackedScene arrowScene = GD.Load<PackedScene>("res://Scenes/EntityScenes/Arrow.tscn");
            RigidBody3D arrow = arrowScene.Instantiate<RigidBody3D>();
            
            GD.Print(GameManager.InfoAutreJoueur[$"attack{Id}"]);
            string[] arrowInfo = GameManager.InfoAutreJoueur[$"attack{Id}"].Split(";");
            
            arrow.Position = new Vector3(float.Parse(arrowInfo[0]), float.Parse(arrowInfo[1]), float.Parse(arrowInfo[2]));
            arrow.Rotation = new Vector3(float.Parse(arrowInfo[3]), float.Parse(arrowInfo[4]), float.Parse(arrowInfo[5]));
            arrow.LinearVelocity = new Vector3(float.Parse(arrowInfo[6]), float.Parse(arrowInfo[7]), float.Parse(arrowInfo[8]));
            GetTree().Root.AddChild(arrow);
            
            GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
        }
    }
    
    public override void _PhysicsProcess(double delta)
    {
        SetPosition();
        SetRotation();
        
        switch (GameManager.InfoAutreJoueur[$"attack{Id}"])
        {
            case "hitbow":
                AnimationOtherTree.Set("parameters/conditions/WhenWalk", false);
                AnimationOtherTree.Set("parameters/conditions/WhenAimWalk", false);
                AnimationOtherTree.Set("parameters/conditions/WhenAim", false);
                AnimationOtherTree.Set("parameters/conditions/WhenShoot", false);
                AnimationOtherTree.Set("parameters/conditions/WhenHitBow", true);
                AnimationOtherTree.Set("parameters/conditions/Idle", false);
                break;
            case "shoot":
                AnimationOtherTree.Set("parameters/conditions/WhenWalk", false);
                AnimationOtherTree.Set("parameters/conditions/WhenAimWalk", false);
                AnimationOtherTree.Set("parameters/conditions/WhenAim", false);
                AnimationOtherTree.Set("parameters/conditions/WhenShoot", true);
                AnimationOtherTree.Set("parameters/conditions/WhenHitBow", false);
                AnimationOtherTree.Set("parameters/conditions/Idle", false);
                GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
                break;
            case "aim":
                AnimationOtherTree.Set("parameters/conditions/WhenWalk", false);
                AnimationOtherTree.Set("parameters/conditions/WhenAimWalk", false);
                AnimationOtherTree.Set("parameters/conditions/WhenAim", true);
                AnimationOtherTree.Set("parameters/conditions/WhenShoot", false);
                AnimationOtherTree.Set("parameters/conditions/WhenHitBow", false);
                AnimationOtherTree.Set("parameters/conditions/Idle", false);
                GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
                break;
            case "walk":
                AnimationOtherTree.Set("parameters/conditions/WhenWalk", true);
                AnimationOtherTree.Set("parameters/conditions/WhenAimWalk", false);
                AnimationOtherTree.Set("parameters/conditions/WhenAim", false);
                AnimationOtherTree.Set("parameters/conditions/WhenShoot", false);
                AnimationOtherTree.Set("parameters/conditions/WhenHitBow", false);
                AnimationOtherTree.Set("parameters/conditions/Idle", false);
                AnimationOtherTree.Set("parameters/Walk/blend_position", new Vector2(0, 1));
                GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
                break;
            case "walkside":
                AnimationOtherTree.Set("parameters/conditions/WhenWalk", true);
                AnimationOtherTree.Set("parameters/conditions/WhenAimWalk", false);
                AnimationOtherTree.Set("parameters/conditions/WhenAim", false);
                AnimationOtherTree.Set("parameters/conditions/WhenShoot", false);
                AnimationOtherTree.Set("parameters/conditions/WhenHitBow", false);
                AnimationOtherTree.Set("parameters/conditions/Idle", false);
                AnimationOtherTree.Set("parameters/Walk/blend_position", new Vector2(1, 0));
                GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
                break;
            case "aimwalk":
                AnimationOtherTree.Set("parameters/conditions/WhenWalk", false);
                AnimationOtherTree.Set("parameters/conditions/WhenAimWalk", true);
                AnimationOtherTree.Set("parameters/conditions/WhenAim", false);
                AnimationOtherTree.Set("parameters/conditions/WhenShoot", false);
                AnimationOtherTree.Set("parameters/conditions/WhenHitBow", false);
                AnimationOtherTree.Set("parameters/conditions/Idle", false);
                AnimationOtherTree.Set("parameters/AimWalk/blend_position", new Vector2(0, 1));
                GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
                break;
            case "aimwalkside":
                AnimationOtherTree.Set("parameters/conditions/WhenWalk", false);
                AnimationOtherTree.Set("parameters/conditions/WhenAimWalk", true);
                AnimationOtherTree.Set("parameters/conditions/WhenAim", false);
                AnimationOtherTree.Set("parameters/conditions/WhenShoot", false);
                AnimationOtherTree.Set("parameters/conditions/WhenHitBow", false);
                AnimationOtherTree.Set("parameters/conditions/Idle", false);
                AnimationOtherTree.Set("parameters/AimWalk/blend_position", new Vector2(1, 0));
                GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
                break;
            case "idle":
                AnimationOtherTree.Set("parameters/conditions/WhenWalk", false);
                AnimationOtherTree.Set("parameters/conditions/WhenAimWalk", false);
                AnimationOtherTree.Set("parameters/conditions/WhenAim", false);
                AnimationOtherTree.Set("parameters/conditions/WhenShoot", false);
                AnimationOtherTree.Set("parameters/conditions/WhenHitBow", false);
                AnimationOtherTree.Set("parameters/conditions/Idle", true);
                GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
                break;
        }
    }
}
