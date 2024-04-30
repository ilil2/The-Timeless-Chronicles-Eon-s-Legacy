using Godot;
using System;

public partial class TestFog : Node3D
{
	public bool Fog = false;
	public Label label;
	public int FrameCount = 0;
	public int LastFrame = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		label = GetNode<Label>("Label");
		label.Text = "Fog: " + Fog;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		FrameCount += 1;
		if (Input.IsMouseButtonPressed(MouseButton.Left) && FrameCount - LastFrame > 10)
		{
			LastFrame = FrameCount;
			Fog = !Fog;
			label.Text = "Fog: " + Fog;
		}
	}
}
