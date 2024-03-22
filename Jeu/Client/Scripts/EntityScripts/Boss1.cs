using Godot;
using System;
//Ceci est un commentaire
public partial class Boss1: CharacterBody3D
{
	//stats
	private int speed = 2; // vitesse
	private int accel = 10; // acceleration
	private int DistVue = 30; // Distance de vue
	private int HP = 100; // health point
	private int AgroMax = 300; // Valeur Maximum de l'Agro
	private float tim = -0.1f;
	
	//debug
	public bool DebugMode = true; // variable debug mode
	
	// Pour le pathfiding... ?
	private NavigationAgent3D Nav; // Cible du pathfiding
	public int Agro = 0; // stop quand agro = 0
	private bool SkipFrame = true; // RayCast3D ne fonctionne pas à la première frame
	
	// Autre
	private Node Parent;
	private Camera3D Cam;
	private Camera3D Player;
	private bool PlayerSet = false;
	private float StateDeath = -0.1f;
	
	public override void _Ready()
	{
		Parent = GetParent().GetParent();
		Nav = GetNode<NavigationAgent3D>("NavigationAgent3D");
		Cam = GetNode<Camera3D>("SpecCam");
		Player = Cam; 
		PlayerSet = true;
	}
	
	public override void _Process(double delta) //NavMesh
	{
		if(PlayerSet)
		{
			GD.Print(Player.Position);
			if(Distance(Player.Position,this.Position)<=DistVue)
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
		if(!PlayerSet) // &&  Parent.IsAncestorOf(GameManager.Joueur1)
		{
			Player = Cam;
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
}
