using Godot;
using System;
using System.Collections.Generic;

public interface IMap
{
	public List<(int, int)> GetSpawnLocation();
	public bool MapIsReady();
	public void Debug(double delta, bool enable);
}
