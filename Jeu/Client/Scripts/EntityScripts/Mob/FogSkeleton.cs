using Godot;
using System;

public partial class FogSkeleton : MobScript
{
	private AnimationTree AniTree;
	private bool Fog;
	[Export] private bool IsHide = true;
	private AnimationNodeStateMachinePlayback PlayBack;
	public override void _Ready()
	{
		Ready();
		AniTree = GetNode<AnimationTree>("AnimationTree");
		
	}
	public override void _PhysicsProcess(double delta)
	{
		if (PlayBack == null)
		{
			PlayBack = (AnimationNodeStateMachinePlayback)AniTree.Get("parameters/playback");
		}
		PhysicsProcess(delta);
		Fog = (GetParent() as TestFog).Fog;
		CanDo = !IsHide;
	}
	public override void _Process(double delta) 
	{
		Process(delta);
		GD.Print(IsHide);
		if(!Fog && !IsHide)
		{
			PlayBack.Travel("StandDown");
			IsHide = true;
		}
		else if(Fog && IsHide)
		{
			PlayBack.Travel("StandUp");
		}
		if(!IsHide&&Alive&&(state == 1 || state == 3 || state == 0))
		{
			if(Ani.CurrentAnimation != "Hit" && Ani.CurrentAnimation!="Walk")
			{
				PlayBack.Travel("Walk");
			}
		}
		if(!IsHide&&state == 2)
		{
			if(Ani.CurrentAnimation != "Atk" && Ani.CurrentAnimation != "Hit" && Alive)
			{
				PlayBack.Travel("Atk");
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
				PlayBack.Travel("Death");
			}
			else
			{
				Ani.Play("Hit");
			}
		}
	}

}

