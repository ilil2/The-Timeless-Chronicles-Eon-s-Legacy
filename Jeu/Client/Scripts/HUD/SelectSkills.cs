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
	
	private TextureRect[] _skills = new TextureRect[3];
	private (string, int)[][][] _skillsName;
	
	public override void _Ready()
	{
		_skills[0] = GetNode<TextureRect>("Skill1");
		_skills[1] = GetNode<TextureRect>("Skill2");
		_skills[2] = GetNode<TextureRect>("Skill3");
		
		_animationPlayer1 = GetNode<AnimationPlayer>("AnimationPlayer1");
		_animationPlayer2 = GetNode<AnimationPlayer>("AnimationPlayer2");
		_animationPlayer3 = GetNode<AnimationPlayer>("AnimationPlayer3");

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
					("dashdega", 5),
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
					("dashdega", 10),
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
		UDP.OneShot("next");
	}
	
	private void _on_skill_control_2_pressed()
	{
		UDP.OneShot("next");
	}
	
	private void _on_skill_control_3_pressed()
	{
		UDP.OneShot("next");
	}
}
