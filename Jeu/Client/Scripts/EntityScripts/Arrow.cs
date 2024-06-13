using Godot;
using System;
using JeuClient.Scripts.EntityScripts.Mob;
using JeuClient.Scripts.PlayerScripts;

public partial class Arrow : RigidBody3D
{
	private Vector3 _hitPoint;
	private Vector3 _hitRotation;
	
	private float _changeRotationValue;
	private bool _isHit;
	public bool IsPlayer = false;
	public float ShootPower;
	
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
		else if (body is not ClassScript && body is not Arrow && body is not PlayerWeapon)
		{
			if (body is MobScript mob && IsPlayer)
			{
				if (new Random().Next(0, GameManager.CriticalChance) != 1)
				{
					mob.TakeDamage(GameManager.Damage);
				}
				else
				{
					mob.TakeDamage((int)(GameManager.Damage * 1.5));
				}
				
				foreach (var skill in GameManager.Skills)
				{
					if (skill.Item1 == "arrowpoison")
					{
						mob.PoisonMob();
					}
					else if (skill.Item1 == "arrowgel")
					{
						mob.GelMob();
					}
				}
				
				QueueFree();
			}
			else if (body is Boss boss && IsPlayer)
			{
				boss.TakeDamage(GameManager.Damage, Lib.Conversions.AtoI(GameManager.InfoJoueur["id"]));
			}
			else
			{
				QueueFree();
			}
		}
	}
}
