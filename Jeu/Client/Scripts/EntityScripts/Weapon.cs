using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class Weapon : Area3D
{
	private CollisionShape3D Shape;
	[Export] private int Damage = 1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Shape = GetNode<CollisionShape3D>("CollisionShape3D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_body_entered(Node3D body)
	{
		if(body is ClassScript player )
		{
			player.TakeDamage(Damage);
		}
	}
}

