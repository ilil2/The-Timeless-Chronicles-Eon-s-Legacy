using Godot;
using System;
using System.Collections.Generic;

public partial class ProgressBarMapLvl1 : Control
{
	public int Load = 0;
	private ProgressBar _progressBar;
	private Label _label;
	
	private float _screenDefalutWidth = 1152;
	private float _textDefaultSize = 30;
	
	//Language
	private int _language;
	private Dictionary<string, string> _languageDict;
	
	public override void _Ready()
	{
		_progressBar = GetNode<ProgressBar>("ProgressBar");
		
		_language = GameManager.SettingsManager.GetAllSettings()["language"];
		_languageDict = GameManager.LanguageManager.GetLanguage(_language);
	}
	
	public void OnResize()
	{
		_label = GetNode<Label>("wait");
        
		_label.LabelSettings.FontSize = (int)(_textDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
	}

	public override void _Process(double delta)
	{
		_progressBar.Value = Load/1.5f;
		if (Load == 2500)
		{
			_label.Text = _languageDict["gameLoadingMapWaitingText"];
		}
		else if (Load >= 150)
		{
			_label.Text = _languageDict["gameLoadingMapText"] + "100%";
		}
		else
		{
			_label.Text = _languageDict["gameLoadingMapText"] + (int)_progressBar.Value + "%";
		}
	}
}
