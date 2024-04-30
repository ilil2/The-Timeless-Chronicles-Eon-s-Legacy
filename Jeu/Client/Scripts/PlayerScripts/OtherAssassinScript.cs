using Godot;

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
				OtherAnimationSet(false, false, true, false);
				GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
				break;
			case "sprint":
				OtherAnimationSet(false, true, false, false);
				GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
				break;
			case "walk":
				OtherAnimationSet(true, false, false, false);
				AnimationOtherTree.Set("parameters/Walk/blend_position", new Vector2(0, 1));
				GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
				break;
			case "walkside":
				OtherAnimationSet(true, false, false, false);
				AnimationOtherTree.Set("parameters/Walk/blend_position", new Vector2(1, 0));
				GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
				break;
			case "idle":
				OtherAnimationSet(false, false, false, true);
				GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
				break;
			case "death":
				OtherAnimationSet(false, false, false, false, false, true);
				GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
				isAlive = false;
				break;
			case "damage":
				OtherAnimationSet(false, false, false, false, true);
				GameManager.InfoAutreJoueur[$"animation{Id}"] = "";
				break;
		}
    }
    
    private void OtherAnimationSet(bool walk, bool sprint, bool hit, bool idle, bool damage = false, bool death = false)
    {
	    AnimationOtherTree.Set("parameters/conditions/WhenWalk", walk);
	    AnimationOtherTree.Set("parameters/conditions/WhenSprint", sprint);
	    AnimationOtherTree.Set("parameters/conditions/WhenHit", hit);
	    AnimationOtherTree.Set("parameters/conditions/Idle", idle);
	    AnimationOtherTree.Set("parameters/conditions/Death", death);
	    AnimationOtherTree.Set("parameters/conditions/Damage", damage);
    }
}
