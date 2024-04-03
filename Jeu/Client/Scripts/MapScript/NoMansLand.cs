using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class NoMansLand : Area3D
{
	private PackedScene Grave = GD.Load<PackedScene>("res://Ressources/Map/Global/Object/Gravestone/Gravestone.tscn");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_body_entered(Node3D body)
	{
		if (body is ClassScript player)
		{
			Node3D Stone = Grave.Instantiate<Node3D>();
			Stone.Name = $"{player.GetId()}";
			Stone.Position = new Vector3(player.Position.X,0,player.Position.Z);
			AddChild(Stone);
			Stone.Rotation = player.Rotation;
			(Stone as Gravestone).Pseudo.Text = player.Pseudo;
		}
	}


	private void _on_body_exited(Node3D body)
	{
		if (body is ClassScript player)
		{
			GetNode<Node3D>($"{player.GetId}").QueueFree();
		}
	
	}
}

