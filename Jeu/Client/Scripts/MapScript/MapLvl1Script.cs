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

public partial class MapLvl1Script : Node3D
{
	private Stopwatch stopwatch = new Stopwatch();
	private Stopwatch fogwatch = new Stopwatch();
	private Random Rand = new Random(42);
	private Random FogRand = new Random(42);
	private static bool MapReady = false;
	private int NbRoom =250;
	private int LenWall = 6;
	private StaticBody3D MainRoom;
	private List<PhysicsBody3D> PseudoRoomList = new List<PhysicsBody3D>();
	private List<Node3D> RoomList = new List<Node3D>();
	private List<CharacterBody3D> MobList = new List<CharacterBody3D>();
	private PackedScene AssetC = GD.Load<PackedScene>("res://Ressources/Map/Egypt1/Temple/Asset/Small_gate.tscn");
	private static float SpawnX;
	private static float SpawnZ;
	private double MaxSpawnDist = 0;
	private int FogState = 0;
	private int StartTime = 0;
	private int Duration = 0;
	private bool SpecMode = false;
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
			if (CheckSleep())
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
		else if (GameManager.StartMap)
		{
			//Process
			if (GameManager.Fog)
				CreateFog();
			DayCycle();
			
			//RenderDist();
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
	
	public void DebugMode(double delta, CharacterBody3D Player)
	{
		Camera3D PlayerCam = Player.GetNode<Camera3D>("CameraPlayer/h/v/Camera3D");
		// a enlever si no compil
		//--
		WorldEnvironment world = GetNode<WorldEnvironment>("World");
		Godot.Environment env = world.Environment;
		if (!SpecMode)
		{
			if (GameManager.DebugMode && FrameCount-StartInput>20)
			{
				StartInput=FrameCount;
				PackedScene SpecCam = GD.Load<PackedScene>("res://Scenes/Debug/SpecCam.tscn");
				Camera3D Cam = SpecCam.Instantiate<Camera3D>();
				Cam.Name = "SpecCam";
				Cam.Position = PlayerCam.Position + new Vector3(0,2,0) + Player.Position;
				Cam.Rotation = PlayerCam.Rotation;
				Label FPS = new Label();
				FPS.Name = "FPS";
				FPS.Text = "FPS:";
				Cam.AddChild(FPS);
				AddChild(Cam);
				PlayerCam.Current = false;
				Cam.Current = true;
				SpecMode = true;
				
				env.VolumetricFogEnabled = false;
				GD.Print("True");
			}
			
		}
		else
		{
			Label Fps = GetNode<Label>("SpecCam/FPS");
			Fps.Text = $"FPS: {(int)(1/delta)}";
			if (!GameManager.DebugMode && FrameCount-StartInput>20)
			{
				StartInput=FrameCount;
				Camera3D Cam = GetNode<Camera3D>("SpecCam");
				RemoveChild(Cam);
				Cam.QueueFree();
				PlayerCam.Current = true;
				Cam.Current = false;
				SpecMode = false;
				
				GD.Print("False");
				env.VolumetricFogEnabled = true;
			}
			
			
		}
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
	
	private bool IsNodeVisible(Node3D node, Camera3D camera)
	{
		// Not Use !
		Vector3 cameraPosition = camera.GlobalTransform.Origin;
		Vector3 nodePosition = node.GlobalTransform.Origin;

		return !camera.IsPositionBehind(nodePosition);
	}
	
	private void RenderDist()
	{
		Camera3D cam = GetNode<Camera3D>("Player/CameraPlayer/h/v/Camera3D");
		CharacterBody3D Player = GetNode<CharacterBody3D>("Player");
		for (int i = 0; i<RoomList.Count; i++)
		{
			Node3D Room = RoomList[i];
			if (!IsNodeVisible(Room,cam) && Distance(Room,(Node3D)Player)>30)
			{
				Room.Visible = false;
			}
			else
			{
				if (Distance(Room,(Node3D)Player)>100)
				{
					Room.GetNode<Node3D>("Misc").Visible = false;
				}
				else
				{
					Room.GetNode<Node3D>("Misc").Visible = true;
				}
				Room.Visible = true;
			}
		}
	}
	
	private void CreateFog()
	{
		WorldEnvironment world = GetNode<WorldEnvironment>("World");
		Godot.Environment env = world.Environment;
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

	private bool CheckSleep()
	{
		for (int i = 0; i<PseudoRoomList.Count;i++)
		{
			step += 2;
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
			Node3D ActualRoom = RoomList[i].GetNode<Node3D>("Wall");
			for (int j = i+1; j < RoomList.Count; j++)
			{
				Node3D TestedRoom = RoomList[j].GetNode<Node3D>("Wall");
				double dist = Distance(ActualRoom, TestedRoom);
				
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
			double DistToMain = Distance(ActualRoom,MainRoom);
			if (MaxSpawnDist<DistToMain)
			{
				MaxSpawnDist = DistToMain;
				SpawnX = ActualRoom.Position.X;
				SpawnZ = ActualRoom.Position.Z;
			}
		}
	}
}
