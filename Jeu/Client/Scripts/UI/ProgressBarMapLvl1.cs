using Godot;
using System;
using System.Collections.Generic;

public partial class ProgressBarMapLvl1 : Control
{
	private Sprite2D _progressBar;
	private Label _label;
	
	private float _screenDefalutWidth = 1152;
	private float _textDefaultSize = 30;
	
	//Language
	private int _language;
	private Dictionary<string, string> _languageDict;
	
	public override void _Ready()
	{
		_language = GameManager.SettingsManager.GetAllSettings()["language"];
		_languageDict = GameManager.LanguageManager.GetLanguage(_language);
	}
	
	public void OnResize()
	{
		_label = GetNode<Label>("wait");
		_progressBar = GetNode<Sprite2D>("Sprite2D");
		
		_label.LabelSettings.FontSize = (int)(_textDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_progressBar.Scale = new Vector2(GetViewportRect().Size.X / _screenDefalutWidth * 0.1f, GetViewportRect().Size.X / _screenDefalutWidth * 0.1f);
		_progressBar.Position = new Vector2(64 * (GetViewportRect().Size.X / _screenDefalutWidth), 576 * (GetViewportRect().Size.Y / 648));

	}

	public override void _PhysicsProcess(double delta)
	{
		_progressBar.RotationDegrees += 1;
		_label.Text = (GameManager.Map as IMap).LoadingStage;
	}
}
