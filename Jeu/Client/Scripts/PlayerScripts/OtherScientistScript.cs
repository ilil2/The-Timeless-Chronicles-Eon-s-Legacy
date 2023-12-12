using Godot;
using System;

public partial class OtherScientistScript : CharacterBody3D
{
    public Vector3 EntityPosition;

    private Label3D _pseudo;
    
    private int ID;

    public override void _Ready()
    {
        EntityPosition = Position;
        _pseudo = GetNode<Label3D>("LabelPseudo");
        SetPseudo();
    }

    public override void _Process(double delta)
    {
        string[] PlayerPositions = GameManager.InfoJoueur["co"].Split(";");
        Vector3 PlayerCoord = new Vector3(Lib.Conversions.AtoF(PlayerPositions[0]), Lib.Conversions.AtoF(PlayerPositions[1]), Lib.Conversions.AtoF(PlayerPositions[2]));
        _pseudo.LookAt(PlayerCoord, Vector3.Up);
    }

    public override void _PhysicsProcess(double delta)
    {
        string[] Positions = GameManager.InfoAutreJoueur[$"co{ID}"].Split(";");
        Vector3 PositionA = new Vector3(Lib.Conversions.AtoF(Positions[0]), Lib.Conversions.AtoF(Positions[1]), Lib.Conversions.AtoF(Positions[2]));

        Position = PositionA;
    }

    private void SetPseudo()
    {
        string PseudoName = GameManager.InfoAutreJoueur[$"pseudo{ID}"];
        switch (PseudoName)
        {
            case "OttoLeBG":
            case "Darkrentin":
            case "ilyann":
            case "Narth":
                _pseudo.Modulate = new Color(224,195,13);
                break;
            default:
                _pseudo.Modulate = new Color(255,255,255);
                break;
        }

        _pseudo.Text = PseudoName;
    }

    public void SetID(int id)
    {
        ID = id;
    }
    
    public int GetID()
    {
        return ID;
    }
}
