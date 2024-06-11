using Godot;
using System;

public partial class SoundManager : Node
{
	private AudioStreamPlayer _shop;
	private AudioStreamPlayer _menu;
	private AudioStreamPlayer _map1;
	private AudioStreamPlayer _map2;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_shop = GetNode<AudioStreamPlayer>("Shop");
		_menu = GetNode<AudioStreamPlayer>("Menu");
		_map1 = GetNode<AudioStreamPlayer>("Lvl1");
		_map2 = GetNode<AudioStreamPlayer>("Lvl2");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (GameManager.Map.CamOnPlayer)
		{
			if (GameManager.StartMap)
			{
				if (GameManager.Map is Shop && _shop.Playing == false)
				{
					_shop.Play();
				}

				else if (GameManager.Map is MapLvl1Script && _map1.Playing == false)
				{
					_map1.Play();
				}
				
				else if (GameManager.Map is MapLvl2Script && _map2.Playing == false)
				{
					_map2.Play();
				}

				else
				{
					_shop.Stop();
					_map1.Stop();
					_map2.Stop();
					_menu.Stop();
				}
			}
		}
		else
		{
			_shop.Stop();
			_map1.Stop();
			_map2.Stop();

			if (_menu.Playing == false)
			{
				_menu.Play();
			}
		}
	}
}
