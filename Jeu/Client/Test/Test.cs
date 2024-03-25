using Godot;
using System;

public partial class Test : Node3D
{
	private LineEdit textbox;
	private TextMesh t;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		textbox = GetNode<LineEdit>("LineEdit");
		t = (TextMesh)GetNode<MeshInstance3D>("T").Mesh;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		t.Text = textbox.Text;
	}
}
