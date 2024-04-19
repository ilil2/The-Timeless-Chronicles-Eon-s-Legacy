using Godot;
using System;
using System.Collections.Generic;
using JeuClient.Scripts.PlayerScripts;
//Ceci est un commentaire
public abstract partial class MobScript : CharacterBody3D
{
	//stats
	public int ID = -1;
	protected int speed = 2; // vitesse
	protected int accel = 10; // acceleration
	protected int DistVue = 30; // Distance de vue
	protected int DistAtk = 1; // Distance d'attaque
	public int HP = 100; // health point
	public int HpMax = 100;
	protected int AgroMax = 100; // Valeur Maximum de l'Agro
	public int Agro = 0; // stop quand agro = 0
	public bool Alive = true;
	private float TimerDeath = -0.1f;
	protected AnimationPlayer Ani;
	public bool CanChange = false;
	
	//debug
	public bool DebugMode = true; // variable debug mode
	
	// Pour le pathfiding... ?
	private NavigationAgent3D Nav; // Cible du pathfiding
	public int state; // -1 = repos | 0 = retour à position initiale | 1 = suivre joueur | 2 = attaque | 3 = suivre sans vue
	private Vector3 PosInnit; // position initiale
	private Vector3 RotInnit; // rotation innitiale
	private bool SkipFrame = true; // RayCast3D ne fonctionne pas à la première frame
	private bool IsTooFar = false; // Indique si le mob est trop loin du joueur
	
	// Autre
	private Node Parent;
	private CharacterBody3D Player = null;
	private CharacterBody3D LastPlayer = null;
	private Vector3 Me = new Vector3();
	private Vector3 PlayerPos = new Vector3();
	private bool PlayerSet = false;
	private float StateDeath = -0.1f;
	private List<RayCast3D> RayList = new List<RayCast3D>();
	
	public void Ready()
	{
		Parent = GetParent().GetParent();
		for(int i = 0; i<GameManager._nbJoueur; i++)
		{
			RayCast3D R = new RayCast3D();
			R.Position = new Vector3(0,1,0);
			AddChild(R);
			RayList.Add(R);
		}
		PosInnit = Position;
		RotInnit = Rotation;
		Nav = GetNode<NavigationAgent3D>("NavigationAgent3D");
		Nav.TargetPosition = PosInnit;
		
		Ani = GetNode<AnimationPlayer>("Animation");
		state = -1;
		
	}

	public void PhysicsProcess(double delta) //Raycast
	{
		
		if(Alive)
		{
			if(state==1 || state==2 || state == 3)
			{
				receive();
			}
			if(Player!=null)
			{
				LastPlayer = Player;
			}
			Player = GetPlayer(SetRay());
			if(Player == null && state!=0)
			{
				if(Agro<=0)
				{
					state = 0;
					Nav.TargetPosition = PosInnit;
				}
				else
				{
					Agro-=1;
					Nav.TargetPosition = LastPlayer.GlobalPosition;
					state = 3;
				}
			}
			if(state==0)
			{
				if(Distance(Position,PosInnit)<=0.1)
				{
					state = -1;
				}
			}
			if(Player!=null && state!=1 && (Ani.CurrentAnimation!="Atk" && Ani.CurrentAnimation!="Atk2"))
			{
				Nav.TargetPosition = Player.GlobalPosition;
				state = 1;
			}
			if(Player!=null && state==1 && Distance(Player.GlobalPosition,GlobalPosition)<DistAtk)
			{
				state=2;
			}
		}
		else
		{
			//Death();
			if(Ani.CurrentAnimation=="")
			{
				QueueFree();
			}
		}
		/*
		if(PlayerSet)
		{
			if(Distance(Player.Position,this.Position)<=DistVue)
			{
				if (Distance(Player.Position,this.Position)<=2 && Alive)
				{
					state = 2;
				}
				else if(state == 2)
				{
					state = 1;
				}
				if(!Alive)
				{
					Death();
				}
				if (Alive) 
				{
					IsTooFar = !(Distance(Player.Position,this.Position)<=DistVue);
					Ray.Rotation = new Vector3(0,-Rotation.Y,0);
					if (state == 0 || state == -1) // Si ne suit pas le joueur
					{
						if (PlayerVisible(Player) && !IsTooFar) // si rien entre mob et joueur
						{
							state = 1;
							//GD.Print("not colliding");
							Nav.TargetPosition = Player.GlobalPosition;
							Agro = AgroMax;
						}
					}
					if (state == 1 && !IsTooFar) // si suit le joueur
					{
						Nav.TargetPosition = Player.GlobalPosition;
						if (!PlayerVisible(Player)) // si qqc entre mob et joueur
						{
							Agro -=1; 
							//GD.Print("colliding");
						}
						else Agro = AgroMax; // si rien entre mob et joueur
					}
				}
				SkipFrame = false; // Faire la prochaine frame
			}
		}*/
	}
	public void Process(double delta) //NavMesh
	{
		if(Alive)
		{
			if(state==0 || state==1 || state == 3)
			{
				var dir = new Vector3();  //Pathfiding
				var NextPos = Nav.GetNextPathPosition();
				dir = NextPos - GlobalPosition;
				dir = dir.Normalized();
				Velocity = Velocity.Lerp(dir*speed,(float)(accel*delta));
				MoveAndSlide();
				//LookAt(new Vector3(Nav.TargetPosition.X, 0, Nav.TargetPosition.Z)); //Orientation
				LookAt(new Vector3(NextPos.X, 1, NextPos.Z)); //Orientation
				Rotation = new Vector3(0,Rotation.Y+(float)Math.PI,0);
			}
			if(state==1)
			{
				Nav.TargetPosition = Player.GlobalPosition;
				Agro = AgroMax;
			}
			if(state==-1)
			{
				Rotation = RotInnit;
				if(Ani.CurrentAnimation!="Idle")
				{
					Ani.Play("Idle");
				}
			}
			if(state==2 && Ani.CurrentAnimation!="Atk" && Ani.CurrentAnimation!="Atk2")
			{
				var NextPos = Nav.GetNextPathPosition();
				LookAt(new Vector3(NextPos.X, 1, NextPos.Z)); //Orientation
				Rotation = new Vector3(0,Rotation.Y+(float)Math.PI,0);
			}
		}
	}
	
	public virtual void TakeDamage(int damage)
	{
		HP -= damage;
		GD.Print("Je suis la");
		if(HP<=0)
		{
			Ani.Stop();
		}
	}
	
	public void SetDebug()
	{
		DebugMode = !DebugMode;
	}
	
	private double Distance(Vector3 Room1, Vector3 Room2)
	{
		return Math.Sqrt(Math.Pow(Room1.X - Room2.X, 2) +
						 Math.Pow(Room1.Z - Room2.Z, 2));
	}
	protected void SetUpDeath()
	{
		Alive = false;
		ShaderMaterial SH = new ShaderMaterial();
		SH.Shader = (Shader)ResourceLoader.Load("res://Ressources/Map/death.tres");
		MeshInstance3D M = GetNode<MeshInstance3D>("Body/Body"); 
		StandardMaterial3D Mat = (StandardMaterial3D)M.GetActiveMaterial(0);
		SH.SetShaderParameter("Albedo",Mat.AlbedoTexture);
		SH.SetShaderParameter("Timer", 0f);
		M.MaterialOverride = SH;
		
	}
	private void Death()
	{
		TimerDeath += 0.01f;
		MeshInstance3D M = GetNode<MeshInstance3D>("Body/Body"); 
		(M.MaterialOverride as ShaderMaterial).SetShaderParameter("Timer",TimerDeath);
		if(TimerDeath>=1.1f)
		{
			QueueFree();
		}
	}
	
	
	private List<int> SetRay()
	{
		List<int> res = new List<int>();
		if(GameManager.ListJoueur!=null)
		{
			for(int i = 0; i<GameManager._nbJoueur;i++)
			{
				CharacterBody3D play = GameManager.ListJoueur[i];
				if(play!=null && play.IsInsideTree() && Distance(GlobalPosition,play.GlobalPosition)<=DistVue)
				{
					RayList[i].TargetPosition = new Vector3(play.GlobalPosition.X - GlobalPosition.X, 0 , play.GlobalPosition.Z - GlobalPosition.Z );
					RayList[i].Rotation = new Vector3(0,-Rotation.Y,0);
					res.Add(i);
				}
			}	
		}
		return res;
	}
	private CharacterBody3D GetPlayer(List<int> PlayerList)
	{
		double MinDist = 9999999999.0;
		CharacterBody3D res = null;
		for(int i = 0; i<PlayerList.Count;i++)
		{
			int id = PlayerList[i];
			if(GameManager.ListJoueur[id]!=null && !((PlayerScript)GameManager.ListJoueur[id]).IsDead && RayList[id].GetCollider()==GameManager.ListJoueur[id])
			{
				double d = Distance(GlobalPosition,GameManager.ListJoueur[id].GlobalPosition);
				if(d<MinDist)
				{
					MinDist = d;
					res = GameManager.ListJoueur[id];
				}
			}
		}
		return res;
	}

	public void receive()
	{
		string rec = GameManager.InfoAutreJoueur["ia"];
		GD.Print(rec);
		GameManager.InfoAutreJoueur["ia"] = "";
		string[] ia = rec.Split("=");
		foreach (var a in ia)
		{
			if (a!="")
			{
				GD.Print(a);
				string[] firstline = a.Split("°");
				string[] secondline = firstline[1].Split("§");
				int id = int.Parse(firstline[0]);
				if(secondline[0]=="TK")
				{
					int damage = int.Parse(secondline[1]);
					if(id==ID)
					{
						GD.Print(damage);
						TakeDamage(damage);
					}
				}
			}
		}
	}
	
}
