using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

public partial class testphyScript2 : Node
{
	private Stopwatch stopwatch = new Stopwatch();
	private Random Rand = new Random();
	private bool MapReady = false;
	private int NbRoom = 200;
	private int LenWall = 6;
	private StaticBody3D MainRoom;
	private List<PhysicsBody3D> PseudoRoomList = new List<PhysicsBody3D>();
	private List<Node3D> RoomList = new List<Node3D>();
	private Dictionary<int,(int,int)> IdToLen = new Dictionary<int,(int,int)>
	{
		{1,(3,3)},
		{2,(5,5)},
		{3,(7,7)},
		{4,(5,3)}
	};
	private Dictionary<(int,int),int> LenToId = new Dictionary<(int,int),int>()
	{
		{(3,3),1},
		{(5,5),2},
		{(7,7),3},
		{(5,3),4}
	};
	
	public override void _Ready()
	{
		stopwatch.Start();
		MainRoom = InitMainRoom();
		CreatePseudoMap();
		stopwatch.Stop();
		GD.Print($"{NbRoom} Room");
		GD.Print($"Map crée en {stopwatch.Elapsed}");
	}
	
	public override void _Process(double delta)
	{
		if (!MapReady)
		{
			if (CheckSleep())
			{
				CreateMainRoom();
				CreateMap();
				OpenRoom();
				MapReady = true;
			}
		}
		
	}

	public bool MapIsReady()
	{
		return MapReady;
	}

	private StaticBody3D InitMainRoom()
	{
		StaticBody3D MainRoom = new StaticBody3D();
		CollisionShape3D MainRoomCollision = new CollisionShape3D();
		BoxShape3D MainBoxShape = new BoxShape3D();
		MainBoxShape.Size = new Vector3(7*LenWall,14*LenWall,7*LenWall);
		MainRoomCollision.Shape = MainBoxShape;
		MainRoomCollision.Position = new Vector3(0,7*LenWall,0);
		MainRoom.AddChild(MainRoomCollision);
		
		//MeshInstance3D MainRoomMesh = new MeshInstance3D();
		//BoxMesh MainRoomM = new BoxMesh();
		//MainRoomM.Size = new Vector3(7*LenWall,14*LenWall,7*LenWall);
		//MainRoomMesh.Mesh = MainRoomM;
		//MainRoomMesh.Position = new Vector3(0,9*LenWall,0);
		//MainRoom.AddChild(MainRoomMesh);
		
		AddChild(MainRoom);
		
		return MainRoom;
	}

	private void CreatePseudoMap()
	{
		for (int i = 0; i < NbRoom; i++)
		{
			double t = 2 * Math.PI * Rand.NextDouble();
			double u = Rand.NextDouble() + Rand.NextDouble();
			double r = 0;
			if (u > 1) r = 2 - u;
			else r = u;
			double x = (NbRoom / 10) * r * Math.Cos(t);
			double z = (NbRoom / 10) * r * Math.Sin(t);

			int ID = Rand.Next(1, 4);
			(int h, int w) = IdToLen[ID];
			
			RigidBody3D PRoom = new RigidBody3D();
			PRoom.LockRotation = true;
			PhysicsBody3D Room = PRoom;
			Room.AxisLockLinearY = true;
			
			CollisionShape3D RoomCollision = new CollisionShape3D();
			BoxShape3D BoxShape = new BoxShape3D();
			MeshInstance3D RoomMesh = new MeshInstance3D();
			BoxMesh BoxM = new BoxMesh();
			BoxShape.Size = new Vector3(h * LenWall, 14 * LenWall, w * LenWall);
			BoxM.Size = new Vector3(h * LenWall, LenWall, w * LenWall);
			
			RoomCollision.Shape = BoxShape;
			RoomCollision.Position = new Vector3(0,7*LenWall,0);
			RoomMesh.Mesh = BoxM;
			RoomMesh.Position = new Vector3(0,LenWall/2,0);
			RoomMesh.Name = "Mesh";
			Room.AddChild(RoomCollision);
			Room.AddChild(RoomMesh);
			Room.Position = Roundm((float)x*LenWall,(float)z*LenWall,LenWall);
			AddChild(Room);
			PseudoRoomList.Add(Room);

		}
	}

	private void CreateMap()
	{
		for (int i = 0; i < PseudoRoomList.Count; i++)
		{
			RigidBody3D PseudoRoom = (RigidBody3D)PseudoRoomList[i];
			MeshInstance3D MeshRoom = PseudoRoom.GetNode<MeshInstance3D>("Mesh");
			BoxMesh BoxM = (BoxMesh)MeshRoom.Mesh;
			(int h, int w) = ((int)BoxM.Size.X/LenWall,(int)BoxM.Size.Z/LenWall);
			int ID = LenToId[(h,w)];
			float X = PseudoRoom.Position.X;
			float Z = PseudoRoom.Position.Z;
			PseudoRoom.QueueFree();
			PackedScene R = GD.Load<PackedScene>($"res://Scenes/MapScenes/RoomScenes/Room{ID}.tscn");
			Node3D Room = R.Instantiate<Node3D>();
			Room.Position = Roundm(X, Z, LenWall);
			AddChild(Room);
			RoomList.Add(Room);

		}
	}

	private void CreateMainRoom()
	{
		MainRoom.QueueFree();
		PackedScene MR = GD.Load<PackedScene>($"res://Scenes/MapScenes/RoomScenes/RoomMain.tscn");
		Node3D MRoom = MR.Instantiate<Node3D>();
		AddChild(MRoom);
		RoomList.Add(MRoom);
		PackedScene MRG = GD.Load<PackedScene>($"res://Scenes/MapScenes/RoomScenes/RoomMainGate.tscn");
		Node3D MRoomGate = MRG.Instantiate<Node3D>();
		RoomList.Add(MRoomGate);
		AddChild(MRoomGate);
		
	}

	private bool CheckSleep()
	{
		for (int i = 0; i<PseudoRoomList.Count;i++)
		{
			if (((RigidBody3D)PseudoRoomList[i]).Sleeping == false)
			{
				return false;
			}
		}
		return true;
	}

	private Vector3 Roundm(float x, float z, int LenTile)
	{
		int NewX = (int)(Math.Floor((x+LenTile-1)/(float)(LenTile))*LenTile);
		int NewZ = (int)(Math.Floor((z+LenTile-1)/(float)(LenTile))*LenTile);
		return new Vector3(NewX,0,NewZ);
	}

	private double Distance(Node3D Room1, Node3D Room2)
	{
		return Math.Sqrt(Math.Pow(Room1.Position.X - Room2.Position.X, 2) +
						 Math.Pow(Room1.Position.Z - Room2.Position.Z, 2));
	}

	private void OpenRoom()
	{
		for (int i = 0; i < RoomList.Count-1; i++)
		{
			Node3D ActualRoom = RoomList[i];
			for (int j = i+1; j < RoomList.Count; j++)
			{
				Node3D TestedRoom = RoomList[j];
				double dist = Distance(ActualRoom, TestedRoom);
				
				if (dist<100)
				{
					int NbWall = 0;
					for (int k = 0; k < ActualRoom.GetChildCount(); k++)
					{
						Node3D ActualChild = ActualRoom.GetChild<Node3D>(k);
						for (int l = 0; l < TestedRoom.GetChildCount(); l++)
						{
							Node3D TestedChild = TestedRoom.GetChild<Node3D>(l);
							bool TestPos = (TestedChild.Position.X+TestedRoom.Position.X == ActualChild.Position.X+ActualRoom.Position.X)&&(TestedChild.Position.Z+TestedRoom.Position.Z == ActualChild.Position.Z+ActualRoom.Position.Z);
							if (TestPos)
							{
								if (l<TestedRoom.GetChildCount())
								{
									NbWall+=1;
									l = TestedRoom.GetChildCount();
								}
							}
						}
					}
					int RandWall = Rand.Next(0,NbWall);
					int index = 0;
					if (i==0) index = NbWall + 1;
					for (int k = 0; k < ActualRoom.GetChildCount(); k++)
					{
						Node3D ActualChild = ActualRoom.GetChild<Node3D>(k);
						for (int l = 0; l < TestedRoom.GetChildCount(); l++)
						{
							Node3D TestedChild = TestedRoom.GetChild<Node3D>(l);
							bool TestPos = (TestedChild.Position.X+TestedRoom.Position.X == ActualChild.Position.X+ActualRoom.Position.X)&&(TestedChild.Position.Z+TestedRoom.Position.Z == ActualChild.Position.Z+ActualRoom.Position.Z);
							if (TestPos)
							{
								if (l<TestedRoom.GetChildCount())
								{
									ActualChild.QueueFree();
									if (index == RandWall)
									{
										TestedChild.QueueFree();
									}
									index+=1;
									l = TestedRoom.GetChildCount();
								}
							}
						}
					}
				}
			}
		}
	}
}
