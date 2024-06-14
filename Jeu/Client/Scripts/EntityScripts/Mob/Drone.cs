using Godot;
using System;

public partial class Drone : MobScript
{
	[Export] public bool Fire = false;
	public Node3D Spawn;
	public PackedScene Proj = GD.Load<PackedScene>("res://Scenes/EntityScenes/Mob/proj.tscn");
	public override void _Ready()
	{
		speed = 3;
		HP = 30;
		Ready();
		DistAtk = 7;
		Spawn = GetNode<Node3D>("Spawn");
	}
	public override void _PhysicsProcess(double delta)
	{
		PhysicsProcess(delta);
	}
	public override void _Process(double delta) 
	{
		if (Fire)
		{
			Fire = false;
			Node3D p = Proj.Instantiate<Node3D>();
			p.Position = Spawn.GlobalPosition;
			p.Rotation = Rotation;
			AddChild(p);

		}
		Process(delta);
		if(Alive && (state == 1 || state == 3 || state == 0))
		{
			if(Ani.CurrentAnimation != "Hit" && Ani.CurrentAnimation!="Walk")
			{
				Ani.Play("Walk");
			}
		}
		if(state == 2)
		{
			if(Ani.CurrentAnimation != "Atk" && Ani.CurrentAnimation != "Hit" && Alive)
			{
				Ani.Play("Atk");
			}
		}
		if(state == -1)
		{
			Ani.Stop();
		}
		
	}
}
