using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class GameHUD : Control
{
	private ProgressBar _HpBar;
	private ProgressBar _MpBar;		
	
	public override void _Ready()
	{
		_HpBar = GetNode<ProgressBar>("HpBar");
		_MpBar = GetNode<ProgressBar>("MpBar");
	}

	public override void _Process(double delta)
	{
		ClassScript player = (ClassScript)GameManager.Joueur1;
		if (player != null)
		{
			_HpBar.Value = (float)player.GetHealth() / player.GetMaxHealth() * 100;
			_MpBar.Value = (float)player.GetStamina() / player.GetMaxStamina() * 100;
		}
	}
}
