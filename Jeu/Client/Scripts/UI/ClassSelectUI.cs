using Godot;
using System;
using System.Collections.Generic;

public partial class ClassSelectUI : Control
{
	public static string ClassChose = "";
	public static bool Supr = false; 
	
	//Variables des buttons du menu
	private Button _buttonArcher;
	private Button _buttonKnight;
	private Button _buttonScientist;
	private Button _buttonAssassin;
	private Button _buttonReady;
	
	//Variable du texte d'attente
	private Label _waitingText;
	
	private Label _readyButtonText;
	private Label _archerButtonText;
	private Label _knightButtonText;
	private Label _scientistButtonText;
	private Label _assassinButtonText;
	
	private float _screenDefalutWidth = 1152;
	private float _waitingTextDefaultSize = 27;
	private float _buttonDefaultSize = 20;
	
	private string _className = "";
	private bool _isReady = false;
	
	public override void _Ready()
	{
		//Recuperation des buttons du menu
		_buttonArcher = GetNode<Button>("ArcherButton");
		_buttonKnight = GetNode<Button>("KnightButton");
		_buttonScientist = GetNode<Button>("ScientistButton");
		_buttonAssassin = GetNode<Button>("AssassinButton");
		_buttonReady = GetNode<Button>("ReadyButton");

		_waitingText.Visible = false;

		Translation();
	}
	
	private void Translation()
	{
		int language = GameManager.SettingsManager.GetAllSettings()["language"];
		Dictionary<string, string> languageDict = GameManager.LanguageManager.GetLanguage(language);
		
		_readyButtonText.Text = languageDict["selectClassMenuReadyButton"];
		_waitingText.Text = languageDict["selectClassMenuWaitingText"];
		_archerButtonText.Text = languageDict["selectClassMenuArcherClass"];
		_knightButtonText.Text = languageDict["selectClassMenuKnightClass"];
		_assassinButtonText.Text = languageDict["selectClassMenuAssassinClass"];
		_scientistButtonText.Text = languageDict["selectClassMenuScientistClass"];
	}
	
	public void OnResize()
	{
		_readyButtonText = GetNode<Label>("ReadyButton/ReadyButtonText");
		_waitingText = GetNode<Label>("EnAttente");
		_archerButtonText = GetNode<Label>("ArcherButton/ArcherButtonText");
		_knightButtonText = GetNode<Label>("KnightButton/KnightButtonText");
		_assassinButtonText	= GetNode<Label>("AssassinButton/AssassinButtonText");
		_scientistButtonText = GetNode<Label>("ScientistButton/ScientistButtonText");
		
		_readyButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_waitingText.LabelSettings.FontSize = (int)(_waitingTextDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_archerButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_knightButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_assassinButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_scientistButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		
	}
	
	public override void _Process(double delta)
	{
		if (Supr)
		{
			QueueFree();
		}
		
		if (_buttonArcher.ButtonPressed && !_isReady)
		{
			//Changement du nom de la classe choisi et masquage du bonton associé
			_className = "Archer";
			
			_buttonArcher.Visible = false;
			_buttonKnight.Visible = true;
			_buttonScientist.Visible = true;
			_buttonAssassin.Visible = true;
		}
		else if (_buttonKnight.ButtonPressed && !_isReady)
		{
			//Changement du nom de la classe choisi et masquage du bonton associé
			_className = "Knight";
			
			_buttonArcher.Visible = true;
			_buttonKnight.Visible = false;
			_buttonScientist.Visible = true;
			_buttonAssassin.Visible = true;
		}
		else if (_buttonScientist.ButtonPressed && !_isReady)
		{
			//Changement du nom de la classe choisi et masquage du bonton associé
			_className = "Scientist";
			
			_buttonArcher.Visible = true;
			_buttonKnight.Visible = true;
			_buttonScientist.Visible = false;
			_buttonAssassin.Visible = true;
		}
		else if (_buttonAssassin.ButtonPressed && !_isReady)
		{
			//Changement du nom de la classe choisi et masquage du bonton associé
			_className = "Assassin";
			
			_buttonArcher.Visible = true;
			_buttonKnight.Visible = true;
			_buttonScientist.Visible = true;
			_buttonAssassin.Visible = false;
		} 
		else if (_buttonReady.ButtonPressed && _className != "")
		{
			_buttonReady.Visible = false;
			_waitingText.Visible = true;

			_isReady = true;

			ClassChose = _className;
		}
		
	}
}
