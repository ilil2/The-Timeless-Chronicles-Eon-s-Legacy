using Godot;
using System;

public partial class LobbyManager : Control
{
	public static bool LobbyUI = false;
	public static bool JoinGameUI = false;
	public static bool CreateGameUI = false;
	public static int MenuState = 0;

	public static bool JoinGamePressed = false;
	public static string IDJoinGame = "";
	public static bool StartGame = false;

	public static bool InRunning = true;
	
	
	public override void _Ready()
	{
		PackedScene LobbyScene = GD.Load<PackedScene>("res://Scenes/LobbyUI.tscn");
		Control LobbyMenu = LobbyScene.Instantiate<Control>();
		AddChild(LobbyMenu);
	}

	public override void _Process(double delta)
	{
		if (LobbyUI)
		{
			PackedScene LobbyScene = GD.Load<PackedScene>("res://Scenes/LobbyUI.tscn");
			Control LobbyMenu = LobbyScene.Instantiate<Control>();
			AddChild(LobbyMenu);

			MenuState = 0;
			LobbyUI = false;
		}

		if (JoinGameUI)
		{
			PackedScene JoinGameScene = GD.Load<PackedScene>("res://Scenes/JoinGameUI.tscn");
			Control JoinGameMenu = JoinGameScene.Instantiate<Control>();
			AddChild(JoinGameMenu);

			MenuState = 1;
			JoinGameUI = false;
		}

		if (CreateGameUI)
		{
			PackedScene CreateGameScene = GD.Load<PackedScene>("res://Scenes/CreateGameUI.tscn");
			Control CreateGameMenu = CreateGameScene.Instantiate<Control>();
			AddChild(CreateGameMenu);

			MenuState = 2;
			CreateGameUI = false;
		}

		if (!InRunning)
		{
			QueueFree();
		}
	}
}
