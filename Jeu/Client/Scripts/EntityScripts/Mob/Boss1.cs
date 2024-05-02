using Godot;
using System;

public partial class Boss1 : MobScript
{
	private IMap map;
	public override void _Ready()
	{
		Ready();
		speed = 3;
		HP = 20;
		map = (IMap)GetParent();
	}
	public override void _PhysicsProcess(double delta)
	{
		PhysicsProcess(delta);
		if(HP<=0)
		{
			map.CanExit = true;
		}
	}
	public override void _Process(double delta) 
	{
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

