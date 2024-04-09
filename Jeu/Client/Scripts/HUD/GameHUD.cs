using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class GameHUD : Control
{
	private ProgressBar _hpBar;
	private ProgressBar _mpBar;		
	private Label _pseudo;
	private TextureRect _icon;
	private bool _classChoose;
	
	private Control _otherPlayer1;
	private ProgressBar _otherPlayer1HpBar;
	private ProgressBar _otherPlayer1MpBar;		
	private Label _otherPlayer1Pseudo;
	private TextureRect _otherPlayer1Icon;
	private bool _otherClassChoose1;
	
	private Control _otherPlayer2;
	private ProgressBar _otherPlayer2HpBar;
	private ProgressBar _otherPlayer2MpBar;		
	private Label _otherPlayer2Pseudo;
	private TextureRect _otherPlayer2Icon;
	private bool _otherClassChoose2;
	
	private Control _otherPlayer3;
	private ProgressBar _otherPlayer3HpBar;
	private ProgressBar _otherPlayer3MpBar;		
	private Label _otherPlayer3Pseudo;
	private TextureRect _otherPlayer3Icon;
	private bool _otherClassChoose3;
	
	public override void _Ready()
	{
		_hpBar = GetNode<ProgressBar>("HpBar");
		_mpBar = GetNode<ProgressBar>("MpBar");
		_pseudo = GetNode<Label>("Pseudo");

		_otherPlayer1 = GetNode<Control>("OtherPlayer1");
		_otherPlayer1HpBar = GetNode<ProgressBar>("OtherPlayer1/HpBar");
		_otherPlayer1MpBar = GetNode<ProgressBar>("OtherPlayer1/MpBar");
		_otherPlayer1Pseudo = GetNode<Label>("OtherPlayer1/Pseudo");
		
		_otherPlayer2 = GetNode<Control>("OtherPlayer2");
		_otherPlayer2HpBar = GetNode<ProgressBar>("OtherPlayer2/HpBar");
		_otherPlayer2MpBar = GetNode<ProgressBar>("OtherPlayer2/MpBar");
		_otherPlayer2Pseudo = GetNode<Label>("OtherPlayer2/Pseudo");
		
		_otherPlayer3 = GetNode<Control>("OtherPlayer3");
		_otherPlayer3HpBar = GetNode<ProgressBar>("OtherPlayer3/HpBar");
		_otherPlayer3MpBar = GetNode<ProgressBar>("OtherPlayer3/MpBar");
		_otherPlayer3Pseudo = GetNode<Label>("OtherPlayer3/Pseudo");
	}

	public override void _Process(double delta)
	{
		ClassScript player = (ClassScript)GameManager.Joueur1;
		OtherClassScript otherPlayer1 = (OtherClassScript)GameManager.Joueur2;
		OtherClassScript otherPlayer2 = (OtherClassScript)GameManager.Joueur3;
		OtherClassScript otherPlayer3 = (OtherClassScript)GameManager.Joueur4;
		
		if (player != null)
		{
			if (!_classChoose)
			{
				switch (player.Classe)
				{
					case "Knight":
						_icon = GetNode<TextureRect>("IconKnight");
						_icon.Visible = true;
						break;
					case "Archer":
						_icon = GetNode<TextureRect>("IconArcher");
						_icon.Visible = true;
						break;
					case "Assassin":
						_icon = GetNode<TextureRect>("IconAssassin");
						_icon.Visible = true;
						break;
					case "Scientist":
						_icon = GetNode<TextureRect>("IconScientist");
						_icon.Visible = true;
						break;
				}
				_classChoose = true;
			}
			
			_pseudo.Text = player.Pseudo;
			_hpBar.Value = (float)player.GetHealth() / player.GetMaxHealth() * 100;
			_mpBar.Value = (float)player.GetStamina() / player.GetMaxStamina() * 100;
		}
		
		if (otherPlayer1 != null)
		{
			_otherPlayer1.Visible = true;
			if (!_otherClassChoose1)
			{
				switch (otherPlayer1.Classe)
				{
					case "Knight":
						_icon = GetNode<TextureRect>("OtherPlayer1/IconKnight");
						_icon.Visible = true;
						break;
					case "Archer":
						_icon = GetNode<TextureRect>("OtherPlayer1/IconArcher");
						_icon.Visible = true;
						break;
					case "Assassin":
						_icon = GetNode<TextureRect>("OtherPlayer1/IconAssassin");
						_icon.Visible = true;
						break;
					case "Scientist":
						_icon = GetNode<TextureRect>("OtherPlayer1/IconScientist");
						_icon.Visible = true;
						break;
				}
				_otherClassChoose1 = true;
			}
			
			_otherPlayer1Pseudo.Text = otherPlayer1.Pseudo;
			_otherPlayer1HpBar.Value = (float)otherPlayer1.GetHealth() / otherPlayer1.GetMaxHealth() * 100;
			_otherPlayer1MpBar.Value = (float)otherPlayer1.GetStamina() / otherPlayer1.GetMaxStamina() * 100;
			
			if (otherPlayer2 != null)
			{
				_otherPlayer2.Visible = true;
				if (!_otherClassChoose2)
				{
					switch (otherPlayer2.Classe)
					{
						case "Knight":
							_icon = GetNode<TextureRect>("OtherPlayer2/IconKnight");
							_icon.Visible = true;
							break;
						case "Archer":
							_icon = GetNode<TextureRect>("OtherPlayer2/IconArcher");
							_icon.Visible = true;
							break;
						case "Assassin":
							_icon = GetNode<TextureRect>("OtherPlayer2/IconAssassin");
							_icon.Visible = true;
							break;
						case "Scientist":
							_icon = GetNode<TextureRect>("OtherPlayer2/IconScientist");
							_icon.Visible = true;
							break;
					}
					_otherClassChoose2 = true;
				}
			
				_otherPlayer2Pseudo.Text = otherPlayer2.Pseudo;
				_otherPlayer2HpBar.Value = (float)otherPlayer2.GetHealth() / otherPlayer2.GetMaxHealth() * 100;
				_otherPlayer2MpBar.Value = (float)otherPlayer2.GetStamina() / otherPlayer2.GetMaxStamina() * 100;
				
				if (otherPlayer3 != null)
				{
					_otherPlayer3.Visible = true;
					if (!_otherClassChoose3)
					{
						switch (otherPlayer3.Classe)
						{
							case "Knight":
								_icon = GetNode<TextureRect>("OtherPlayer3/IconKnight");
								_icon.Visible = true;
								break;
							case "Archer":
								_icon = GetNode<TextureRect>("OtherPlayer3/IconArcher");
								_icon.Visible = true;
								break;
							case "Assassin":
								_icon = GetNode<TextureRect>("OtherPlayer3/IconAssassin");
								_icon.Visible = true;
								break;
							case "Scientist":
								_icon = GetNode<TextureRect>("OtherPlayer3/IconScientist");
								_icon.Visible = true;
								break;
						}
						_otherClassChoose3 = true;
					}
			
					_otherPlayer3Pseudo.Text = otherPlayer3.Pseudo;
					_otherPlayer3HpBar.Value = (float)otherPlayer3.GetHealth() / otherPlayer3.GetMaxHealth() * 100;
					_otherPlayer3MpBar.Value = (float)otherPlayer3.GetStamina() / otherPlayer3.GetMaxStamina() * 100;
				}
			}
		}
	}
}
