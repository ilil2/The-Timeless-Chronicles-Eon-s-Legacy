using Godot;
using System;

public partial class SoundManager : Node
{
	private AudioStreamPlayer _shop;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_shop = GetNode<AudioStreamPlayer>("Shop");
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
			}
		}
		else
		{
			_shop.Stop();
		}
	}
}
