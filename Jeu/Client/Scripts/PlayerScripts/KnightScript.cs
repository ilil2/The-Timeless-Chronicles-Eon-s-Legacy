using Godot;
using System;

public partial class KnightScript : CharacterBody3D
{
	//Variables des noeuds
	private Node3D _h;
	private Camera3D _camera;
	private MeshInstance3D _playerMesh;

	//Variables des differentes forces
	private float _gravity = 9.8f;
	private float _jumpForce = 9.0f;
	private float _walkSpeed = 3.9f;
	private float _runSpeed = 7.5f;
	private float _dashPower = 80.0f;
	
	//Variables du Fov du joueur
	private float _fovMax = 120;
	private float _fovMin = 30;

	//Variables des mouvements
	private bool _isWalking;
	private bool _isRunning;
	private bool _isRolling;

	//Variables de direction
	private Vector3 _direction;
	private Vector3 _horizontalVelocity;
	private Vector3 _movement;
	private Vector3 _verticalVelocity;
	private float _movementSpeed;
	private float _angularAcceleration;
	private int _acceleration;
	
	public override void _Ready()
	{
		_camera = GetNode<Camera3D>("CameraPlayer/h/v/Camera3D");
		
		//initialisation de la variable direction
		_h = GetNode<Node3D>("CameraPlayer/h");
		_direction = Vector3.Back.Rotated(Vector3.Up, _h.GlobalTransform.Basis.GetEuler().Y);
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("scroll_forward"))
		{
			if (_camera.Fov >= _fovMin)
			{
				_camera.Fov -= 1;
			}
		}

		if (Input.IsActionPressed("scroll_backward"))
		{
			if (_camera.Fov <= _fovMax)
			{
				_camera.Fov  += 1;
			}
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		_h = GetNode<Node3D>("CameraPlayer/h");
		_playerMesh = GetNode<MeshInstance3D>("PlayerBody");
		
		_movementSpeed = 0f;
		_angularAcceleration = 10;
		_acceleration = 15;

		//Calcul de la gravité
		if (!IsOnFloor())
		{
			_verticalVelocity += Vector3.Down * _gravity * 2 * (float)delta;
		}
		else
		{
			_verticalVelocity = Vector3.Down * _gravity / 10 * (float)delta;
		}
		
		//Calcul du saut
		//if (Input.IsActionJustPressed("jump") && IsOnFloor() && !_isRolling)
		//{
		//	 _verticalVelocity = Vector3.Up * _jumpForce;
		//}
		
		//Mouvement du dash
		if (Input.IsActionPressed("dash"))
		{
			_horizontalVelocity = _direction * _dashPower;
		}

		//Mouvement du joueur
		if (Input.IsActionPressed("forward") || Input.IsActionPressed("backward") || Input.IsActionPressed("left") || Input.IsActionPressed("right"))
		{
			_direction = new Vector3(Input.GetActionStrength("left") - Input.GetActionStrength("right"), 0,
				Input.GetActionStrength("forward") - Input.GetActionStrength("backward"));
			_direction = _direction.Rotated(Vector3.Up, _h.GlobalTransform.Basis.GetEuler().Y).Normalized();
			_isWalking = true;

			//Changement de la vitesse du joueur si il sprint
			if (Input.IsActionPressed("sprint") && _isWalking)
			{ 
				_movementSpeed = _runSpeed;
				_isRunning = true;
			}
			else
			{
				_movementSpeed = _walkSpeed;
				_isRunning = false;
			}
		}
		else
		{
			_isWalking = false;
			_isRunning = false;
		}
		

		//Calcul de la rotation du joueur
		_playerMesh.Rotation = new Vector3(_playerMesh.Rotation.X, (float)Mathf.Lerp(_playerMesh.Rotation.Y, Mathf.Atan2(_direction.X, _direction.Z) - Rotation.Y, delta * _angularAcceleration), _playerMesh.Rotation.Z);
		
		
		if (_isRolling)
		{
			_horizontalVelocity = _horizontalVelocity.Lerp(_direction.Normalized() * .01f, (float)(_acceleration * delta));
		}
		else
		{
			_horizontalVelocity = _horizontalVelocity.Lerp(_direction.Normalized() * _movementSpeed, (float)(_acceleration * delta));
		}
		
		//Calcul du movement du joueur
		Vector3 velocity = Velocity;
		velocity.Z = _horizontalVelocity.Z + _verticalVelocity.Z;
		velocity.X = _horizontalVelocity.X + _verticalVelocity.X;
		velocity.Y = _verticalVelocity.Y;
		
		//Application du mouvement au joueur
		Velocity = velocity;
		MoveAndSlide();
	}
}
