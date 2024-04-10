using Godot;
using System;
using Lib;

public partial class FastAccess : Panel
{
	public static Potion[] Access = new Potion[8];
	public static TextureRect[] FastSlot = new TextureRect[8];
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for(int i = 0;i<3;i++)
		{
			Control slot = GetNode<Control>($"Slot{i+1}");
			FastSlot[i] = slot.GetNode<TextureRect>("Logo");
		}
		UpdateSlot();
		//Logo.Texture = GD.Load<Texture2D>(Inventory[0].img);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[9].Item2))
		{
			if (Access[0] != null)
			{
				Access[0].UsePotion();
				Access[0] = null;
				UpdateSlot();
			}
		}
		else if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[10].Item2))
		{
			if (Access[1] != null)
			{
				Access[1].UsePotion();
				Access[1] = null;
				UpdateSlot();
			}
		}
		else if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[11].Item2))
		{
			if (Access[2] != null)
			{
				Access[2].UsePotion();
				Access[2] = null;
				UpdateSlot();
			}
		}
	}
	
	private static void UpdateSlot()
	{
		for(int i = 0; i<3 ; i++)
		{
			if(Access[i]!=null)
			{
				(FastSlot[i] as TextureRect).Texture = GD.Load<Texture2D>(Access[i].img);
			}
			else
			{
				(FastSlot[i] as TextureRect).Texture = null;
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
		for(int i = 0; i<3 ; i++)
		{
			if(Access[i]==null)
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
		while(Access[i]!=null)
		{
			i++;
		}
		Access[i] = potion;
		UpdateSlot();
	}
	public static Potion RemovePotion(int ID)
	{
		Potion res = Access[ID];
		Access[ID] = null;
		UpdateSlot();
		return res;
	}
}
