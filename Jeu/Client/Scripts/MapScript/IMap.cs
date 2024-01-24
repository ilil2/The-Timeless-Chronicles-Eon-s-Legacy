using Godot;
using System;
using System.Collections.Generic;

public interface IMap
{
	public (int,int) GetSpawnLocation();
	public bool MapIsReady();
	public void DebugMode(double delta, CharacterBody3D Player);
}
