using System;
using Godot;
using JeuClient.Scripts.PlayerScripts;
using Lib;

namespace JeuClient.Scripts.EntityScripts.Mob;

public abstract partial class Boss : CharacterBody3D
{
	public int ID = 1;
	public int HP = 100;
	public int MaxHP = 100;
	public int State = 0;
	protected int speed = 2;
	protected int accel = 10;
	public int DistAtk = 1;
	public bool Alive = true;

	public int[] Agro = { -1, -1, -1, -1 };
	protected CharacterBody3D Player;
	public int Phase = 1;
	
	//Godot Node
	protected NavigationAgent3D Nav;
	protected AnimationPlayer Ani;
	

	public void Ready()
	{
		Nav = GetNode<NavigationAgent3D>("NavigationAgent3D");
		Ani = GetNode<AnimationPlayer>("Animation");
	}

	public void Process(double delta)
	{
		if(Alive)
		{
			var NextPos = Nav.GetNextPathPosition();
			LookAt(new Vector3(NextPos.X, 1, NextPos.Z)); //Orientation
			Rotation = new Vector3(0,Rotation.Y+(float)Math.PI,0);
			if (State == 0)
			{
				var dir = new Vector3();  //Pathfiding
				dir = NextPos - GlobalPosition;
				dir = dir.Normalized();
				Velocity = Velocity.Lerp(dir*speed,(float)(accel*delta));
				MoveAndSlide();
				Ani.Play("Walk");
			}
			else if (State == 1)
			{
				//Attack
				Ani.Play("Atk");
			}
		}
		else
		{
			//Death();
			if(!Ani.IsPlaying())
			{
				AtDeath();
				QueueFree();
			}
		}
	}
	
	public virtual void AtDeath()
	{
		GD.Print("The Boss is Dead");
	}
	
	public void PhysicsProcess(double delta)
	{
		Player = GetPlayer();
		if(Player is not null && Alive)
		{
			Nav.TargetPosition = Player.GlobalPosition;
			UpdateAgro();
			if (State == 0 && MapTool.Distance(GlobalPosition,Player.GlobalPosition)<DistAtk)
			{
				State = 1;
			}
			else if (State == 1 && MapTool.Distance(GlobalPosition,Player.GlobalPosition)>DistAtk)
			{
				State = 0;
			}
		}
	}

	private CharacterBody3D GetPlayer()
	{
		double MaxAgro = 0;
		CharacterBody3D BestPlayer = GameManager.Joueur1;
		foreach (var characterBody3D in GameManager.ListJoueur)
		{
			if(characterBody3D!=null)
			{
				var player = (PlayerScript)characterBody3D;
				int PersonalAgro = (int)(Agro[player.Id]-MapTool.Distance(GlobalPosition,player.GlobalPosition));
				if(PersonalAgro>MaxAgro)
				{
					MaxAgro = PersonalAgro;
					BestPlayer = player;
				}
			}
		}

		return BestPlayer;
	}

	private void UpdateAgro()
	{
		for (int i = 0; i < 4; i++)
		{
			if(Agro[i]>-1) Agro[i]--;
		}
		
		//Print Agro
		string res = "";
		foreach (var a in Agro)
		{
			res+=" "+a;
		}
		GD.Print(res);
	}
	
	public virtual void TakeDamage(int damage, int id, bool send = true)
	{
		Agro[id] += 10;
		if(Alive)
		{
			HP -= damage;
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
				Ani.Play("Hit");
			}

			if (send)
			{
				GameManager.InfoJoueur[$"ia"]  += $"{ID}°TK§{damage}°{Position.X}?{Position.Z}=";
			}
			
		}
	}

}