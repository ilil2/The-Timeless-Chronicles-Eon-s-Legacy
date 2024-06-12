using Godot;
using System;
using System.Collections.Generic;
using JeuClient.Scripts.PlayerScripts;
using Lib;

public partial class SelectSkills : Control
{
	[Export] private bool _isRecto1;
	[Export] private bool _isRecto2;
	[Export] private bool _isRecto3;
	
	private AnimationPlayer[] _animationPlayer = new AnimationPlayer[3];
	
	private Label[] _labelSkill  = new Label[3];

	private TextureRect[] _skillImage = new TextureRect[3];
	
	private Label[] _skillDescription = new Label[3];
	private Label _title;
	private Label _error;
	
	private TextureRect[] _skills = new TextureRect[3];
	private (string, int)[][][] _skillsName;
	private ClassScript _player;

	private Dictionary<string, string> _languageDict;
	
	public override void _Ready()
	{
		_skills[0] = GetNode<TextureRect>("Skill1");
		_skills[1] = GetNode<TextureRect>("Skill2");
		_skills[2] = GetNode<TextureRect>("Skill3");
		
		_labelSkill[0] = GetNode<Label>("Label1");
		_labelSkill[1] = GetNode<Label>("Label2");
		_labelSkill[2] = GetNode<Label>("Label3");
		
		_skillImage[0] = GetNode<TextureRect>("SkillImage1");
		_skillImage[1] = GetNode<TextureRect>("SkillImage2");
		_skillImage[2] = GetNode<TextureRect>("SkillImage3");
		
		_skillDescription[0] = GetNode<Label>("SkillDescription1");
		_skillDescription[1] = GetNode<Label>("SkillDescription2");
		_skillDescription[2] = GetNode<Label>("SkillDescription3");
		
		_animationPlayer[0] = GetNode<AnimationPlayer>("AnimationPlayer1");
		_animationPlayer[1] = GetNode<AnimationPlayer>("AnimationPlayer2");
		_animationPlayer[2] = GetNode<AnimationPlayer>("AnimationPlayer3");
		
		_animationPlayer[0].Play("RESET");
		_animationPlayer[1].Play("RESET");
		_animationPlayer[2].Play("RESET");
		
		_title = GetNode<Label>("Title");
		_error = GetNode<Label>("Error");

		_player = (ClassScript)GameManager.Joueur1;
		_languageDict = GameManager.LanguageManager.GetLanguage(GameManager.SettingsManager.GetAllSettings()["language"]);

		_skillsName = new []
		{
			new []
			{
				new []
				{
					("reload", 1),
					("staminause", 5),
					RandomSkill(1)
				},
				new []
				{
					("arrow", 1),
					("shootspeed", 5),
					RandomSkill(1)
				},
				new []
				{
					("arrowpoison", 5),
					RandomChoice(new List<(string, int)> {("reload", 1), ("staminause", 5)}),
					RandomSkill(2)
				},
				new []
				{
					("arrowgel", 5),
					("shootspeed", 5),
					RandomSkill(2)
				},
				new []
				{
					RandomChoice(new List<(string, int)> {("arrowpoison", 5), ("arrowgel", 5)}),
					("arrow", 1),
					RandomSkill(3)
				}
			},
			new []
			{
				new []
				{
					("reload", 1),
					("staminause", 5),
					RandomSkill(1)
				},
				new []
				{
					("healspeed", 2),
					("lasermove", 0),
					RandomSkill(1)
				},
				new []
				{
					("revive", 0),
					RandomChoice(new List<(string, int)> {("reload", 1), ("staminause", 5)}),
					RandomSkill(2)
				},
				new []
				{
					("vampire", 0),
					("healspeed", 3),
					RandomSkill(2)
				},
				new []
				{
					("reviveall", 0),
					("reloadprotection", 0),
					RandomSkill(3)
				}
			},
			new []
			{
				new []
				{
					("reload", 1),
					("crit", 5),
					RandomSkill(1)
				},
				new []
				{
					("range", 1),
					("agro", 0),
					RandomSkill(1)
				},
				new []
				{
					("spike", 0),
					("absorption", 0),
					RandomSkill(2)
				},
				new []
				{
					("invincibility", 0),
					RandomChoice(new List<(string, int)> {("reload", 1), ("crit", 5)}),
					RandomSkill(2)
				},
				new []
				{
					("escalibur", 0),
					("range", 1),
					RandomSkill(3)
				}
			},
			new []
			{
				new []
				{
					("reload", 1),
					("crit", 5),
					RandomSkill(1)
				},
				new []
				{
					("staminause", 5),
					("dashdegat", 5),
					RandomSkill(1)
				},
				new []
				{
					("dague", 0),
					RandomChoice(new List<(string, int)> {("reload", 1), ("crit", 5)}),
					RandomSkill(2)
				},
				new []
				{
					("invisibility", 0),
					("dashdegat", 10),
					RandomSkill(2)
				},
				new []
				{
					("doubleattack", 0),
					("poison", 5),
					RandomSkill(3)
				}
			}
		};
		
		_player = (ClassScript)GameManager.Joueur1;
		
		_skillImage[0].Texture = GD.Load<Texture2D>(GetSkillTexture(_skillsName[ClassToInt(_player.Classe)][GameManager.levelStart][0].Item1));
		_skillImage[1].Texture = GD.Load<Texture2D>(GetSkillTexture(_skillsName[ClassToInt(_player.Classe)][GameManager.levelStart][1].Item1));
		_skillImage[2].Texture = GD.Load<Texture2D>(GetSkillTexture(_skillsName[ClassToInt(_player.Classe)][GameManager.levelStart][2].Item1));

		_labelSkill[0].Text = GetSkillName(_skillsName[ClassToInt(_player.Classe)][GameManager.levelStart][0].Item1);
		_labelSkill[1].Text = GetSkillName(_skillsName[ClassToInt(_player.Classe)][GameManager.levelStart][1].Item1);
		_labelSkill[2].Text = GetSkillName(_skillsName[ClassToInt(_player.Classe)][GameManager.levelStart][2].Item1);
		
		_skillDescription[0].Text = GetSkillDescription(_skillsName[ClassToInt(_player.Classe)][GameManager.levelStart][0]);
		_skillDescription[1].Text = GetSkillDescription(_skillsName[ClassToInt(_player.Classe)][GameManager.levelStart][1]);
		_skillDescription[2].Text = GetSkillDescription(_skillsName[ClassToInt(_player.Classe)][GameManager.levelStart][2]);
		
		_title.Text = _languageDict["skillChooseTitle"];
	}

	public override void _Process(double delta)
	{
		if (_isRecto1)
		{
			_skills[0].Texture = GD.Load<Texture2D>($"res://Ressources/Graphismes/Card/{_player.Classe}Power.png");
		}
		else
		{
			_skills[0].Texture = GD.Load<Texture2D>($"res://Ressources/Graphismes/Card/CardTemplate.png");
		}
		
		if (_isRecto2)
		{
			_skills[1].Texture = GD.Load<Texture2D>($"res://Ressources/Graphismes/Card/{_player.Classe}Power.png");
		}
		else
		{
			_skills[1].Texture = GD.Load<Texture2D>($"res://Ressources/Graphismes/Card/CardTemplate.png");
		}
		
		if (_isRecto3)
		{
			_skills[2].Texture = GD.Load<Texture2D>($"res://Ressources/Graphismes/Card/{_player.Classe}Power.png");
		}
		else
		{
			_skills[2].Texture = GD.Load<Texture2D>($"res://Ressources/Graphismes/Card/CardTemplate.png");
		}
	}

	private string GetSkillName(string skill)
	{
		switch (skill)
		{
			case "speed":
				return "Speed";
			case "health":
				return "Health";
			case "damage":
				return "Damage";
			case "stamina":
				return "Stamina";
			case "reload":
				return "Reload";
			case "staminause":
				return "Stamina Use";
			case "arrow":
				return "New Arrow";
			case "shootspeed":
				return "Shoot Speed";
			case "arrowpoison":
				return "Poison Arrow";
			case "arrowgel":
				return "Gel Arrow";
			case "healspeed":
				return "Heal Speed";
			case "lasermove":
				return "Laser Move";
			case "revive":
				return "Revive";
			case "vampire":
				return "Vampire";
			case "reviveall":
				return "Revive All";
			case "reloadprotection":
				return "Reload Protection";
			case "crit":
				return "Critical Chance";
			case "range":
				return "Range";
			case "agro":
				return "Agro";
			case "spike":
				return "Spike";
			case "absorption":
				return "Absorption";
			case "invincibility":
				return "Invincibility";
			case "escalibur":
				return "Escalibur";
			case "dashdegat":
				return "Dash Degat";
			case "dague":
				return "Dague";
			case "invisibility":
				return "Invisibility";
			case "doubleattack":
				return "Double Attack";
			case "poison":
				return "Poison";
			default:
				GD.Print(skill);
				return "ERROR";
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
				return "res://Ressources/Icons/Error.png";
		}
	}

	private string GetSkillDescription((string, int) skill)
	{
		switch (skill.Item1)
		{
		    case "speed":
			    return _languageDict["skillDescriptionSpeed"] + "+" + skill.Item2;
		    case "health":
		        return _languageDict["skillDescriptionHealth"] + "+" + skill.Item2;
		    case "damage":
			    return _languageDict["skillDescriptionDamage"] + "+" + skill.Item2;
		    case "stamina":
		        return _languageDict["skillDescriptionStamina"] + "+" + skill.Item2;
		    case "reload":
		        return _languageDict["skillDescriptionReload"] + "+" + skill.Item2;
		    case "staminause":
		        return _languageDict["skillDescriptionStaminaUse"] + "-" + skill.Item2;
		    case "arrow":
		        return _languageDict["skillDescriptionArrow"];
		    case "shootspeed":
		        return _languageDict["skillDescriptionShootSpeed"] + "+" + skill.Item2 + "%";
		    case "arrowpoison":
		        return _languageDict["skillDescriptionArrowPoison"];
		    case "arrowgel":
		        return _languageDict["skillDescriptionArrowGel"];
		    case "healspeed":
		        return _languageDict["skillDescriptionHealSpeed"] + "+" + skill.Item2;
		    case "lasermove":
		        return _languageDict["skillDescriptionLaserMove"];
		    case "revive":
		        return _languageDict["skillDescriptionRevive"];
		    case "vampire":
		        return _languageDict["skillDescriptionVampire"];
		    case "reviveall":
		        return _languageDict["skillDescriptionReviveAll"];
		    case "reloadprotection":
		        return _languageDict["skillDescriptionReloadProtection"];
		    case "crit":
			    return _languageDict["skillDescriptionCrit"] + "+" + skill.Item2 + "%";
		    case "range":
		        return _languageDict["skillDescriptionRange"];
		    case "agro":
		        return _languageDict["skillDescriptionAgro"];
		    case "spike":
		        return _languageDict["skillDescriptionSpike"];
		    case "absorption":
		        return _languageDict["skillDescriptionAbsorption"];
		    case "invincibility":
		        return _languageDict["skillDescriptionInvincibility"];
		    case "escalibur":
		        return _languageDict["skillDescriptionEscalibur"];
		    case "dashdegat":
		        return _languageDict["skillDescriptionDashDegat"];
		    case "dague":
		        return _languageDict["skillDescriptionDague"];
		    case "invisibility":
		        return _languageDict["skillDescriptionInvisibility"];
		    case "doubleattack":
		        return _languageDict["skillDescriptionDoubleAttack"];
		    case "poison":
		        return _languageDict["skillDescriptionPoison"];
		    default:
		        return "res://Ressources/Icons/Error.png";
		}
	}
	
	private (string, int) RandomSkill(int mult)
	{
		Random rand = new Random();
		(string, int)[] skill =
		{
			("damage", 2 * mult),
			("health", 10 * mult),
			("stamina", 100 * mult),
			("speed", 1 * mult)
		};
		
		return skill[rand.Next(0, 4)];
	}
	
	private (string, int) RandomChoice(List<(string, int)> skills)
	{
		Random rand = new Random();
		return skills[rand.Next(0, skills.Count)];
	}
	
	private void _on_skill_control_1_mouse_entered()
	{
		_animationPlayer[0].Play("Card1");
		GD.Print("test");
	}
	
	private void _on_skill_control_2_mouse_entered()
	{
		_animationPlayer[1].Play("Card2");
	}
	
	private void _on_skill_control_3_mouse_entered()
	{
		_animationPlayer[2].Play("Card3");
	}

	private void _on_skill_control_1_mouse_exited()
	{
		_animationPlayer[0].PlayBackwards("Card1");
	}
	
	private void _on_skill_control_2_mouse_exited()
	{
		_animationPlayer[1].PlayBackwards("Card2");
	}
	
	private void _on_skill_control_3_mouse_exited()
	{
		_animationPlayer[2].PlayBackwards("Card3");
	}

	private void _on_skill_control_1_pressed()
	{
		skillSelected(0);
	}
	
	private void _on_skill_control_2_pressed()
	{
		skillSelected(1);
	}
	
	private void _on_skill_control_3_pressed()
	{
		skillSelected(2);
	}

	private void skillSelected(int skillid)
	{
		if (GameManager.Skillnumber < 3 || skillid == 2)
		{
			(string, int) skill = _skillsName[ClassToInt(_player.Classe)][GameManager.levelStart][skillid];
			ApplySkill(skill);
			GameManager.levelStart += 1;
			if (GameManager.levelStart < GameManager.level)
			{
				Control skillmenu = GD.Load<PackedScene>("res://Scenes/HUD/SelectSkills.tscn").Instantiate<Control>();
				GetParent().AddChild(skillmenu);
			}
			else
			{
				GameManager.levelStart = GameManager.level;
				UDP.OneShot("next");
			}	
		}
		else
		{
			_error.Text = _languageDict["skillError"];
		}
	}
	
	private int ClassToInt(string classe)
	{
		switch (classe)
		{
			case "Archer":
				return 0;
			case "Scientist":
				return 1;
			case "Knight":
				return 2;
			case "Assassin":
				return 3;
			default:
				return -1;
		}
	}
	
	private void ApplySkill((string, int) skill, bool reverse = false)
	{
		switch (skill.Item1)
		{
			case "damage":
				GameManager.Damage += skill.Item2;
				break;
			case "health":
				GameManager.MaxHealth += skill.Item2;
				GameManager.Health += skill.Item2;
				break;
			case "stamina":
				GameManager.MaxStamina += skill.Item2;
				GameManager.Stamina += skill.Item2;
				break;
			case "speed":
				GameManager.WalkSpeed += skill.Item2;
				break;
			default:
				switch (skill.Item1)
				{
					case "reload":
						GameManager.ChargeSpeed += skill.Item2;
						break;
					case "staminause":
						GameManager.ManaUse -= skill.Item2;
						break;
					case "crit":
						GameManager.CriticalChance -= skill.Item2;
						break;
					case "arrow":
						GameManager.NbArrow += skill.Item2; 
						break;
					case "shootspeed":
						GameManager.ShootSpeed += skill.Item2 / 100f;
						break;
					case "healspeed":
						GameManager.HealSpeed += skill.Item2;
						break;
					case "range":
						CollisionShape3D sword = ((KnightScript)_player).Sword;
						sword.Scale = new Vector3(1, sword.Scale.Y + skill.Item2 / 4f, 1);
						break;
					case "dashdegat":
						GameManager.DashDegat += skill.Item2;
						break;
				}

				if (reverse)
				{
					int i = 0;
					while (i < 3 && GameManager.Skills[i].Item1 != skill.Item1)
					{
						i++;
					}

					if (i != 3)
					{
						GameManager.Skills[i] = ("", 0);
						GameManager.Skillnumber--;
					}
				}
				else
				{
					GameManager.Skills[GameManager.Skillnumber] = skill;
					GameManager.Skillnumber++;
				}
				break;
		}
	}
}
