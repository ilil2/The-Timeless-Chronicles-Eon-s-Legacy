using Godot;
using System;

public partial class FogSkeleton : MobScript
{
	private AnimationTree AniTree;
	private bool Fog;
	private bool IsHide = true;
	public override void _Ready()
	{
		Ready();
		AniTree = GetNode<AnimationTree>("AnimationTree");
	}
	public override void _PhysicsProcess(double delta)
	{
		PhysicsProcess(delta);
		Fog = (GetParent() as TestFog).Fog;
		CanDo = !IsHide;
	}
	public override void _Process(double delta) 
	{
		Process(delta);
		if(!Fog && !IsHide)
		{
			AniTree.Set("parameters/conditions/Hide",true);
			AniTree.Set("parameters/conditions/Show",false);
			IsHide = true;
		}
		else if(Fog && IsHide)
		{
			AniTree.Set("parameters/conditions/Hide",false);
			AniTree.Set("parameters/conditions/Show",true);
			IsHide = false;
		}
		if(!IsHide&&Alive&&(state == 1 || state == 3 || state == 0))
		{
			if(Ani.CurrentAnimation != "Hit" && Ani.CurrentAnimation!="Walk")
			{
				Ani.Play("Walk");
			}
		}
		if(!IsHide&&state == 2)
		{
			if(Ani.CurrentAnimation != "Atk" && Ani.CurrentAnimation != "Hit" && Alive)
			{
				Ani.Play("Atk");
			}
		}
		if(!IsHide&&state == -1)
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
