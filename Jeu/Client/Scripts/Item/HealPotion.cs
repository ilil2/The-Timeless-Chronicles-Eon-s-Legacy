using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class HealPotion : Potion
{	
	public HealPotion()
	{
		ID = 0;
		img = "res://Ressources/Graphismes/Potion/HealthPotion.png";
	}

	public override void UsePotion()
	{
		int newHealth = (GameManager.Joueur1 as ClassScript).GetHealth()+50;
		(GameManager.Joueur1 as ClassScript).SetHealth(newHealth);
	}
}
