using Godot;
using System;
using System.Collections.Generic;

public partial class LobbyManager : Control
{
	public static bool LobbyUI_ = false;
	public static bool JoinGameUI_ = false;
	public static bool CreateGameUI_ = false;
	public static int MenuState = 0;

	public static bool JoinGamePressed = false;
	public static bool BackButtonPressed = false;
	public static bool CreateButtonPressed = false;
	public static string IDJoinGame = "";
	public static string IDConnectGame = "";
	public static bool StartGame = false;
	public static bool ValidID = false;
	public static string IDError = "";

	public static string[] NamePlayer = new string[4];

	public static bool InRunning = true;
	public static bool JoinGameWithID = false;
	public static bool kill = false;
	
	public DirectionalLight3D Sun;
	
	public override void _Ready()
	{
		PackedScene LobbyScene = GD.Load<PackedScene>("res://Scenes/UI/LobbyUI.tscn");
		Control LobbyMenu = LobbyScene.Instantiate<Control>();
		AddChild(LobbyMenu);
		GetNode<AnimationPlayer>("Lobby3D/AnimationPlayer").Play("Enter");
		Sun = GetNode<DirectionalLight3D>("Lobby3D/Sun");
	}

	public override void _Process(double delta)
	{
		RotateSun();
		IDConnectGame = GameManager.IDGame;
		ValidID = GameManager.ValidIDGame;
		
		if (LobbyUI_)
		{
			PackedScene LobbyScene = GD.Load<PackedScene>("res://Scenes/UI/LobbyUI.tscn");
			Control LobbyMenu = LobbyScene.Instantiate<Control>();
			AddChild(LobbyMenu);

			MenuState = 0;
			LobbyUI_ = false;
		}
		else if (JoinGameUI_)
		{
			PackedScene JoinGameScene = GD.Load<PackedScene>("res://Scenes/UI/JoinGameUI.tscn");
			Control JoinGameMenu = JoinGameScene.Instantiate<Control>();
			AddChild(JoinGameMenu);

			MenuState = 1;
			JoinGameUI_ = false;
		}
		else if (CreateGameUI_)
		{
			PackedScene CreateGameScene = GD.Load<PackedScene>("res://Scenes/UI/CreateGameUI.tscn");
			Control CreateGameMenu = CreateGameScene.Instantiate<Control>();
			AddChild(CreateGameMenu);

			MenuState = 2;
			CreateGameUI_ = false;
			CreateGameUI.StartButtonVisible = true;
		}
		else if(JoinGameWithID)
		{
			PackedScene CreateGameScene = GD.Load<PackedScene>("res://Scenes/UI/CreateGameUI.tscn");
			Control CreateGameMenu = CreateGameScene.Instantiate<Control>();
			AddChild(CreateGameMenu);

			MenuState = 2;
			CreateGameUI.StartButtonVisible = false;
			JoinGameWithID = false;
			
			JoinGamePressed = false;
			CreateButtonPressed = false;
			StartGame = false;

			InRunning = true;
			GameManager.LobbyReset = false;
		}

		if (!InRunning)
		{
			QueueFree();
		}
		
		if (GameManager.LobbyReset)
		{
			JoinGamePressed = false;
			BackButtonPressed = false;
			CreateButtonPressed = false;
			StartGame = false;

			InRunning = true;
			GameManager.LobbyReset = false;
		}
	}
	public Vector2 GetMouseAngle()
	{
		Vector2 screenSize = GetViewportRect().Size / 2; // Taille de l'écran divisée par 2
		Vector2 mousePosition = GetGlobalMousePosition(); // Position de la souris globale

		// Calculer la position de la souris avec (0, 0) au milieu de l'écran
		Vector2 centeredPosition = mousePosition - screenSize;

		// Calculer l'angle en radians
		float angleX = Mathf.Atan2(centeredPosition.X, screenSize.X);
		float angleY = Mathf.Atan2(centeredPosition.Y, screenSize.Y);


		return new Vector2(angleX, angleY);
	}
	public void RotateSun()
	{
		Vector2 Rotation = GetMouseAngle();
		Sun.Rotation = new Vector3(Sun.Rotation.X,-Rotation.X/10,0);
	}
}
