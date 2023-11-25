using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

public partial class MapLvl1Script : Node3D
{
	private int _NbRoom = 0;
	private int _NbTypeRoom = 4;
	private int _LenWall = 6;
	private int _Radius = 20;
	private PackedScene AssetC = GD.Load<PackedScene>("res://Ressources/Map/Egypt1/Temple/Asset/Small_gate.tscn");
	private Dictionary<string,Node3D> DataGrid = new Dictionary<string,Node3D>();
	private Dictionary<string,(int,int)> DataLen = new Dictionary<string,(int,int)>();
	
	public override void _Ready()
	{
		PackedScene MG = GD.Load<PackedScene>($"res://Scenes/MapScenes/RoomScenes/Room3Grid.tscn");
		Node3D MGrid = MG.Instantiate<Node3D>();
		DataGrid[$"res://Scenes/MapScenes/RoomScenes/RoomMain.tscn"] = MGrid;
		DataLen[$"res://Scenes/MapScenes/RoomScenes/RoomMain.tscn"] = (7, 7);
		
		PackedScene MGG = GD.Load<PackedScene>($"res://Scenes/MapScenes/RoomScenes/Room3Grid.tscn");
		Node3D MGridG = MGG.Instantiate<Node3D>();
		DataGrid[$"res://Scenes/MapScenes/RoomScenes/RoomMainGate.tscn"] = MGridG;
		DataLen[$"res://Scenes/MapScenes/RoomScenes/RoomMainGate.tscn"] = (7, 7);
		
		for (int i = 1; i < _NbTypeRoom; i++)
		{
			PackedScene TG = GD.Load<PackedScene>($"res://Scenes/MapScenes/RoomScenes/Room{i}Grid.tscn");
			Node3D Grid = TG.Instantiate<Node3D>();
			DataGrid[$"res://Scenes/MapScenes/RoomScenes/Room{i}.tscn"] = Grid;
			int len = -1;
			switch (i)
			{
				case 1 :
					len = 3;
					break;
				case 2:
					len = 5;
					break;
				case 3:
					len = 7;
					break;
			}
			DataLen[$"res://Scenes/MapScenes/RoomScenes/Room{i}.tscn"] = (len, len);
		}
		
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		CreateMap();
		stopwatch.Stop();
		GD.Print($"Map créer en {stopwatch.Elapsed}");
	}
	
	public override void _Process(double delta)
	{
	}

	public double Distance(Node3D Room1, Node3D Room2)
	{
		return Math.Sqrt(Math.Pow(Room1.Position.X - Room2.Position.X, 2) +
						 Math.Pow(Room1.Position.Z - Room2.Position.Z, 2));
	}

	public (double, double) LenRange(Node3D Room1, Node3D Room2)
	{
		(int h1,int w1) = DataLen[Room1.SceneFilePath];
		(int h2,int w2) = DataLen[Room2.SceneFilePath];
		h1*=_LenWall;
		h2*=_LenWall;
		w1*=_LenWall;
		w2*=_LenWall;
		double LenMin = h1 / 2 + h2 / 2 - 1;
		double LenMax = Math.Sqrt(Math.Pow(h1 / 2 + h2 / 2 - 1, 2) + Math.Pow(h1 / 2 + h2 / 2 - 1, 2));
		return (LenMin, LenMax);
	}

	public bool CheckPlace(Node3D ActualRoom, int PrevX, int PrevZ, List<Node3D> RoomList)
	{
		bool TestX = true;
		bool TestZ = true;
		bool TestXZ = true;
		for (int i = 0; i < RoomList.Count; i++)
		{
			Node3D TestedRoom = RoomList[i];
			double dist = Distance(ActualRoom, TestedRoom);
			(double LenMin, double LenMax) = LenRange(ActualRoom, TestedRoom);
			//GD.Print(LenMax);
			if (dist<=60)
			{
				Node3D TestedGrid = DataGrid[TestedRoom.SceneFilePath];
				Node3D ActualGrid = DataGrid[ActualRoom.SceneFilePath];
				for (int j = 0; j < ActualGrid.GetChildCount(); j++)
				{
					Node3D ActualTile = ActualGrid.GetChild<Node3D>(j);

					for (int k = 0; k < TestedGrid.GetChildCount(); k++)
					{
						Node3D TestedTile = TestedGrid.GetChild<Node3D>(k);
						bool pos = (ActualRoom.Position.X+ActualTile.Position.X+PrevX == TestedRoom.Position.X+TestedTile.Position.X) && (ActualRoom.Position.Z+ActualTile.Position.Z+PrevZ == TestedRoom.Position.Z+TestedTile.Position.Z);
						if (pos)
						{
							TestXZ = false;
						}
						bool pos2 = (ActualRoom.Position.X+ActualTile.Position.X+PrevX == TestedRoom.Position.X+TestedTile.Position.X) && (ActualRoom.Position.Z+ActualTile.Position.Z == TestedRoom.Position.Z+TestedTile.Position.Z) || ActualRoom.Position.X+ActualTile.Position.X+PrevX==0;
						if (pos2)
						{
							TestX = false;
						}
						bool pos3 = (ActualRoom.Position.X+ActualTile.Position.X == TestedRoom.Position.X+TestedTile.Position.X) && (ActualRoom.Position.Z+ActualTile.Position.Z+PrevZ == TestedRoom.Position.Z+TestedTile.Position.Z) || ActualRoom.Position.Z+ActualTile.Position.Z+PrevZ==0;
						if (pos3)
						{
							TestZ = false;
						}		
					}
				}

			}
		}
		return (TestXZ);
	}

	public void CreateMap()
	{
		List<Node3D> RoomList = new List<Node3D>();
		
		PackedScene MR = GD.Load<PackedScene>($"res://Scenes/MapScenes/RoomScenes/RoomMain.tscn");
		Node3D MRoom = MR.Instantiate<Node3D>();
		RoomList.Add(MRoom);
		AddChild(MRoom);
		
		PackedScene MRG = GD.Load<PackedScene>($"res://Scenes/MapScenes/RoomScenes/RoomMainGate.tscn");
		Node3D MRoomG = MRG.Instantiate<Node3D>();
		RoomList.Add(MRoomG);
		AddChild(MRoomG);
		

		for (int i = 0; i < _NbRoom; i++)
		{
			//---------------------------------------------------------------------------------- code à modif
			//HELP ME
			int RandIntID = new Random().Next(1,4);
			PackedScene R = GD.Load<PackedScene>($"res://Scenes/MapScenes/RoomScenes/Room{RandIntID}.tscn");
			Node3D Room = R.Instantiate<Node3D>();
			Room.Position = new Vector3(0, 0, 0);
			double RandAngle = new Random().NextDouble()*Math.PI*2;
			double pX = Math.Cos(RandAngle);
			double pZ = Math.Sin(RandAngle);
			double tX = 0;
			double tZ = 0;
			int NX = 0;
			int NZ = 0;
			while (!CheckPlace(Room,NX,NZ,RoomList))
			{
				tX+=pX;
				tZ+=pZ;
				NX = 6*(int)(tX);
				NZ = 6*(int)(tZ);
			}
			tX+=pX;
			tZ+=pZ;
			NX = 6*(int)(tX);
			NZ = 6*(int)(tZ);
			Room.Position = new Vector3(NX, 0, NZ);
			
			//----------------------------------------------------------------------------------
			for (int j = 1; j < RoomList.Count; j++)
			{
				Node3D TestedRoom = RoomList[j];
				double Dist = Distance(Room,TestedRoom);
				(double LenMin, double LenMax) = LenRange(Room, TestedRoom);
				//GD.Print(Dist);
				if (Dist<LenMax && Dist>LenMin)
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
			RoomList.Add(Room);
			AddChild(Room);

		}
		GD.Print(RoomList.Count);
	}
}
