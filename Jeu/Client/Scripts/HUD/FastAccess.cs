using Godot;
using System;
using JeuClient.Scripts.HUD;
using Lib;

public partial class FastAccess : Panel
{
	public TextureRect[] FastSlot = new TextureRect[3];
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
			if (Access.AccessArray[0] != null)
			{
				Access.AccessArray[0].UsePotion();
				Access.AccessArray[0] = null;
				UpdateSlot();
			}
		}
		else if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[10].Item2))
		{
			if (Access.AccessArray[1] != null)
			{
				Access.AccessArray[1].UsePotion();
				Access.AccessArray[1] = null;
				UpdateSlot();
			}
		}
		else if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[11].Item2))
		{
			if (Access.AccessArray[2] != null)
			{
				Access.AccessArray[2].UsePotion();
				Access.AccessArray[2] = null;
				UpdateSlot();
			}
		}
		else
		{
			UpdateSlot();
		}
	}
	
	private void UpdateSlot()
	{
		for(int i = 0; i<3 ; i++)
		{
			if(Access.AccessArray[i]!=null)
			{
				(FastSlot[i] as TextureRect).Texture = GD.Load<Texture2D>(Access.AccessArray[i].img);
			}
			else
			{
				(FastSlot[i] as TextureRect).Texture = null;
			}
		}
	}
	private Potion NewPotion(int ID)
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
	public bool IsFull()
	{
		for(int i = 0; i<3 ; i++)
		{
			if(Access.AccessArray[i]==null)
			{
				return false;
			}
		}
		return true;
	}
	public void AddPotion(int ID)
	{
		Potion potion = NewPotion(ID);
		int i = 0;
		while(Access.AccessArray[i]!=null)
		{
			i++;
		}
		Access.AccessArray[i] = potion;
		UpdateSlot();
	}
	public Potion RemovePotion(int ID)
	{
		Potion res = Access.AccessArray[ID];
		Access.AccessArray[ID] = null;
		UpdateSlot();
		return res;
	}
}
