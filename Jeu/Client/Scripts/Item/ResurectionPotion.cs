using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;
using Lib;

public partial class ResurectionPotion : Potion
{
	public ResurectionPotion()
	{
		ID = 4;
		img = "res://Ressources/Graphismes/Potion/resurrection potion.png";
	}

	public override void UsePotion()
	{
		OtherClassScript player = null;
		double distMin = 1000000;
		for (int i = 0; i < GameManager._nbJoueur; i++)
		{
			if (GameManager.ListJoueur[i] is  OtherClassScript && (GameManager.ListJoueur[i] as OtherClassScript).isAlive == false)
			{
				double dist = MapTool.Distance(GameManager.ListJoueur[i].Position,GameManager.Joueur1.Position);
				if (dist < distMin)
				{
					distMin = dist;
					player = (OtherClassScript)GameManager.ListJoueur[i];
				}
			}
		}

		if (player == null)
		{
			GD.Print("No player to resurect");
		}
		else
		{
			GD.Print("Revive");
			GameManager.InfoJoueur["attack"] = $"revive{player.Id}";
		}
	}
}
