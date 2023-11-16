using Godot;
using System;

public partial class CreateGameUI : Control
{
	//Variables des boutons
	private Button _backButton;
	private Button _startGameButton;
	
	public static Label IDGame;
	
	public override void _Ready()
	{
		_backButton = GetNode<Button>("BackButton");
		_startGameButton = GetNode<Button>("StartGameButton");
		LobbyManager.CreateButtonPressed = true;
		
		IDGame = GetNode<Label>("IDGameText");
	}

	public override void _Process(double delta)
	{
		if (_backButton.ButtonPressed)
		{
			LobbyManager.LobbyUI = true;
			LobbyManager.BackButtonPressed = true;
			QueueFree();
		}

		if (_startGameButton.ButtonPressed)
		{
			LobbyManager.StartGame = true;
		}
		IDGame.Text = LobbyManager.IDJoinGame;
	}
}
