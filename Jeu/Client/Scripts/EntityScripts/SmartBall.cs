using Godot;
using System;
using Lib;
using System.Collections.Generic;

public partial class SmartBall : Node3D
{
	private Node3D MeshSkin;
	private Vector3 TargetPosition = new Vector3(0,0,0);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MeshSkin = GetNode<Node3D>("Skin");
		TargetPosition = GetPlayer().GlobalPosition+new Vector3(0,1,0);
		//LookAt(TargetPosition);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		LookAt(TargetPosition);
		MeshSkin.Rotation+=new Vector3(0.1f,0.1f,0.1f);
		//TargetPosition = GetPlayer().GlobalPosition+new Vector3(0,1,0);
		Vector3 direction = (TargetPosition - GlobalTransform.Origin).Normalized();

		// Déplacer l'objet dans la direction de la cible
		Translate(direction * 1);
	}
	private CharacterBody3D GetPlayer()
	{
		double distmin = 99999999999.0;
		CharacterBody3D res = null;
		for(int i = 0; i<GameManager.ListJoueur.Count;i++)
		{
			if(GameManager.ListJoueur[i]!=null)
			{
				double dist = MapTool.Distance(this.GlobalPosition,GameManager.ListJoueur[i].GlobalPosition);
				if(dist<distmin)
				{
					distmin = dist;
					res = GameManager.ListJoueur[i];
				}
			}
		}
		return res;
	}
	private void _on_corps_body_entered(Node3D body)
	{
		QueueFree();
	}
}

