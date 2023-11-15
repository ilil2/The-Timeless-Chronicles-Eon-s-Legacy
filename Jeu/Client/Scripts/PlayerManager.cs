using Godot;
using System;

public partial class PlayerManager : CharacterBody3D
{
	Node3D h;
	MeshInstance3D player_mesh;

	[Export]
	float _gravity = 9.8f;
	float _jump_force = 9.0f;
	float _walk_speed = 3.9f;
	float _run_speed = 7.5f;
	float _dash_power = 12.0f;

	private bool _is_walking;
	private bool _is_running;

	private Vector3 _direction;
	private Vector3 _horizontal_velocity;
	private Vector3 _movement;
	private Vector3 _vertical_velocity;
	private float _movement_speed;
	private float _angular_acceleration;
	private int _acceleration;
	
	public override void _Ready()
	{
		h = GetNode<Node3D>("CameraPlayer/h");
		_direction = Vector3.Back.Rotated(Vector3.Up, h.GlobalTransform.Basis.GetEuler().Y);
	}

	public override void _PhysicsProcess(double delta)
	{
		bool on_floor = IsOnFloor();
		h = GetNode<Node3D>("CameraPlayer/h");
		player_mesh = GetNode<MeshInstance3D>("PlayerBody");
		float h_rot = h.GlobalTransform.Basis.GetEuler().Y;

		_movement_speed = 0f;
		_angular_acceleration = 10;
		_acceleration = 15;

		if (!on_floor)
		{
			_vertical_velocity += Vector3.Down * _gravity * 2 * (float)delta;
		}
		else
		{
			_vertical_velocity = Vector3.Down * _gravity / 10;
		}
		
		if (!GameManager._pausemode)
		{
			if (Input.IsActionJustPressed("jump") && on_floor)
			{
				_vertical_velocity = Vector3.Up * _jump_force;
			}

			if (Input.IsActionPressed("forward") || Input.IsActionPressed("backward") || Input.IsActionPressed("left") || Input.IsActionPressed("right"))
			{
				_direction = new Vector3(Input.GetActionStrength("left") - Input.GetActionStrength("right"), 0,
					Input.GetActionStrength("forward") - Input.GetActionStrength("backward"));
				_direction = _direction.Rotated(Vector3.Up, h_rot).Normalized();
				_is_walking = true;

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
		}

		player_mesh.Rotation = new Vector3(player_mesh.Rotation.X, (float)Mathf.Lerp(player_mesh.Rotation.Y, Mathf.Atan2(_direction.X, _direction.Z) - Rotation.Y, delta * _angular_acceleration), player_mesh.Rotation.Z);

		_horizontal_velocity = _horizontal_velocity.Lerp(_direction.Normalized() * _movement_speed, (float)(_acceleration * delta));
		

		Vector3 velocity = Velocity;
		velocity.Z = _horizontal_velocity.Z + _vertical_velocity.Z;
		velocity.X = _horizontal_velocity.X + _vertical_velocity.X;
		velocity.Y = _vertical_velocity.Y;
		
		Velocity = velocity;
		MoveAndSlide();
	}
}
