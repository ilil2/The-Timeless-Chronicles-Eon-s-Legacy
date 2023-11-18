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
	
	public static string s1 = "";
	public static string s2 = "";
	public static string s3 = "";
	public static string s4 = "";
	public static List<string> NamePlayer = new List<string> {s1,s2,s3,s4};

	public static bool InRunning = true;
	public static bool JoinGameWithID = false;
	
	public override void _Ready()
	{
		PackedScene LobbyScene = GD.Load<PackedScene>("res://Scenes/LobbyUI.tscn");
		Control LobbyMenu = LobbyScene.Instantiate<Control>();
		AddChild(LobbyMenu);
	}

	public override void _Process(double delta)
	{
		IDConnectGame = GameManager.IDGame;
		ValidID = GameManager.ValidIDGame;
		
		if (LobbyUI_)
		{
			PackedScene LobbyScene = GD.Load<PackedScene>("res://Scenes/LobbyUI.tscn");
			Control LobbyMenu = LobbyScene.Instantiate<Control>();
			AddChild(LobbyMenu);

			MenuState = 0;
			LobbyUI_ = false;
		}

		if (JoinGameUI_)
		{
			PackedScene JoinGameScene = GD.Load<PackedScene>("res://Scenes/JoinGameUI.tscn");
			Control JoinGameMenu = JoinGameScene.Instantiate<Control>();
			AddChild(JoinGameMenu);

			MenuState = 1;
			JoinGameUI_ = false;
		}

		if (CreateGameUI_)
		{
			PackedScene CreateGameScene = GD.Load<PackedScene>("res://Scenes/CreateGameUI.tscn");
			Control CreateGameMenu = CreateGameScene.Instantiate<Control>();
			AddChild(CreateGameMenu);

			MenuState = 2;
			CreateGameUI_ = false;
		}
		
		if(JoinGameWithID)
		{
			PackedScene CreateGameScene = GD.Load<PackedScene>("res://Scenes/CreateGameUI.tscn");
			Control CreateGameMenu = CreateGameScene.Instantiate<Control>();
			AddChild(CreateGameMenu);

			MenuState = 2;
			CreateGameUI.StartButtonVisible = false;
			JoinGameWithID = false;
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
			IDJoinGame = "";
			IDConnectGame = "";
			StartGame = false;

			InRunning = true;
			GameManager.LobbyReset = false;
		}
	}
}
