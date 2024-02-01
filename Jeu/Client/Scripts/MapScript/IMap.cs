using Godot;
using System;
using System.Collections.Generic;

public abstract partial class IMap : Node3D
{
	protected bool MapReady = false;
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
	public abstract void SetSeed(int seed, int seed2);
}
