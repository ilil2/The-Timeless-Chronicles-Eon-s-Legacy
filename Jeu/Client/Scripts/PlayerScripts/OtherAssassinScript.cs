using Godot;
using System;

public partial class OtherAssassinScript : CharacterBody3D
{
	public Vector3 EntityPosition;

	public override void _Ready()
	{
		EntityPosition = Position;
	}

	public override void _Process(double delta)
	{
		Position = EntityPosition;
	}
}
