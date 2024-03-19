using Godot;
using System;
using System.Collections.Generic;
using Lib;
[Tool]
public partial class Boss3Map : IMap
{
	private int[,] MapGrid;
	private int MapLenght = 11;
	private const int Gap = 30;
	private const int LenI = (10)/2;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MapGrid = new int[MapLenght,MapLenght];
		for (int i = 0; i < MapLenght; i++)
		{
			for (int j = 0; j < MapLenght; j++)
			{
				if((i>2 && i<8)&&(j>2 && j<8))
				{
					MapGrid[i, j] = 1;
				}
				else
				{
					MapGrid[i, j] = 0;
				}
			}
		}
		CreateCity();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!MapReady)
		{
			MapReady = true;
			LoadingStage = "En attente des autres joueurs :(";
		}
	}

	public override List<(int, int, int)> GetSpawnLocation()
	{
		List<(int,int,int)> res = new List<(int,int,int)>();
		res.Add((1,35,0));
		res.Add((0,35,1));
		res.Add((-1,35,0));
		res.Add((0,35,-1));
		
		return res;
	}


	public override void DebugMode(CharacterBody3D Player, bool DebugMode)
	{
		MapTool.Debug(Player,this,DebugMode);
	}
	
	private void CreateCity()
	{
		for (int i = 0; i < MapLenght; i++)
		{
			for (int j = 0; j < MapLenght; j++)
			{
				switch (MapGrid[i, j])
				{
					case 0:
						int nb = Rand.Next(6,11);
						if(nb==11)
						{
							nb=3;
						}
						int rot = Rand.Next(0,4)*90;
						Node3D Bat0 = GD.Load<PackedScene>($"res://Ressources/Map/Moderne/Building/Building{nb}.tscn").Instantiate<Node3D>();
						Bat0.Position = new Vector3((i-LenI)*Gap,0,(j-LenI)*Gap);
						Bat0.Rotation = new Vector3(0,(float)Mathf.DegToRad(rot),0);
						AddChild(Bat0);
						break;
					case 1:
						Node3D Bat1 = GD.Load<PackedScene>("res://Ressources/Map/Moderne/Building/Building5.tscn").Instantiate<Node3D>();
						Bat1.Position = new Vector3((i-LenI)*Gap,0,(j-LenI)*Gap);
						AddChild(Bat1);
						break;
				}
			}
		}
	}
}
