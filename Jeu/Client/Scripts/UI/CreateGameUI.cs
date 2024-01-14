using Godot;
using System;
using System.Collections.Generic;

public partial class CreateGameUI : Control
{
	//Variables des boutons
	private Button _backButton;
	private Button _startGameButton;
	
	private Label _title;
	private Label _idGame;
	private Label _backButtonText;
	private Label _startButtonText;
	private Label _namePlayer1;
	private Label _namePlayer2;
	private Label _namePlayer3;
	private Label _namePlayer4;
	
	public static bool StartButtonVisible = true;
	
	private float _screenDefalutWidth = 1152;
	private float _titleDefaultSize = 40;
	private float _textDefaultSize = 25;
	private float _buttonDefaultSize = 20;
	
	//Language
	private int _language;
	private Dictionary<string, string> _languageDict;
	
	public override void _Ready()
	{
		_backButton = GetNode<Button>("BackButton");
		_startGameButton = GetNode<Button>("StartGameButton");
		LobbyManager.CreateButtonPressed = true;
		
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
		_idGame = GetNode<Label>("IDGameText");
		_backButtonText = GetNode<Label>("BackButton/BackButtonText");
		_startButtonText = GetNode<Label>("StartGameButton/StartButtonText");
		_namePlayer1 = GetNode<Label>("Player1Text");
		_namePlayer2 = GetNode<Label>("Player2Text");
		_namePlayer3 = GetNode<Label>("Player3Text");
		_namePlayer4 = GetNode<Label>("Player4Text");
		
		_title.LabelSettings.FontSize = (int)(_titleDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_idGame.LabelSettings.FontSize = (int)(_textDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_backButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_startButtonText.LabelSettings.FontSize = (int)(_textDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_namePlayer1.LabelSettings.FontSize = (int)(_textDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_namePlayer2.LabelSettings.FontSize = (int)(_textDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_namePlayer3.LabelSettings.FontSize = (int)(_textDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_namePlayer4.LabelSettings.FontSize = (int)(_textDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
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
		
		_idGame.Text = _languageDict["createGameMenuID"] + LobbyManager.IDConnectGame;
		_namePlayer1.Text = _languageDict["createGameMenuPlayer1"] + LobbyManager.NamePlayer[0];
		_namePlayer2.Text = _languageDict["createGameMenuPlayer2"] + LobbyManager.NamePlayer[1];
		_namePlayer3.Text = _languageDict["createGameMenuPlayer3"] + LobbyManager.NamePlayer[2];
		_namePlayer4.Text = _languageDict["createGameMenuPlayer4"] + LobbyManager.NamePlayer[3];
	}
}
