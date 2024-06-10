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
		if (Show)
		{
			(Hp as CanvasItem).Modulate = new Color((Hp as CanvasItem).Modulate.R, (Hp as CanvasItem).Modulate.G, (Hp as CanvasItem).Modulate.B, 100);
			Show = false;
		}
		if(Hp.Value!=Value)
		{
			Hp.Value = Value;
		}

		if ((Hp as CanvasItem).Modulate.A != 0)
		{
			(Hp as CanvasItem).Modulate = new Color((Hp as CanvasItem).Modulate.R, (Hp as CanvasItem).Modulate.G, (Hp as CanvasItem).Modulate.B, (Hp as CanvasItem).Modulate.A-0.2f);
		}
	}
}
