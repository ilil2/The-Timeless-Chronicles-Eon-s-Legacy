using Godot;
using System;

public partial class FogSkeleton : MobScript
{
	private AnimationTree AniTree;
	private bool Fog;
	public override void _Ready()
	{
		Ready();
		AniTree = GetNode<AnimationTree>("AnimationTree");
	}
	public override void _PhysicsProcess(double delta)
	{
		PhysicsProcess(delta);
		Fog = (GetParent() as TestFog).Fog;
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
			else
			{
				Ani.Play("Hit");
			}
		}
	}
}
