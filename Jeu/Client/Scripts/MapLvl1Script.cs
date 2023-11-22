using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class MapLvl1Script : Node3D
{
	private int _NbRoom = 300;
	private int _NbTypeRoom = 4;
	private int _LenWall = 6;
	private double _LenMin = 10;
	private PackedScene AssetC = GD.Load<PackedScene>("res://Ressources/Map/Egypt1/Temple/Asset/Small_gate.tscn");
	private Dictionary<string,Node3D> Dico = new Dictionary<string,Node3D>();
	
	public override void _Ready()
	{
		for (int i = 1; i < _NbTypeRoom; i++)
		{
			PackedScene TG = GD.Load<PackedScene>($"res://Scenes/MapScenes/RoomScenes/Room{i}Grid.tscn");
			Node3D Grid = TG.Instantiate<Node3D>();
			Dico[$"res://Scenes/MapScenes/RoomScenes/Room{i}.tscn"] = Grid;
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
						 Math.Pow(Room1.Position.X - Room2.Position.X, 2));
	}

	public bool CheckPlace(Node3D ActualRoom, List<Node3D> RoomList)
	{
		for (int i = 0; i < RoomList.Count; i++)
		{
			Node3D TestedRoom = RoomList[i];
			double dist = Distance(ActualRoom, TestedRoom);
			if (dist<=_LenMin || true)
			{
				Node3D TestedGrid = Dico[TestedRoom.SceneFilePath];
				Node3D ActualGrid = Dico[ActualRoom.SceneFilePath];
				for (int j = 0; j < ActualGrid.GetChildCount(); j++)
				{
					Node3D ActualTile = ActualGrid.GetChild<Node3D>(j);
					//GD.Print($"Actual Tile = {j}");
					for (int k = 0; k < TestedGrid.GetChildCount(); k++)
					{
						Node3D TestedTile = TestedGrid.GetChild<Node3D>(k);
						//GD.Print($"({ActualRoom.Position.X + ActualTile.Position.X},{ActualRoom.Position.Z + ActualTile.Position.Z}) = ({TestedRoom.Position.X + TestedTile.Position.X},{TestedRoom.Position.Z + TestedTile.Position.Z}) tile test {k} ");
						bool pos = (((ActualRoom.Position.X + ActualTile.Position.X) == (TestedRoom.Position.X + TestedTile.Position.X)) &&
							((ActualRoom.Position.Z + ActualTile.Position.Z) == (TestedRoom.Position.Z + TestedTile.Position.Z)));
						if (pos)
						{
							return false;
						}
					}
				}

			}
		}
		return true;
	}

	public void CreateMap()
	{
		List<Node3D> RoomList = new List<Node3D>();

		for (int i = 0; i < _NbRoom; i++)
		{
			int RandIntID = new Random().Next(1, 4);
			int X = (int)new Random().Next(-21,22);
			int Y = (int)new Random().Next(-21,22);
		
			PackedScene R = GD.Load<PackedScene>($"res://Scenes/MapScenes/RoomScenes/Room{RandIntID}.tscn");
			Node3D Room = R.Instantiate<Node3D>();
			Room.Position = new Vector3(X * _LenWall, 0, Y * _LenWall);
			bool p = CheckPlace(Room,RoomList);
			GD.Print(p);
			if (p)
			{	
				for (int j = 0; j < RoomList.Count; j++)
				{
					Node3D TestedRoom = RoomList[j];
					double Dist = Distance(Room,TestedRoom);
					//GD.Print(Dist);
					if (Dist<_LenMin || true)
					{
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
										ActualChild.QueueFree();
										l = TestedRoom.GetChildCount();
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
				RoomList.Add(Room);
				AddChild(Room);
			}
		}
	}
}
