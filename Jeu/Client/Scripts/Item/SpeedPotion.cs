using Godot;
using System;
using System.Diagnostics;
using JeuClient.Scripts.PlayerScripts;

public partial class SpeedPotion : Potion
{
	public int time = 20;
	public SpeedPotion()
	{
		ID = 2;
		img = "res://Ressources/Graphismes/Potion/agility potion.png";
	}

	public override void UsePotion()
	{
		GD.Print("I use SpeedPotion");
		Timer timer = (GameManager.Joueur1 as ClassScript).GetNode<Timer>("PotionTimer");
		(GameManager.Joueur1 as ClassScript).CurrentWalkSpeed = (GameManager.Joueur1 as ClassScript).WalkSpeed;
		(GameManager.Joueur1 as ClassScript).WalkSpeed *= 1.5f;
		timer.WaitTime = time;
		timer.Start();

	}
}
