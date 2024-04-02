using Godot;
using System;
using System.Collections.Generic;

public partial class JoinGameUI : Control
{
	private Button _backButton;
	private Button _joinGameButton;
	
	private LineEdit _gameID;
	private TextMesh _gameIDMesh;
	
	private TextMesh _title;
	private TextMesh _backButtonText;
	private TextMesh _idError;
	
	private AnimationPlayer _animationPlayer;

	private Dictionary<string, string> _languageDict;
	
	public override void _Ready()
	{
		_backButton = GetNode<Button>("BackButton");
		_joinGameButton = GetNode<Button>("JoinButton");
		_animationPlayer = GetParent().GetNode<AnimationPlayer>("Lobby3D/AnimationPlayer");
		_gameIDMesh = GetParent().GetNode<MeshInstance3D>("Lobby3D/JoinGame/JoinGameIDLine/TextMesh").Mesh as TextMesh;
		_idError = GetParent().GetNode<MeshInstance3D>("Lobby3D/JoinGame/JoinTextError").Mesh as TextMesh;
		_gameID = GetNode<LineEdit>("JoinGameIDLine");
		
		Translation();
	}
	
	private void Translation()
	{
		int language = GameManager.SettingsManager.GetAllSettings()["language"];
		_languageDict = GameManager.LanguageManager.GetLanguage(language);
		
		_title = GetParent().GetNode<MeshInstance3D>("Lobby3D/JoinGame/JoinTextMenu").Mesh as TextMesh;
		_backButtonText = GetParent().GetNode<MeshInstance3D>("Lobby3D/JoinGame/BackButton/TextMesh").Mesh as TextMesh;
		
		_title.Text = _languageDict["joinGameMenuTitle"];
		_backButtonText.Text = _languageDict["joinGameMenuBackButton"];
	}
	

	public override void _Process(double delta)
	{
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
				_animationPlayer.Play("JoinGame-CreateGame");
				LobbyManager.JoinGamePressed = false;
				LobbyManager.JoinGameWithID = true;
				QueueFree();
			}
			else
			{
				_animationPlayer.Play("JoinGame-Error");
			}
		}
		
		_idError.Text = LobbyManager.IDError;
		
		if (_gameID.Text == "")
		{
			((StandardMaterial3D)_gameIDMesh.Material).AlbedoColor = new Color(0.5f, 0.5f, 0.5f);
			if (_gameID.HasFocus())
			{
				_gameIDMesh.Text = "";
			}
			else
			{
				_gameIDMesh.Text = _languageDict["joinGameMenuGameID"];
			}
		}
		else
		{
			((StandardMaterial3D)_gameIDMesh.Material).AlbedoColor = new Color(0, 0, 0);
			_gameIDMesh.Text = _gameID.Text;
		}
	}
}
