using Godot;
using System;

public partial class OtherClassScript : CharacterBody3D
{
	public Vector3 EntityPosition;
	protected Label3D Pseudo;

	protected int Id;
	protected string Classe;

	public override void _Ready()
	{
		EntityPosition = Position;
		Pseudo = GetNode<Label3D>("LabelPseudo");
		SetPseudo();
	}

	public override void _Process(double delta)
	{
		string[] PlayerPositions = GameManager.InfoJoueur["co"].Split(";");
		Pseudo.LookAt(new Vector3(Lib.Conversions.AtoF(PlayerPositions[0]), Lib.Conversions.AtoF(PlayerPositions[1]), Lib.Conversions.AtoF(PlayerPositions[2])), Vector3.Up);
		Pseudo.Rotation = new Vector3(0, Pseudo.Rotation.Y + (float)Math.PI, 0);
	}

	public override void _PhysicsProcess(double delta)
	{
		string[] Positions = GameManager.InfoAutreJoueur[$"co{Id}"].Split(";");
		Vector3 PositionA = new Vector3(Lib.Conversions.AtoF(Positions[0]), Lib.Conversions.AtoF(Positions[1]), Lib.Conversions.AtoF(Positions[2]));

		Position = PositionA;
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
	
	public void SetClasse(string classe)
	{
		Classe = classe;
	}
}
