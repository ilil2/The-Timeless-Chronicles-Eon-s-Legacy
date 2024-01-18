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
	public static Button FastConnectionButton;
	public static CheckBox FastConnectionSaveButton;
	private Button _noCompteButton;
	private Button _dejaCompteButton;
	private OptionButton _languageChooseButton;
	private Button _fastConnectionWarningButton;
	
	private Label _connectionError;
	private Label _inscriptionError;
	private Label _menuName;
	private Label _noCompteButtonText;
	private Label _dejaCompteButtonText;
	private Label _connectionButtonText;
	private Label _inscriptionButtonText;
	private Label _fastConnectionButtonText;
	private Label _fastConnectionWarningText;
	private Label _fastConnectionSaveText;
	private Label _fastConnectionWarningButtonText;

	private ColorRect _fastConnectionWarningBackground;
	
	private float _screenDefalutWidth = 1152;
	private float _titleDefaultSize = 45;
	private float _buttonDefaultSize = 20;
	private float _transparentButtonDefaultSize = 16;
	private float _errorDefaultSize = 16;
	private float _saveDefaultSize = 14;
	private float _warningDefaultSize = 10;
	
	public static string _pseudo = "";
	public static string _password = "";
	public static string _confirm_password = "";
	
	public static bool in_connection = true;
	public static string erreur = "";
	
	private bool _fastConnectionWarning = false;
	private int _fastConnectionWarningTimer = 0;
	
	private static int _errorNumber = -1;
	
	//Language
	private List<Dictionary<string, string>> _allLanguages;
	private int _language;
	private static Dictionary<string, string> _languageDict;

	public override void _Ready()
	{
		//Recuperation des differents elements du menu
		
		//Boutons
		ConnectionButton = GetNode<Button>("Connection");
		InscriptionButton = GetNode<Button>("Inscription");
		FastConnectionButton = GetNode<Button>("FastConnectionButton");
		FastConnectionSaveButton = GetNode<CheckBox>("FastConnectionSaveButton");
		_noCompteButton = GetNode<Button>("NoCompte");
		_dejaCompteButton = GetNode<Button>("DejaCompte");
		_fastConnectionWarningButton = GetNode<Button>("FastConnectionWarningButton");
		
		_fastConnectionWarningBackground = GetNode<ColorRect>("FastConnectionWarningBackground");
		
		InscriptionButton.Visible = false;
		_dejaCompteButton.Visible = false;
		
		_inscriptionError.Visible = false;

		_pseudoInscriptionNode.Visible = false;
		_passwordInscriptionNode.Visible = false;
		_passwordConfirmInscriptionNode.Visible = false;
		
		_fastConnectionWarningBackground.Visible = false;
		
		//Language
		_language = GameManager.SettingsManager.GetAllSettings()["language"];
		_languageDict = GameManager.LanguageManager.GetLanguage(_language);
		_allLanguages = GameManager.LanguageManager.GetAllLanguages();
		Translation();
		
		foreach (var language in _allLanguages)
		{
			_languageChooseButton.AddItem(language["languageName"]);
		}
		_languageChooseButton.Selected = _language;
	}
	
	private void Translation()
	{
		//Connection
		_menuName.Text = _languageDict["connectionMenuTitle"];
		_pseudoConnectionNode.PlaceholderText = _languageDict["connectionMenuPseudoText"];
		_passwordConnectionNode.PlaceholderText = _languageDict["connectionMenuPasswordText"];
		_connectionButtonText.Text = _languageDict["connectionMenuConnectionButton"];
		_noCompteButtonText.Text = _languageDict["connectionMenuNoAccountButton"];
		_fastConnectionButtonText.Text = _languageDict["connectionMenuFastConnectionButton"];
		_fastConnectionWarningText.Text = _languageDict["connectionMenuFastConnectionSaveWarningText"];
		_fastConnectionSaveText.Text = _languageDict["connectionMenuFastConnectionSaveButton"];
		
		//Inscription
		_pseudoInscriptionNode.PlaceholderText = _languageDict["inscriptionMenuPseudoText"];
		_passwordInscriptionNode.PlaceholderText = _languageDict["inscriptionMenuPasswordText"];
		_passwordConfirmInscriptionNode.PlaceholderText = _languageDict["inscriptionMenuPasswordConfirmText"];
		_inscriptionButtonText.Text = _languageDict["inscriptionMenuInscriptionButton"];
		_dejaCompteButtonText.Text = _languageDict["inscriptionMenuAlreadyAccountButton"];
	}

	public static void ErrorTranslation(int errorNumber)
	{
		_errorNumber = errorNumber;
		switch (errorNumber)
		{
			case 0:
				erreur = _languageDict["connectionMenuErrorUsernameOrPasswordText"];
				break;
			case 1:
				erreur = _languageDict["connectionMenuErrorFastConnectionText"];
				break;
			case 2:
				erreur = _languageDict["connectionMenuErrorNoFastConnectionText"];
				break;
			case 3:
				erreur = _languageDict["inscriptionMenuErrorIncorrectConfirmText"];
				break;
			case 4:
				erreur = _languageDict["inscriptionMenuErrorAlreadyExistText"];
				break;
			default:
				erreur = "";
				break;
		}
	}
	
	public void OnResize()
	{
		//Input
		_pseudoConnectionNode = GetNode<LineEdit>("PseudoConnection");
		_passwordConnectionNode = GetNode<LineEdit>("PasswordConnection");
		_pseudoInscriptionNode = GetNode<LineEdit>("PseudoInscription");
		_passwordInscriptionNode = GetNode<LineEdit>("PasswordInscription");
		_passwordConfirmInscriptionNode = GetNode<LineEdit>("PasswordConfirmInscription");
		_languageChooseButton = GetNode<OptionButton>("LanguageChooseButton");
		
		//Label
		_menuName = GetNode<Label>("MenuName");
		_connectionError = GetNode<Label>("ConnectionError");
		_inscriptionError = GetNode<Label>("InscriptionError");
		_dejaCompteButtonText = GetNode<Label>("DejaCompte/DejaCompteButtonText");
		_noCompteButtonText = GetNode<Label>("NoCompte/NoCompteButtonText");
		_connectionButtonText = GetNode<Label>("Connection/ConnectionButtonText");
		_inscriptionButtonText = GetNode<Label>("Inscription/InscriptionButtonText");
		_fastConnectionButtonText = GetNode<Label>("FastConnectionButton/FastConnectionText");
		_fastConnectionWarningText = GetNode<Label>("FastConnectionWarningBackground/FastConnectionWarningText");
		_fastConnectionSaveText = GetNode<Label>("FastConnectionSaveText");
		_fastConnectionWarningButtonText = GetNode<Label>("FastConnectionWarningButton/FastConnectionWarningText");
		
		
		//Line Edit Size
		_pseudoConnectionNode.AddThemeFontSizeOverride("font_size", (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth)));
		_passwordConnectionNode.AddThemeFontSizeOverride("font_size", (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth)));
		_pseudoInscriptionNode.AddThemeFontSizeOverride("font_size", (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth)));
		_passwordInscriptionNode.AddThemeFontSizeOverride("font_size", (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth)));
		_passwordConfirmInscriptionNode.AddThemeFontSizeOverride("font_size", (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth)));
		_languageChooseButton.AddThemeFontSizeOverride("font_size", (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth)));
		
		//Label Size
		_menuName.LabelSettings.FontSize = (int)(_titleDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_connectionButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_inscriptionButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_dejaCompteButtonText.LabelSettings.FontSize = (int)(_transparentButtonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_noCompteButtonText.LabelSettings.FontSize = (int)(_transparentButtonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_connectionError.LabelSettings.FontSize = (int)(_errorDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_inscriptionError.LabelSettings.FontSize = (int)(_errorDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_fastConnectionButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_fastConnectionWarningText.LabelSettings.FontSize = (int)(_warningDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_fastConnectionSaveText.LabelSettings.FontSize = (int)(_saveDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
		_fastConnectionWarningButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_languageChooseButton.Selected != _language)
		{
			_language = _languageChooseButton.Selected;
			_languageDict = GameManager.LanguageManager.GetLanguage(_language);
			Translation();
			ErrorTranslation(_errorNumber);
			GameManager.SettingsManager.SetSetting("language", _language);
			GameManager.SettingsManager.SaveSettings();
		}
		
		if (ConnectionButton.ButtonPressed)
		{
			_pseudo = _pseudoConnectionNode.Text;
			_password = _passwordConnectionNode.Text;
		}
		else if (InscriptionButton.ButtonPressed)
		{
			if (_passwordInscriptionNode.Text == _passwordConfirmInscriptionNode.Text)
			{
				
				_pseudo = _pseudoInscriptionNode.Text;
				_password = _passwordInscriptionNode.Text;
				_confirm_password = _passwordConfirmInscriptionNode.Text;
			}
			else
			{
				ErrorTranslation(3);
			}
		}
		else if (FastConnectionButton.ButtonPressed)
		{
			(string pseudo, string password) = GameManager.FastConnectionManager.GetLastConnection();
			if (pseudo != "" && password != "")
			{
				_pseudo = pseudo;
				_password = password;
			}
			else
			{
				ErrorTranslation(2);
			}
		}
		else if (_fastConnectionWarningButton.ButtonPressed)
		{
			if (_fastConnectionWarningTimer >= 20)
			{
				if (_fastConnectionWarning)
				{
					_fastConnectionWarningBackground.Visible = false;
					_fastConnectionWarning = false;
				}
				else
				{
					_fastConnectionWarningBackground.Visible = true;
					_fastConnectionWarning = true;
				}
				_fastConnectionWarningTimer = 0;
			}
		}

		_fastConnectionWarningTimer += 1;
		
		if (in_connection == false)
		{
			QueueFree();
		}
		_connectionError.Text = erreur;
		_inscriptionError.Text = erreur;
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
		
		FastConnectionSaveButton.Visible = false;
		_fastConnectionWarningBackground.Visible = false;
		_fastConnectionSaveText.Visible = false;
		_fastConnectionWarningButton.Visible = false;
		
		_fastConnectionWarning = false;
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
		
		FastConnectionSaveButton.Visible = true;
		_fastConnectionSaveText.Visible = true;
		_fastConnectionWarningButton.Visible = true;
	}
}
