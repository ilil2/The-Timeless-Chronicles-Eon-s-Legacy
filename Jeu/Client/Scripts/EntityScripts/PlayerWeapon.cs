using Godot;
using System;

public partial class PlayerWeapon : Area3D
{
	[Export] private int Damage = 10;
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
		if(body is MobScript mob)
		{
			mob.TakeDamage(Damage);
		}
	}
}
