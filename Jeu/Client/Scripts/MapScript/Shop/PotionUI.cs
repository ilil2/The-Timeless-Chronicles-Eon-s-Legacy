using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;
[Tool]
public partial class PotionUI : Panel
{
	private TextureRect _img;
	private Label _desc;
	private Label _price;
	private Label _title;//tilt
	private Label _buttonText;
	
	private Button _buy;

	[Export] private CompressedTexture2D PotionLogo;
	[Export] private string Title;
	[Export] private string Description;
	[Export] private int Price;
	[Export] public int ID;
	
	private float _screenDefaultWidth = 1152;
	private float _labelDefaultSize = 16;
	private float _titleDefaultSize = 23;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_img = GetNode<TextureRect>("Img");
		_buy = GetNode<Button>("Button");
		_img.Texture = PotionLogo;
		_desc.Text = Description;
		_price.Text = $"Price: {Price}";
		_title.Text = Title;
	}
	
	public void OnResize()
	{
		_desc = GetNode<Label>("Desc");
		_price = GetNode<Label>("Price");
		_title = GetNode<Label>("Title");
		_buttonText = GetNode<Label>("Button/Label");
		
		_desc.LabelSettings.FontSize = (int)(_labelDefaultSize * (GetViewportRect().Size.X / _screenDefaultWidth));
		_price.LabelSettings.FontSize = (int)(_labelDefaultSize * (GetViewportRect().Size.X / _screenDefaultWidth));
		_buttonText.LabelSettings.FontSize = (int)(_labelDefaultSize * (GetViewportRect().Size.X / _screenDefaultWidth));
		_title.LabelSettings.FontSize = (int)(_titleDefaultSize * (GetViewportRect().Size.X / _screenDefaultWidth));
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
			GameManager.Gold=GameManager.Gold-Price;
			ShopInventory.AddPotion(ID);
		}
	}
	private void UpdatePrice()
	{
		if(GameManager.Gold<Price)
		{
			_price.LabelSettings.FontColor = new Color(1,0,0);
			_buy.Disabled = true;
			_buy.MouseDefaultCursorShape = CursorShape.Forbidden;
			
		}
		else
		{
			_price.LabelSettings.FontColor= new Color(1,1,1);
			_buy.Disabled = false;
			_buy.MouseDefaultCursorShape = CursorShape.Arrow;
		}
	}
}

