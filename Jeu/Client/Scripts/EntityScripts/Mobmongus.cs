using Godot;
using System;

public partial class Mobmongus : MobScript
{
	public override void _Ready()
	{
		Ready();
	}
	public override void _PhysicsProcess(double delta)
	{
		PhysicsProcess(delta);
	}
	public override void _Process(double delta) 
	{
		Process(delta);
	}
}
