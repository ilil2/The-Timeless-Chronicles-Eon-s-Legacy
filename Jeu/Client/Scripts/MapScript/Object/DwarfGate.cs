using Godot;
using System;

public partial class DwarfGate : IRender
{
	[Export] private bool PlayOpen = false;
	private AnimationPlayer Ani;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Ani = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if(RenderSetup())
		{
			Render();
		}
		if(PlayOpen)
		{
			Ani.Play("Open");
			PlayOpen = false;
		}
	}
}
