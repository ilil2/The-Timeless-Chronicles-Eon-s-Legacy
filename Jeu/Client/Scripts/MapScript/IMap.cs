using Godot;
using System;
using System.Collections.Generic;

public abstract partial class IMap : Node3D
{
	protected bool MapReady = false;
	protected bool SetUp = false;
	protected Random Rand = new Random(42);
	protected Random Rand2 = new Random(42);
	protected int step = 0;
	public int Step()
	{
		return step;
	}
	public abstract List<(int,int,int)> GetSpawnLocation();
	public bool MapIsReady()
	{
		return MapReady;
	}
	public abstract void DebugMode(CharacterBody3D Player, bool DebugMode);
	public void SetSeed(int seed, int seed2)
	{
		Rand = new Random(seed);
		Rand2 = new Random(seed2);
		GD.Print($"Seed1 set : {seed}");
		GD.Print($"Seed2 set : {seed2}");
	}
}
