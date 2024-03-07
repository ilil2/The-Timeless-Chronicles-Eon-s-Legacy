using Godot;
using System;

public partial class MainMenu : Node3D
{
	private Camera3D Cam;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Cam = GetNode<Camera3D>("Cam");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Cam.Rotation+=new Vector3(0,-0.001f,0);
	}
}
