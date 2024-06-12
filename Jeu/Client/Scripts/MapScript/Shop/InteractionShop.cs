using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class InteractionShop : Node3D
{
	public static bool OnShop = false;
	private Control InteractionUI;
	private AnimationPlayer Ani;
	private Label Gold;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Ani = GetNode<AnimationPlayer>("Shop/AnimationPlayer");
		Ani.Play("Reset");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(OnShop)
		{
			GameHUD.IsVisible = false;
		}
		else
		{
			GameHUD.IsVisible = true;
		}
	}
	private void _on_shop_body_entered(Node3D body)
	{
		if(body is ClassScript)
		{
			OnShop = true;
			Ani.Play("Open");
			Input.MouseMode = Input.MouseModeEnum.Visible;	
		}
	}


	private void _on_shop_body_exited(Node3D body)
	{
		if(body is ClassScript)
		{
			OnShop = false;
			Ani.Play("Close");	
			Input.MouseMode = Input.MouseModeEnum.Captured;	
		}
	}
}
