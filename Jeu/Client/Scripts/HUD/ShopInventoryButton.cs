using Godot;
using System;
using JeuClient.Scripts.HUD;
using Lib;

public partial class ShopInventoryButton : Button
{
	public int id;
	public Panel Parent;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		id = int.Parse(Name);
		Parent = (Panel)GetParent().GetParent();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_pressed()
	{
		GD.Print($"{Name}!");
		if(Parent is FastAccess F)
		{
			if((Access.AccessArray[id-1])!=null && !ShopInventory.IsFull())
			{
				Potion potion = F.RemovePotion(id-1);
				ShopInventory.AddPotion(potion.ID);
			}
		}
		else
		{
			if((ShopInventory.Inventory[id-1])!=null && !(ShopInventory._fastaccess).IsFull())
			{
				Potion potion = ShopInventory.RemovePotion(id-1);
				(ShopInventory._fastaccess).AddPotion(potion.ID);
			}	
		}
	}

}
