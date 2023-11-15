using Godot;
using System;

public partial class ConnectionUI : Control
{
	private LineEdit PseudoConnectionNode;
	private LineEdit PasswordConnectionNode;
	private LineEdit PseudoInscriptionNode;
	private LineEdit PasswordInscriptionNode;
	private LineEdit PasswordConfirmInscriptionNode;
	
	public static Button ConnectionButton;
	public static Button InscriptionButton;
	private Button NoCompteButton;
	private Button DejaCompteButton;
	
	private Label ConnectionError;
	private Label PseudoError;
	private Label PasswordError;
	private Label PasswordConfirmError;
	
	private Label MenuName;
	
	public static string _pseudo = "";
	public static string _password = "";
	public static string _confirm_password = "";
	
	public static bool in_connection = true;

	public override void _Ready()
	{
		//Recuperation des differents elements du menu
		//Input
		PseudoConnectionNode = GetNode<LineEdit>("PseudoConnection");
		PasswordConnectionNode = GetNode<LineEdit>("PasswordConnection");
		PseudoInscriptionNode = GetNode<LineEdit>("PseudoInscription");
		PasswordInscriptionNode = GetNode<LineEdit>("PasswordInscription");
		PasswordConfirmInscriptionNode = GetNode<LineEdit>("PasswordConfirmInscription");
		
		//Boutons
		ConnectionButton = GetNode<Button>("Connection");
		InscriptionButton = GetNode<Button>("Inscription");
		NoCompteButton = GetNode<Button>("NoCompte");
		DejaCompteButton = GetNode<Button>("DejaCompte");
		
		//Error
		ConnectionError = GetNode<Label>("ConnectionError");
		PseudoError = GetNode<Label>("PseudoError");
		PasswordError = GetNode<Label>("PasswordError");
		PasswordConfirmError = GetNode<Label>("PasswordConfirmError");
		
		//Titre
		MenuName = GetNode<Label>("MenuName");
		
		InscriptionButton.Visible = false;
		DejaCompteButton.Visible = false;

		PseudoInscriptionNode.Visible = false;
		PasswordInscriptionNode.Visible = false;
		PasswordConfirmInscriptionNode.Visible = false;
	}
	
	public override void _Process(double delta)
	{
		if (ConnectionButton.ButtonPressed)
		{
			Connection();
		}
		else if (InscriptionButton.ButtonPressed)
		{
			Inscription();
		}
		if (in_connection == false)
		{
			QueueFree();
		}
	}

	public void Connection()
	{
		ConnectionError.Text = "";
		
		_pseudo = PseudoConnectionNode.Text;
		_password = PasswordConnectionNode.Text;

		if (CheckConnection() == false)
		{
			ConnectionError.Text = "Pseudo ou mot de passe incorrect";
		}
	}
	
	public bool CheckConnection()
	{
		return true;
	}
	
	public void Inscription()
	{
		PseudoError.Text = "";
		PasswordError.Text = "";
		PasswordConfirmError.Text = "";
		
		_pseudo = PseudoInscriptionNode.Text;
		_password = PasswordInscriptionNode.Text;
		_confirm_password = PasswordConfirmInscriptionNode.Text;
		
		if (CheckPseudo(_pseudo))
		{
			if (CheckPassword(_password))
			{
				GetTree().ChangeSceneToFile("res://Scene/jeu.tscn");
			}
			else
			{
				PasswordError.Text = "Mot de passe invalide";
			}
			
		}
		else
		{
			PseudoError.Text = "Pseudo invalide";
		}
	}
	
	public bool CheckPseudo(string pseudo)
	{
		if (pseudo != "" && pseudo.Length >= 4 && pseudo.Length <= 32)
		{
			return true;
		}
		return false;
	}
	
	public bool CheckPassword(string password)
	{
		if (password != "" && password.Length >= 8 && password.Length <= 32)
		{
			return true;
		}
		return false;
	}
	
	public void NoCompte()
	{
		MenuName.Text = "Inscription";
		ConnectionError.Text = "";
		
		ConnectionButton.Visible = false;
		InscriptionButton.Visible = true;
		NoCompteButton.Visible = false;
		DejaCompteButton.Visible = true;

		PseudoConnectionNode.Visible = false;
		PasswordConnectionNode.Visible = false;
		PseudoInscriptionNode.Visible = true;
		PasswordInscriptionNode.Visible = true;
		PasswordConfirmInscriptionNode.Visible = true;
	}
	
	public void DejaCompte()
	{
		MenuName.Text = "Connection";
		PseudoError.Text = "";
		PasswordError.Text = "";
		PasswordConfirmError.Text = "";
		
		ConnectionButton.Visible = true;
		InscriptionButton.Visible = false;
		NoCompteButton.Visible = true;
		DejaCompteButton.Visible = false;
		
		PseudoConnectionNode.Visible = true;
		PasswordConnectionNode.Visible = true;
		PseudoInscriptionNode.Visible = false;
		PasswordInscriptionNode.Visible = false;
		PasswordConfirmInscriptionNode.Visible = false;
	}
}
