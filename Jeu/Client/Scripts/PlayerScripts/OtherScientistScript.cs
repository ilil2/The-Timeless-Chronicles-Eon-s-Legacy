using Godot;
using System;

public partial class OtherScientistScript : CharacterBody3D
{
	public Vector3 EntityPosition;

	private int ID;
	public override void _Ready()
	{
		EntityPosition = Position;
	}

	public override void _Process(double delta)
	{
		string[] Positions = GameManager.InfoAutreJoueur[$"co{ID}"].Split(";");
		Position = new Vector3(Lib.Conversions.AtoF(Positions[0]), Lib.Conversions.AtoF(Positions[1]), Lib.Conversions.AtoF(Positions[2]));
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
