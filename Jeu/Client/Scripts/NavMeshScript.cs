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
	public void InitNavMesh()
	{
		NavigationMesh Nav = new NavigationMesh();
		Nav.CellHeight = 0.01f;
		NavigationMesh = Nav;
	}
	
	private void _on_bake_finished()
	{
		NavMeshReady = true;
	}
}

