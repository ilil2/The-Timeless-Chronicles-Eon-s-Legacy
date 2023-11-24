using Godot;
using System;

public partial class OtherArcherScript : CharacterBody3D
{
    public Vector3 EntityPosition;
    
    public static int ID;

    public override void _Ready()
    {
        EntityPosition = Position;
    }

    public override void _Process(double delta)
    {
        string[] Positions = GameManager.InfoAutreJoueur[$"co{ID}"].Split(":")[1].Split(";");
        Position = new Vector3(Lib.Conversions.AtoF(Positions[0]), Lib.Conversions.AtoF(Positions[1]), Lib.Conversions.AtoF(Positions[2]));
    }
}
