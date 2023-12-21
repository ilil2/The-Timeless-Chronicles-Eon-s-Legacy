using Godot;
using System;

public partial class MobScript : CharacterBody3D
{
	private int speed = 2;
	private int accel = 10;
	NavigationAgent3D Nav;
	public bool Sleep = false;
	public bool DebugMode = true;
	
	public override void _Ready()
	{
		Nav = GetNode<NavigationAgent3D>("NavigationAgent3D");
	}

	public override void _Process(double delta)
	{
		Nav.DebugEnabled = DebugMode;
		Label3D State = GetNode<Label3D>("Label3D");
		State.Text = $"Sleep:{Sleep}";
		if (!Sleep)
		{
			var dir = new Vector3();
			Node Parent = GetParent();
			Nav.TargetPosition = Parent.GetNode<CharacterBody3D>("Player").GlobalPosition;
			dir = Nav.GetNextPathPosition() - GlobalPosition;
			dir = dir.Normalized();
			Velocity = Velocity.Lerp(dir*speed,(float)(accel*delta));
			MoveAndSlide();
		}
		
		
	}
	public void SetDebug()
	{
		DebugMode = !DebugMode;
	}
}
