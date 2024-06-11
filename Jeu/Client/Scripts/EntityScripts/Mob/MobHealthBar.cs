using Godot;
using System;

public partial class MobHealthBar : Node3D
{
	
	[Export] public int Max = 100;
	[Export] public int Value = 50;
	[Export] public bool Show = false;
	
	private ProgressBar Hp;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hp = GetNode<ProgressBar>("SubViewport/ProgressBar");
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
			_hp.Modulate = new Color(_hp.Modulate.R, _hp.Modulate.G, _hp.Modulate.B, 100);
			Show = false;
		}
		if(Hp.Value!=Value)
		{
			Hp.Value = Value;
		}

		if (_hp.Modulate.A != 0)
		{
			_hp.Modulate = new Color(_hp.Modulate.R, _hp.Modulate.G, _hp.Modulate.B, _hp.Modulate.A-1f);
		}
	}
}
