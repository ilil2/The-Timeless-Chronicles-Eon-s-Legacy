using Godot;
using System;
using JeuClient.Scripts.EntityScripts.Mob;
using JeuClient.Scripts.PlayerScripts;

public partial class Boss1 : Boss
{
	private PackedScene Son = GD.Load<PackedScene>("res://Scenes/EntityScenes/Mob/Boss2.tscn");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Ready();
		DistAtk = (int)(DistAtk*Scale.X);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Process(delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		PhysicsProcess(delta);
	}

	public override void TakeDamage(int damage, int id, bool send = true)
	{
		Agro[id] += 10;
		if(Alive)
		{
			HP -= damage;
			GD.Print("HP" + HP);
			if(HP<=0)
			{
				GD.Print("Mort");
				Alive = false;
				Ani.Play("Death");
				GameManager.Gold += 10;
				GameManager.xp += 1;
				GameManager.InfoJoueur[$"ia"] += $"{ID}°{42}°{Position.X}?{Position.Z}°{(GameManager.Joueur1 as ClassScript).Id}=";
			}
			else
			{
				//Ani.Play("Hit");
			}
			if (send)
			{
				GameManager.InfoJoueur[$"ia"]  += $"{ID}°TK§{damage}°{Position.X}?{Position.Z}=";
			}
			
		}
	}
	
	public override void AtDeath()
	{
		//Do nothing
	}
}
