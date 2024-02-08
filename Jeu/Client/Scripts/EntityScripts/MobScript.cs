using Godot;
using System;
//Ceci est un commentaire
public partial class MobScript : CharacterBody3D
{
	//stats
	private int speed = 2; // vitesse
	private int accel = 10; // acceleration
	private int DistVue = 30; // Distance de vue
	private int HP = 100; // health point
	private int AgroMax = 300; // Valeur Maximum de l'Agro
	private bool Alive = true;
	private float tim = -0.1f;
	
	//debug
	public bool DebugMode = true; // variable debug mode
	
	// Pour le pathfiding... ?
	private NavigationAgent3D Nav; // Cible du pathfiding
	private RayCast3D Ray; // Activer le pathfiding
	public int Agro = 0; // stop quand agro = 0
	private int state; // -1 = repos | 0 = retour à position initiale | 1 = suivre joueur
	private Vector3 PosInnit; // position initiale
	private Vector3 RotInnit; // rotation innitiale
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
	
	public override void _Ready()
	{
		Parent = GetParent().GetParent();
		PosInnit = Position;
		RotInnit = Rotation;
		Nav = GetNode<NavigationAgent3D>("NavigationAgent3D");
		Nav.TargetPosition = PosInnit;
		Ray = GetNode<RayCast3D>("Ray");
		state = -1;
		
	}

	public override void _PhysicsProcess(double delta) //Raycast
	{
		if(PlayerSet)
		{
			if(Distance(Player.Position,this.Position)<=DistVue)
			{
				if (Distance(Player.Position,this.Position)<=1 && Alive)
				{
					SetUpDeath();
				}
				if(!Alive)
				{
					Death();
				}
				if (PlayerSet) 
				{
					IsTooFar = !(Distance(Player.Position,this.Position)<=DistVue);
					Ray.Rotation = new Vector3(0,-Rotation.Y,0);
					Ray.TargetPosition = new Vector3(Player.GlobalPosition.X - GlobalPosition.X, 1 , Player.GlobalPosition.Z - GlobalPosition.Z );
					Label3D St = GetNode<Label3D>("State");
					St.Text = $"State: {state}";
					if (state == 0 || state == -1) // Si ne suit pas le joueur
					{
						if (!Ray.IsColliding() && !IsTooFar) // si rien entre mob et joueur
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
						if (Ray.IsColliding()) // si qqc entre mob et joueur
						{
							Agro -=1; 
							//GD.Print("colliding");
						}
						else Agro = AgroMax; // si rien entre mob et joueur
					}
				}
				SkipFrame = false; // Faire la prochaine frame
			}
		}
	}
	public override void _Process(double delta) //NavMesh
	{
		if(PlayerSet)
		{
			if(Distance(Player.Position,this.Position)<=DistVue)
			{
				if (IsTooFar && Agro > 0) Agro -= 1;
				if (Agro <= 0) 
				{
					state = 0;
					if (Distance(Position,PosInnit)<1 && state == 0) state = -1;
					else Nav.TargetPosition = PosInnit;
				}
				if (state == -1) 
				{
					Rotation = RotInnit;
				}
				else
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
