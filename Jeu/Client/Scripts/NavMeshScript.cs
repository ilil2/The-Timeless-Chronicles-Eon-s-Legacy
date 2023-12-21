using Godot;
using System;

public partial class NavMeshScript : NavigationRegion3D
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
	
	public void CreateNavMesh()
	{
		BakeNavigationMesh(true);
	}
}
