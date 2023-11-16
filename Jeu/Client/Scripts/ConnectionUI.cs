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
	private Label InscriptionError;
	
	private Label MenuName;
	
	public static string _pseudo = "";
	public static string _password = "";
	public static string _confirm_password = "";
	
	public static bool in_connection = true;
	public static string erreur = "";

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
		InscriptionError = GetNode<Label>("InscriptionError");
		
		//Titre
		MenuName = GetNode<Label>("MenuName");
		
		InscriptionButton.Visible = false;
		DejaCompteButton.Visible = false;
		
		InscriptionError.Visible = false;

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
			if (PasswordInscriptionNode.Text == PasswordConfirmInscriptionNode.Text)
				Inscription();
			else
				erreur = "confirmation incorect";
		}
		if (in_connection == false)
		{
			QueueFree();
		}
		ConnectionError.Text = erreur;
		InscriptionError.Text = erreur;
	}

	public void Connection()
	{
		_pseudo = PseudoConnectionNode.Text;
		_password = PasswordConnectionNode.Text;
	}
	
	public void Inscription()
	{
		_pseudo = PseudoInscriptionNode.Text;
		_password = PasswordInscriptionNode.Text;
		_confirm_password = PasswordConfirmInscriptionNode.Text;
	}
	
	public void NoCompte()
	{
		MenuName.Text = "Inscription";
		ConnectionError.Visible = false;
		InscriptionError.Visible = true;
		
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
		InscriptionError.Visible = false;
		ConnectionError.Visible = true;
		
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
