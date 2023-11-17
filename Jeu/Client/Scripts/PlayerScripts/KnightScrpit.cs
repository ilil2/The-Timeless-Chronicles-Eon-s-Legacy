using Godot;
using System;

public partial class KnightScrpit : CharacterBody3D
{
	//Variables des noeuds
	private Node3D h;
	private Camera3D Camera;
	private MeshInstance3D player_mesh;
	private Timer dash_timer;

	//Variables des differentes forces
	private float _gravity = 9.8f;
	private float _jump_force = 9.0f;
	private float _walk_speed = 3.9f;
	private float _run_speed = 7.5f;
	private float _dash_power = 80.0f;
	
	//Variables du Fov du joueur
	private float _FovMax = 120;
	private float _FovMin = 30;

	//Variables des mouvements
	private bool _is_walking;
	private bool _is_running;
	private bool _is_rolling;

	//Variables de direction
	private Vector3 _direction;
	private Vector3 _horizontal_velocity;
	private Vector3 _movement;
	private Vector3 _vertical_velocity;
	private float _movement_speed;
	private float _angular_acceleration;
	private int _acceleration;
	
	public override void _Ready()
	{
		Camera = GetNode<Camera3D>("CameraPlayer/h/v/Camera3D");
		
		//initialisation de la variable direction
		h = GetNode<Node3D>("CameraPlayer/h");
		_direction = Vector3.Back.Rotated(Vector3.Up, h.GlobalTransform.Basis.GetEuler().Y);
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("scroll_forward"))
		{
			if (Camera.Fov >= _FovMin)
			{
				Camera.Fov -= 1;
			}
		}

		if (Input.IsActionPressed("scroll_backward"))
		{
			if (Camera.Fov <= _FovMax)
			{
				Camera.Fov  += 1;
			}
		}
	}

	private void sprint_and_roll(Timer DashTimer)
	{
		//Mouvement du dash
		if (Input.IsActionPressed("dash") && !_is_rolling)
		{
			DashTimer.Start();
			_is_rolling = true;
		}

		if (!DashTimer.IsStopped())
		{
			_horizontal_velocity = _direction * _dash_power;
		}
		
		if (DashTimer.IsStopped() && _is_rolling)
		{
			_is_rolling = false;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		h = GetNode<Node3D>("CameraPlayer/h");
		player_mesh = GetNode<MeshInstance3D>("PlayerBody");
		dash_timer = GetNode<Timer>("DashTimer");
		
		sprint_and_roll(dash_timer);
		
		bool on_floor = IsOnFloor();
		float h_rot = h.GlobalTransform.Basis.GetEuler().Y;
		
		_movement_speed = 0f;
		_angular_acceleration = 10;
		_acceleration = 15;

		//Calcul de la gravité
		if (!on_floor)
		{
			_vertical_velocity += Vector3.Down * _gravity * 2 * (float)delta;
		}
		else
		{
			_vertical_velocity = Vector3.Down * _gravity / 10;
		}
		
		//Calcul du saut
		if (Input.IsActionJustPressed("jump") && on_floor && !_is_rolling)
		{
			 _vertical_velocity = Vector3.Up * _jump_force;
		}

		//Mouvement du joueur
		if (Input.IsActionPressed("forward") || Input.IsActionPressed("backward") || Input.IsActionPressed("left") || Input.IsActionPressed("right"))
		{
			_direction = new Vector3(Input.GetActionStrength("left") - Input.GetActionStrength("right"), 0,
				Input.GetActionStrength("forward") - Input.GetActionStrength("backward"));
			_direction = _direction.Rotated(Vector3.Up, h_rot).Normalized();
			_is_walking = true;

			//Changement de la vitesse du joueur si il sprint
			if (Input.IsActionPressed("sprint") && _is_walking)
			{ 
				_movement_speed = _run_speed;
				_is_running = true;
			}
			else
			{
				_movement_speed = _walk_speed;
				_is_running = false;
			}
		}
		else
		{
			_is_walking = false;
			_is_running = false;
		}
		

		//Calcul de la rotation du joueur
		player_mesh.Rotation = new Vector3(player_mesh.Rotation.X, (float)Mathf.Lerp(player_mesh.Rotation.Y, Mathf.Atan2(_direction.X, _direction.Z) - Rotation.Y, delta * _angular_acceleration), player_mesh.Rotation.Z);
		
		
		if (_is_rolling)
		{
			_horizontal_velocity = _horizontal_velocity.Lerp(_direction.Normalized() * .01f, (float)(_acceleration * delta));
		}
		else
		{
			_horizontal_velocity = _horizontal_velocity.Lerp(_direction.Normalized() * _movement_speed, (float)(_acceleration * delta));
		}
		
		//Calcul du movement du joueur
		_horizontal_velocity = _horizontal_velocity.Lerp(_direction.Normalized() * _movement_speed, (float)(_acceleration * delta));
		Vector3 velocity = Velocity;
		velocity.Z = _horizontal_velocity.Z + _vertical_velocity.Z;
		velocity.X = _horizontal_velocity.X + _vertical_velocity.X;
		velocity.Y = _vertical_velocity.Y;
		
		//Application du mouvement au joueur
		Velocity = velocity;
		MoveAndSlide();
	}
}
