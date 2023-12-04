using Godot;
using System;

public partial class OtherKnightScript : CharacterBody3D
{
	public Vector3 EntityPosition;

	private int ID;
	public override void _Ready()
	{
		EntityPosition = Position;
	}

	public override void _PhysicsProcess(double delta)
	{
		string[] Positions = GameManager.InfoAutreJoueur[$"co{ID}"].Split(";");
		Vector3 PositionA = new Vector3(Lib.Conversions.AtoF(Positions[0]), Lib.Conversions.AtoF(Positions[1]), Lib.Conversions.AtoF(Positions[2]));
		Vector3 PositionD = Position;
		Vector3 AddPosition = PositionA - PositionD;

		if (Math.Sqrt(Lib.Conversions.Pow(AddPosition.X, 2) + Lib.Conversions.Pow(AddPosition.Y, 2) + Lib.Conversions.Pow(AddPosition.Z, 2)) >= 0.1f)
		{
			Vector3 Move = PositionA - PositionD;
		
			Vector3 velocity = Velocity;
			velocity.Z = Move.Z;
			velocity.X = Move.X;
			velocity.Y = Move.Y;
			Velocity = velocity;
			MoveAndSlide();
		}
		else
		{
			Position = PositionA;
		}
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
