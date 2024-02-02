using Godot;
using System;
using System.Collections.Generic;
using Lib;

public partial class MapLvl3Script : IMap
{
	// Called when the node enters the scene tree for the first time.
	private int MapLenght = 33;//Nombre impaire uniquement
	private int[,] MapGrid;
	public override void _Ready()
	{
		MapGrid = new int[MapLenght,MapLenght];
		for (int i = 0; i < MapLenght; i++)
		{
			for (int j = 0; j < MapLenght; j++)
			{
				MapGrid[i, j] = 0;
			}
		}
		CreateMap(10,16,16);
		CreateCity();
		MapGrid[16, 16] = 2;
		PrintMatrix(MapGrid);
		MapReady = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override List<(int, int, int)> GetSpawnLocation()
	{
		List<(int, int, int)> Res = new List<(int, int, int)>();
		Res.Add((0,200,0));
		Res.Add((0,200,0));
		Res.Add((0,200,0));
		Res.Add((0,200,0));
		return Res;
	}

	public override void DebugMode(CharacterBody3D Player, bool DebugMode)
	{
		MapTool.Debug(Player,this,DebugMode);
	}
	
	private void CreateMap(int n, int i, int j)
	{
		MapGrid[i,j] = 1;
		if(n>0)
		{
			for(int m = 0; m<2 ;m++)
			{
				List<(int,int)> PossibleAction = new List<(int,int)>();
				if(i<MapLenght-2 && MapGrid[i+2,j]==0)
				{
					PossibleAction.Add((2,0));
				}
				if(i>1 && MapGrid[i-2,j]==0)
				{
					PossibleAction.Add((-2,0));
				}
				if(j<MapLenght-2 && MapGrid[i,j+2]==0)
				{ 
					PossibleAction.Add((0,2));
				}
				if(j>1 && MapGrid[i,j-2]==0)
				{
					PossibleAction.Add((0,-2));
				}
				if(PossibleAction.Count!=0 && n!=1)
				{
					int random = Rand.Next(0,PossibleAction.Count);
					(int x,int y) = PossibleAction[random];
					if(x==0) MapGrid[i+(x)/2,j+(y)/2] = -1;
					else MapGrid[i+(x)/2,j+(y)/2] = -2;
					CreateMap(n-1,i+x,j+y);
				}
			}	
		}
	}
	private void PrintMatrix(int[,] matrix)
	{
		for (int i = 0; i < MapLenght; i++)
		{
			string res = "";
			for (int j = 0; j < MapLenght; j++)
			{
				switch (matrix[i, j])
				{
					case 0:
						res += " ";
						break;
					case 1:
						res += "o";
						break;
					case -1:
						res += "-";
						break;
					case -2:
						res += "|";
						break;
					default:
						res += "#";
						break;
				}
			}
			GD.Print(res);
		}
	}
	
	private void CreateCity()
	{
		const int LenI = 16;
		for (int i = 0; i < MapLenght; i++)
		{
			for (int j = 0; j < MapLenght; j++)
			{
				switch (MapGrid[i, j])
				{
					case 0:
						Node3D Bat0 = GD.Load<PackedScene>("res://Ressources/Map/Moderne/Building/Building10.tscn").Instantiate<Node3D>();
						Bat0.Position = new Vector3((i-LenI)*27,0,(j-LenI)*25);
						AddChild(Bat0);
						break;
					case 1:
						Node3D Bat1 = GD.Load<PackedScene>("res://Ressources/Map/Moderne/Building/Building5.tscn").Instantiate<Node3D>();
						Bat1.Position = new Vector3((i-LenI)*27,0,(j-LenI)*25);
						AddChild(Bat1);
						break;
					case -1:
						Node3D Bat2 = GD.Load<PackedScene>("res://Ressources/Map/Moderne/Building/Bridge.tscn").Instantiate<Node3D>();
						Node3D R1 = Bat2.GetNode<Node3D>("Roof");
						Bat2.RemoveChild(R1);
						R1.Position = new Vector3((i-LenI)*27,R1.Position.Y,(j-LenI)*25);
						AddChild(R1);
						break;
					case -2:
						Node3D Bat3 = GD.Load<PackedScene>("res://Ressources/Map/Moderne/Building/Bridge.tscn").Instantiate<Node3D>();
						Node3D R2 = Bat3.GetNode<Node3D>("Roof2");
						Bat3.RemoveChild(R2);
						R2.Position = new Vector3((i-LenI)*27,R2.Position.Y,(j-LenI)*25);
						AddChild(R2);
						break;
				}
			}
		}
	}
}
