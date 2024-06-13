using Godot;
using System;
using JeuClient.Scripts.EntityScripts.Mob;
using JeuClient.Scripts.PlayerScripts;

public partial class Boss2 : Boss
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

	private void SpawnSon()
	{
		if (MaxHP>25)
		{
			for (int i = 0; i < 2; i++)
			{
				GD.Print("NewSpawn");
				Boss2 son = Son.Instantiate<Boss2>();
				GetParent().AddChild(son);
				son.Scale = new Vector3(0.5f, 0.5f, 0.5f);
				son.GlobalPosition = new Vector3(new Random().Next(-10,10)/10.0f,0,0)+GlobalPosition;
				son.MaxHP = MaxHP / 2;
				son.HP = son.MaxHP;
				son.ID = (2 * ID) + i;
			}
		}
	}
	
	public override void AtDeath()
	{
		SpawnSon();
	}
}
