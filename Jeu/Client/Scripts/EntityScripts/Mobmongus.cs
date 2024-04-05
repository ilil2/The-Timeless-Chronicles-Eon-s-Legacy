using Godot;
using System;

public partial class Mobmongus : MobScript
{
	private Label3D St;
	private Label3D Hp;
	public override void _Ready()
	{
		Ready();
		St = GetNode<Label3D>("State");
		Hp = GetNode<Label3D>("HP");
		Hp.Text = $"HP: {HP}";
	}
	public override void _PhysicsProcess(double delta)
	{
		PhysicsProcess(delta);
	}
	public override void _Process(double delta) 
	{
		Process(delta);
		//St.Text = $"State: {state}";
		if(state == 2)
		{
			if(Ani.CurrentAnimation != "Hit" && Alive)
			{
				Ani.Play("Hit");
			}
		}
		
	}
	public override void TakeDamage(int damage)
	{
		HP -= damage;
		Hp.Text = $"HP: {HP}";
		if(HP<=0)
		{
			Ani.Stop();
			SetUpDeath();
		}
	}
}
