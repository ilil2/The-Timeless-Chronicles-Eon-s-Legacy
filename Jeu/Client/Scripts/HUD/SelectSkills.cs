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
	
	private AnimationPlayer _animationPlayer1;
	private AnimationPlayer _animationPlayer2;
	private AnimationPlayer _animationPlayer3;
	
	private Label _labelSkill1;
	private Label _labelSkill2;
	private Label _labelSkill3;
	
	private TextureRect[] _skills = new TextureRect[3];
	private (string, int)[][][] _skillsName;
	private ClassScript _player;
	
	public override void _Ready()
	{
		_skills[0] = GetNode<TextureRect>("Skill1");
		_skills[1] = GetNode<TextureRect>("Skill2");
		_skills[2] = GetNode<TextureRect>("Skill3");
		
		_labelSkill1 = GetNode<Label>("Label1");
		_labelSkill2 = GetNode<Label>("Label2");
		_labelSkill3 = GetNode<Label>("Label3");
		
		_animationPlayer1 = GetNode<AnimationPlayer>("AnimationPlayer1");
		_animationPlayer2 = GetNode<AnimationPlayer>("AnimationPlayer2");
		_animationPlayer3 = GetNode<AnimationPlayer>("AnimationPlayer3");

		_player = (ClassScript)GameManager.Joueur1;

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
	}

	public override void _Process(double delta)
	{
		ClassScript player = (ClassScript)GameManager.Joueur1;
		
		if (_isRecto1)
		{
			_skills[0].Texture = GD.Load<Texture2D>($"res://Ressources/Graphismes/Card/{player.Classe}Power.png");
		}
		else
		{
			_skills[0].Texture = GD.Load<Texture2D>($"res://Ressources/Graphismes/Card/CardTemplate.png");
		}
		
		if (_isRecto2)
		{
			_skills[1].Texture = GD.Load<Texture2D>($"res://Ressources/Graphismes/Card/{player.Classe}Power.png");
		}
		else
		{
			_skills[1].Texture = GD.Load<Texture2D>($"res://Ressources/Graphismes/Card/CardTemplate.png");
		}
		
		if (_isRecto3)
		{
			_skills[2].Texture = GD.Load<Texture2D>($"res://Ressources/Graphismes/Card/{player.Classe}Power.png");
		}
		else
		{
			_skills[2].Texture = GD.Load<Texture2D>($"res://Ressources/Graphismes/Card/CardTemplate.png");
		}
		
		_labelSkill1.Text = GetSkillName(_skillsName[ClassToInt(player.Classe)][GameManager.level][0].Item1);
		_labelSkill2.Text = GetSkillName(_skillsName[ClassToInt(player.Classe)][GameManager.level][1].Item1);
		_labelSkill3.Text = GetSkillName(_skillsName[ClassToInt(player.Classe)][GameManager.level][2].Item1);
		
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
			case "skill1":
				return "res://Assets/Textures/Skills/Attack.png";
			case "skill2":
				return "res://Assets/Textures/Skills/Block.png";
			case "skill3":
				return "res://Assets/Textures/Skills/Charge.png";
			default:
				return "";
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
		_animationPlayer1.Play("Card1");
	}
	
	private void _on_skill_control_2_mouse_entered()
	{
		_animationPlayer2.Play("Card2");
	}
	
	private void _on_skill_control_3_mouse_entered()
	{
		_animationPlayer3.Play("Card3");
	}

	private void _on_skill_control_1_mouse_exited()
	{
		_animationPlayer1.PlayBackwards("Card1");
	}
	
	private void _on_skill_control_2_mouse_exited()
	{
		_animationPlayer2.PlayBackwards("Card2");
	}
	
	private void _on_skill_control_3_mouse_exited()
	{
		_animationPlayer3.PlayBackwards("Card3");
	}

	private void _on_skill_control_1_pressed()
	{
		(string, int) skill = _skillsName[ClassToInt(_player.Classe)][GameManager.level][0];
		ApplySkill(skill);
		UDP.OneShot("next");
	}
	
	private void _on_skill_control_2_pressed()
	{
		(string, int) skill = _skillsName[ClassToInt(_player.Classe)][GameManager.level][1];
		ApplySkill(skill);
		UDP.OneShot("next");
	}
	
	private void _on_skill_control_3_pressed()
	{
		(string, int) skill = _skillsName[ClassToInt(_player.Classe)][GameManager.level][2];
		ApplySkill(skill);
		UDP.OneShot("next");
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
				_player.Damage += skill.Item2;
				break;
			case "health":
				_player.MaxHealth += skill.Item2;
				_player.Health += skill.Item2;
				break;
			case "stamina":
				_player.MaxStamina += skill.Item2;
				_player.Stamina += skill.Item2;
				break;
			case "speed":
				_player.WalkSpeed += skill.Item2;
				break;
			default:
				switch (skill.Item1)
				{
					case "reload":
						_player.ChargeSpeed += skill.Item2;
						break;
					case "staminause":
						_player.ManaUse -= skill.Item2;
						break;
					case "crit":
						_player.CriticalChance -= skill.Item2;
						break;
					case "arrow":
						((ArcherScript)_player).nbArrow += skill.Item2; 
						break;
					case "shootspeed":
						((ArcherScript)_player).shootspeed += skill.Item2 / 100f;
						break;
					case "arrowpoison":
						((ArcherScript)_player).PoisonArrow = !((ArcherScript)_player).PoisonArrow;
						break;
					case "arrowgel":
						((ArcherScript)_player).GelArrow = !((ArcherScript)_player).GelArrow;
						break;
					case "healspeed":
						((ScientistScript)_player).healspeed += skill.Item2;
						break;
					case "lasermove":
						((ScientistScript)_player).LaserMove = !((ScientistScript)_player).LaserMove;
						break;
					case "vampire":
						((ScientistScript)_player).Vampire = !((ScientistScript)_player).Vampire;
						break;
					case "reloadprotection":
						((ScientistScript)_player).RealoadProtection = !((ScientistScript)_player).RealoadProtection;
						break;
					case "range":
						CollisionShape3D sword = ((KnightScript)_player).Sword;
						sword.Scale = new Vector3(1, sword.Scale.Y + skill.Item2 / 4f, 1);
						break;
					case "spike":
						((KnightScript)_player).Spike = !((KnightScript)_player).Spike;
						break;
					case "escalibur":
						((KnightScript)_player).Escalibur = !((KnightScript)_player).Escalibur;
						break;
					case "dashdegat":
						((AssassinScript)_player).DashDegat += skill.Item2;
						break;
					case "doubleattack":
						((AssassinScript)_player).DoubleAttack = !((AssassinScript)_player).DoubleAttack;
						break;
					case "poison":
						((AssassinScript)_player).Poison = !((AssassinScript)_player).Poison;
						break;
				}

				if (reverse)
				{
					int i = 0;
					while (i < 3 && _player.Skills[i].Item1 != skill.Item1)
					{
						i++;
					}

					if (i != 3)
					{
						_player.Skills[i] = ("", 0);
						_player.Skillnumber--;
					}
				}
				else
				{
					_player.Skills[_player.Skillnumber] = skill;
					_player.Skillnumber++;
				}
				break;
		}
	}
}
