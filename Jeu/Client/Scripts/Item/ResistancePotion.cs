using Godot;
using System;

public partial class ResistancePotion : Potion
{
	public ResistancePotion()
	{
		ID = 3;
		img = "res://Ressources/Graphismes/Potion/defense potion.png";
	}

	public override void UsePotion()
	{
		GD.Print("I use ResistancePotion");
	}
}
