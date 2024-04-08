using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class OtherClassScript : Player
{
	public Vector3 EntityPosition;
	protected Label3D Pseudo;
	
	protected AnimationPlayer AnimationOtherPlayer;
	protected AnimationTree AnimationOtherTree;

	protected int Id;
	protected string Classe;
	
	protected void InitOtherPlayer()
	{
		EntityPosition = Position;
		Pseudo = GetNode<Label3D>("LabelPseudo");
		SetPseudo();
	}

	protected void PseudoManager()
	{
		string[] playerPositions = GameManager.InfoJoueur["co"].Split(";");
		Pseudo.LookAt(new Vector3(Lib.Conversions.AtoF(playerPositions[0]), Lib.Conversions.AtoF(playerPositions[1]), Lib.Conversions.AtoF(playerPositions[2])), Vector3.Up);
		Pseudo.Rotation = new Vector3(0, Pseudo.Rotation.Y + (float)Math.PI, 0);
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
		string pseudoName = GameManager.InfoAutreJoueur[$"pseudo{Id}"];
		switch (pseudoName)
		{
			case "OttoLeBG":
			case "Darkrentin":
			case "ilyann":
			case "Narth":
				Pseudo.Modulate = new Color(0.99f,0.82f,0.11f);
				break;
			default:
				Pseudo.Modulate = new Color(1,1,1);
				break;
		}

		Pseudo.Text = pseudoName;
	}
	
	public void SetID(int id)
	{
		Id = id;
	}
	
	public int GetID()
	{
		return Id;
	}
	
	public void SetClasse(string classe)
	{
		Classe = classe;
	}
}
