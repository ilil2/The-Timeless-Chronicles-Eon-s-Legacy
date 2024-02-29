using Godot;
using System;
using Lib;

public partial class pointer : Node3D
{
	public CharacterBody3D Target;
	public bool Ready = false;
	public Label3D t;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		t = GetNode<Label3D>("Dist");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		try
		{
			if(Ready)
			{
				LookAt(Target.GlobalPosition);	
				double d = MapTool.Distance(GlobalPosition,Target.GlobalPosition);
				t.Text = $"{Mathf.Round(d)}m";
			}
		}
		catch
		{
			QueueFree();
		}
	}
	
	public void SetTarget(CharacterBody3D target)
	{
		Target = target;
		Ready = true;
	}
}
