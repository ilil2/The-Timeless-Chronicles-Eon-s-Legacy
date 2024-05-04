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
		speed = 5;
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
		Fog = (GetParent() as MapLvl1Script).FogState == 2;
		CanDo = !IsHide;
		if(PlayBack.GetCurrentNode()=="Atk"||PlayBack.GetCurrentNode()=="Hit")
		{
			CanDo = false;
		}
	}
	public override void _Process(double delta)
	{
		Process(delta);
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
			if(Ani.CurrentAnimation != "Hit" && Alive)
			{
				PlayBack.Travel("Atk");
				
			}
		}
		if(!IsHide&&state == -1)
		{
			PlayBack.Travel("Idle");
		}
		
	}
  
	public override void TakeDamage(int damage, bool send = true)
	{
		if(Alive)
		{
			HP -= damage;
			if(HP<=0)
			{
				GD.Print("Mort");
				Alive = false;
				Ani.Stop();
				PlayBack.Travel("Death");
				GameManager.Gold += 10;
				GameManager.xp += 1;
			}
			else
			{
				PlayBack.Stop();
				GD.Print("Hit");
				PlayBack.Travel("Hit");
			}

			if (send)
			{
				GameManager.InfoJoueur[$"ia"]  += $"{ID}°TK§{damage}°{Position.X}?{Position.Z}=";
			}
			
		}
	}

}

