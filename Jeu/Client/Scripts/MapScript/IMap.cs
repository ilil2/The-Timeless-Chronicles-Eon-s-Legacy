using Godot;
using System;
using System.Collections.Generic;

using JeuClient.Scripts.PlayerScripts;

public abstract partial class IMap : Node3D
{
	protected bool MapReady = false;
	protected bool SetUp = false;
	protected Random Rand = new Random(69);
	protected Random Rand2 = new Random(69);
	protected int step = 0;
	public bool PlayerSet = false;
	public string LoadingStage = "rien pour l'instant";
	public int Step()
	{
		return step;
	}
	public abstract List<(int,int,int)> GetSpawnLocation();
	public bool MapIsReady()
	{
		return MapReady;
	}
	public abstract void DebugMode(CharacterBody3D Player, bool DebugMode);
	public void SetSeed(int seed, int seed2)
	{
		Rand = new Random(seed);
		Rand2 = new Random(seed2);
		GD.Print($"Seed1 set : {seed}");
		GD.Print($"Seed2 set : {seed2}");
	}
	public void SetUpCursor(CharacterBody3D Player, bool t)
	{
		if(t)
		{
			switch (GameManager._nbJoueur)
			{
				case 2:
					Node3D pointer1 = GD.Load<PackedScene>($"res://Scenes/HUD/pointer.tscn").Instantiate<Node3D>();
					pointer1.Name = "Pointer";
					(pointer1 as pointer).SetTarget(GameManager.Joueur2);
					Player.AddChild(pointer1);
					GD.Print($"nb J = {GameManager._nbJoueur}");
					break;
				default:
					GD.Print($"nb J = {GameManager._nbJoueur}");
					break;
					
			}
		}
		else
		{
			switch (GameManager._nbJoueur)
			{
				case 2:
					Player.RemoveChild(Player.GetNode<Node3D>("Pointer"));
					GD.Print($"nb J = {GameManager._nbJoueur}");
					break;
				default:
					GD.Print($"nb J = {GameManager._nbJoueur}");
					break;
					
			}
		}
			
	}
}
