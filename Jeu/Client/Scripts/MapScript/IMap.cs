using Godot;
using System;
using System.Collections.Generic;

public interface IMap
{
	public List<(int,int,int)> GetSpawnLocation();
	public bool MapIsReady();
	public void DebugMode(double delta, CharacterBody3D Player);
	public void SetSeed(int seed, int seed2);
}
