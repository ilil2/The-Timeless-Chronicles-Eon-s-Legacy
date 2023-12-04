using Godot;
using System;

public partial class Arrow : RigidBody3D
{
	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = LinearVelocity;
		LinearVelocity = velocity;
	}
	
	public void Dispawn()
	{
		QueueFree();
	}
	
	public void OnCollision(StaticBody3D body)
	{
		QueueFree();
	}
}
