using Godot;
using System;

public partial class Moon : Node3D
{
	public override void _Ready()
	{
	}
	public override void _Process(double delta)
	{
		Rotation += new Vector3(0,0,0.000001f);
	}
}
