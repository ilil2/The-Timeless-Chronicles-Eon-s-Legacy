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
	[Export] private int damage = 10;
	
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
				if(CanAtk && _laserRay.GetCollider() is MobScript mob)
				{
					CanAtk = false;
					Atk.Start();
					mob.TakeDamage(damage);
				
				}
				if(CanAtk && _laserRay.GetCollider() is OtherClassScript)
				{
					CanAtk = false;
					Atk.Start();
					GameManager.InfoJoueur["attack"] = "heal";
				}
				if(CanAtk && _laserRay.GetCollider() is Gravestone gravestone)
				{
					CanAtk = false;
					int id = int.Parse(gravestone.Name);
					Atk.Start();
					GameManager.InfoJoueur["attack"] = "revive";
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
		Visible = true;
	}
	private void _on_atk_timeout()
	{
		CanAtk = true;
	}
}
