using System;
using System.Collections.Generic;
using Lib;
using Godot;
namespace JeuClient.Scripts.MapScript;

public partial class Boss2Map : IMap
{
	private PackedScene Wa = GD.Load<PackedScene>("res://Scenes/MapScenes/Lvl2/R.tscn");
	private NavigationRegion3D Nav;
	private AnimationPlayer Ani;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Nav = GetNode<NavigationRegion3D>("Nav");
		Ani = GetNode<AnimationPlayer>("AnimationPlayer");
		CreateBorder();
		Ani.Play("Enter");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!MapReady)
		{
			MapReady = true;
			LoadingStage = "En attente des autres joueurs :(";
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		//GD.Print(LoadingStage);
		SyncCam();
	}

	public override List<(int, int, int)> GetSpawnLocation()
	{
		List<(int,int,int)> res = new List<(int,int,int)>();
		res.Add((52,0,-1));
		res.Add((52,0,1));
		res.Add((53,0,-1));
		res.Add((53,0,1));
		
		return res;
	}


	public override void DebugMode(CharacterBody3D Player, bool DebugMode)
	{
		MapTool.Debug(Player,this,DebugMode);
	}
	private void CreateBorder()
	{
		const int Rayon = 60;
		const int Pas = 10;
		for (int i = 0; i<360; i+=Pas)
		{
			float rot = Mathf.DegToRad(i);
			Node3D W = Wa.Instantiate<Node3D>();
			Node3D Wall = W.GetNode<Node3D>($"R{Rand.Next(1,5)}");
			W.RemoveChild(Wall);
			double X = Math.Cos(rot);
			double Z = Math.Sin(rot);
			Wall.Position = new Vector3((float)X*Rayon,0,(float)Z*Rayon);
			Wall.Rotation += new Vector3(0,-rot,0);
			Nav.AddChild(Wall);
		}
		Random nRand = new Random(42);
		for (int i = 12; i<354; i+=6)
		{
			Node3D Tr = GD.Load<PackedScene>($"res://Ressources/Map/Global/tre2/Model/Tree{nRand.Next(1,6)}.tscn").Instantiate<Node3D>();
			float rot2 = Mathf.DegToRad(nRand.Next(0,361));
			float rot = Mathf.DegToRad(i);
			double X = Math.Cos(rot);
			double Z = Math.Sin(rot);
			(Tr as IRender).DoRender = false;
			var t = Tr.GetNode<MeshInstance3D>("ForNav");
			Tr.RemoveChild(t);
			Tr.Position = new Vector3((float)X*(Rayon-5),0,(float)Z*(Rayon-5));
			Tr.Rotation = new Vector3(0,rot2,0);
			AddChild(Tr);
			
			
		}
	}
}
