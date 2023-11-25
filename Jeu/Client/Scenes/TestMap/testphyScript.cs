using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class testphyScript : Node3D
{
	private List<RigidBody3D> RoomList = new List<RigidBody3D>();
	private List<int> RoomIDList = new List<int>();
	private List<Node3D> PlaceRoomList = new List<Node3D>();
	private StaticBody3D MainRoom;
	private bool _MapReady = false;
	private Stopwatch stopwatch = new Stopwatch();
	private PackedScene AssetC = GD.Load<PackedScene>("res://Ressources/Map/Egypt1/Temple/Asset/Small_gate.tscn");
	public override void _Ready()
	{
		stopwatch.Start();
		Random Rand = new Random();
		MainRoom = new StaticBody3D();
		CollisionShape3D MainRoomCollision = new CollisionShape3D();
		BoxShape3D MainBoxShape = new BoxShape3D();
		MainBoxShape.Size = new Vector3(7,14,7);
		MainRoomCollision.Shape = MainBoxShape;
		MainRoomCollision.Position = new Vector3(MainRoomCollision.Position.X,MainRoomCollision.Position.Y+7,MainRoomCollision.Position.Z);
		MainRoom.AddChild(MainRoomCollision);
		AddChild(MainRoom);
		for (int i = 0; i<150;i++)
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
			int X = (int)(5*r*Math.Cos(T));
			int Z = (int)(9*r*Math.Sin(T));
			
			int RandID = Rand.Next(1,11);
			if (RandID<6)
			{
				RandID = 1;
			}
			else if (RandID<9)
			{
				RandID = 2;
			}
			else
			{
				RandID = 3;
			}
			
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
				BoxShape.Size = new Vector3(3,14,3);
				BoxM.Size = new Vector3(3,2,3);
			}
			else if (RandID == 2)
			{
				BoxShape.Size = new Vector3(5,14,5);
				BoxM.Size = new Vector3(5,2,5);
			}
			else if (RandID == 3)
			{
				BoxShape.Size = new Vector3(7,14,7);
				BoxM.Size = new Vector3(7,2,7);
			}
			
			RoomCollision.Shape = BoxShape;
			RoomCollision.Position = new Vector3(RoomCollision.Position.X,RoomCollision.Position.Y+7,RoomCollision.Position.Z);
			RoomMesh.Mesh = BoxM;
			RoomMesh.Position = new Vector3(RoomMesh.Position.X,RoomMesh.Position.Y+1,RoomMesh.Position.Z);
			Room.AddChild(RoomCollision);
			Room.AddChild(RoomMesh);
			Room.Position = new Vector3(X,Room.Position.Y,Z);
			AddChild(Room);
			RoomList.Add(Room1);
			RoomIDList.Add(RandID);
			
			
			
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_MapReady == false)
		{
			if (CheckSleep(RoomList))
			{
				PackedScene MR = GD.Load<PackedScene>($"res://Scenes/MapScenes/RoomScenes/RoomMain.tscn");
				Node3D MRoom = MR.Instantiate<Node3D>();
				AddChild(MRoom);
				PlaceRoomList.Add(MRoom);
				PackedScene MRG = GD.Load<PackedScene>($"res://Scenes/MapScenes/RoomScenes/RoomMainGate.tscn");
				Node3D MRoomG = MRG.Instantiate<Node3D>();
				PlaceRoomList.Add(MRoomG);
				AddChild(MRoomG);
				MainRoom.QueueFree();
				
				for (int i = 0; i<RoomList.Count;i++)
				{
					RigidBody3D Room2 = RoomList[i];
					float X = Room2.Position.X;
					float Z = Room2.Position.Z;
					Room2.QueueFree();
					int ID = RoomIDList[i];
					PackedScene R = GD.Load<PackedScene>($"res://Scenes/MapScenes/RoomScenes/Room{ID}.tscn");
					Node3D Room = R.Instantiate<Node3D>();
					Room.Position = new Vector3(X*6,0,Z*6);
					Roundm(Room);
					//PackedScene RG = GD.Load<PackedScene>($"res://Scenes/MapScenes/RoomScenes/Room{ID}Grid.tscn");
					//Node3D RoomGrid = RG.Instantiate<Node3D>();
					//RoomGrid.Position = new Vector3(X*6,0,Z*6);
					//Roundm(RoomGrid);
					//AddChild(RoomGrid);
					//---------------------------
					
					for (int j = 1; j < PlaceRoomList.Count; j++)
					{
						Node3D TestedRoom = PlaceRoomList[j];
						double Dist = Distance(Room,TestedRoom);
						//GD.Print(Dist);
						if (Dist<70)
						{
							bool doorplace = false;
							Node3D LastActualChild = Room.GetChild<Node3D>(0);
							Node3D LastTestedChild = TestedRoom.GetChild<Node3D>(0);
							for (int k = 0; k < Room.GetChildCount(); k++)
							{
								Node3D ActualChild = Room.GetChild<Node3D>(k);
								for (int l = 0; l < TestedRoom.GetChildCount(); l++)
								{
									Node3D TestedChild = TestedRoom.GetChild<Node3D>(l);
									bool TestPos = (TestedChild.Position.X+TestedRoom.Position.X == ActualChild.Position.X+Room.Position.X)&&(TestedChild.Position.Z+TestedRoom.Position.Z == ActualChild.Position.Z+Room.Position.Z)&&(TestedChild.Position.Y+TestedRoom.Position.Y == ActualChild.Position.Y+Room.Position.Y);
									if (TestPos)
									{
										if (l<TestedRoom.GetChildCount())
										{
											LastActualChild = ActualChild;
											LastTestedChild = TestedChild;
												
												
											ActualChild.QueueFree();
											l = TestedRoom.GetChildCount();
											if (doorplace==false)
											{
												int RandInt = new Random().Next(0, 2);
												if (RandInt==0)
												{
													doorplace = true;
													Node3D Gate = AssetC.Instantiate<Node3D>();
													Gate.Rotation = new Vector3(0,ActualChild.Rotation.Y,0);
													Gate.Position = new Vector3(ActualChild.Position.X+Room.Position.X,ActualChild.Position.Y+Room.Position.Y,ActualChild.Position.Z+Room.Position.Z);
													TestedChild.QueueFree();
													AddChild(Gate);
												}
											}									
										}
									}
								}
							} 
							bool Test = (LastTestedChild.Position.X+TestedRoom.Position.X == LastActualChild.Position.X+Room.Position.X)&&(LastTestedChild.Position.Z+TestedRoom.Position.Z == LastActualChild.Position.Z+Room.Position.Z)&&(LastTestedChild.Position.Y+TestedRoom.Position.Y == LastActualChild.Position.Y+Room.Position.Y);
							if (doorplace==false && Test)
							{
								Node3D LGate = AssetC.Instantiate<Node3D>();
								LGate.Rotation = new Vector3(0,LastActualChild.Rotation.Y,0);
								LGate.Position = new Vector3(LastActualChild.Position.X+Room.Position.X,LastActualChild.Position.Y+Room.Position.Y,LastActualChild.Position.Z+Room.Position.Z);
								LastTestedChild.QueueFree();
								AddChild(LGate);
							}
						}
					}
					PlaceRoomList.Add(Room);
					AddChild(Room);
					
					//---------------------------
				}
				

				_MapReady = true;
				stopwatch.Stop();
				GD.Print("Map Ready ! (100 Room)");
				GD.Print($"Map fait en {stopwatch.Elapsed}");
			}
			
			
		}
		
	}
	
	public bool CheckSleep(List<RigidBody3D> RoomList)
	{
		for (int i = 0; i<RoomList.Count;i++)
		{
			if (RoomList[i].Sleeping == false)
			{
				return false;
			}
		}
		return true;
	}
	
	public void Roundm(Node3D Room)
	{
		Room.Position = new Vector3((float)Math.Floor(((Room.Position.X+6-1)/6))*6,Room.Position.Y,(float)Math.Floor(((Room.Position.Z+6-1)/6))*6);
	}
	
	public double Distance(Node3D Room1, Node3D Room2)
	{
		return Math.Sqrt(Math.Pow(Room1.Position.X - Room2.Position.X, 2) +
						 Math.Pow(Room1.Position.Z - Room2.Position.Z, 2));
	}


}
