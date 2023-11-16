using Godot;
using System;

public partial class JoinGameUI : Control
{
	
	//Variables des boutons
	private Button _backButton;
	private Button _JoinGameButton;
	
	//Variable de la zone de texte
	private LineEdit _GameID;
	
	public override void _Ready()
	{
		//Recuperation des elements du menu
		_backButton = GetNode<Button>("BackButton");
		_JoinGameButton = GetNode<Button>("JoinButton");
		_GameID = GetNode<LineEdit>("JoinGameIDLine");
	}

	public override void _Process(double delta)
	{
		//Verifie un bouton est presser et lequel c'est.
		if (_backButton.ButtonPressed)
		{
			LobbyManager.ValidID = false;
			LobbyManager.LobbyUI = true;
			QueueFree();
		}

		if (_JoinGameButton.ButtonPressed)
		{
			LobbyManager.IDJoinGame = _GameID.Text;
			LobbyManager.JoinGamePressed = true;
			if (LobbyManager.ValidID)
			{
				LobbyManager.JoinGamePressed = false;
			}
		}
		
	}
}
