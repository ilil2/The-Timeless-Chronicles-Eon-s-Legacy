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
	public IMap Map;
	public Random Rand;
	
	[Export] public bool Active = false;

	public int[] Agro = { -1, -1, -1, -1 };
	protected CharacterBody3D Player;
	public int Phase = 1;
	
	//Godot Node
	protected NavigationAgent3D Nav;
	protected AnimationPlayer Ani;
	public BossHealthBar _Hp;
	

	public void Ready()
	{
		_Hp = (BossHealthBar)GetNode<Control>("BossHealthBar");
		Nav = GetNode<NavigationAgent3D>("NavigationAgent3D");
		Ani = GetNode<AnimationPlayer>("Animation");
		Map = (IMap)GetParent();
		Rand = Map.Rand;
	}

	public void Process(double delta)
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
					//Attack
					if(Rand.Next(0,2)==1)
					{
						Ani.Play("Atk");
					}
					else
					{
						Ani.Play("Atk2");
					}
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
	}
	
	public virtual void AtDeath()
	{
		GD.Print("The Boss is Dead");
	}
	
	public void PhysicsProcess(double delta)
	{
		(_Hp as BossHealthBar).Process(delta);
		if(Active)
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
		
	}
	
	public virtual void TakeDamage(int damage, int id, bool send = true)
	{
		Agro[id] += 10;
		if(Alive)
		{
			HP -= damage;
			(_Hp as BossHealthBar).Value = HP;
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
				
			}

			if (send)
			{
				GameManager.InfoJoueur[$"ia"]  += $"{ID}°TK§{damage}°{Position.X}?{Position.Z}=";
			}
			
		}
	}

}
