using Godot;
using System;

public partial class JoinGameUI : Control
{
	
	//Variables des boutons
	private Button _backButton;
	private Button _JoinGameButton;
	
	//Variable de la zone de texte
	private LineEdit _GameID;
	
	//variable des label
	private Label _IDError;
	
	public override void _Ready()
	{
		//Recuperation des elements du menu
		_backButton = GetNode<Button>("BackButton");
		_JoinGameButton = GetNode<Button>("JoinButton");
		_GameID = GetNode<LineEdit>("JoinGameIDLine");
		_IDError = GetNode<Label>("IDErrorText");
	}

	public override void _Process(double delta)
	{
		//Verifie un bouton est presser et lequel c'est.
		if (_backButton.ButtonPressed)
		{
			LobbyManager.ValidID = false;
			LobbyManager.LobbyUI_ = true;
			QueueFree();
		}

		if (_JoinGameButton.ButtonPressed)
		{
			LobbyManager.IDJoinGame = _GameID.Text;
			LobbyManager.JoinGamePressed = true;
			if (LobbyManager.ValidID)
			{
				LobbyManager.JoinGamePressed = false;
				LobbyManager.JoinGameWithID = true;
				QueueFree();
			}
		}
		
		if (LobbyManager.kill)
		{
			LobbyManager.kill = false;
			LobbyManager.ValidID = false;
			LobbyManager.LobbyUI_ = true;
			QueueFree();
		}
		
		_IDError.Text = LobbyManager.IDError;
	}
}
