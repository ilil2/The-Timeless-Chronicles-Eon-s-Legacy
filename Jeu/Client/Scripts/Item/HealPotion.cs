using Godot;
using System;

public partial class HealPotion : Potion
{	
	public HealPotion()
	{
		ID = 0;
		img = "res://Ressources/Graphismes/Potion/HealthPotion.png";
	}

	public override void UsePotion()
	{
		GD.Print("I use HealPotion");
	}
}
