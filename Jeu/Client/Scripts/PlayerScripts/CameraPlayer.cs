using Godot;
using System;

public partial class CameraPlayer : Node3D
{
	private Camera3D _camera;
	
	private float camrot_h;
	private float camrot_v;
	
	private float cam_v_max = 75;
	private float cam_v_min = -55;
	
	private float h_sensitivity = 10;
	private float v_sensitivity = 10;
	private float h_acceleration = 5;
	private float v_acceleration = 5;

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
		_camera = GetNode<Camera3D>("h/v/Camera3D");
	}

	public override void _Input(InputEvent @event)
	{
		if (_camera.Current && !GameManager._pausemode && !GameManager.LockCamera && !InteractionShop.OnShop && !GameHUD.OnInventory)
		{
			if (@event is InputEventMouseMotion eventMouseMotion)
			{
				camrot_h += -eventMouseMotion.Relative.X * h_sensitivity / 1000;
				camrot_v += eventMouseMotion.Relative.Y * v_sensitivity / 1000;
			}
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_camera.Current && !GameManager._pausemode && !GameManager.LockCamera)
		{
			camrot_v = Mathf.Clamp(camrot_v, Mathf.DegToRad(cam_v_min), Mathf.DegToRad(cam_v_max));
			Node3D h = GetNode<Node3D>("h");
			SpringArm3D v = GetNode<SpringArm3D>("h/v");
		
			h.Rotation = new Vector3(h.Rotation.X, (float)Mathf.Lerp(h.Rotation.Y, camrot_h, delta * h_acceleration), h.Rotation.Z);
			v.Rotation = new Vector3((float)Mathf.Lerp(v.Rotation.X, camrot_v, delta * v_acceleration), v.Rotation.Y, v.Rotation.Z);
		}
	}
	
	public void ChangeSensibility(int sensibility)
	{
		h_sensitivity = sensibility;
		v_sensitivity = sensibility;
	}
}
