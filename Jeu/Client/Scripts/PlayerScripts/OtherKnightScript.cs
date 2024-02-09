using Godot;
using System;

public partial class OtherKnightScript : OtherClassScript
{
	public override void _Ready()
	{
		InitOtherPlayer();
		AnimationOtherPlayer = GetNode<AnimationPlayer>("AnimationOtherPlayer");
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
				AnimationOtherPlayer.Play("Hit");
				GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
				break;
			case "protection":
				AnimationOtherPlayer.Play("Protection");
				GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
				break;
			case "init":
				AnimationOtherPlayer.Play("Init");
				GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
				break;
			case "walk":
				AnimationOtherPlayer.Play("Walk");
				GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
				break;
			case "walkside":
				AnimationOtherPlayer.Play("WalkSide");
				GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
				break;
			case "walkdiagleft":
				AnimationOtherPlayer.Play("WalkDiagLeft");
				GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
				break;
			case "walkdiagright":
				AnimationOtherPlayer.Play("WalkDiagRight");
				GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
				break;
		}
	}
}
