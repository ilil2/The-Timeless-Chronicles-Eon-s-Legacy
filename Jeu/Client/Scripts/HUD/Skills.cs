using Godot;
using JeuClient.Scripts.PlayerScripts;

public partial class Skills : Control
{
	
	private TextureRect[] _skills = new TextureRect[3];
	
	public override void _Ready()
	{
		_skills[0] = GetNode<TextureRect>("Skill1");
		_skills[1] = GetNode<TextureRect>("Skill2");
		_skills[2] = GetNode<TextureRect>("Skill3");
	}

	public override void _Process(double delta)
	{
		ClassScript player = (ClassScript)GameManager.Joueur1;

		for (int i = 0; i < 3; i++)
		{
			//_skills[i].Texture = GD.Load<Texture2D>(GetSkillTexture(player.Skills[i]));
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
}
