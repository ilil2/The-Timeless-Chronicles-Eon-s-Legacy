using Godot;
using System;

public partial class ClassSelectUI : Control
{
	public static string ClassChose = "";
	
	//Variables des buttons du menu
	private Button _buttonArcher;
	private Button _buttonKnight;
	private Button _buttonScientist;
	private Button _buttonAssassin;
	private Button _buttonReady;
	
	//Variable du texte d'attente
	private Label _waitingText;
	
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
		
		//Recuperation du texte d'attente
		_waitingText = GetNode<Label>("EnAttente");

		_waitingText.Visible = false;
	}

	public override void _Process(double delta)
	{
		if (_buttonArcher.ButtonPressed && !_isReady)
		{
			//Changement du nom de la classe choisi et masquage du bonton associé
			_className = "archer";
			
			_buttonArcher.Visible = false;
			_buttonKnight.Visible = true;
			_buttonScientist.Visible = true;
			_buttonAssassin.Visible = true;
		}
		else if (_buttonKnight.ButtonPressed && !_isReady)
		{
			//Changement du nom de la classe choisi et masquage du bonton associé
			_className = "knight";
			
			_buttonArcher.Visible = true;
			_buttonKnight.Visible = false;
			_buttonScientist.Visible = true;
			_buttonAssassin.Visible = true;
		}
		else if (_buttonScientist.ButtonPressed && !_isReady)
		{
			//Changement du nom de la classe choisi et masquage du bonton associé
			_className = "scientist";
			
			_buttonArcher.Visible = true;
			_buttonKnight.Visible = true;
			_buttonScientist.Visible = false;
			_buttonAssassin.Visible = true;
		}
		else if (_buttonAssassin.ButtonPressed && !_isReady)
		{
			//Changement du nom de la classe choisi et masquage du bonton associé
			_className = "assassin";
			
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
