using Godot;
using System;
using System.Collections.Generic;
[Tool]
public partial class MapLvl2Script : Node3D, IMap
{
	[Export] private int Rayon = 0;
	[Export] private int Pas = 360;
	private Random Rand = new Random();
	private PackedScene Wa = GD.Load<PackedScene>("res://Scenes/MapScenes/Lvl2/R.tscn");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
			AddChild(Wall);
		}
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public (int,int) GetSpawnLocation()
	{
		throw new NotImplementedException();
	}

	public bool MapIsReady()
	{
		throw new NotImplementedException();
	}

	public void DebugMode(double delta, CharacterBody3D Player)
	{
		throw new NotImplementedException();
	}
}
