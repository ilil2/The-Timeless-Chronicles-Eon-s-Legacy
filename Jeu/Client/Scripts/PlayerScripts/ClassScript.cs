using System;
using Godot;
using Lib;

namespace JeuClient.Scripts.PlayerScripts;

public partial class ClassScript : CharacterBody3D
{
	//Variable de base du joueur
    protected int _id;
    protected string _pseudo;
    protected string _classe;
    
    //Variable des objets
    protected Node3D _cameraH;
    protected SpringArm3D _cameraV;
    protected Camera3D _camera;
    protected MeshInstance3D _playerMesh;
    protected AnimationPlayer _animationPlayer;
    
    //Variable de camera
    private float _fovMax = 80;
    private float _fovMin = 40;
    
    //Variable de mouvement
    protected float _gravity = 9.8f;
    protected float _walkSpeed = 4.2f;
    protected float _runSpeed = 7.5f;
    protected float _dashPower = 80.0f;
    protected bool _canDash = true;
    protected int dashTimer;

    protected bool _isWalking;
    protected bool _isRunning;
    
    protected Vector3 _direction;
    protected Vector3 _horizontalVelocity;
    protected Vector3 _movement;
    protected Vector3 _verticalVelocity;
    protected float _movementSpeed;
    protected float _angularAcceleration = 10;
    protected int _acceleration = 15;
    
    private int pauseTimer;
    
    protected void InitPlayer()
	{
		//Initialisation des variables du joueur
		_id = Conversions.AtoI(GameManager.InfoJoueur["id"]);
		_pseudo = GameManager.InfoJoueur["pseudo"];
		_classe = GameManager.InfoJoueur["class"];
        
		_camera = GetNode<Camera3D>("CameraPlayer/h/v/Camera3D");
		_cameraV = GetNode<SpringArm3D>("CameraPlayer/h/v");
		_cameraH = GetNode<Node3D>("CameraPlayer/h");
		_direction = Vector3.Back.Rotated(Vector3.Up, _cameraH.GlobalTransform.Basis.GetEuler().Y);
		
		_playerMesh = GetNode<MeshInstance3D>("PlayerBody");
        
		Position = new Vector3(new Random().Next(-10, 10), 0, new Random().Next(-10, 10));
	}

    protected void Zoom(InputEvent @event)
    {
	    if (@event.AsText() == "Mouse Wheel Down")
	    {
		    if (_camera.Fov <= _fovMax)
		    {
			    _camera.Fov  += 2;
		    }
	    }
	    else if (@event.AsText() == "Mouse Wheel Up")
	    {
		    if (_camera.Fov >= _fovMin)
		    {
			    _camera.Fov -= 2;
		    }
	    }
    }
    
    protected void SendPosition()
	{
		//Envoie de la position du joueur au serveur
	    GameManager.InfoJoueur["co"] = $"{Position.X};{Position.Y};{Position.Z}";
	}

    protected void Pause()
    {
	    if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[15].Item2) && !GameManager._pausemode && pauseTimer > 20)
	    {
		    pauseTimer = 0;
		    GameManager._pausemode = true;
		    Input.MouseMode = Input.MouseModeEnum.Visible;
		    PackedScene pauseUI = GD.Load<PackedScene>("res://Scenes/UI/PauseMenuManager.tscn");
		    Control pauseMenu = pauseUI.Instantiate<Control>();
		    AddChild(pauseMenu);
	    }
	    else if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[15].Item2) && GameManager._pausemode && pauseTimer > 20)
	    {
		    pauseTimer = 0;
		    GameManager._pausemode = false;
	    }
	    
	    pauseTimer += 1;
    }

    protected void PhysicsReset()
    {
	    //Reset du mouvement du joueur
	    _movementSpeed = 0f;
    }
    
    protected void Gravity(double delta)
	{
	    if (!IsOnFloor())
	    {
		    _verticalVelocity += Vector3.Down * _gravity * 2 * (float)delta;
	    }
	    else
	    {
		    _verticalVelocity = Vector3.Down * _gravity / 10 * (float)delta;
	    }
	}

    protected void Dash()
    {
	    if (_canDash)
	    {
		    if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[5].Item2))
		    {
			    _horizontalVelocity = _direction * _dashPower;
			    _canDash = false;
		    }
	    }
	    else
	    {
		    dashTimer += 1;
		    if (dashTimer % 20 == 0)
		    {
			    _canDash = true;
			    dashTimer = 0;
		    }
	    }
    }

    protected void Move(double delta)
    {
	    if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[0].Item2) || Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[1].Item2) || Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[2].Item2) ||
	        Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[3].Item2))
	    {
		    _direction = new Vector3(Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[2].Item2)) - Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[3].Item2)), 0,
			    Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[0].Item2)) - Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[1].Item2)));
		    _direction = _direction.Rotated(Vector3.Up, _cameraH.GlobalTransform.Basis.GetEuler().Y).Normalized();
		    _isWalking = true;

		    //Changement de la vitesse du joueur si il sprint
		    if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[4].Item2) && _isWalking)
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
	    //_playerMesh.LookAt(_camera.GlobalPosition, Vector3.Up);
	    //_playerMesh.Rotation = new Vector3(0, _playerMesh.Rotation.Y + (float)Math.PI, 0);
		
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