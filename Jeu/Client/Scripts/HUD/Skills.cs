using Godot;
using JeuClient.Scripts.PlayerScripts;

public partial class Skills : Control
{
	private ClassScript _player;
	private TextureRect[] _skills = new TextureRect[3];
	
	public override void _Ready()
	{
		_skills[0] = GetNode<TextureRect>("Skill1/Icon");
		_skills[1] = GetNode<TextureRect>("Skill2/Icon");
		_skills[2] = GetNode<TextureRect>("Skill3/Icon");
	}

	public override void _Process(double delta)
	{
		for (int i = 0; i < 3; i++)
		{
			string skill = GetSkillTexture(GameManager.Skills[i].Item1);
			if (skill == "")
			{
				_skills[i].Texture = null;
			}
			else
			{
				_skills[i].Texture = GD.Load<Texture2D>(skill);
			}
		}
	}
	
	private string GetSkillTexture(string skill)
	{
		switch (skill)
		{
			case "speed":
				return "res://Ressources/Icons/Other/Speed.png";
			case "health":
				return "res://Ressources/Icons/Other/Heath.png";
			case "damage":
				return "res://Ressources/Icons/Other/Damage.png";
			case "stamina":
				return "res://Ressources/Icons/Other/Mana.png";
			case "reload":
				return "res://Ressources/Icons/Other/Reload.png";
			case "staminause":
				return "res://Ressources/Icons/Other/ManaUse.png";
			case "arrow":
				return "res://Ressources/Icons/Archer/Arrow.png";
			case "shootspeed":
				return "res://Ressources/Icons/Archer/ArrowSpeed.png";
			case "arrowpoison":
				return "res://Ressources/Icons/Archer/PoisonArrow.png";
			case "arrowgel":
				return "res://Ressources/Icons/Archer/GelArrow.png";
			case "healspeed":
				return "res://Ressources/Icons/Scientist/Heal.png";
			case "lasermove":
				return "res://Ressources/Icons/Scientist/LaserMove.png";
			case "revive":
				return "res://Ressources/Icons/Scientist/Revive.png";
			case "vampire":
				return "res://Ressources/Icons/Scientist/Vampire.png";
			case "reviveall":
				return "res://Ressources/Icons/Scientist/BigRevive.png";
			case "reloadprotection":
				return "res://Ressources/Icons/Scientist/ReloadProtection.png";
			case "crit":
				return "res://Ressources/Icons/Other/Crit.png";
			case "range":
				return "res://Ressources/Icons/Knight/Range.png";
			case "agro":
				return "res://Ressources/Icons/Knight/Agro.png";
			case "spike":
				return "res://Ressources/Icons/Knight/Thorns.png";
			case "absorption":
				return "res://Ressources/Icons/Knight/Absorption.png";
			case "invincibility":
				return "res://Ressources/Icons/Knight/Invinsibility.png";
			case "escalibur":
				return "res://Ressources/Icons/Knight/Escalibur.png";
			case "dashdegat":
				return "res://Ressources/Icons/Assassin/DashDegat.png";
			case "dague":
				return "res://Ressources/Icons/Assassin/Dague.png";
			case "invisibility":
				return "res://Ressources/Icons/Assassin/Invisibility.png";
			case "doubleattack":
				return "res://Ressources/Icons/Assassin/DoubleDague.png";
			case "poison":
				return "res://Ressources/Icons/Assassin/Poison.png";
			default:
				return "";
		}
	}
}
