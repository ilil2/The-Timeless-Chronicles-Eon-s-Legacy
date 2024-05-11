using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class ResistancePotion : Potion
{
	public int time = 20;
	public ResistancePotion()
	{
		ID = 3;
		img = "res://Ressources/Graphismes/Potion/defense potion.png";
	}

	public override void UsePotion()
	{
		GD.Print("I use ResistancePotion");
		Timer timer = (GameManager.Joueur1 as ClassScript).GetNode<Timer>("PotionTimer");
		(GameManager.Joueur1 as ClassScript).CurrentHealth = (GameManager.Joueur1 as ClassScript).Health;
		(GameManager.Joueur1 as ClassScript).MaxHealth *= 2;
		(GameManager.Joueur1 as ClassScript).Health *= 2;
		timer.WaitTime = time;
		timer.Start();
	}
}
