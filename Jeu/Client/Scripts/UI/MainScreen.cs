using Godot;
using System;

public partial class MainScreen : Control
{
	private Label _label;
	public override void _Ready()
	{
		GetNode<AnimationPlayer>("AnimationPlayer").Play("Text");
	}
	
	public void OnResize()
	{
		_label = GetNode<Label>("Title");
		_label.LabelSettings.FontSize = (int)(40 * (GetViewportRect().Size.X / 1152));
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsKeyPressed(Key.Enter))
		{
			PackedScene LobbyScene = GD.Load<PackedScene>("res://Scenes/UI/LobbyManager.tscn");
			Control LobbyMenu = LobbyScene.Instantiate<Control>();
			AddChild(LobbyMenu);
			GameManager.state = 0;
			QueueFree();
		}
	}
}
