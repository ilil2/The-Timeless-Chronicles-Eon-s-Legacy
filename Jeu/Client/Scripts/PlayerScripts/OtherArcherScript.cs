using Godot;

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
        
        switch (GameManager.InfoAutreJoueur[$"animation{Id}"])
        {
            case "hitbow":
                OtherAnimationSet(false, false, false, false, true, false);
                GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
                break;
            case "shoot":
                OtherAnimationSet(false, false, false, true, false, false);
                GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
                break;
            case "aim":
                OtherAnimationSet(false, false, true, false, false, false);
                GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
                break;
            case "walk":
                OtherAnimationSet(true, false, false, false, false, false);
                AnimationOtherTree.Set("parameters/Walk/blend_position", new Vector2(0, 1));
                GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
                break;
            case "walkside":
                OtherAnimationSet(true, false, false, false, false, false);
                AnimationOtherTree.Set("parameters/Walk/blend_position", new Vector2(1, 0));
                GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
                break;
            case "aimwalk":
                OtherAnimationSet(false, true, false, false, false, false);
                AnimationOtherTree.Set("parameters/AimWalk/blend_position", new Vector2(0, 1));
                GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
                break;
            case "aimwalkside":
                OtherAnimationSet(false, true, false, false, false, false);
                AnimationOtherTree.Set("parameters/AimWalk/blend_position", new Vector2(1, 0));
                GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
                break;
            case "idle":
                OtherAnimationSet(false, false, false, false, false, true);
                GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
                break;
            case "death":
                OtherAnimationSet(false, false, false, false, false, false, false, true);
                GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
                isAlive = false;
                break;
            case "damage":
                OtherAnimationSet(false, false, false, false, false, false, true);
                GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
                break;
        }
    }
    
    private void OtherAnimationSet(bool walk, bool aimwalk, bool aim, bool shoot, bool hit, bool idle, bool damage = false, bool death = false)
    {
        AnimationOtherTree.Set("parameters/conditions/WhenWalk", walk);
        AnimationOtherTree.Set("parameters/conditions/WhenAimWalk", aimwalk);
        AnimationOtherTree.Set("parameters/conditions/WhenAim", aim);
        AnimationOtherTree.Set("parameters/conditions/WhenShoot", shoot);
        AnimationOtherTree.Set("parameters/conditions/WhenHitBow", hit);
        AnimationOtherTree.Set("parameters/conditions/Idle", idle);
        AnimationOtherTree.Set("parameters/conditions/Death", death);
        AnimationOtherTree.Set("parameters/conditions/Damage", damage);
        
        
    }
}
