using Godot;
using System;

public partial class Mummy : MobScript
{
	public override void _Ready()
	{
		Ready();
		DistAtk = 2;
		HP = 40;
	}
	public override void _PhysicsProcess(double delta)
	{
		PhysicsProcess(delta);
	}
	public override void _Process(double delta) 
	{
		Process(delta);
		if(Alive&&(state == 1 || state == 3 || state == 0))
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
	public override void TakeDamage(int damage)
	{
		GameManager.InfoJoueur["ia"] += $"{ID}°TK§{damage}=";
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
			else
			{
				Ani.Play("Hit");
			}
		}
	}
}
