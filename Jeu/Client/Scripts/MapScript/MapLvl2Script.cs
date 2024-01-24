using Godot;
using System;
using System.Collections.Generic;

public partial class MapLvl2Script : Node3D, IMap
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public List<(int, int)> GetSpawnLocation()
	{
		throw new NotImplementedException();
	}

	public bool MapIsReady()
	{
		throw new NotImplementedException();
	}

	public void Debug(double delta, bool enable)
	{
		throw new NotImplementedException();
	}
}
