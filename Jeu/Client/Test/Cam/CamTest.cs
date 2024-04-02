using Godot;
using System;

public partial class CamTest : Node3D
{
	private RayCast3D RayCast;
	private CharacterBody3D Player;
	private StaticBody3D CamBody;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RayCast = GetNode<RayCast3D>("Camera3D/RayCast3D");
		Player = GetNode<CharacterBody3D>("Player");
		CamBody = GetNode<StaticBody3D>("Camera3D/CamBody");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		RayCast.TargetPosition = Player.Position;
		if(RayCast.IsColliding() && (RayCast.GetCollider() as Node).Name == "CamBody")
		{
			GD.Print("Player On Screen");
		}
		else
		{
			GD.Print("Player Out Screen");	
		}
		
	}
}
