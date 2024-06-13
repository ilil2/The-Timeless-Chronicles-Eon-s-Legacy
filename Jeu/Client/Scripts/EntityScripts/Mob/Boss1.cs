using Godot;
using System;
using JeuClient.Scripts.EntityScripts.Mob;
using JeuClient.Scripts.PlayerScripts;

public partial class Boss1 : Boss
{
	private PackedScene[] Mob = {GD.Load<PackedScene>("res://Scenes/EntityScenes/Mob/Mummy.tscn"),GD.Load<PackedScene>("res://Scenes/EntityScenes/Mob/Pharaon.tscn"),GD.Load<PackedScene>("res://Scenes/EntityScenes/Mob/FogSkeleton.tscn")};
	[Export] private bool CastSpell;
	[Export] private bool SummonMob;
	private Random Rand;
	private Node3D Spawn;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Map = (IMap)GetParent();
		Rand = Map.Rand;
		Spawn = GetNode<Node3D>("Spawn");
		HP = 300;
		MaxHP = 300;
		Ready();
		DistAtk = 7;
		Ani.Play("Idle");
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		(_Hp as Control).Visible = Map.ShowHud; 
		if(Active)
		{
			if(Alive)
			{
				var NextPos = Nav.GetNextPathPosition();
				LookAt(new Vector3(NextPos.X, 1, NextPos.Z)); //Orientation
				Rotation = new Vector3(0,Rotation.Y+(float)Math.PI,0);
				if (State == 0 &&  (Ani.CurrentAnimation != "Atk" && Ani.CurrentAnimation != "Atk2"))
				{
					var dir = new Vector3();  //Pathfiding
					dir = NextPos - GlobalPosition;
					dir = dir.Normalized();
					Velocity = Velocity.Lerp(dir*speed,(float)(accel*delta));
					MoveAndSlide();
					Ani.Play("Walk");
				}
				else if (State == 1 && (Ani.CurrentAnimation != "Atk" && Ani.CurrentAnimation != "Atk2"))
				{
					Attack();
				}
			}
			SendInfo();
		}
	}

	public override void Attack()
	{
		if(Rand.Next(0,4)!=1)
			Ani.Play("Atk");
		else
			Ani.Play("Atk2");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		PhysicsProcess(delta);
		if(CastSpell)
		{
			(Map as Boss1Map).MapAtk();
			CastSpell = false;
		}
		if(SummonMob)
		{
			SummonMob = false;
			MobScript SMob = Mob[Rand.Next(0,3)].Instantiate<MobScript>();
			SMob.Position = Spawn.GlobalPosition;
			Map.AddChild(SMob);
			
		}
	}
	
	public override void AtDeath()
	{
		Map.CanExit = true;
	}
}
