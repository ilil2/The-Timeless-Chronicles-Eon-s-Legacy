using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class GameHUD : Control
{
	public static bool OnInventory;
	
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
	
	private Color _deathColor = new Color(0.4f,0.4f,0.4f);
	
	private ProgressBar _xpBar;
	private Label _level;
	private Label _gold;
	
	public override void _Ready()
	{
		OnInventory = false;
		_xpBar = GetNode<ProgressBar>("XpBar");
		_level = GetNode<Label>("Level");
		_gold = GetNode<Label>("Money");
		
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
		if (OnInventory)
		{
			Visible = false;
		}
		else
		{
			Visible = true;
		}
		
		
		_gold.Text = $"{GameManager.Gold}";
		_xpBar.Value = GameManager.xp % 100;
		_level.Text = $"Level {GameManager.level}";
		
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
					default:
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
						_otherPlayer1Icon = GetNode<TextureRect>("OtherPlayer1/IconKnight");
						_otherPlayer1Icon.Visible = true;
						break;
					case "Archer":
						_otherPlayer1Icon = GetNode<TextureRect>("OtherPlayer1/IconArcher");
						_otherPlayer1Icon.Visible = true;
						break;
					case "Assassin":
						_otherPlayer1Icon = GetNode<TextureRect>("OtherPlayer1/IconAssassin");
						_otherPlayer1Icon.Visible = true;
						break;
					default:
						_otherPlayer1Icon = GetNode<TextureRect>("OtherPlayer1/IconScientist");
						_otherPlayer1Icon.Visible = true;
						break;
				}
				_otherClassChoose1 = true;
			}
			
			if (otherPlayer1.isAlive)
			{
				_otherPlayer1Pseudo.Text = GameManager.InfoAutreJoueur[$"pseudo{otherPlayer1.Id}"];
				_otherPlayer1HpBar.Value = Lib.Conversions.AtoF(GameManager.InfoAutreJoueur[$"hp{otherPlayer1.Id}"]) / otherPlayer1.GetMaxHealth() * 100;
				_otherPlayer1MpBar.Value = Lib.Conversions.AtoF(GameManager.InfoAutreJoueur[$"mp{otherPlayer1.Id}"]) / otherPlayer1.GetMaxStamina() * 100;
				_otherPlayer1HpBar.Modulate = new Color(1,0,0);
				_otherPlayer1MpBar.Modulate = new Color(0,0.56f,0);
				_otherPlayer1Icon.Modulate = new Color(1,1,1);
			}
			else
			{
				_otherPlayer1Pseudo.Text = GameManager.InfoAutreJoueur[$"pseudo{otherPlayer1.Id}"] + " X";
				_otherPlayer1HpBar.Value = 100;
				_otherPlayer1MpBar.Value = 100;
				_otherPlayer1HpBar.Modulate = _deathColor;
				_otherPlayer1MpBar.Modulate = _deathColor;
				_otherPlayer1Icon.Modulate = _deathColor;
			}
			
			if (otherPlayer2 != null)
			{
				_otherPlayer2.Visible = true;
				
				if (!_otherClassChoose2)
				{
					switch (otherPlayer2.Classe)
					{
						case "Knight":
							_otherPlayer2Icon = GetNode<TextureRect>("OtherPlayer2/IconKnight");
							_otherPlayer2Icon.Visible = true;
							break;
						case "Archer":
							_otherPlayer2Icon = GetNode<TextureRect>("OtherPlayer2/IconArcher");
							_otherPlayer2Icon.Visible = true;
							break;
						case "Assassin":
							_otherPlayer2Icon = GetNode<TextureRect>("OtherPlayer2/IconAssassin");
							_otherPlayer2Icon.Visible = true;
							break;
						default:
							_otherPlayer2Icon = GetNode<TextureRect>("OtherPlayer2/IconScientist");
							_otherPlayer2Icon.Visible = true;
							break;
					}
					_otherClassChoose2 = true;
				}
				
				if (otherPlayer2.isAlive)
				{
					_otherPlayer2Pseudo.Text = GameManager.InfoAutreJoueur[$"pseudo{otherPlayer2.Id}"];
					_otherPlayer2HpBar.Value = Lib.Conversions.AtoF(GameManager.InfoAutreJoueur[$"hp{otherPlayer2.Id}"]) / otherPlayer2.GetMaxHealth() * 100;
					_otherPlayer2MpBar.Value = Lib.Conversions.AtoF(GameManager.InfoAutreJoueur[$"mp{otherPlayer2.Id}"]) / otherPlayer2.GetMaxStamina() * 100;
					_otherPlayer2HpBar.Modulate = new Color(1,0,0);
					_otherPlayer2MpBar.Modulate = new Color(0,0.56f,0);
					_otherPlayer2Icon.Modulate = new Color(1,1,1);
				}
				else
				{
					_otherPlayer2Pseudo.Text = GameManager.InfoAutreJoueur[$"pseudo{otherPlayer2.Id}"] + " X";
					_otherPlayer2HpBar.Value = 100;
					_otherPlayer2MpBar.Value = 100;
					_otherPlayer2HpBar.Modulate = _deathColor;
					_otherPlayer2MpBar.Modulate = _deathColor;
					_otherPlayer2Icon.Modulate = _deathColor;
				}
				
				if (otherPlayer3 != null)
				{
					_otherPlayer3.Visible = true;
					
					if (!_otherClassChoose3)
					{
						switch (otherPlayer3.Classe)
						{
							case "Knight":
								_otherPlayer3Icon = GetNode<TextureRect>("OtherPlayer3/IconKnight");
								_otherPlayer3Icon.Visible = true;
								break;
							case "Archer":
								_otherPlayer3Icon = GetNode<TextureRect>("OtherPlayer3/IconArcher");
								_otherPlayer3Icon.Visible = true;
								break;
							case "Assassin":
								_otherPlayer3Icon = GetNode<TextureRect>("OtherPlayer3/IconAssassin");
								_otherPlayer3Icon.Visible = true;
								break;
							default:
								_otherPlayer3Icon = GetNode<TextureRect>("OtherPlayer3/IconScientist");
								_otherPlayer3Icon.Visible = true;
								break;
						}
						_otherClassChoose3 = true;
					}
					
					if (otherPlayer3.isAlive)
					{
						_otherPlayer3Pseudo.Text = GameManager.InfoAutreJoueur[$"pseudo{otherPlayer3.Id}"];
						_otherPlayer3HpBar.Value = Lib.Conversions.AtoF(GameManager.InfoAutreJoueur[$"hp{otherPlayer3.Id}"]) / otherPlayer3.GetMaxHealth() * 100;
						_otherPlayer3MpBar.Value = Lib.Conversions.AtoF(GameManager.InfoAutreJoueur[$"mp{otherPlayer3.Id}"])  / otherPlayer3.GetMaxStamina() * 100;
						_otherPlayer3HpBar.Modulate = new Color(1,0,0);
						_otherPlayer3MpBar.Modulate = new Color(0,0.56f,0);
						_otherPlayer3Icon.Modulate = new Color(1,1,1);
					}
					else
					{
						_otherPlayer3Pseudo.Text = GameManager.InfoAutreJoueur[$"pseudo{otherPlayer3.Id}"] + " X";
						_otherPlayer3HpBar.Value = 100;
						_otherPlayer3MpBar.Value = 100;
						_otherPlayer3HpBar.Modulate = _deathColor;
						_otherPlayer3MpBar.Modulate = _deathColor;
						_otherPlayer3Icon.Modulate = _deathColor;
					}
				}
			}
		}
	}
}
