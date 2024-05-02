using Godot;
using System;
using System.Collections.Generic;
using Lib;

public partial class Ghost : CharacterBody3D
{
	public Camera3D Camera;
	private Node3D _cameraH;
	private Node3D _playerMesh;
	
	private Vector3 _direction;
	private Vector3 _horizontalVelocity;
	private Vector3 _movement;
	private Vector3 _verticalVelocity;
	
	private float _walkSpeed = 4.2f;
	private int _acceleration = 15;
	private float _fovMax = 80;
	private float _fovMin = 40;
	private int _uiTimer;
	private float _movementSpeed;
	
	public override void _Ready()
	{
		Camera = GetNode<Camera3D>("CameraPlayer/h/v/Camera3D");
		_cameraH = GetNode<Node3D>("CameraPlayer/h");
		_direction = Vector3.Back.Rotated(Vector3.Up, _cameraH.GlobalTransform.Basis.GetEuler().Y);
		
		_playerMesh = GetNode<Node3D>("Ghost");
	}
	
	public override void _Input(InputEvent @event)
	{
		if (Camera.Current && !GameManager._pausemode)
		{
			Zoom(@event);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		_uiTimer += 1;
		_movementSpeed = 0f;
		Pause();
		if (Camera.Current && !GameManager._pausemode && !((ChatUI)GameManager._chat).IsOnChat())
		{
			Move(delta);
		}
		else
		{
			Velocity = new Vector3(0, 0, 0);
		}
	}
	
	protected void Zoom(InputEvent @event)
	{
		if (@event.AsText() == "Mouse Wheel Down")
		{
			if (Camera.Fov <= _fovMax)
			{
				Camera.Fov  += 2;
			}
		}
		else if (@event.AsText() == "Mouse Wheel Up")
		{
			if (Camera.Fov >= _fovMin)
			{
				Camera.Fov -= 2;
			}
		}
	}
	
	protected void Pause()
	{
		if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[16].Item2) && !GameManager._pausemode && _uiTimer > 20)
		{
			_uiTimer = 0;
			GameManager._pausemode = true;
			Input.MouseMode = Input.MouseModeEnum.Visible;
			PackedScene pauseUI = GD.Load<PackedScene>("res://Scenes/UI/PauseMenuManager.tscn");
			Control pauseMenu = pauseUI.Instantiate<Control>();
			AddChild(pauseMenu);
		}
		else if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[16].Item2) && GameManager._pausemode && _uiTimer > 20)
		{
			_uiTimer = 0;
			GameManager._pausemode = false;
		}
	}
	
	private void Move(double delta)
	{
		List<(string, Key)> controls = GameManager.InputManger.GetAllControl();
		if (Input.IsKeyPressed(controls[0].Item2) || Input.IsKeyPressed(controls[1].Item2) || Input.IsKeyPressed(controls[2].Item2) ||
			Input.IsKeyPressed(controls[3].Item2))
		{
			int left = Conversions.BtoI(Input.IsKeyPressed(controls[2].Item2));
			int right = Conversions.BtoI(Input.IsKeyPressed(controls[3].Item2)); 
			int forward = Conversions.BtoI(Input.IsKeyPressed(controls[0].Item2));
			int backward = Conversions.BtoI(Input.IsKeyPressed(controls[1].Item2));
					
			_direction = new Vector3(left - right, 0, forward - backward);
			_direction = _direction.Rotated(Vector3.Up, _cameraH.Rotation.Y).Normalized();
			_movementSpeed = _walkSpeed;
		}

		_playerMesh.Rotation = new Vector3(0, _cameraH.Rotation.Y + (float) Math.PI, 0);
		
		_horizontalVelocity = _horizontalVelocity.Lerp(_direction.Normalized() * _movementSpeed, (float)(_acceleration * delta));
		
		Vector3 velocity = Velocity;
		velocity.Z = _horizontalVelocity.Z + _verticalVelocity.Z;
		velocity.X = _horizontalVelocity.X + _verticalVelocity.X;
		velocity.Y = _verticalVelocity.Y;
		
		Velocity = velocity;
		MoveAndSlide();
	}
}
