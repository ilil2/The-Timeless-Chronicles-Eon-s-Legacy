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
	public bool CanExit = false;
	public AnimationPlayer Ani;
	[Export] public bool CamOnPlayer = false;
	public string LoadingStage = "rien pour l'instant";
	[Export]public bool SkillsMenu = false;
	
	public int Step()
	{
		return step;
	}
	public void SyncCam()
	{
		if(GameManager.Joueur1!=null)
		{
			GameManager.Joueur1.GetNode<Camera3D>("CameraPlayer/h/v/Camera3D").Current = CamOnPlayer;
		}
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
					Player.AddChild(pointer1);
					(pointer1 as pointer).SetTarget(GameManager.Joueur2);
					GD.Print($"nb J = {GameManager._nbJoueur}");
					break;
				case 3:
					Node3D p2ointer1 = GD.Load<PackedScene>($"res://Scenes/HUD/pointer.tscn").Instantiate<Node3D>();
					p2ointer1.Name = "Pointer";
					Player.AddChild(p2ointer1);
					(p2ointer1 as pointer).SetTarget(GameManager.Joueur2);
					Node3D p2ointer2 = GD.Load<PackedScene>($"res://Scenes/HUD/pointer.tscn").Instantiate<Node3D>();
					p2ointer2.Name = "Pointer";
					Player.AddChild(p2ointer2);
					(p2ointer2 as pointer).SetTarget(GameManager.Joueur3);
					GD.Print($"nb J = {GameManager._nbJoueur}");
					break;
				case 4:
					Node3D p3ointer1 = GD.Load<PackedScene>($"res://Scenes/HUD/pointer.tscn").Instantiate<Node3D>();
					p3ointer1.Name = "Pointer";
					Player.AddChild(p3ointer1);
					(p3ointer1 as pointer).SetTarget(GameManager.Joueur2);
					Node3D p3ointer2 = GD.Load<PackedScene>($"res://Scenes/HUD/pointer.tscn").Instantiate<Node3D>();
					p3ointer2.Name = "Pointer";
					Player.AddChild(p3ointer2);
					(p3ointer2 as pointer).SetTarget(GameManager.Joueur3);
					Node3D p3ointer3 = GD.Load<PackedScene>($"res://Scenes/HUD/pointer.tscn").Instantiate<Node3D>();
					p3ointer3.Name = "Pointer";
					Player.AddChild(p3ointer3);
					(p3ointer3 as pointer).SetTarget(GameManager.Joueur4);
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
	
	public void ShowSkillsMenu()
	{
		Control skillmenu = GD.Load<PackedScene>("res://Scenes/HUD/SelectSkills.tscn").Instantiate<Control>();
		AddChild(skillmenu);
		Input.MouseMode = Input.MouseModeEnum.Visible;	
	}
}
