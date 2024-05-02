using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;
[Tool]
public partial class PotionUI : Panel
{
	private TextureRect Img;
	private Label Desc;
	private Label Pri;
	private Label Tilt;//title
	private Button Buy;

	[Export] private CompressedTexture2D PotionLogo;
	[Export] private string Title;
	[Export] private string Description;
	[Export] private int Price;
	[Export] public int ID;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Img = GetNode<TextureRect>("Img");
		Desc = GetNode<Label>("Desc");
		Pri = GetNode<Label>("Price");
		Tilt = GetNode<Label>("Title");
		Buy = GetNode<Button>("Button");
		Img.Texture = PotionLogo;
		Desc.Text = Description;
		Pri.Text = $"Price: {Price}";
		Pri.LabelSettings = new LabelSettings();
		Tilt.Text = Title;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(InteractionShop.OnShop)
		{
			UpdatePrice();
		}
	}
	private void _on_button_pressed()
	{
		if(GameManager.Gold>=Price && !ShopInventory.IsFull())
		{
			GD.Print($"Achat {Title}");
			GameManager.Gold-=Price;
			ShopInventory.AddPotion(ID);
		}
	}
	private void UpdatePrice()
	{
		if(GameManager.Gold<Price)
		{
			((LabelSettings)Pri.LabelSettings).FontColor = new Color(1,0,0);
			((BaseButton)Buy).Disabled = true;
			((Control)Buy).MouseDefaultCursorShape = CursorShape.Forbidden;
			
		}
		else
		{
			((LabelSettings)Pri.LabelSettings).FontColor= new Color(1,1,1);
			((BaseButton)Buy).Disabled = false;
			((Control)Buy).MouseDefaultCursorShape = CursorShape.Arrow;
		}
	}
}

