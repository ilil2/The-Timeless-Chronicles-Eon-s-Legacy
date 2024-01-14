using Godot;
using System;
using System.Collections.Generic;

public partial class ConnectionUI : Control
{
	private LineEdit _pseudoConnectionNode;
	private LineEdit _passwordConnectionNode;
	private LineEdit _pseudoInscriptionNode;
	private LineEdit _passwordInscriptionNode;
	private LineEdit _passwordConfirmInscriptionNode;
	
	public static Button ConnectionButton;
	public static Button InscriptionButton;
	private Button _noCompteButton;
	private Button _dejaCompteButton;
	
	private Label _connectionError;
	private Label _inscriptionError;
	private Label _menuName;
	private Label _noCompteButtonText;
	private Label _dejaCompteButtonText;
	private Label _connectionButtonText;
	private Label _inscriptionButtonText;
	
	private float _screenDefalutWidth = 1152;
	private float _titleDefaultSize = 40;
	private float _buttonDefaultSize = 20;
	private float _transparentButtonDefaultSize = 16;
	private float _errorDefaultSize = 16;
	
	public static string _pseudo = "";
	public static string _password = "";
	public static string _confirm_password = "";
	
	public static bool in_connection = true;
	public static string erreur = "";
	
	//Language
	private int _language;
	private Dictionary<string, string> _languageDict;

	public override void _Ready()
	{
		//Recuperation des differents elements du menu
		//Input
		_pseudoConnectionNode = GetNode<LineEdit>("PseudoConnection");
		_passwordConnectionNode = GetNode<LineEdit>("PasswordConnection");
		_pseudoInscriptionNode = GetNode<LineEdit>("PseudoInscription");
		_passwordInscriptionNode = GetNode<LineEdit>("PasswordInscription");
		_passwordConfirmInscriptionNode = GetNode<LineEdit>("PasswordConfirmInscription");
		
		//Boutons
		ConnectionButton = GetNode<Button>("Connection");
		InscriptionButton = GetNode<Button>("Inscription");
		_noCompteButton = GetNode<Button>("NoCompte");
		_dejaCompteButton = GetNode<Button>("DejaCompte");
		
		InscriptionButton.Visible = false;
		_dejaCompteButton.Visible = false;
		
		_inscriptionError.Visible = false;

		_pseudoInscriptionNode.Visible = false;
		_passwordInscriptionNode.Visible = false;
		_passwordConfirmInscriptionNode.Visible = false;
		
		//Language
		_language = GameManager.SettingsManager.GetAllSettings()["language"];
		_languageDict = GameManager.LanguageManager.GetLanguage(_language);
		Translation();
	}
	
	private void Translation()
	{
		//Connection
		_menuName.Text = _languageDict["connectionMenuTitle"];
		_pseudoConnectionNode.PlaceholderText = _languageDict["connectionMenuPseudoText"];
		_passwordConnectionNode.PlaceholderText = _languageDict["connectionMenuPasswordText"];
		_connectionButtonText.Text = _languageDict["connectionMenuConnectionButton"];
		_noCompteButtonText.Text = _languageDict["connectionMenuNoAccountButton"];
		
		//Inscription
		_pseudoInscriptionNode.PlaceholderText = _languageDict["inscriptionMenuPseudoText"];
		_passwordInscriptionNode.PlaceholderText = _languageDict["inscriptionMenuPasswordText"];
		_passwordConfirmInscriptionNode.PlaceholderText = _languageDict["inscriptionMenuPasswordConfirmText"];
		_inscriptionButtonText.Text = _languageDict["inscriptionMenuInscriptionButton"];
		_dejaCompteButtonText.Text = _languageDict["inscriptionMenuAlreadyAccountButton"];
	}
	
	public void OnResize()
	{
		_menuName = GetNode<Label>("MenuName");
		_connectionError = GetNode<Label>("ConnectionError");
		_inscriptionError = GetNode<Label>("InscriptionError");
		_dejaCompteButtonText = GetNode<Label>("DejaCompte/DejaCompteButtonText");
		_noCompteButtonText = GetNode<Label>("NoCompte/NoCompteButtonText");
		_connectionButtonText = GetNode<Label>("Connection/ConnectionButtonText");
		_inscriptionButtonText = GetNode<Label>("Inscription/InscriptionButtonText");
		
		_menuName.LabelSettings.FontSize = (int)(_titleDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_connectionButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_inscriptionButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_dejaCompteButtonText.LabelSettings.FontSize = (int)(_transparentButtonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_noCompteButtonText.LabelSettings.FontSize = (int)(_transparentButtonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_connectionError.LabelSettings.FontSize = (int)(_errorDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_inscriptionError.LabelSettings.FontSize = (int)(_errorDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
	}
	
	public override void _Process(double delta)
	{
		erreur = GameManager.ConnectionError;
		
		if (ConnectionButton.ButtonPressed)
		{
			Connection();
		}
		else if (InscriptionButton.ButtonPressed)
		{
			if (_passwordInscriptionNode.Text == _passwordConfirmInscriptionNode.Text)
				Inscription();
			else
				GameManager.ConnectionError = "confirmation incorect";
		}
		if (in_connection == false)
		{
			QueueFree();
		}
		_connectionError.Text = erreur;
		_inscriptionError.Text = erreur;
	}

	public void Connection()
	{
		_pseudo = _pseudoConnectionNode.Text;
		_password = _passwordConnectionNode.Text;
	}
	
	public void Inscription()
	{
		_pseudo = _pseudoInscriptionNode.Text;
		_password = _passwordInscriptionNode.Text;
		_confirm_password = _passwordConfirmInscriptionNode.Text;
	}
	
	public void NoCompte()
	{
		_menuName.Text = _languageDict["inscriptionMenuTitle"];
		_connectionError.Visible = false;
		_inscriptionError.Visible = true;
		
		ConnectionButton.Visible = false;
		InscriptionButton.Visible = true;
		_noCompteButton.Visible = false;
		_dejaCompteButton.Visible = true;

		_pseudoConnectionNode.Visible = false;
		_passwordConnectionNode.Visible = false;
		_pseudoInscriptionNode.Visible = true;
		_passwordInscriptionNode.Visible = true;
		_passwordConfirmInscriptionNode.Visible = true;
	}
	
	public void DejaCompte()
	{
		_menuName.Text = _languageDict["connectionMenuTitle"];
		_inscriptionError.Visible = false;
		_connectionError.Visible = true;
		
		ConnectionButton.Visible = true;
		InscriptionButton.Visible = false;
		_noCompteButton.Visible = true;
		_dejaCompteButton.Visible = false;
		
		_pseudoConnectionNode.Visible = true;
		_passwordConnectionNode.Visible = true;
		_pseudoInscriptionNode.Visible = false;
		_passwordInscriptionNode.Visible = false;
		_passwordConfirmInscriptionNode.Visible = false;
	}
}
