using Godot;
using System;
using System.Diagnostics;
using JeuClient.Scripts.PlayerScripts;

public partial class SpeedPotion : Potion
{
	public int time = 30000;
	public SpeedPotion()
	{
		ID = 2;
		img = "res://Ressources/Graphismes/Potion/agility potion.png";
	}

	public override void UsePotion()
	{
		GD.Print("I use SpeedPotion");
		(GameManager.Joueur1 as ClassScript).GetNode<Timer>("PotionTimer").Start();
		
	}
}
