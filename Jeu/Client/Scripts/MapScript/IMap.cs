using Godot;
using System;
using System.Collections.Generic;

public abstract partial class IMap : Node3D
{
	protected bool MapReady = false;
	public abstract int Step();
	public abstract List<(int,int,int)> GetSpawnLocation();
	public abstract bool MapIsReady();
	public abstract void DebugMode(CharacterBody3D Player, bool DebugMode);
	public abstract void SetSeed(int seed, int seed2);
}
