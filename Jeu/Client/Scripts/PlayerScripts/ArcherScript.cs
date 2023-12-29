using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class ArcherScript : ClassScript
{
	//Varibale du tir
	private int _shootTimer;
	private float _shootPower = 1;
	private bool _isAiming;
	
	public static bool IsShooting;
	
	public override void _Ready()
	{
		InitPlayer();
	}

	public override void _Input(InputEvent @event)
	{
		if (_camera.Current && !GameManager._pausemode)
		{
			Zoom(@event);
		}
	}

	public override void _Process(double delta)
	{
		SendPosition();
		Pause();
	}
	
	public override void _PhysicsProcess(double delta)
	{
		PhysicsReset();
		Gravity(delta);

		if (_camera.Current && !GameManager._pausemode)
		{
			Dash();
			Move(delta);
			ShootArrow(_playerMesh);
		}
		else
		{
			_shootTimer = 0;
		}
	}
	
	private void ShootArrow(MeshInstance3D playerMesh)
	{
		_shootTimer += 1;
		
		if (Input.IsMouseButtonPressed(MouseButton.Left) && _shootTimer > 30 && _shootPower <= 3)
		{
			IsShooting = true;
			_isAiming = true;
			_shootPower += 0.015f;
			_cameraV.SpringLength = 0;
			
			PackedScene crossHairScene = GD.Load<PackedScene>("res://Scenes/UI/ViewFinder.tscn");
			Control crossHair = crossHairScene.Instantiate<Control>();
			AddChild(crossHair);
		}
		else if (IsShooting || _shootPower > 3)
		{
			_isAiming = false;
			_shootTimer = 0;
		}
		
		if (!_isAiming && IsShooting)
		{
			PackedScene arrowScene = GD.Load<PackedScene>("res://Scenes/EntityScenes/Arrow.tscn");
			RigidBody3D arrow = arrowScene.Instantiate<RigidBody3D>();

			double rotationY = playerMesh.Rotation.Y;
				
			arrow.Position = new Vector3(Position.X + (float)Math.Sin(rotationY)*2, Position.Y + 2, Position.Z + (float)Math.Cos(rotationY)*2);
			arrow.LinearVelocity = new Vector3((float)(Math.Sin(rotationY)*10), 2, (float)(Math.Cos(rotationY)*10)) * _shootPower;
			arrow.Rotation = new Vector3(arrow.Rotation.X, playerMesh.Rotation.Y + (float)Math.PI / 2f, arrow.Rotation.Z);
			GetTree().Root.AddChild(arrow);
			
			IsShooting = false;
			_shootPower = 1;
			_cameraV.SpringLength = -4;
		}
	}
}
