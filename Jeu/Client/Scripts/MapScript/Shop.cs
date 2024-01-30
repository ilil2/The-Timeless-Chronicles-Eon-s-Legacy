using Godot;
using System;
using System.Collections.Generic;

public partial class Shop : Node3D, IMap
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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
