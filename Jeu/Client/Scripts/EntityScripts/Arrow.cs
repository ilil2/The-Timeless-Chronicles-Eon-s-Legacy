using Godot;
using System;

public partial class Arrow : RigidBody3D
{
	private Vector3 _hitPoint;
	private Vector3 _hitRotation;
	
	private float _changeRotationValue;
	private bool _isHit;
	public override void _PhysicsProcess(double delta)
	{
		if (LinearVelocity.Y < 0 && _changeRotationValue < 1f)
		{
			Rotation = new Vector3(Rotation.X, Rotation.Y, Rotation.Z + 0.03f);
			_changeRotationValue += 0.03f;
		}

		if (_isHit)
		{
			GlobalPosition = _hitPoint;
			Rotation = _hitRotation;
		}
	}

	public void Dispawn()
	{
		QueueFree();
	}
	
	public void OnCollision(Node3D body)
	{
		if (body is StaticBody3D)
		{
			if (!_isHit)
			{
				_isHit = true;
				_hitPoint = GlobalPosition;
				_hitRotation = Rotation;
				LinearVelocity = new Vector3(0, 0, 0);
			}
		}
		else if (body is not ArcherScript && body is not ScientistScript && body is not KnightScript && body is not AssassinScript && body is not Arrow)
		{
			QueueFree();
		}
	}
}
