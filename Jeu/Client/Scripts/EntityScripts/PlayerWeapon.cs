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
			ClassScript player = (ClassScript)GameManager.Joueur1;
			if (new Random().Next(0, player.CriticalChance) != 1)
			{
				mob.TakeDamage(player.Damage);
			}
			else
			{
				mob.TakeDamage((int)(player.Damage * 1.5));
			}
		}

		if (body is Boss boss)
		{
			ClassScript player = (ClassScript)GameManager.Joueur1;
			if (new Random().Next(0, player.CriticalChance) != 1)
			{
				boss.TakeDamage(player.Damage,player.Id);
			}
			else
			{
				boss.TakeDamage((int)(player.Damage * 1.5),player.Id);
			}
		}
	}
}
