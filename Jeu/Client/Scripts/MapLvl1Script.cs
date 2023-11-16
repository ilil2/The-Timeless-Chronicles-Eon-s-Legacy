using Godot;
using System;
using System.Collections.Generic;

public partial class MapLvl1Script : Node3D
{
	public static List<Node3D> RoomList;
	private int _NbRoom = 1;
	private int _LenMap = 18;
	private double _LenMin = 10;
	public override void _Ready()
	{
		RoomList = CreateMap();
		SupprDouble(RoomList);
		
	}
	

	public List<Node3D> CreateMap()
	{
		//Fonction pour créer les salles et les ajouter a une liste
		List<Node3D> RoomList = new List<Node3D>();
		for (int i = -_NbRoom; i <= _NbRoom; i++)
		{
			for (int j = -_NbRoom; j <= _NbRoom; j++)
			{
				PackedScene R = GD.Load<PackedScene>("res://Scenes/MapScenes/RoomScenes/Room1.tscn");
				Node3D Room = R.Instantiate<Node3D>();
				Room.Position = new Vector3(i * _LenMap, 0, j * _LenMap);
				RoomList.Add(Room);
			}

		}
		return RoomList;
	}
	
	public void SupprDouble(List<Node3D> RoomList)
	{
		for (int i = 0; i < RoomList.Count; i++)
		//Parcoure toutes les salles
		{
			Node3D ActualRoom = RoomList[i];
			for (int j = i+1; j < RoomList.Count; j++)
			//Parcoure toutes les autres salles
			{
				Node3D TestedRoom = RoomList[j];
				double Distance = Math.Sqrt((ActualRoom.Position.X+TestedRoom.Position.X)*(ActualRoom.Position.Y+TestedRoom.Position.Y)*(ActualRoom.Position.Z+TestedRoom.Position.Z));
				//Verifie que il ne traite que les salles les plus proches 
				if (Distance<_LenMin)
				{
					for (int k = 0; k < ActualRoom.GetChildCount(); k++)
					//Parcoure tout les murs de la salle
					{
						Node3D ActualChild = ActualRoom.GetChild<Node3D>(k);
						for (int l = 0; l < TestedRoom.GetChildCount(); l++)
						//Parcoure tout les murs d'une salle proche
						{
							Node3D TestedChild = TestedRoom.GetChild<Node3D>(l);
							bool TestPos = (TestedChild.Position.X+TestedRoom.Position.X == ActualChild.Position.X+ActualRoom.Position.X)&&(TestedChild.Position.Y+TestedRoom.Position.Y == ActualChild.Position.Y+ActualRoom.Position.Y)&&(TestedChild.Position.Z+TestedRoom.Position.Z == ActualChild.Position.Z+ActualRoom.Position.Z);
							//Test si il existe deux murs superposées
							if (TestPos)
							{
								if (l<200)
								{
									//supprime le murs actuel
									ActualChild.QueueFree();
								}
								//supprime le murs test
								//TestedChild.QueueFree();
								l = 200;
							
							}
						}
					}		
				}
			}
			AddChild(ActualRoom);
		}
	}

}
