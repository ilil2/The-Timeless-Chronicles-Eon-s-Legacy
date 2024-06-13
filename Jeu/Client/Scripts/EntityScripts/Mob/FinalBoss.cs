using Godot;
using System;
using JeuClient.Scripts.EntityScripts.Mob;
using JeuClient.Scripts.PlayerScripts;

public partial class Boss1 : Boss
{
	private PackedScene[] Mob = {GD.Load<PackedScene>("res://Scenes/EntityScenes/Mob/Mummy.tscn"),GD.Load<PackedScene>("res://Scenes/EntityScenes/Mob/Pharaon.tscn"),GD.Load<PackedScene>("res://Scenes/EntityScenes/Mob/FogSkeleton.tscn")};
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
					Attack()
				}
			}
		}
	}

	public override void Attack()
	{
		x = Rand.Next(0,4);
		if (x == 1)
			Ani.Play("AttackDown");
		if (x == 2)
			Ani.Play("AttackUp2");
		if (x == 3)
			Ani.Play("AttackWingDouble");
		else
			Ani.Play("AttackWingsDown");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		PhysicsProcess(delta);
	}

	public override void TakeDamage(int damage, int id, bool send = true)
	{
		Agro[id] += 10;
		if(Alive)
		{
			HP -= damage;
			(_Hp as BossHealthBar).Value = HP;
			GD.Print("HP" + HP);
			if(HP<=0)
			{
				GD.Print("Mort");
				Alive = false;
				Ani.Play("Death");
				GameManager.Gold += 10;
				GameManager.xp += 1;
				GameManager.InfoJoueur[$"ia"] += $"{ID}°{42}°{Position.X}?{Position.Z}°{(GameManager.Joueur1 as ClassScript).Id}=";
			}
			else
			{
				//Ani.Play("Hit");
			}
			if (send)
			{
				GameManager.InfoJoueur[$"ia"]  += $"{ID}°TK§{damage}°{Position.X}?{Position.Z}=";
			}
			
		}
	}
	
	public override void AtDeath()
	{
		Map.CanExit = true;
	}
}
