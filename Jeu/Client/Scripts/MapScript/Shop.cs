using Godot;
using System;
using System.Collections.Generic;

public partial class Shop : Node3D, IMap
{
	private bool MapReady = false;
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
		}
	}
	
	public int Step()
	{
		return 0;
	}

	public List<(int, int, int)> GetSpawnLocation()
	{
		List<(int,int,int)> res = new List<(int,int,int)>();
		res.Add((0,0,0));
		res.Add((0,0,0));
		res.Add((0,0,0));
		res.Add((0,0,0));
		
		return res;
	}

	public bool MapIsReady()
	{
		return MapReady;
	}

	public void DebugMode(CharacterBody3D Player, bool DebugMode)
	{
		throw new NotImplementedException();
	}

	public void SetSeed(int seed, int seed2)
	{
		Random Rand = new Random(seed);
	}
}
