using Godot;
using System;

public partial class Test : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for (int i = 0 ; i <= 3 ; i++)
		{
			CharacterBody3D Mob = GD.Load<PackedScene>("res://TestIA/Mob.tscn").Instantiate<CharacterBody3D>();
			Mob.Position = new Vector3(2 * i,0,0);
			AddChild(Mob);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
