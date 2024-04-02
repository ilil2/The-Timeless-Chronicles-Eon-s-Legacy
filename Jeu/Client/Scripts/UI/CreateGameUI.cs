using Godot;
using System;
using System.Collections.Generic;

public partial class CreateGameUI : Control
{
	private Button _backButton;
	private Button _startGameButton;
	
	private TextMesh _idGame;
	private TextMesh _namePlayer1;
	private TextMesh _namePlayer2;
	private TextMesh _namePlayer3;
	private TextMesh _namePlayer4;
	private TextMesh _title;
	private TextMesh _backButtonText;
	private TextMesh _startButtonText;
	
	public static bool StartButtonVisible = true;
	
	private int _language;
	private Dictionary<string, string> _languageDict;
	
	private AnimationPlayer _animationPlayer;
	private string LastAnimation = "";
	
	public override void _Ready()
	{
		_backButton = GetNode<Button>("BackButton");
		_startGameButton = GetNode<Button>("StartGameButton");
		LobbyManager.CreateButtonPressed = true;
		
		_animationPlayer = GetParent().GetNode<AnimationPlayer>("Lobby3D/AnimationPlayer");
		
		_idGame = GetParent().GetNode<MeshInstance3D>("Lobby3D/CreateGame/IDGameText").Mesh as TextMesh;
		_namePlayer1 = GetParent().GetNode<MeshInstance3D>("Lobby3D/CreateGame/Player1Text").Mesh as TextMesh;
		_namePlayer2 = GetParent().GetNode<MeshInstance3D>("Lobby3D/CreateGame/Player2Text").Mesh as TextMesh;
		_namePlayer3 = GetParent().GetNode<MeshInstance3D>("Lobby3D/CreateGame/Player3Text").Mesh as TextMesh;
		_namePlayer4 = GetParent().GetNode<MeshInstance3D>("Lobby3D/CreateGame/Player4Text").Mesh as TextMesh;
		
		_language = GameManager.SettingsManager.GetAllSettings()["language"];
		_languageDict = GameManager.LanguageManager.GetLanguage(_language);
		Translation();
	}
	
	private void Translation()
	{
		_title = GetParent().GetNode<MeshInstance3D>("Lobby3D/CreateGame/CreateTextMenu").Mesh as TextMesh;
		_backButtonText = GetParent().GetNode<MeshInstance3D>("Lobby3D/CreateGame/BackButton/TextMesh").Mesh as TextMesh;
		_startButtonText = GetParent().GetNode<MeshInstance3D>("Lobby3D/CreateGame/StartGameButton/TextMesh").Mesh as TextMesh;
		
		_title.Text = _languageDict["createGameMenuTitle"];
		_backButtonText.Text = _languageDict["createGameMenuBackButton"];
		_startButtonText.Text = _languageDict["createGameMenuStartGame"];
	}

	public override void _Process(double delta)
	{
		if (!StartButtonVisible)
		{
			((StandardMaterial3D)_startButtonText.Material).AlbedoColor = new Color(0.3f, 0.3f, 0.3f);
			_startGameButton.Disabled = true;
			_startGameButton.MouseDefaultCursorShape = CursorShape.Forbidden;
		}

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
		}
		if(_animationPlayer.CurrentAnimation == "" && LastAnimation == "StartGame")
		{
			LobbyManager.StartGame = true;
		}
		LastAnimation = _animationPlayer.CurrentAnimation;
		
		
		_idGame.Text = _languageDict["createGameMenuID"] + LobbyManager.IDConnectGame;
		_namePlayer1.Text = _languageDict["createGameMenuPlayer1"] + LobbyManager.NamePlayer[0];
		_namePlayer2.Text = _languageDict["createGameMenuPlayer2"] + LobbyManager.NamePlayer[1];
		_namePlayer3.Text = _languageDict["createGameMenuPlayer3"] + LobbyManager.NamePlayer[2];
		_namePlayer4.Text = _languageDict["createGameMenuPlayer4"] + LobbyManager.NamePlayer[3];
	}
}
