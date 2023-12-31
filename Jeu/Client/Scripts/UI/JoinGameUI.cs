using Godot;
using System;

public partial class JoinGameUI : Control
{
	
	//Variables des boutons
	private Button _backButton;
	private Button _joinGameButton;
	
	//Variable de la zone de texte
	private LineEdit _gameID;
	
	//variable des label
	private Label _title;
	private Label _idError;
	private Label _backButtonText;
	private Label _joinButtonText;
	
	private float _screenDefalutWidth = 1152;
	private float _titleDefaultSize = 40;
	private float _buttonDefaultSize = 20;
	private float _errorDefaultSize = 16;
	
	public override void _Ready()
	{
		//Recuperation des elements du menu
		_backButton = GetNode<Button>("BackButton");
		_joinGameButton = GetNode<Button>("JoinButton");
		_gameID = GetNode<LineEdit>("JoinGameIDLine");
	}
	
	public void OnResize()
	{
		_title = GetNode<Label>("JoinTextMenu");
		_idError = GetNode<Label>("IDErrorText");
		_backButtonText = GetNode<Label>("BackButton/BackButtonText");
		_joinButtonText = GetNode<Label>("JoinButton/JoinButtonText");
		
		_title.LabelSettings.FontSize = (int)(_titleDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_backButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_joinButtonText.LabelSettings.FontSize = (int)(_titleDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_idError.LabelSettings.FontSize = (int)(_errorDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
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

		if (_joinGameButton.ButtonPressed)
		{
			LobbyManager.IDJoinGame = _gameID.Text;
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
		
		_idError.Text = LobbyManager.IDError;
	}
}
