using Godot;
using System;

public partial class Gravestone : StaticBody3D
{
	public Label3D Pseudo;
	public Label3D Info;
	public int ID = -1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pseudo = GetNode<Label3D>("Pseudo");
		Info = GetNode<Label3D>("Info");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
