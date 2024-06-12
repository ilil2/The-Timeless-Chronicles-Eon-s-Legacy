using Godot;
using System;

public partial class Mummy : MobScript
{
	
	private Label3D Tar;
	public override void _Ready()
	{
		DistAtk = 2;
		HP = 40;
		Tar = GetNode<Label3D>("TARGET");
		GetNode<Label3D>("ID").Text = $"ID: {ID}";
		Ready();
	}
	public override void _PhysicsProcess(double delta)
	{
		PhysicsProcess(delta);
		if(LastPlayer!=Player)
		{
			if(Player==null)
			{
				Tar.Text = "Player: null";
			}
			else
			{
				Tar.Text = $"Player: {Player.Name}";
			}
		}
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
}
