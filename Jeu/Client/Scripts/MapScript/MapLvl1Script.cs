using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Environment = Godot.Environment;
using Lib;


public partial class MapLvl1Script : Node3D, IMap
{
	private Stopwatch stopwatch = new Stopwatch();
	private Stopwatch fogwatch = new Stopwatch();
	private Random Rand = new Random(42);
	private Random FogRand = new Random(42);
	private static bool MapReady = false;
	private int NbRoom = 250;
	private int LenWall = 6;
	private StaticBody3D MainRoom;
	private List<RigidBody3D> PseudoRoomList = new List<RigidBody3D>();
	private List<Node3D> RoomList = new List<Node3D>();
	private List<CharacterBody3D> MobList = new List<CharacterBody3D>();
	private PackedScene AssetC = GD.Load<PackedScene>("res://Ressources/Map/Egypt/Temple/Asset/Small_gate.tscn");
	private Node3D SpawnRoom;
	private double MaxSpawnDist = 0;
	private int FogState = 0;
	private int StartTime = 0;
	private int Duration = 0;
	private int FrameCount = 0;
	private int StartInput = 0;
	private NavigationRegion3D NavMesh = GD.Load<PackedScene>("res://Scenes/NavMesh.tscn").Instantiate<NavigationRegion3D>();
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

	public int step;
	
	public override void _Ready()
	{
		step = 0;
		stopwatch.Start();
		MainRoom = InitMainRoom();
		CreatePseudoMap();
	}
	
	public override void _Process(double delta)
	{
		FrameCount+=1;
		if (!MapReady)
		{
			if (MapTool.CheckSleep(PseudoRoomList))
			{
				AddChild(NavMesh);
				CreateMainRoom();
				CreateMap();
				OpenRoom();
				//((NavMeshScript)NavMesh).CreateNavMesh();
				//CreateMob();
				
				
				MapReady = true;
				stopwatch.Stop();
		
				GD.Print($"{NbRoom} Room");
				GD.Print($"Map crée en {stopwatch.Elapsed}");
				
				Duration = FogRand.Next(120,260);
				fogwatch.Start();
				GD.Print($"Fog Start in {Duration}");	
			}
		}
		else
		{
			//Process
			CreateFog();
			DayCycle();
			//Debug Map onl
			
			//--------------
			//RenderDist();
		}
		
	}
	
	public int Step()
	{
		return step;
	}
	
	public List<(int,int,int)> GetSpawnLocation()
	{
		Node3D SpawnPoint = SpawnRoom.GetNode<Node3D>("Spawn");
		List<(int,int,int)> Spawn = new List<(int,int,int)>();
		for (int i = 0; i<4;i++)
		{
			Node3D Pos = SpawnPoint.GetChild<Node3D>(i);
			(int,int,int) res = ((int)Pos.GlobalPosition.X,(int)Pos.GlobalPosition.Y,(int)Pos.GlobalPosition.Z);
			GD.Print(res);
			Spawn.Add(res); 
		}
		return Spawn;
	}
	
	public bool MapIsReady()
	{
		return MapReady;
	}
	
	public void DebugMode(CharacterBody3D Player, bool DebugMode)
	{
		MapTool.Debug(Player,this,DebugMode);
		WorldEnvironment world = GetNode<WorldEnvironment>("World");
		Godot.Environment env = world.Environment;
		env.VolumetricFogEnabled = !DebugMode;
		
	}
	
	public void SetSeed(int seed, int seed2)
	{
		Rand = new Random(seed);
		FogRand = new Random(seed2);
		GD.Print($"Seed set : {seed}");
	}
	
	private void CreateMob()
	{
		for(int i = 0; i<RoomList.Count; i++)
		{
			Node3D Room = RoomList[i].GetNode<Node3D>("Spawn");
			for(int j = 0; j<Room.GetChildCount(); j++)
			{
				Node3D SpawnPoint = Room.GetChild<Node3D>(j);
				if(Rand.Next(1,4)==1)
				{
					PackedScene M = GD.Load<PackedScene>("res://Scenes/EntityScenes/Mob.tscn");
					CharacterBody3D Mob = M.Instantiate<CharacterBody3D>();
					Mob.Position = SpawnPoint.GlobalTransform.Origin;
					MobList.Add(Mob);
					AddChild(Mob);
				}
			}
			
		}
	}
	
	private void DayCycle()
	{
		DirectionalLight3D Sun = GetNode<DirectionalLight3D>("Sun");
		const double time = 0.00001;
		Sun.Rotation = new Vector3(Sun.Rotation.X,(float)(Sun.Rotation.Y + time),Sun.Rotation.Z);
	}
	
	private void CreateFog()
	{
		WorldEnvironment world = GetNode<WorldEnvironment>("World");
		Godot.Environment env = world.Environment;

		if (GameManager.Fog)
			env.VolumetricFogEnabled = true;
		else
			env.VolumetricFogEnabled = false;
		
		if (FogState==0)
		{
			if ((int)fogwatch.Elapsed.TotalSeconds-StartTime>=Duration)
			{
					FogState=1;
					GD.Print("Starting Fog....");
			}
		}
		else if (FogState==1)
		{
			if (env.VolumetricFogDensity<0.05)
			{
				env.VolumetricFogDensity=(float)(env.VolumetricFogDensity+0.0001);
			}
			else
			{
				env.VolumetricFogDensity=(float)0.1;
				FogState=2;
				Duration = FogRand.Next(120,260);
				StartTime = (int)fogwatch.Elapsed.TotalSeconds;
				GD.Print($"Fog Start! End in {Duration} seconde");
			}
		}
		else if (FogState==2)
		{
			if ((int)fogwatch.Elapsed.TotalSeconds-StartTime>=Duration)
			{
					FogState=3;
					GD.Print("Ending Fog...");
			}
		}
		else if (FogState==3)
		{
			if (env.VolumetricFogDensity>0)
			{
				env.VolumetricFogDensity=(float)(env.VolumetricFogDensity-0.0001);
			}
			else
			{
				env.VolumetricFogDensity=(float)0;
				FogState=0;
				Duration = FogRand.Next(120,260);
				StartTime = (int)fogwatch.Elapsed.TotalSeconds;
				GD.Print($"Fog End! Next Fog in {Duration} seconde");		
			}
		}
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
			int SubID;
			int RandInt = Rand.Next(1,101);
			if (RandInt<=35) ID = 1;
			else if (RandInt<=60) ID = 2;
			else if (RandInt<=80) ID = 3;
			else if (RandInt<=95) ID = 4;
			else ID = 5;
			(int h, int w) = IdToLen[ID];
			float Angle = 90*Rand.Next(0,4);
			
			
			RigidBody3D Room = new RigidBody3D();
			Room.LockRotation = true;
			Room.AxisLockLinearY = true;
			
			CollisionShape3D RoomCollision = new CollisionShape3D();
			BoxShape3D BoxShape = new BoxShape3D();
			
			BoxShape.Size = new Vector3(h * LenWall, 14 * LenWall, w * LenWall);
			
			RoomCollision.Shape = BoxShape;
			RoomCollision.Position = new Vector3(0,7*LenWall,0);
			RoomCollision.Name = "Coll";
			Room.AddChild(RoomCollision);
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
			RigidBody3D PseudoRoom = PseudoRoomList[i];
			CollisionShape3D MeshRoom = PseudoRoom.GetNode<CollisionShape3D>("Coll");
			BoxShape3D BoxM = (BoxShape3D)MeshRoom.Shape;
			
			(int h, int w) = ((int)BoxM.Size.X/LenWall,(int)BoxM.Size.Z/LenWall);
			int ID = LenToId[(h,w)];
			float X = PseudoRoom.Position.X;
			float Z = PseudoRoom.Position.Z;
			float A = PseudoRoom.RotationDegrees.Y;
			
			PseudoRoom.QueueFree();
			int SubID = 0;
			if (ID == 1) SubID = Rand.Next(1,5);
			if (ID == 2) SubID = Rand.Next(1,4);
			if (ID == 3) SubID = Rand.Next(1,4);
			if (ID == 4) SubID = Rand.Next(1,4);
			if (ID == 5) SubID = 1;
			PackedScene R = GD.Load<PackedScene>($"res://Scenes/MapScenes/Lvl1/RoomScenes/Room{ID}_{SubID}.tscn");
			Node3D Room = R.Instantiate<Node3D>();
			Room.Position = Roundm(X, Z, LenWall);
			Room.RotationDegrees = new Vector3(0,A,0);
			NavMesh.AddChild(Room);
			RoomList.Add(Room);
			
			step += 3;
		}
	}

	private void CreateMainRoom()
	{
		MainRoom.QueueFree();
		
		PackedScene MR = GD.Load<PackedScene>($"res://Scenes/MapScenes/Lvl1/RoomScenes/RoomMain.tscn");
		Node3D MRoom = MR.Instantiate<Node3D>();
		NavMesh.AddChild(MRoom);
		RoomList.Add(MRoom);
		
		PackedScene MRG = GD.Load<PackedScene>($"res://Scenes/MapScenes/Lvl1/RoomScenes/RoomMainGate.tscn");
		Node3D MRoomGate = MRG.Instantiate<Node3D>();
		RoomList.Add(MRoomGate);
		NavMesh.AddChild(MRoomGate);
	}
	

	private Vector3 Roundm(float x, float z, int LenTile)
	{
		int NewX = (int)(Math.Floor((x+LenTile-1)/(float)(LenTile))*LenTile);
		int NewZ = (int)(Math.Floor((z+LenTile-1)/(float)(LenTile))*LenTile);
		return new Vector3(NewX,0,NewZ);
	}
	

	private void OpenRoom()
	{
		for (int i = 0; i < RoomList.Count-1; i++)
		{
			Node3D ActualRoom = RoomList[i].GetNode<Node3D>("Wall");
			for (int j = i+1; j < RoomList.Count; j++)
			{
				Node3D TestedRoom = RoomList[j].GetNode<Node3D>("Wall");
				double dist = MapTool.Distance(ActualRoom, TestedRoom);
				
				if (dist<100)
				{
					List<Node3D> OverlapWallList = new List<Node3D>();
					for (int k = 0; k < ActualRoom.GetChildCount(); k++)
					{
						Node3D ActualChild = ActualRoom.GetChild<Node3D>(k);
						for (int l = 0; l < TestedRoom.GetChildCount(); l++)
						{
							Node3D TestedChild = TestedRoom.GetChild<Node3D>(l);
							bool TestPos = Math.Round(ActualChild.GlobalTransform.Origin.X)==Math.Round(TestedChild.GlobalTransform.Origin.X) && Math.Round(ActualChild.GlobalTransform.Origin.Z)==Math.Round(TestedChild.GlobalTransform.Origin.Z);//(TestedChild.Position.X+TestedRoom.Position.X == ActualChild.Position.X+ActualRoom.Position.X)&&(TestedChild.Position.Z+TestedRoom.Position.Z == ActualChild.Position.Z+ActualRoom.Position.Z);
							if (TestPos)
							{
								TestedRoom.RemoveChild(TestedChild);
								TestedChild.QueueFree();
								OverlapWallList.Add(ActualChild);
								l = TestedRoom.GetChildCount();
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
							Gate.Rotation = Child.Rotation;
							Gate.Position = Child.Position;
							ActualRoom.AddChild(Gate);
							ActualRoom.RemoveChild(Child);
							Child.QueueFree();
							step += 1;
						}
					}
				}
			}
			double DistToMain = MapTool.Distance(ActualRoom,MainRoom);
			if (MaxSpawnDist<DistToMain)
			{
				MaxSpawnDist = DistToMain;
				SpawnRoom = RoomList[i];
			}
		}
	}
}
