using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

/*
To Do List du code:
- La rotation des salles -OK
- La séparation des types dans les scenes Room -PB
- Récup coo de la salle la plus loin -OK
- Ajout du dernier type de salle -OK
- Corrigé les % et la génération -OK
- Déco des salles
- SpawnPoint des mobs
- Optimiser le jeu
*/

public partial class MapLvl1Script : Node
{
	private Stopwatch stopwatch = new Stopwatch();
	private Random Rand = new Random();
	private bool MapReady = false;
	private int NbRoom = 300;
	private int LenWall = 6;
	private StaticBody3D MainRoom;
	private List<PhysicsBody3D> PseudoRoomList = new List<PhysicsBody3D>();
	private List<Node3D> RoomList = new List<Node3D>();
	private PackedScene AssetC = GD.Load<PackedScene>("res://Ressources/Map/Egypt1/Temple/Asset/Small_gate.tscn");
	private float SpawnX;
	private float SpawnZ;
	private double MaxSpawnDist = 0;
	private Dictionary<int,(int,int)> IdToLen = new Dictionary<int,(int,int)>
	{
		{1,(3,3)},
		{2,(5,3)},
		{3,(5,5)},
		{4,(7,5)},
		{5,(7,7)}
	};
	private Dictionary<(int,int),int> LenToId = new Dictionary<(int,int),int>()
	{
		{(3,3),1},
		{(5,3),2},
		{(5,5),3},
		{(7,5),4},
		{(7,7),5}
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
				GD.Print($"SpawnPoint Player X:{SpawnX} Z:{SpawnZ}");
				CharacterBody3D Player = GetNode<CharacterBody3D>("DebugPack/Player");
				Player.Position = new Vector3(SpawnX,2,SpawnZ);
			}
		}
		
	}

	public bool MapIsReady()
	{
		return MapReady;
	}
	
	public (int,int) GetSpawnLocation()
	{
		return ((int)SpawnX,(int)SpawnZ);
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
			double x = ((NbRoom / 10)+0) * r * Math.Cos(t);
			double z = ((NbRoom / 10)+0) * r * Math.Sin(t);

			int ID;// = Rand.Next(1, 6);
			int RandInt = Rand.Next(1,101);
			if (RandInt<=35) ID = 1;
			else if (RandInt<=60) ID = 2;
			else if (RandInt<=80) ID = 3;
			else if (RandInt<=95) ID = 4;
			else ID = 5;
			(int h, int w) = IdToLen[ID];
			float Angle = 90*Rand.Next(0,4);
			
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
			Room.RotationDegrees = new Vector3(0,Angle,0);
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
			float A = PseudoRoom.RotationDegrees.Y;
			
			PseudoRoom.QueueFree();
			PackedScene R = GD.Load<PackedScene>($"res://Scenes/MapScenes/Lvl1/RoomScenes/Room{ID}.tscn");
			Node3D Room = R.Instantiate<Node3D>();
			Room.Position = Roundm(X, Z, LenWall);
			Vector3 rotationY = new Vector3(0, 1, 0);
			Room.RotationDegrees = new Vector3(0,A,0);
			AddChild(Room);
			RoomList.Add(Room);

		}
	}

	private void CreateMainRoom()
	{
		MainRoom.QueueFree();
		
		PackedScene MR = GD.Load<PackedScene>($"res://Scenes/MapScenes/Lvl1/RoomScenes/RoomMain.tscn");
		Node3D MRoom = MR.Instantiate<Node3D>();
		AddChild(MRoom);
		RoomList.Add(MRoom);
		
		PackedScene MRG = GD.Load<PackedScene>($"res://Scenes/MapScenes/Lvl1/RoomScenes/RoomMainGate.tscn");
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
			int find = ActualRoom.GetChildCount();
			for (int j = i+1; j < RoomList.Count; j++)
			{
				Node3D TestedRoom = RoomList[j];
				double dist = Distance(ActualRoom, TestedRoom);
				
				if (dist<100)
				{
					List<Node3D> OverlapWallList = new List<Node3D>();
					for (int k = 2; k < ActualRoom.GetChildCount(); k++)
					{
						Node3D ActualChild = ActualRoom.GetChild<Node3D>(k);
						for (int l = 2; l < TestedRoom.GetChildCount(); l++)
						{
							Node3D TestedChild = TestedRoom.GetChild<Node3D>(l);
							bool TestPos = (int)ActualChild.GlobalTransform.Origin.X==(int)TestedChild.GlobalTransform.Origin.X && (int)ActualChild.GlobalTransform.Origin.Z==(int)TestedChild.GlobalTransform.Origin.Z;//(TestedChild.Position.X+TestedRoom.Position.X == ActualChild.Position.X+ActualRoom.Position.X)&&(TestedChild.Position.Z+TestedRoom.Position.Z == ActualChild.Position.Z+ActualRoom.Position.Z);
							if (TestPos)
							{
								if (l<TestedRoom.GetChildCount())
								{
									//TestedRoom.RemoveChild(TestedChild);
									TestedChild.QueueFree();
									OverlapWallList.Add(ActualChild);
									l = TestedRoom.GetChildCount();
								}
							}
						}
						
					}
					int RandWall = Rand.Next(0,OverlapWallList.Count);
					for (int k = 0; k < OverlapWallList.Count; k++)
					{
						if (i==0) k = OverlapWallList.Count+1;
						if (k == RandWall)
						{
							Node3D Child = OverlapWallList[k];
							Node3D Gate = AssetC.Instantiate<Node3D>();
							Gate.Rotation = Child.Rotation+ActualRoom.Rotation;
							Gate.Position = Child.GlobalTransform.Origin;
							AddChild(Gate);
							//ActualRoom.RemoveChild(Child);
							Child.QueueFree();
						}
					}
				}
			}
			double DistToMain = Distance(ActualRoom,MainRoom);
			if (MaxSpawnDist<DistToMain)
			{
				MaxSpawnDist = DistToMain;
				SpawnX = ActualRoom.Position.X;
				SpawnZ = ActualRoom.Position.Z;
			}
			if (ActualRoom.GetChildCount()==find && false)
			{
				GD.Print("/!\\ Room Non Connectée !");
				MeshInstance3D M = new MeshInstance3D();
				BoxMesh B = new BoxMesh();
				B.Size = new Vector3(10,50,10);
				M.Mesh = B;
				M.Position = new Vector3(ActualRoom.Position.X,35,ActualRoom.Position.Z);
				AddChild(M);
			}
		}
	}
}
