using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

//Ceci est un commentaire
public abstract partial class MobBasiqueScript : CharacterBody3D
{
	//stats
	private int speed = 7; // vitesse
	private int accel = 10; // acceleration
	private int DistVue = 50; // Distance de vue
	private int HP = 100; // health point
	private int AgroMax = 300; // Valeur Maximum de l'Agro
	
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
	
	//Pour le multi
	private CharacterBody3D Target;
	
	// Autre
	private Node Parent;
	private Camera3D SpecCam;
	
	public void Ready()
	{
		Parent = GetParent();
		PosInnit = Position;
		RotInnit = Rotation;
		Nav = GetNode<NavigationAgent3D>("NavigationAgent3D");
		Nav.TargetPosition = PosInnit;
		Ray = GetNode<RayCast3D>("Ray");
		state = -1;
		SpecCam = Parent.GetNode<Camera3D>("SpecCam");
		Target = SetTarget();
	}

	public void PhysicsProcess(double delta) //Raycast
	{
		Target = SetTarget();
		if (Distance(SpecCam.Position,this.Position)<=100) // Pour l'opti
		{
			if (!SkipFrame) 
			{
				IsTooFar = !(Distance(Target.Position,this.Position)<=DistVue);
				Ray.Rotation = new Vector3(0,-Rotation.Y,0);
				Ray.TargetPosition = new Vector3(Target.GlobalPosition.X - GlobalPosition.X, 1 , Target.GlobalPosition.Z - GlobalPosition.Z );
				//GD.Print($"Sate : {state} & Agro : {Agro}");
				if (state == 0 || state == -1) // Si ne suit pas le joueur
				{
					if (!Ray.IsColliding() && !IsTooFar) // si rien entre mob et joueur
					{
						state = 1;
						//GD.Print("not colliding");
						Nav.TargetPosition = Target.GlobalPosition;
						Agro = AgroMax;
					}
				}
				if (state == 1 && !IsTooFar) // si suit le joueur
				{
					Nav.TargetPosition = Target.GlobalPosition;
					if (Ray.IsColliding()) // si qqc entre mob et joueur
					{
						Agro -=1; 
						//GD.Print("colliding");
					}
					else Agro = AgroMax; // si rien entre mob et joueur
				}
				if (Distance(Target.Position,this.Position)<=1) state = 2;
				else state = 1;
			}
			SkipFrame = false; // Faire la prochaine frame
		}
	}
	public void Process(double delta) //NavMesh
	{
		if (Distance(SpecCam.Position,this.Position)<=100) // Pour l'opti
		{
			Nav.DebugEnabled = DebugMode; // Afficher chemin du joueur si DebugMode activé
			
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
			else if (state != 2)
			{
				var dir = new Vector3();  //Pathfiding
				var NextPos = Nav.GetNextPathPosition();
				dir = NextPos - GlobalPosition;
				dir = dir.Normalized();
				Velocity = Velocity.Lerp(dir*speed,(float)(accel*delta));
				MoveAndSlide();
				LookAt(new Vector3(NextPos.X,1,NextPos.Z));   //Orientation
				Rotation = new Vector3(0,Rotation.Y+(float)Math.PI,0);  
			}
			else
			{
				// Patern d'attaques ici
			}
		}
	}
	
	public void SetDebug()
	{
		DebugMode = !DebugMode;
	}
	
	private double Distance(Vector3 Room1, Vector3 Room2)
	{
		return Math.Sqrt(Math.Pow(Room1.X - Room2.X, 2) + Math.Pow(Room1.Z - Room2.Z, 2));
	}
	
	private CharacterBody3D SetTarget() // Modifier CETTE fonction pour le multijoueur
	{
		var l = double[4]{};
		double d0 = Distance(Position,SpecCam.Position);
		l[0]=d0;
		double d1 = Distance(Position,Parent.GetNode<CharacterBody3D>("OtherJoueurTest").Position);
		l[1]=d1;
		double d2 = Distance(Position,Parent.GetNode<CharacterBody3D>("OtherJoueurTest2").Position);
		l[2]=d2;
		double d3 = Distance(Position,Parent.GetNode<CharacterBody3D>("OtherJoueurTest3").Position);
		l[3]=d3;
		double min = d0;
		for(int i=1;i<4;i++) if(l[i] < min) min = l[i];
		if (min == d0)
			return SpecCam.GetNode<CharacterBody3D>("CamBody");
		if (min == d1)
			return Parent.GetNode<CharacterBody3D>("OtherJoueurTest");
		if (min == d2)
			return Parent.GetNode<CharacterBody3D>("OtherJoueurTest2");
		else
			return Parent.GetNode<CharacterBody3D>("OtherJoueurTest3");
	}
}
