using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class Laser : Node3D
{
	private RayCast3D _laserRay;
	private MeshInstance3D _laserMesh;
	private StaticBody3D _rangeMax;
	private Timer Atk;
	private bool CanAtk = true;
	
	private Vector3 _startPoint;

	private float _laserSize = 0.8f;

	private int _laserId = -1;
	
	public override void _Ready()
	{
		_laserRay = GetNode<RayCast3D>("LaserRay");
		_laserMesh = GetNode<MeshInstance3D>("LaserRay/LaserMesh");
		_rangeMax = GetNode<StaticBody3D>("Range");
		Atk = GetNode<Timer>("Atk");
		
		_startPoint = GlobalPosition;
		_laserRay.Enabled = false;
		Visible = false;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if (_laserRay.IsColliding())
		{
			Vector3 stopPoint = _laserRay.GetCollisionPoint();
			Vector3 middle = new Vector3((_startPoint.X + stopPoint.X) / 2, (_startPoint.Y + stopPoint.Y) / 2, (_startPoint.Z + stopPoint.Z) / 2);
		
			_laserMesh.GlobalPosition = middle;
			_laserMesh.Scale = new Vector3(_laserSize, _startPoint.DistanceTo(middle), _laserSize);
			
			_rangeMax.Visible = false;
			if (_laserId == -1)
			{
				ScientistScript player = (ScientistScript)GameManager.Joueur1;
				if(CanAtk && _laserRay.GetCollider() is MobScript mob)
				{
					CanAtk = false;
					Atk.Start();
					
					foreach (var skill in GameManager.Skills)
					{
						if (skill.Item1 == "vampire") player.SetHealth(GameManager.Health + GameManager.Damage / 5);
					}
					
					if (new Random().Next(0, GameManager.CriticalChance) != 1)
					{
						mob.TakeDamage(GameManager.Damage);
					}
					else
					{
						mob.TakeDamage((int)(GameManager.Damage * 1.5));
					}
				}
				if(CanAtk && _laserRay.GetCollider() is OtherClassScript otherplayer)
				{
					CanAtk = false;
					Atk.Start();
					GameManager.InfoJoueur["attack"] = $"heal{otherplayer.Id}*{GameManager.HealSpeed}";
				}
				if(CanAtk && _laserRay.GetCollider() is Gravestone gravestone)
				{
					CanAtk = false;
					Atk.Start();
					GD.Print("Revive");
					GameManager.InfoJoueur["attack"] = $"revive{gravestone.ID}";
				}
				else
				{
					GD.Print($"getcollider {_laserRay.GetCollider()}");
				}
			}
		}
		else
		{
			_rangeMax.Visible = true;
		}

		if (_laserId != -1)
		{
			if (GameManager.InfoAutreJoueur[$"attack{_laserId}"] == "stop")
			{
				QueueFree();
			}
		}
		else
		{
			if (GameManager.InfoJoueur["attack"] == "stop")
			{
				QueueFree();
			}
		}
	}
	
	public void SetLaserID(int id)
	{
		_laserId = id;
	}
	
	public void TimeOut()
	{
		_laserRay.Enabled = true;
		Visible = true;
	}
	private void _on_atk_timeout()
	{
		CanAtk = true;
	}
}
