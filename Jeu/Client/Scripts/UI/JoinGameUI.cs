using Godot;
using System;
using System.Collections.Generic;

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
	private float _lineEditDefaultSize = 30;
	private float _buttonDefaultSize = 20;
	private float _errorDefaultSize = 16;
	
	private AnimationPlayer _animationPlayer;
	
	public override void _Ready()
	{
		//Recuperation des elements du menu
		_backButton = GetNode<Button>("BackButton");
		_joinGameButton = GetNode<Button>("JoinButton");
		_animationPlayer = GetParent().GetNode<AnimationPlayer>("Lobby3D/AnimationPlayer");
		Translation();
	}
	
	private void Translation()
	{
		int language = GameManager.SettingsManager.GetAllSettings()["language"];
		Dictionary<string, string> languageDict = GameManager.LanguageManager.GetLanguage(language);
		
		_title.Text = languageDict["joinGameMenuTitle"];
		_backButtonText.Text = languageDict["joinGameMenuBackButton"];
		_gameID.PlaceholderText = languageDict["joinGameMenuGameID"];
	}
	
	public void OnResize()
	{
		_gameID = GetNode<LineEdit>("JoinGameIDLine");
		_title = GetNode<Label>("JoinTextMenu");
		_idError = GetNode<Label>("IDErrorText");
		_backButtonText = GetNode<Label>("BackButton/BackButtonText");
		_joinButtonText = GetNode<Label>("JoinButton/JoinButtonText");
		
		_gameID.AddThemeFontSizeOverride("font_size", (int)(_lineEditDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth)));
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
			_animationPlayer.Play("JoinGame-Lobby");
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
