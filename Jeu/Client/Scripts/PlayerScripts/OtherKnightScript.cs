using Godot;
using System;

public partial class OtherKnightScript : OtherClassScript
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
	}
	
	public override void _PhysicsProcess(double delta)
	{
		SetPosition();
		SetRotation();

		switch (GameManager.InfoAutreJoueur[$"attack{Id}"])
		{
			case "hit":
				AnimationOtherTree.Set("parameters/conditions/WhenWalk", false);
				AnimationOtherTree.Set("parameters/conditions/WhenBlock", false);
				AnimationOtherTree.Set("parameters/conditions/WhenHit", true);
				AnimationOtherTree.Set("parameters/conditions/Idle", false);
				GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
				break;
			case "protection":
				AnimationOtherTree.Set("parameters/conditions/WhenWalk", false);
				AnimationOtherTree.Set("parameters/conditions/WhenBlock", true);
				AnimationOtherTree.Set("parameters/conditions/WhenHit", false);
				AnimationOtherTree.Set("parameters/conditions/Idle", false);
				GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
				break;
			case "init":
				AnimationOtherTree.Set("parameters/conditions/WhenWalk", false);
				AnimationOtherTree.Set("parameters/conditions/WhenBlock", false);
				AnimationOtherTree.Set("parameters/conditions/WhenHit", false);
				AnimationOtherTree.Set("parameters/conditions/Idle", true);
				GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
				break;
			case "walk":
				AnimationOtherTree.Set("parameters/conditions/WhenWalk", true);
				AnimationOtherTree.Set("parameters/conditions/WhenBlock", false);
				AnimationOtherTree.Set("parameters/conditions/WhenHit", false);
				AnimationOtherTree.Set("parameters/conditions/Idle", false);
				AnimationOtherTree.Set("parameters/Walk/blend_position", new Vector2(0, 1));
				GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
				break;
			case "walkside":
				AnimationOtherTree.Set("parameters/conditions/WhenWalk", true);
				AnimationOtherTree.Set("parameters/conditions/WhenBlock", false);
				AnimationOtherTree.Set("parameters/conditions/WhenHit", false);
				AnimationOtherTree.Set("parameters/conditions/Idle", false);
				AnimationOtherTree.Set("parameters/Walk/blend_position", new Vector2(1, 0));
				GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
				break;
		}
	}
}
