using Godot;
using System;
using JeuClient.Scripts.EntityScripts.Mob;
using JeuClient.Scripts.PlayerScripts;

public partial class PlayerWeapon : Area3D
{
	private void _on_body_entered(Node3D body)
	{
		if(body is MobScript mob)
		{
			if (new Random().Next(0, GameManager.CriticalChance) != 1)
			{
				mob.TakeDamage(GameManager.Damage);
			}
			else
			{
				mob.TakeDamage((int)(GameManager.Damage * 1.5));
			}
			
			foreach (var skill in GameManager.Skills)
			{
				if (skill.Item1 == "escalibur" && new Random().Next(0, 5) == 1)
				{
					mob.GelMob();
				}
				else if (skill.Item1 == "poison")
				{
					mob.PoisonMob();
				}
				else if (skill.Item1 == "doubleattack")
				{
					mob.TakeDamage(GameManager.Damage);
				}
			}
		}

		if (body is Boss boss)
		{
			ClassScript player = (ClassScript)GameManager.Joueur1;
			boss.TakeDamage(GameManager.Damage, player.Id);
		}
	}
}
