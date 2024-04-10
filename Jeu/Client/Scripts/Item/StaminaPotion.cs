using Godot;
using System;

public partial class StaminaPotion : Potion
{
	public StaminaPotion()
	{
		ID = 1;
		img = "res://Ressources/Graphismes/Potion/antidote.png";
	}

	public override void UsePotion()
	{
		GD.Print("I use StaminaPotion");
	}
}