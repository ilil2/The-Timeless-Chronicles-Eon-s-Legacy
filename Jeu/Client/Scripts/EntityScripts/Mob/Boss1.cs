using Godot;
using System;
using JeuClient.Scripts.EntityScripts.Mob;
using JeuClient.Scripts.PlayerScripts;

public partial class Boss1 : Boss
{
	private PackedScene[] Mob = {GD.Load<PackedScene>("res://Scenes/EntityScenes/Mob/Mummy.tscn"),GD.Load<PackedScene>("res://Scenes/EntityScenes/Mob/Pharaon.tscn"),GD.Load<PackedScene>("res://Scenes/EntityScenes/Mob/FogSkeleton.tscn")};
	[Export] private bool CastSpell;
	[Export] private bool SummonMob;
	private Random Rand;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Ready();
		DistAtk = 10;
		Map = (IMap)GetParent();
		Rand = Map.Rand;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Process(delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		PhysicsProcess(delta);
		if(CastSpell)
		{
			(Map as Boss1Map).MapAtk();
			CastSpell = false;
		}
		if(SummonMob)
		{
			SummonMob = false;
			MobScript SMob = Mob[Rand.Next(0,3)].Instantiate<MobScript>();
			SMob.Position = GlobalPosition+new Vector3(2,0,2);
			Map.AddChild(SMob);
			
		}
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
		Map.CanExit = true;
	}
}
