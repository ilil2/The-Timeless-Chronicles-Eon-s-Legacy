using Godot;
using JeuClient.Scripts.PlayerScripts;

public partial class Skills : Control
{
	private ClassScript _player;
	private TextureRect[] _skills = new TextureRect[3];
	
	private ColorRect[] _skillCooldown = new ColorRect[3];
	private Label[] _skillCooldownLabel = new Label[3];
	private Timer[] _skillCooldownTimer = new Timer[3];
	private int[] _cooldown = new int[3];
	
	public override void _Ready()
	{
		_skills[0] = GetNode<TextureRect>("Skill1/Icon");
		_skills[1] = GetNode<TextureRect>("Skill2/Icon");
		_skills[2] = GetNode<TextureRect>("Skill3/Icon");
		
		_skillCooldown[0] = GetNode<ColorRect>("SkillCooldown1");
		_skillCooldown[1] = GetNode<ColorRect>("SkillCooldown2");
		_skillCooldown[2] = GetNode<ColorRect>("SkillCooldown3");
		
		_skillCooldownLabel[0] = GetNode<Label>("SkillCooldown1/Label");
		_skillCooldownLabel[1] = GetNode<Label>("SkillCooldown2/Label");
		_skillCooldownLabel[2] = GetNode<Label>("SkillCooldown3/Label");
		
		_skillCooldownTimer[0] = GetNode<Timer>("CooldownTimer1");
		_skillCooldownTimer[1] = GetNode<Timer>("CooldownTimer2");
		_skillCooldownTimer[2] = GetNode<Timer>("CooldownTimer3");

		foreach (var skill in _skillCooldown)
		{
			skill.Visible = false;
		}
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

			if (skill == "reviveall" && GameManager.ReviveAll)
			{
				_skillCooldownLabel[i].Text = "X";
				_skillCooldown[i].Visible = true;
			}
			else
			{
				_skillCooldownLabel[i].Text = _cooldown[i].ToString();
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

	private void _on_cooldown_timer_1_timeout()
	{
		AddCooldown(0);
	}
	
	private void _on_cooldown_timer_2_timeout()
	{
		AddCooldown(1);
	}
	
	private void _on_cooldown_timer_3_timeout()
	{
		AddCooldown(2);
	}

	private void AddCooldown(int skill)
	{
		_cooldown[skill] -= 1;
		if (_cooldown[skill] < 0)
		{
			_skillCooldownTimer[skill].Stop();
			_skillCooldown[skill].Visible = false;
		}

		if (_cooldown[skill] % 20 == 1)
		{
			StopSkill(GameManager.Skills[skill].Item1);
		}
	}

	private void StopSkill(string skill)
	{
		switch (skill)
		{
			case "invincibility":
				((ClassScript)GameManager.Joueur1).Invincibility = false;
				break;
			case "invisibility":
				GameManager.Joueur1.Visible = true;
				break;
			case "agro":
				((ClassScript)GameManager.Joueur1).Agro = false;
				break;
			case "absorption":
				((ClassScript)GameManager.Joueur1).Absorption = false;
				break;
		}
	}

	public void StartCooldown(int skill, int time)
	{
		_skillCooldownTimer[skill].Start();
		_skillCooldown[skill].Visible = true;
		_cooldown[skill] = time;
	}
	
	public bool IsCooldown(int skill)
	{
		return _cooldown[skill] >= 0;
	}
}
