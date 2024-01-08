using Godot;
using System;

public partial class Arrow : RigidBody3D
{
	public void Dispawn()
	{
		QueueFree();
	}
	
	public void OnCollision(StaticBody3D body)
	{
		QueueFree();
	}
}
