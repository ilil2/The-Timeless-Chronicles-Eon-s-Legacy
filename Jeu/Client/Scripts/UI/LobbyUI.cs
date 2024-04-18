using Godot;
using System;
using System.Collections.Generic;

public partial class LobbyUI : Control
{
	private Button _joinGameButton;
	private Button _createGameButton;
	
	private TextMesh _title;
	private TextMesh _createButtonText;
	private TextMesh _joinButtonText;
	
	private AnimationPlayer _animationPlayer;

	public override void _Ready()
	{
		_joinGameButton = GetNode<Button>("JoinGameButton");
		_createGameButton = GetNode<Button>("CreateGameButton");
		_animationPlayer = GetParent().GetNode<AnimationPlayer>("Lobby3D/AnimationPlayer");
		
		_title = GetParent().GetNode<MeshInstance3D>("Lobby3D/Lobby/LobbyMenuText").Mesh as TextMesh;
		_createButtonText = GetParent().GetNode<MeshInstance3D>("Lobby3D/Lobby/CreateGameButton/TextMesh").Mesh as TextMesh;
		_joinButtonText = GetParent().GetNode<MeshInstance3D>("Lobby3D/Lobby/JoinGameButton/TextMesh").Mesh as TextMesh;
		
		Translation();
	}

	private void Translation()
	{
		int language = GameManager.SettingsManager.GetAllSettings()["language"];
		Dictionary<string, string> languageDict = GameManager.LanguageManager.GetLanguage(language);
		
		_title.Text = languageDict["lobbyMenuTitle"];
		_createButtonText.Text = languageDict["lobbyMenuCreateGame"];
		_joinButtonText.Text = languageDict["lobbyMenuJoinGame"];
	}

	public override void _Process(double delta)
	{
		if (_joinGameButton.ButtonPressed)
		{
			_animationPlayer.Play("Lobby-JoinGame");
			LobbyManager.JoinGameUI_ = true;
			QueueFree();
		}

		if (_createGameButton.ButtonPressed)
		{
			_animationPlayer.Play("Lobby-CreateGame");
			LobbyManager.CreateGameUI_ = true;
			QueueFree();
		}
	}
}
