using Godot;
using System;
using System.Collections.Generic;
using Lib;

public partial class Shop : IMap
{
	private Node3D Emax;
	private AnimationPlayer EmaxAnimation;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Ani = GetNode<AnimationPlayer>("Animation/AnimationPlayer");
		Emax = GetNode<Node3D>("Shop/Emax");
		EmaxAnimation = Emax.GetNode<AnimationPlayer>("AnimationPlayer");
		EmaxAnimation.Play("Animation");
		
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
