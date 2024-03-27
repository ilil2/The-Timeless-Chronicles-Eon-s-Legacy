using Godot;
using System;
using System.Collections.Generic;

public partial class CreateGameUI : Control
{
	//Variables des boutons
	private Button _backButton;
	private Button _startGameButton;
	
	private Label _title;
	private TextMesh _idGame;
	private Label _backButtonText;
	private Label _startButtonText;
	private TextMesh _namePlayer1;
	private TextMesh _namePlayer2;
	private TextMesh _namePlayer3;
	private TextMesh _namePlayer4;
	
	
	public static bool StartButtonVisible = true;
	
	private float _screenDefalutWidth = 1152;
	private float _titleDefaultSize = 40;
	private float _textDefaultSize = 25;
	private float _buttonDefaultSize = 20;
	
	//Language
	private int _language;
	private Dictionary<string, string> _languageDict;
	
	private AnimationPlayer _animationPlayer;
	
	public override void _Ready()
	{
		_backButton = GetNode<Button>("BackButton");
		_startGameButton = GetNode<Button>("StartGameButton");
		LobbyManager.CreateButtonPressed = true;
		
		_animationPlayer = GetParent().GetNode<AnimationPlayer>("Lobby3D/AnimationPlayer");
		
		//Language
		_language = GameManager.SettingsManager.GetAllSettings()["language"];
		_languageDict = GameManager.LanguageManager.GetLanguage(_language);
		Translation();
	}
	
	private void Translation()
	{
		_title.Text = _languageDict["createGameMenuTitle"];
		_backButtonText.Text = _languageDict["createGameMenuBackButton"];
		_startButtonText.Text = _languageDict["createGameMenuStartGame"];
	}
	
	public void OnResize()
	{
		_title = GetNode<Label>("CreateTextMenu");
		_idGame = GetParent().GetNode<MeshInstance3D>("Lobby3D/CreateGame/IDGameText").Mesh as TextMesh;
		_backButtonText = GetNode<Label>("BackButton/BackButtonText");
		_startButtonText = GetNode<Label>("StartGameButton/StartButtonText");
		_namePlayer1 = GetParent().GetNode<MeshInstance3D>("Lobby3D/CreateGame/Player1Text").Mesh as TextMesh;
		_namePlayer2 = GetParent().GetNode<MeshInstance3D>("Lobby3D/CreateGame/Player2Text").Mesh as TextMesh;
		_namePlayer3 = GetParent().GetNode<MeshInstance3D>("Lobby3D/CreateGame/Player3Text").Mesh as TextMesh;
		_namePlayer4 = GetParent().GetNode<MeshInstance3D>("Lobby3D/CreateGame/Player4Text").Mesh as TextMesh;
		
		_title.LabelSettings.FontSize = (int)(_titleDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_backButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_startButtonText.LabelSettings.FontSize = (int)(_textDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
	}

	public override void _Process(double delta)
	{
		_startGameButton.Visible = StartButtonVisible;

		if (_backButton.ButtonPressed)
		{
			_animationPlayer.Play("CreateGame-Lobby");
			LobbyManager.LobbyUI_ = true;
			LobbyManager.BackButtonPressed = true;
			QueueFree();
		}

		if (_startGameButton.ButtonPressed)
		{
			_animationPlayer.Play("StartGame");
            
			LobbyManager.StartGame = true;
		}
		
		_idGame.Text = _languageDict["createGameMenuID"] + LobbyManager.IDConnectGame;
		_namePlayer1.Text = _languageDict["createGameMenuPlayer1"] + LobbyManager.NamePlayer[0];
		_namePlayer2.Text = _languageDict["createGameMenuPlayer2"] + LobbyManager.NamePlayer[1];
		_namePlayer3.Text = _languageDict["createGameMenuPlayer3"] + LobbyManager.NamePlayer[2];
		_namePlayer4.Text = _languageDict["createGameMenuPlayer4"] + LobbyManager.NamePlayer[3];
	}
}
