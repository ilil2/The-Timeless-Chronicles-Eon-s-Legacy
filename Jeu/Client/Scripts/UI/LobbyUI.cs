using Godot;
using System;

public partial class LobbyUI : Control
{
	//Variables des boutons
	private Button _joinGameButton;
	private Button _createGameButton;
	
	private Label _title;
	private Label _createButtonText;
	private Label _joinButtonText;
	
	private float _screenDefalutWidth = 1152;
	private float _titleDefaultSize = 40;
	private float _buttonDefaultSize = 20;

	public override void _Ready()
	{
		_joinGameButton = GetNode<Button>("JoinGameButton");
		_createGameButton = GetNode<Button>("CreateGameButton");
	}
	
	public void OnResize()
	{
		_title = GetNode<Label>("LobbyMenuText");
		_createButtonText = GetNode<Label>("CreateGameButton/CreateButtonText");
		_joinButtonText = GetNode<Label>("JoinGameButton/JoinButtonText");
		
		_title.LabelSettings.FontSize = (int)(_titleDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_createButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_joinButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
	}

	public override void _Process(double delta)
	{
		if (_joinGameButton.ButtonPressed)
		{
			LobbyManager.JoinGameUI_ = true;
			QueueFree();
		}

		if (_createGameButton.ButtonPressed)
		{
			LobbyManager.CreateGameUI_ = true;
			QueueFree();
		}
	}
}
