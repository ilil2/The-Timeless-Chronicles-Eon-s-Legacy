using Godot;
using System;

public partial class CreateGameUI : Control
{
	//Variables des boutons
	private Button _backButton;
	private Button _startGameButton;
	
	public static Label IDGame;

	public static bool StartButtonVisible = true; 
	
	private static Label NamePlayer1;
	private static Label NamePlayer2;
	private static Label NamePlayer3;
	private static Label NamePlayer4;

	
	public override void _Ready()
	{
		_backButton = GetNode<Button>("BackButton");
		_startGameButton = GetNode<Button>("StartGameButton");
		LobbyManager.CreateButtonPressed = true;
		
		IDGame = GetNode<Label>("IDGameText");
		
		NamePlayer1 = GetNode<Label>("Player1Text");
		NamePlayer2 = GetNode<Label>("Player2Text");
		NamePlayer3 = GetNode<Label>("Player3Text");
		NamePlayer4 = GetNode<Label>("Player4Text");
	}

	public override void _Process(double delta)
	{
		_startGameButton.Visible = StartButtonVisible;
		
		if (_backButton.ButtonPressed)
		{
			LobbyManager.LobbyUI_ = true;
			LobbyManager.BackButtonPressed = true;
			QueueFree();
		}

		if (_startGameButton.ButtonPressed)
		{
			LobbyManager.StartGame = true;
		}
		
		IDGame.Text = "Game ID: " + LobbyManager.IDConnectGame;
		NamePlayer1.Text = "Player 1: " + LobbyManager.NamePlayer[0];
		NamePlayer2.Text = "Player 2: " + LobbyManager.NamePlayer[1];
		NamePlayer3.Text = "Player 3: " + LobbyManager.NamePlayer[2];
		NamePlayer4.Text = "Player 4: " + LobbyManager.NamePlayer[3];
	}
}
