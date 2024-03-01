using Godot;
using System;
public partial class Boss_niv1_Script : CharacterBody3D
{
	//stats
	private int speed = 5; // vitesse
	private int accel = 15; // acceleration
	private int DistVue = 300; // Distance de vue
	private int HP = 1000; // health point
	private bool Alive = true;
	private float tim = -0.1f;
	
	//debug
	public bool DebugMode = true; // variable debug mode
	
	// Pour le pathfiding... ?
	private NavigationAgent3D Nav; // Cible du pathfiding
	private RayCast3D Ray; // Activer le pathfiding
	private bool SkipFrame = true; // RayCast3D ne fonctionne pas à la première frame
	private bool IsTooFar = false; // Indique si le mob est trop loin du joueur
	
	// Autre
	private Node Parent;
	private CharacterBody3D Player = new CharacterBody3D();
	private Vector3 Me = new Vector3();
	private Vector3 PlayerPos = new Vector3();
	private bool PlayerSet = false;
	protected int RenderDist = 150;
	private float StateDeath = -0.1f;
	private Random R = new Ramdom(); 
	
	public override void _Ready()
	{
		Parent = GetParent().GetParent();
		Nav = GetNode<NavigationAgent3D>("NavigationAgent3D");
		attackTimer = new Timer();
		AddChild(attackTimer);
		attackTimer.WaitTime = 2f; // Cooldown de 2 secondes entre les attaques
		attackTimer.OneShot = true;
		attackTimer.Connect("timeout", this, nameof(OnAttackTimerTimeout));
	}

	
	public override void _Process(double delta) //NavMesh
	{
		if(PlayerSet)
		{
			

					var dir = new Vector3();  //Pathfiding
					var NextPos = Nav.GetNextPathPosition();
					dir = NextPos - GlobalPosition;
					dir = dir.Normalized();
					Velocity = Velocity.Lerp(dir*speed,(float)(accel*delta));
					MoveAndSlide();
					
					try
					{
						LookAt(new Vector3(NextPos.X, 1, NextPos.Z)); //Orientation
					}
					catch
					{
						GD.Print("Still Error");
					}
					Rotation = new Vector3(0,Rotation.Y+(float)Math.PI,0);  
				}
			}
		}
		if(!PlayerSet && GameManager.Joueur1!=null && Parent.IsAncestorOf(GameManager.Joueur1))
		{
			Player = GameManager.Joueur1;
			PlayerSet = true;
			GD.Print("Player Set !");
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
	private void SetUpDeath()
	{
		GD.Print("Set");
		ShaderMaterial SH = new ShaderMaterial();
		SH.Shader = (Shader)ResourceLoader.Load("res://Ressources/Map/death.tres");
		MeshInstance3D M = GetNode<MeshInstance3D>("Body/Body"); 
		StandardMaterial3D Mat = (StandardMaterial3D)M.GetActiveMaterial(0);
		SH.SetShaderParameter("Albedo",Mat.AlbedoTexture);
		SH.SetShaderParameter("Timer", 0f);
		M.MaterialOverride = SH;
		Alive = false;
		
	}

	// Processus d'attaque
	
	private void OnAttackTimerTimeout() // check le cooldown 
	{
		canAttack = true; // Permet au boss d'attaquer à nouveau
	}
	
	private void Attack()
	{
		if (HP <= 500) 
		{
			state = 3; 
		}
		if (state == 2 && Alive)
		{
				
			// Implémentez la logique d'attaque ici
			a = R.Next(1,3); // choisir atk
			if (a = 1 )
			{
				Slash(); // Exec l'attaque
				canAttack = false ; // empèche atk overflow
				attackTimer.Start(); // init cooldown 
			}
			
			if (a = 2 )
			{
				Slash(); // Exec l'attaque
				canAttack = false ; // empèche atk overflow
				attackTimer.Start(); // init cooldown 
			}
			
		}
		else if (state == 3)
		{
			// Implémentez la logique d'attaque spéciale ici
			
			a = R.Next(1,5); // choisir atk & spé 
			if (a = 1 )
			{
				Slash(); // Exec l'attaque
				canAttack = false ; // empèche atk overflow
				attackTimer.Start(); // init cooldown 
			}
			if (a = 2 )
			{
				Bump(); // Exec l'attaque
				canAttack = false ; // empèche atk overflow
				attackTimer.Start(); // init cooldown 
			}
			if (a = 3 )
			{
				Bouboule(); // Exec l'attaque
				canAttack = false ; // empèche atk overflow
				attackTimer.Start(); // init cooldown 
			}
			if (a = 4)
			{
				Buff(); // Exec l'attaque
				canAttack = false ; // empèche atk overflow
				attackTimer.Start(); // init cooldown 
			}
		}
	}
	
	// Types d'attaques 
	
	private void Slash()
	{
		GD.Print("Je me slash");
	}
	private void Bump()
	{
		GD.Print("Je te Bump");
	}
	private void Buff()
	{
		GD.Print("POWAAAAAAAA");
	}
	private void Bouboule()
	{
		GD.Print("Bouffe mes boules");
	}

	// Dégâts reçus par le boss
	public void TakeDamage(int damage)
	{
		HP -= damage;
		if (HP <= 0)
		{
			Die();
		}
	}
	private void Death()
	{
		tim += 0.01f;
		MeshInstance3D M = GetNode<MeshInstance3D>("Body/Body"); 
		(M.MaterialOverride as ShaderMaterial).SetShaderParameter("Timer",tim);
		if(tim>=1.1f)
		{
			QueueFree();
		}
	}
}
