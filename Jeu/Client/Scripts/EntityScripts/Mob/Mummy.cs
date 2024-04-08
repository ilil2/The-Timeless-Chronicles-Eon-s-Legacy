using Godot;
using System;

public partial class Mummy : MobScript
{
	public override void _Ready()
	{
		Ready();
		DistAtk = 2;
	}
	public override void _PhysicsProcess(double delta)
	{
		PhysicsProcess(delta);
	}
	public override void _Process(double delta) 
	{
		Process(delta);
		if(state == 1 || state == 3 || state == 0)
		{
			if(Ani.CurrentAnimation != "Hit" && Ani.CurrentAnimation!="Walk")
			{
				Ani.Play("Walk");
			}
		}
		if(state == 2)
		{
			if(Ani.CurrentAnimation != "Spell" && Ani.CurrentAnimation != "Hit" && Alive)
			{
				Ani.Play("Spell");
			}
		}
		if(state == -1)
		{
			Ani.Stop();
		}
		
	}
	public override void TakeDamage(int damage)
	{
		HP -= damage;
		GD.Print(HP);
		//Hp.Text = $"HP: {HP}/{HpMax}";
		Ani.Play("Hit");
		if(HP<=0)
		{
			Alive = false;
			Ani.Stop();
			Ani.Play("Die");
			//SetUpDeath();
		}
	}
}
