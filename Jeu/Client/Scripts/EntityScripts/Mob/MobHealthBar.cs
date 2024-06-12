using Godot;
using System;

public partial class MobHealthBar : Node3D
{
	
	[Export] public int Max = 100;
	[Export] public int Value = 50;
	[Export] public bool Show = false;
	
	private AnimationPlayer Ani;
	
	private ProgressBar Hp;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hp = GetNode<ProgressBar>("SubViewport/ProgressBar");
		Ani = GetNode<AnimationPlayer>("AnimationPlayer");
		Hp.MaxValue = Max;
		Hp.Value = Max;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void Process(double delta)
	{
		Hp.MaxValue = Max;
		CanvasItem _hp = Hp;
		if (Show)
		{
			Ani.Play("show");
			Show = false;
		}
		if(Hp.Value!=Value)
		{
			Hp.Value = Value;
		}
	}
}
