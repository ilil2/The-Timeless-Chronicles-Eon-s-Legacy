using Godot;
using System;

public partial class LobbyUI : Control
{
	//Variables des boutons
	private Button _joinGameButton;
	private Button _createGameButton;

	public override void _Ready()
	{
		_joinGameButton = GetNode<Button>("JoinGameButton");
		_createGameButton = GetNode<Button>("CreateGameButton");
	}

	public override void _Process(double delta)
	{
		if (_joinGameButton.ButtonPressed)
		{
			LobbyManager.JoinGameUI = true;
			QueueFree();
		}

		if (_createGameButton.ButtonPressed)
		{
			LobbyManager.CreateGameUI = true;
			QueueFree();
		}
	}
}
