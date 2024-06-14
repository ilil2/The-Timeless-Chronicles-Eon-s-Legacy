using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public abstract partial class OtherClassScript : PlayerScript
{
	public Vector3 EntityPosition;
	public Label3D PseudoText;
	
	protected AnimationPlayer AnimationOtherPlayer;
	protected AnimationTree AnimationOtherTree;
	
	private int Maxhealth = 100;
	private int Maxstamina = 1000;
	
	protected void InitOtherPlayer()
	{
		EntityPosition = Position;
		PseudoText = GetNode<Label3D>("LabelPseudo");
		Pseudo = GameManager.InfoAutreJoueur[$"pseudo{Id}"];
		SetPseudo();
	}

	protected void PseudoManager()
	{
		string[] playerPositions = GameManager.InfoJoueur["co"].Split(";");
		PseudoText.LookAt(new Vector3(Lib.Conversions.AtoF(playerPositions[0]), Lib.Conversions.AtoF(playerPositions[1]), Lib.Conversions.AtoF(playerPositions[2])), Vector3.Up);
		PseudoText.Rotation = new Vector3(0, PseudoText.Rotation.Y + (float)Math.PI, 0);
	}

	protected void SetPosition()
	{
		try
		{
			string[] positions = GameManager.InfoAutreJoueur[$"co{Id}"].Split(";");
			Position = new Vector3(Lib.Conversions.AtoF(positions[0]), Lib.Conversions.AtoF(positions[1]), Lib.Conversions.AtoF(positions[2]));
		}
		catch{}
	}
	
	protected void SetRotation()
	{
		try
		{
			string[] orientations = GameManager.InfoAutreJoueur[$"orientation{Id}"].Split(";");
			Rotation = new Vector3(Lib.Conversions.AtoF(orientations[0]), Lib.Conversions.AtoF(orientations[1]), Lib.Conversions.AtoF(orientations[2]));
		}
		catch{}
	}
	
	private void SetPseudo()
	{
		switch (Pseudo)
		{
			case "OttoLeBG":
			case "Darkrentin":
			case "ilyann":
			case "Narth":
				PseudoText.Modulate = new Color(0.99f,0.82f,0.11f);
				break;
			default:
				PseudoText.Modulate = new Color(1,1,1);
				break;
		}

		PseudoText.Text = Pseudo;
	}
	
	public void SetID(int id)
	{
		Id = id;
	}
	
	
	public void SetClasse(string classe)
	{
		Classe = classe;
	}
	
	public int GetMaxHealth()
	{
		return Maxhealth;
	}
	
	public int GetMaxStamina()
	{
		return Maxstamina;
	}
}
