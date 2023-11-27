using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class testphyScript2 : Node
{
	private Stopwatch stopwatch = new Stopwatch();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		stopwatch.Start();
		Random Rand = new Random();

		for (int i = 0; i<100;i++)
		{
			double T = 2*Math.PI*Rand.NextDouble();
			double U = Rand.NextDouble() + Rand.NextDouble();
			double r = 0;
			if (U>1)
			{
				r = 2-U;
			}
			else
			{
				r = U;
			}
			int X = (int)(40*r*Math.Cos(T));
			int Z = (int)(40*r*Math.Sin(T));
			
			int RandID = Rand.Next(1,4);
			
			
			RigidBody3D Room1 = new RigidBody3D();
			Room1.LockRotation = true;

			PhysicsBody3D Room = Room1;
			Room.AxisLockLinearY = true;
			
			CollisionShape3D RoomCollision = new CollisionShape3D();
			BoxShape3D BoxShape = new BoxShape3D();
			MeshInstance3D RoomMesh = new MeshInstance3D();
			BoxMesh BoxM = new BoxMesh();
			
			if (RandID == 1)
			{
				BoxShape.Size = new Vector3(3*6,120,3*6);
				BoxM.Size = new Vector3(3*6,2,3*6);
			}
			else if (RandID == 2)
			{
				BoxShape.Size = new Vector3(5*6,120,5*6);
				BoxM.Size = new Vector3(5*6,2,5*6);
			}
			else if (RandID == 3)
			{
				BoxShape.Size = new Vector3(7*6,120,7*6);
				BoxM.Size = new Vector3(7*6,2,7*6);
			}

			RoomCollision.Shape = BoxShape;
			RoomCollision.Position = new Vector3(RoomCollision.Position.X,RoomCollision.Position.Y+60,RoomCollision.Position.Z);
			RoomMesh.Mesh = BoxM;
			RoomMesh.Position = new Vector3(RoomMesh.Position.X,RoomMesh.Position.Y+1,RoomMesh.Position.Z);
			Room.AddChild(RoomCollision);
			Room.AddChild(RoomMesh);
			Room.Position = new Vector3(X,Room.Position.Y+60,Z);
			AddChild(Room);
			
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
