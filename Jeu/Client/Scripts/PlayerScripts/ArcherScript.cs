using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class ArcherScript : ClassScript
{
	//Varibale du tir
	private int _shootTimer;
	private bool _isShooting;
	
	public override void _Ready()
	{
		InitPlayer();
	}

	public override void _Input(InputEvent @event)
	{
		Zoom(@event);
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

		if (_camera.Current)
		{
			Dash();
			Move(delta);
			ShootTimer();
			ShootArrow(_playerMesh);
		}
	}
	
	private void ShootTimer()
	{
		_shootTimer += 1;
		if (_shootTimer % 60 == 0)
		{
			_isShooting = false;
		}
	}
	
	private void ShootArrow(MeshInstance3D playerMesh)
	{
		if (Input.IsKeyPressed(Key.L) && !_isShooting)
		{
			_isShooting = true;
			
			PackedScene ArrowScene = GD.Load<PackedScene>("res://Scenes/EntityScenes/Arrow.tscn");
			RigidBody3D Arrow = ArrowScene.Instantiate<RigidBody3D>();

			double rotationY = playerMesh.Rotation.Y;
				
			Arrow.Position = new Vector3(Position.X + (float)Math.Sin(rotationY)*2, Position.Y + 2, Position.Z + (float)Math.Cos(rotationY)*2);
			Arrow.LinearVelocity = new Vector3((float)(Math.Sin(rotationY)*10), 2, (float)(Math.Cos(rotationY)*10)) * 2;
			Arrow.Rotation = new Vector3(Arrow.Rotation.X, playerMesh.Rotation.Y + (float)Math.PI / 2f, Arrow.Rotation.Z);
			GetTree().Root.AddChild(Arrow);
		}
	}
}
