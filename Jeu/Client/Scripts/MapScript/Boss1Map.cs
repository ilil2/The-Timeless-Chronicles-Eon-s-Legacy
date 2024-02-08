using Godot;
using System;
using System.Collections.Generic;
using Lib;

[Tool]
public partial class Boss1Map : IMap
{
	[Export] private int Rayon = 0;
	[Export] private int Pas = 360;
	private PackedScene Wa = GD.Load<PackedScene>("res://Scenes/MapScenes/Lvl1/BossScenes/w.tscn");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LoadingStage = "Create Border";
		for (int i = 0; i<360; i+=Pas)
		{
			float rot = Mathf.DegToRad(i);
			Node3D Wall = Wa.Instantiate<Node3D>();
			double X = Math.Cos(rot);
			double Z = Math.Sin(rot);
			Wall.Position = new Vector3((float)X*Rayon,0,(float)Z*Rayon);
			Wall.Rotation += new Vector3(0,-rot,0);
			AddChild(Wall);
		}
	}

	public override void _Process(double delta)
	{
		if (!MapReady)
		{
			MapReady = true;
			LoadingStage = "En attente des autres joueurs :(";
		}
	}

	public override List<(int, int, int)> GetSpawnLocation()
	{
		List<(int,int,int)> res = new List<(int,int,int)>();
		res.Add((1,10,0));
		res.Add((0,5,1));
		res.Add((-1,3,0));
		res.Add((0,0,-1));
		
		return res;
	}


	public override void DebugMode(CharacterBody3D Player, bool DebugMode)
	{
		MapTool.Debug(Player,this,DebugMode);
	}

}
