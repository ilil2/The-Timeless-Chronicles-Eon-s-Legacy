using Godot;
using System;
using Lib;

public partial class Tree : IRender
{
	public override void _Ready()
	{
	
	}

	
	public override void _Process(double delta)
	{
		
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if(RenderSetup())
		{
			Render();
		}
	}
}
