using Godot;
using System;

public partial class ShopInventory : Panel
{
	public static Potion[] Inventory = new Potion[8];
	public static TextureRect[] Slot = new TextureRect[8];
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for(int i = 0;i<8;i++)
		{
			Control slot = GetNode<Control>($"Slot{i+1}");
			Slot[i] = slot.GetNode<TextureRect>("Logo");
		}
		UpdateSlot();
		//Logo.Texture = GD.Load<Texture2D>(Inventory[0].img);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private static void UpdateSlot()
	{
		for(int i = 0; i<8 ; i++)
		{
			if(Inventory[i]!=null)
			{
				(Slot[i] as TextureRect).Texture = GD.Load<Texture2D>(Inventory[i].img);
			}
			else
			{
				(Slot[i] as TextureRect).Texture = null;
			}
		}
	}
	private static Potion NewPotion(int ID)
	{
		switch (ID)
		{
			case 0:
				return new HealPotion();
			case 1:
				return new StaminaPotion();
			case 2:
				return new SpeedPotion();
			case 3:
				return new ResistancePotion();
			case 4:
				return new ResurectionPotion();
			case 5:
				return new AmnesiaPotion();
			default:
				return new HealPotion();
				
		}
	}
	public static bool IsFull()
	{
		for(int i = 0; i<8 ; i++)
		{
			if(Inventory[i]==null)
			{
				return false;
			}
		}
		return true;
	}
	public static void AddPotion(int ID)
	{
		Potion potion = NewPotion(ID);
		int i = 0;
		while(Inventory[i]!=null)
		{
			i++;
		}
		Inventory[i] = potion;
		UpdateSlot();
	}
}
