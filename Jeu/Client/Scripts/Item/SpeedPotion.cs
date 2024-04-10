using Godot;
using System;

public partial class SpeedPotion : Potion
{
	public SpeedPotion()
	{
		ID = 2;
		img = "res://Ressources/Graphismes/Potion/agility potion.png";
	}

	public override void UsePotion()
	{
		GD.Print("I use SpeedPotion");
	}
}
