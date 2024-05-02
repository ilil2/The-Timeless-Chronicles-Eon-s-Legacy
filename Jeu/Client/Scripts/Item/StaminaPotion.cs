using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

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
		int newStamina = (GameManager.Joueur1 as ClassScript).GetStamina()+500;
		(GameManager.Joueur1 as ClassScript).SetStamina(newStamina);
	}
}
