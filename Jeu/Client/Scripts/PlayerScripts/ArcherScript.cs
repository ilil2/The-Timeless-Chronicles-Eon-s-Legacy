using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class ArcherScript : ClassScript
{
	//Varibale du tir
	private int _shootTimer;
	private float _shootPower = 0.25f;
	private bool _isAiming;
	private bool _shootAnimation;
	
	public static bool IsShooting;
	
	public override void _Ready()
	{
		InitPlayer();

		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
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

		if (_camera.Current && !GameManager._pausemode && !((ChatUI)GameManager._chat).IsOnChat())
		{
			Move(delta);
			ShootArrow();
		}
		else
		{
			_shootTimer = 0;
		}
	}
	
	private void ShootArrow()
	{
		_shootTimer += 1;
		
		if (Input.IsMouseButtonPressed(MouseButton.Left) && _shootTimer > 30 && _shootPower <= 3)
		{
			if (!_shootAnimation)
			{
				_shootAnimation = true;
				_animationPlayer.Play("ArrowShootView");
			}
			
			IsShooting = true;
			_isAiming = true;
			_shootPower += 0.015f;
			
			PackedScene crossHairScene = GD.Load<PackedScene>("res://Scenes/HUD/ViewFinder.tscn");
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

			double rotationY = _cameraH.Rotation.Y;
			
			arrow.Position = new Vector3(_cameraV.GlobalPosition.X + (float)Math.Sin(rotationY)*2, Position.Y + 1, Position.Z + (float)Math.Cos(rotationY)*2);
			arrow.Rotation = new Vector3(arrow.Rotation.X, (float)(rotationY + Math.PI / 2f), _cameraV.Rotation.X);
			arrow.LinearVelocity = new Vector3((float)(Math.Sin(rotationY)*10), -Mathf.RadToDeg(_cameraV.Rotation.X) / 5, (float)(Math.Cos(rotationY)*10)) * _shootPower;
			GetTree().Root.AddChild(arrow);
			
			IsShooting = false;
			_shootPower = 1;

			_animationPlayer.Play("ArrowShootViewReset");
			_shootAnimation = false;
		}
	}
}
