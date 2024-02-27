using Godot;
using System;

public partial class pointer : Node3D
{
	public CharacterBody3D Target;
	public bool Ready = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Ready)
		{
			LookAt(Target.GlobalPosition);	
		}
	}
	
	public void SetTarget(CharacterBody3D target)
	{
		Target = target;
		Ready = true;
	}
}
