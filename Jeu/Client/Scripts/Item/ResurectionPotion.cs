using Godot;
using System;

public partial class ResurectionPotion : Potion
{
	public ResurectionPotion()
	{
		ID = 4;
		img = "res://Ressources/Graphismes/Potion/resurrection potion.png";
	}

	public override void UsePotion()
	{
		GD.Print("I use ResurectionPotion");
	}
}
