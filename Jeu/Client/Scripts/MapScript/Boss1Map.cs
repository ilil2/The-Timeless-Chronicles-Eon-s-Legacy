using Godot;
using System;
using System.Collections.Generic;
using JeuClient.Scripts.PlayerScripts;
using Lib;

[Tool]
public partial class Boss1Map : IMap
{
	private int Rayon = 57;
	private int Pas = 6;
	private int Pas2 = 12;
	private PackedScene Wa = GD.Load<PackedScene>("res://Scenes/MapScenes/Lvl1/BossScenes/w.tscn");
	private PackedScene Pi = GD.Load<PackedScene>("res://Scenes/MapScenes/Lvl1/BossScenes/p.tscn");
	private NavigationRegion3D Nav;
	private List<Node3D> AnimationSpawn = new List<Node3D>();
	private List<Vector3> BallPos = new List<Vector3>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach(Node3D s in GetNode<Node3D>("AnimationSpawn").GetChildren())
		{
			AnimationSpawn.Add(s);
		}
		Ani = GetNode<AnimationPlayer>("Animation/AnimationPlayer");
		Nav = GetNode<NavigationRegion3D>("Nav");
		LoadingStage = "Create Border";
		for (int i = 0; i<360; i+=Pas)
		{
			float rot = Mathf.DegToRad(i);
			Node3D Wall = Wa.Instantiate<Node3D>();
			double X = Math.Cos(rot);
			double Z = Math.Sin(rot);
			Wall.Position = new Vector3((float)X*Rayon,0,(float)Z*Rayon);
			Wall.Rotation += new Vector3(0,-rot,0);
			Nav.AddChild(Wall);
		}
		for (int i = 0; i<360; i+=Pas2)
		{
			float rot = Mathf.DegToRad(i);
			Node3D Wall = Pi.Instantiate<Node3D>();
			double X = Math.Cos(rot);
			double Z = Math.Sin(rot);
			Wall.Position = new Vector3((float)X*Rayon,0,(float)Z*Rayon);
			Wall.Rotation += new Vector3(0,-rot,0);
			Nav.AddChild(Wall);
			BallPos.Add(Wall.GetNode<Node3D>("Spawn").GlobalPosition);
		}
	}

	public override void _Process(double delta)
	{
		SyncCam();
		GetNode<Label>("t").Text = $"CamOnPlayer = {CamOnPlayer}\n Cam1 = {GetNode<Camera3D>("Animation/Cam1").Current}\n Cam2 = {GetNode<Camera3D>("PortalExit/Cam").Current} \n CA = {Ani.CurrentAnimation}";
		if (!MapReady)
		{
			MapReady = true;
			GameManager.SettingsManager.SetSetting("enableChat",0);
			Ani.Play("Enter");
			LoadingStage = "En attente des autres joueurs :(";
		}
		else
		{
			PlayCinematic();
		}
	}

	public override List<(int, int, int)> GetSpawnLocation()
	{
		/**/
		List<(int,int,int)> res = new List<(int,int,int)>();
		res.Add((1,10,0));
		res.Add((0,5,1));
		res.Add((-1,3,0));
		res.Add((0,0,-1));
		
		return res;
	}

	public override Vector3 GetEndLocation()
	{
		throw new NotImplementedException();
	}


	public override void DebugMode(CharacterBody3D Player, bool DebugMode)
	{
		MapTool.Debug(Player,this,DebugMode);
	}
	
	public void PlayCinematic()
	{
		if(Ani.CurrentAnimation == "Enter")
		{
			for(int i = 0; i<GameManager._nbJoueur;i++)
			{
				if(GameManager.ListJoueur[i].IsInsideTree())
				{
					GameManager.ListJoueur[i].GlobalPosition = AnimationSpawn[i].GlobalPosition;
				}
				
			}
		}
	}
	
	public void MapAtk()
	{
		PackedScene Ball = GD.Load<PackedScene>("res://Scenes/EntityScenes/SmartBall.tscn");
		List<int> random = MapTool.GenerateRandomArray(0,BallPos.Count,10);
		for(int i = 0; i<random.Count;i++)
		{
			Node3D ball = Ball.Instantiate<Node3D>();
			ball.Position = BallPos[random[i]];
			AddChild(ball);
		}
	}
	private void _on_area_3d_body_entered(Node3D body)
	{
		if(body is PlayerScript)
		{
			MapAtk();
		}
	}

}

