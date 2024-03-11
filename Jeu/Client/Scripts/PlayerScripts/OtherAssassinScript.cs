using Godot;
using System;

public partial class OtherAssassinScript : OtherClassScript
{
    public override void _Ready()
    {
        InitOtherPlayer();
    }
    
    public override void _Process(double delta)
    {
        PseudoManager();
    }
    
    public override void _PhysicsProcess(double delta)
    {
        SetPosition();
        SetRotation();
        
        switch (GameManager.InfoAutreJoueur[$"animation{Id}"])
		{
			case "hit":
				AnimationOtherTree.Set("parameters/conditions/WhenWalk", false);
				AnimationOtherTree.Set("parameters/conditions/WhenSprint", false);
				AnimationOtherTree.Set("parameters/conditions/WhenHit", true);
				AnimationOtherTree.Set("parameters/conditions/Idle", false);
				GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
				break;
			case "sprint":
				AnimationOtherTree.Set("parameters/conditions/WhenWalk", false);
				AnimationOtherTree.Set("parameters/conditions/WhenSprint", true);
				AnimationOtherTree.Set("parameters/conditions/WhenHit", false);
				AnimationOtherTree.Set("parameters/conditions/Idle", false);
				GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
				break;
			case "walk":
				AnimationOtherTree.Set("parameters/conditions/WhenWalk", true);
				AnimationOtherTree.Set("parameters/conditions/WhenSprint", false);
				AnimationOtherTree.Set("parameters/conditions/WhenHit", false);
				AnimationOtherTree.Set("parameters/conditions/Idle", false);
				AnimationOtherTree.Set("parameters/Walk/blend_position", new Vector2(0, 1));
				GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
				break;
			case "walkside":
				AnimationOtherTree.Set("parameters/conditions/WhenWalk", true);
				AnimationOtherTree.Set("parameters/conditions/WhenSprint", false);
				AnimationOtherTree.Set("parameters/conditions/WhenHit", false);
				AnimationOtherTree.Set("parameters/conditions/Idle", false);
				AnimationOtherTree.Set("parameters/Walk/blend_position", new Vector2(1, 0));
				GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
				break;
			case "idle":
				AnimationOtherTree.Set("parameters/conditions/WhenWalk", false);
				AnimationOtherTree.Set("parameters/conditions/WhenSprint", false);
				AnimationOtherTree.Set("parameters/conditions/WhenHit", false);
				AnimationOtherTree.Set("parameters/conditions/Idle", true);
				GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
				break;
		}
    }
}
