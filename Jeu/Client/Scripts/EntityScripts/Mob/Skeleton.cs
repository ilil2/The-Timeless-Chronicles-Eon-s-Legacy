using Godot;
using System;

public partial class Skeleton : MobScript
{
	public override void _Ready()
	{
		Ready();
	}
	public override void _PhysicsProcess(double delta)
	{
		PhysicsProcess(delta);
	}
	public override void _Process(double delta) 
	{
		Process(delta);
		if(Alive && (state == 1 || state == 3 || state == 0))
		{
			if(Ani.CurrentAnimation != "Hit" && Ani.CurrentAnimation!="Run")
			{
				Ani.Play("Run");
			}
		}
		if(state == 2)
		{
			if(Ani.CurrentAnimation != "Box" && Ani.CurrentAnimation != "Hit" && Alive)
			{
				Ani.Play("Box");
			}
		}
		if(state == -1)
		{
			Ani.Stop();
		}
		
	}
	public override void TakeDamage(int damage)
	{
		if(Alive)
		{
			HP -= damage;
			GD.Print(HP);
			if(HP<=0)
			{
				GD.Print("Mort");
				Alive = false;
				Ani.Stop();
				Ani.Play("Die");
			}
			else
			{
				Ani.Play("Hit");
			}
		}
	}
}
