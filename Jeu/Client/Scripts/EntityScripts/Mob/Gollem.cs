using Godot;
using System;

public partial class Gollem : MobScript
{
	public override void _Ready()
	{
		Ready();
		speed = 3;
		DistAtk = 2;
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
			if(Ani.CurrentAnimation!="Walk")
			{
				Ani.Play("Walk");
			}
		}
		if(state == 2)
		{
			int rand = new Random().Next(0,2);
			if((Ani.CurrentAnimation != "Atk" && Ani.CurrentAnimation != "Atk2") && Alive)
			{
				if(rand==0)
				{
					Ani.Play("Atk");
				}
				else
				{
					Ani.Play("Atk2");
				}	
			}
		}
		if(state == -1)
		{
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
				Ani.Play("Death");
			}
			
		}
	}
}