using Godot;
using System;

public partial class CreateGameUI : Control
{
	//Variables des boutons
	private Button _backButton;
	private Button _startGameButton;
	
	public override void _Ready()
	{
		_backButton = GetNode<Button>("BackButton");
		_startGameButton = GetNode<Button>("StartGameButton");
	}

	public override void _Process(double delta)
	{
		if (_backButton.ButtonPressed)
		{
			LobbyManager.LobbyUI = true;
			QueueFree();
		}

		if (_startGameButton.ButtonPressed)
		{
			LobbyManager.StartGame = true;
		}
	}
}
