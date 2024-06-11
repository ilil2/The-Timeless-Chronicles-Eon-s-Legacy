using Godot;
using System;
using System.Collections.Generic;
using Lib;
[Tool]
public partial class WIP : IMap
{
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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

	public override List<Vector3> GetEndLocation()
	{
		throw new NotImplementedException();
	}


	public override void DebugMode(CharacterBody3D Player, bool DebugMode)
	{
		MapTool.Debug(Player,this,DebugMode);
	}
}
