using Godot;
using System;

public partial class RoomLvl1 : Node3D
{
	/*
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
	*/
	public int Id;
	public int X;
	public int Z;
	private int Length;
	private int Width;
	private Node3D Room;
	private PhysicsBody3D PseudoRoom;
	
	public RoomLvl1(int id,int x,int z)
	{
		Id = id;
		X = x;
		Z = z;
	}
}
