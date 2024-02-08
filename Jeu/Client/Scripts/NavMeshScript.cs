using Godot;
using System;

public partial class NavMeshScript : NavigationRegion3D
{
	public bool NavMeshReady = false;
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
	
	private void _on_bake_finished()
	{
		NavMeshReady = true;
	}
}

