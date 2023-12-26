using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class ArcherScript : ClassScript
{
	//Varibale du tir
	private int _shootTimer;
	private float _shootPower = 1;
	private bool _isShooting;
	private bool _isAiming;
	
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
	}
	
	private void ShootArrow(MeshInstance3D playerMesh)
	{
		_shootTimer += 1;
		
		if (Input.IsMouseButtonPressed(MouseButton.Left) && _shootTimer > 30 && _shootPower <= 3)
		{
			_isShooting = true;
			_isAiming = true;
			_shootPower += 0.015f;
		}
		else if (_isShooting || _shootPower > 3)
		{
			_isAiming = false;
			_shootTimer = 0;
		}
		
		if (!_isAiming && _isShooting)
		{
			PackedScene ArrowScene = GD.Load<PackedScene>("res://Scenes/EntityScenes/Arrow.tscn");
			RigidBody3D Arrow = ArrowScene.Instantiate<RigidBody3D>();

			double rotationY = playerMesh.Rotation.Y;
				
			Arrow.Position = new Vector3(Position.X + (float)Math.Sin(rotationY)*2, Position.Y + 2, Position.Z + (float)Math.Cos(rotationY)*2);
			Arrow.LinearVelocity = new Vector3((float)(Math.Sin(rotationY)*10), 2, (float)(Math.Cos(rotationY)*10)) * _shootPower;
			Arrow.Rotation = new Vector3(Arrow.Rotation.X, playerMesh.Rotation.Y + (float)Math.PI / 2f, Arrow.Rotation.Z);
			GetTree().Root.AddChild(Arrow);
			
			_isShooting = false;
			_shootPower = 1;
		}
	}
}
