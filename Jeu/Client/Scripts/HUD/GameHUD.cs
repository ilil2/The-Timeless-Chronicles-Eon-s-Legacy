using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class GameHUD : Control
{
	private ProgressBar _HpBar;
	private ProgressBar _MpBar;		
	private Label _Pseudo;
	
	private TextureRect _Icon;
	
	public override void _Ready()
	{
		_HpBar = GetNode<ProgressBar>("HpBar");
		_MpBar = GetNode<ProgressBar>("MpBar");
		_Pseudo = GetNode<Label>("Pseudo");
	}

	public override void _Process(double delta)
	{
		ClassScript player = (ClassScript)GameManager.Joueur1;
		if (player != null)
		{
			switch (player.Classe)
			{
				case "Knight":
					_Icon = GetNode<TextureRect>("IconKnight");
					_Icon.Visible = true;
					break;
				case "Archer":
					_Icon = GetNode<TextureRect>("IconArcher");
					_Icon.Visible = true;
					break;
				case "Assassin":
					_Icon = GetNode<TextureRect>("IconAssassin");
					_Icon.Visible = true;
					break;
				case "Scientist":
					_Icon = GetNode<TextureRect>("IconScientist");
					_Icon.Visible = true;
					break;
			}
			
			_Pseudo.Text = player.Pseudo;
			_HpBar.Value = (float)player.GetHealth() / player.GetMaxHealth() * 100;
			_MpBar.Value = (float)player.GetStamina() / player.GetMaxStamina() * 100;
		}
	}
}
