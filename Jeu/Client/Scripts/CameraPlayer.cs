using Godot;
using System;

public partial class CameraPlayer : Node3D
{
	private float camrot_h;
	private float camrot_v;
	
	[Export]
	float cam_v_max = 75;
	float cam_v_min = -55;
	float joystick_sensitivity = 20; 
	
	private float h_sensitivity = 0.01f;
	private float v_sensitivity = 0.01f;
	private float h_acceleration = 10;
	private float v_acceleration = 10;
	private Vector2 joyview;
	

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion eventMouseMotion)
		{
			camrot_h += -eventMouseMotion.Relative.X * h_sensitivity;
			camrot_v += eventMouseMotion.Relative.Y * v_sensitivity;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
			camrot_v = Mathf.Clamp(camrot_v, Mathf.DegToRad(cam_v_min), Mathf.DegToRad(cam_v_max));
			Node3D h = GetNode<Node3D>("h");
			SpringArm3D v = GetNode<SpringArm3D>("h/v");
			h.Rotation = new Vector3(h.Rotation.X, (float)Mathf.Lerp(h.Rotation.Y, camrot_h, delta * h_acceleration), h.Rotation.Z);
			v.Rotation = new Vector3((float)Mathf.Lerp(v.Rotation.X, camrot_v, delta * v_acceleration), v.Rotation.Y, v.Rotation.Z);
	}
}
