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
		GD.Print("I use HealPotion");
		(GameManager.Joueur1 as ClassScript p).SetHealth(p.GetHealth()+50);
	}
}
