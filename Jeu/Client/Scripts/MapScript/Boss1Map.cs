using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class Boss1Map : Node3D, IMap
{
	[Export] private int Rayon = 0;
	[Export] private int Pas = 360;
	private PackedScene Wa = GD.Load<PackedScene>("res://Scenes/MapScenes/Lvl1/BossScenes/w.tscn");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public int Step()
	{
		return 0;
	}

	public List<(int, int, int)> GetSpawnLocation()
	{
		throw new NotImplementedException();
	}

	public bool MapIsReady()
	{
		throw new NotImplementedException();
	}

	public void DebugMode(CharacterBody3D Player, bool DebugMode)
	{
		throw new NotImplementedException();
	}

	public void SetSeed(int seed, int seed2)
	{
		throw new NotImplementedException();
	}
}
