using System;
using Godot;
using JeuClient.Scripts.PlayerScripts;
using Lib;

namespace JeuClient.Scripts.EntityScripts.Mob;

public abstract partial class Boss : CharacterBody3D
{
	public int ID;
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
		Rand = Map.Rand2;
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
					Attack();
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
	public virtual void Attack()
	{
		if(Rand.Next(0,2)==1)
			Ani.Play("Atk");
		else
			Ani.Play("Atk2");
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
			receive();
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
			SendInfo();
		}
	}

	protected void SendInfo()
	{
		if (Alive && GameManager.InfoJoueur["boss"] == "" && (Player is ClassScript))
		{
			GameManager.InfoJoueur["boss"] = $"{ID}°{State}°{Position.X}?{Position.Z}°{(GameManager.Joueur1 as ClassScript).Id}=";
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
				if (send) GameManager.InfoJoueur[$"boss"] += $"{ID}°{42}°{Position.X}?{Position.Z}°{(GameManager.Joueur1 as ClassScript).Id}=";
			}

			if (send && Alive)
			{
				GameManager.InfoJoueur[$"boss"] += $"{ID}°TK§{damage}°{Position.X}?{Position.Z}=";
				GD.Print(GameManager.InfoJoueur["boss"]);
			}
			
		}
	}
	
	public void receive()
	{
		string rec = "";
		for(int i = 0;i<GameManager._nbJoueur;i++)
		{
			if (GameManager.InfoJoueur["id"]!=i.ToString() && GameManager.InfoAutreJoueur[$"boss{i}"]!="")
			{
				rec = GameManager.InfoAutreJoueur[$"boss{i}"]; 
				GameManager.InfoAutreJoueur[$"boss{i}"] = "";
			}
		}
		string[] ia = rec.Split("=");
		foreach (var a in ia)
		{
			if (!string.IsNullOrEmpty(a))
			{
				GD.Print("Received: " + a);
				string[] firstline = a.Split("°");
				int id = Lib.Conversions.AtoI(firstline[0]);

				if (id == ID)
				{
					if (firstline[1].Contains("§"))
					{
						string[] secondline = firstline[1].Split("§");
						if (secondline[0] == "TK")
						{
							int damage = int.Parse(secondline[1]);
							TakeDamage(damage, Conversions.AtoI(secondline[1]),false);
							GD.Print("TK: " + damage);
						}
					}
					else
					{
						//GD.Print(firstline[1]);	
						if (firstline[1]=="0")
						{
							State = 0;
						}
						else if (firstline[1]=="1")
						{
							State = 1;
						}

						else if (firstline[1] == "42")
						{
							TakeDamage(10000, 0,false);
							//GD.Print("Rec Mort");
						}
						
					}
				}
				string[] pos = firstline[2].Split("?");
				Vector3 NPosition = new Vector3(float.Parse(pos[0]), Position.Y, float.Parse(pos[1]));
				if(MapTool.Distance(Position,NPosition)>0.1)
				{
					Position = NPosition;
				}
			}
		}
	}
}
