using Godot;
using System;

public partial class OtherClassScript : CharacterBody3D
{
	public Vector3 EntityPosition;
	private Label3D _pseudo;
	
	public int id { get; set; }
	public string classe { get; set; }
	
	public OtherClassScript(int id, string classe)
	{
		this.id = id;
		this.classe = classe;
	}

	public override void _Ready()
	{
		EntityPosition = Position;
		_pseudo = GetNode<Label3D>("LabelPseudo");
		SetPseudo();
	}

	public override void _PhysicsProcess(double delta)
	{
		string[] PlayerPositions = GameManager.InfoJoueur["co"].Split(";");
		_pseudo.LookAt(new Vector3(Lib.Conversions.AtoF(PlayerPositions[0]), Lib.Conversions.AtoF(PlayerPositions[1]), Lib.Conversions.AtoF(PlayerPositions[2])), Vector3.Up);
		_pseudo.Rotation = new Vector3(0, _pseudo.Rotation.Y + (float)Math.PI, 0);
		
		string[] Positions = GameManager.InfoAutreJoueur[$"co{id}"].Split(";");
		Vector3 PositionA = new Vector3(Lib.Conversions.AtoF(Positions[0]), Lib.Conversions.AtoF(Positions[1]), Lib.Conversions.AtoF(Positions[2]));

		Position = PositionA;
	}
	
	private void SetPseudo()
	{
		string PseudoName = GameManager.InfoAutreJoueur[$"pseudo{id}"];
		switch (PseudoName)
		{
			case "OttoLeBG":
			case "Darkrentin":
			case "ilyann":
			case "Narth":
				_pseudo.Modulate = new Color(0.99f,0.82f,0.11f);
				break;
			default:
				_pseudo.Modulate = new Color(1,1,1);
				break;
		}

		_pseudo.Text = PseudoName;
	}
}
