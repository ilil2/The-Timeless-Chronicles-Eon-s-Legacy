using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class KnightScript : CharacterBody3D
{
	private ClassScript _characterClass;
	
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

	//Variables des mouvements
	private bool _isWalking;
	private bool _isRunning;

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
		_characterClass = new ClassScript(Lib.Conversions.AtoI(GameManager.InfoJoueur["id"]), GameManager.InfoJoueur["pseudo"], GameManager.InfoJoueur["class"]);
		_camera = GetNode<Camera3D>("CameraPlayer/h/v/Camera3D");
		
		//initialisation de la variable direction
		_h = GetNode<Node3D>("CameraPlayer/h");
		_direction = Vector3.Back.Rotated(Vector3.Up, _h.GlobalTransform.Basis.GetEuler().Y);
		
		Position = new Vector3(new Random().Next(-10, 10), 0, new Random().Next(-10, 10));
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.AsText() == "Mouse Wheel Down")
		{
			_characterClass.Zoom(_camera);
		}
		else if (@event.AsText() == "Mouse Wheel Up")
		{
			_characterClass.DeZoom(_camera);
		}
	}

	public override void _Process(double delta)
	{
		GameManager.InfoJoueur["co"] = $"{Position.X};{Position.Y};{Position.Z}";
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
		
		//Mouvement du dash
		if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()["dash"]))
		{
			_horizontalVelocity = _characterClass.Dash(_direction, _dashPower);
		}

		//Mouvement du joueur
		if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()["forward"]) || Input.IsKeyPressed(GameManager.InputManger.GetAllControl()["backward"]) || Input.IsKeyPressed(GameManager.InputManger.GetAllControl()["left"]) ||
		    Input.IsKeyPressed(GameManager.InputManger.GetAllControl()["right"]))
		{
			_direction = new Vector3(Input.GetActionStrength("left") - Input.GetActionStrength("right"), 0,
				Input.GetActionStrength("forward") - Input.GetActionStrength("backward"));
			_direction = _direction.Rotated(Vector3.Up, _h.GlobalTransform.Basis.GetEuler().Y).Normalized();
			_isWalking = true;

			//Changement de la vitesse du joueur si il sprint
			if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()["sprint"]) && _isWalking)
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
		//_playerMesh.Rotation = new Vector3(_playerMesh.Rotation.X, (float)Mathf.Lerp(_playerMesh.Rotation.Y, Mathf.Atan2(_direction.X, _direction.Z) - Rotation.Y, delta * _angularAcceleration), _playerMesh.Rotation.Z);
		_playerMesh.LookAt(_camera.GlobalPosition, Vector3.Up);
		_playerMesh.Rotation = new Vector3(0, _playerMesh.Rotation.Y + (float)Math.PI, 0);
		
		_horizontalVelocity = _horizontalVelocity.Lerp(_direction.Normalized() * _movementSpeed, (float)(_acceleration * delta));
		
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
