using Godot;
using System;

public partial class key : Node3D
{
	private Node3D Model;
	private int FrameCount = 0;
	private Vector3 InitPos;
	private const int LinearY = 5;
	private const float SpeedRotation = 1f;
	private const int SpeedLinearY = 3;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Model = GetNode<Node3D>("Mesh");
		InitPos = Model.Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		FrameCount+=SpeedLinearY;
		//GD.Print(Math.Sin(Mathf.DegToRad(FrameCount)));
		Model.Position = InitPos + new Vector3(0,(float)Math.Sin(Mathf.DegToRad(FrameCount))/LinearY,0);
		Model.RotationDegrees += new Vector3(0,SpeedRotation,0);
	}
}
