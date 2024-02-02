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
		CreateMap(20,16,16);
		PrintMatrix(MapGrid);
		CreateCity();
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
		MapGrid[i,j] = n;
		if(n>0)
		{
			for(int m = 0; m<2 ;m++)
			{
				List<(int,int)> PossibleAction = new List<(int,int)>();
				if(i<MapLenght-2)
				{
					if(MapGrid[i+2,j]==0)
					{
						PossibleAction.Add((2,0));
					}
				}
				if(i>1)
				{
					if(MapGrid[i-2,j]==0)
					{
						PossibleAction.Add((-2,0));
					}
				}
				if(j<MapLenght-2)
				{
					if(MapGrid[i,j+2]==0)
					{
						PossibleAction.Add((0,2));
					}
				}
				if(j>1)
				{
					if(MapGrid[i,j-2]==0)
					{
						PossibleAction.Add((0,-2));
					}
				}
				if(PossibleAction.Count!=0)
				{
					int random = Rand.Next(0,PossibleAction.Count);
					(int x,int y) = PossibleAction[random];
					if(n!=1)
					{
						if(x==0)
						{
							MapGrid[i+(x)/2,j+(y)/2] = -1;
						}
						else
						{
							MapGrid[i+(x)/2,j+(y)/2] = -2;
						}
					}
					CreateMap(n-1,i+x,j+y);
				}
			}
			
		}
	}
	private void PrintMatrix(int[,] matrix)
	{
		int rows = matrix.GetLength(0);
		int cols = matrix.GetLength(1);

		for (int i = 0; i < rows; i++)
		{
			string res = "";
			for (int j = 0; j < cols; j++)
			{	
				if(matrix[i, j]>0)
				{
					string r = matrix[i, j].ToString().PadRight(2);
					res+=$"O";
				}
				else if(matrix[i, j]==-1)
				{
					res+=$"-";
				}
				else if(matrix[i, j]==-2)
				{
					res+=$"|";
				}
				else
				{
					res+=$".";
				}
			}
			GD.Print(res);
		}
	}
	
	private void CreateCity()
	{
		for (int i = 0; i < MapLenght; i++)
		{
			for (int j = 0; j < MapLenght; j++)
			{
				if(MapGrid[i, j] > 0)
				{
					Node3D Bat = GD.Load<PackedScene>("res://Ressources/Map/Moderne/Building/Building5.tscn").Instantiate<Node3D>();
					Bat.Position = new Vector3((i-16)*27,0,(j-16)*25);
					AddChild(Bat);
				}
				else if(MapGrid[i, j]==-1)
				{
					Node3D Bat = GD.Load<PackedScene>("res://Ressources/Map/Moderne/Building/Bridge.tscn").Instantiate<Node3D>();
					Node3D R = Bat.GetNode<Node3D>("Roof");
					Bat.RemoveChild(R);
					R.Position = new Vector3((i-16)*27,R.Position.Y,(j-16)*25);
					
					AddChild(R);
				}
				else if(MapGrid[i, j]==-2)
				{
					Node3D Bat = GD.Load<PackedScene>("res://Ressources/Map/Moderne/Building/Bridge.tscn").Instantiate<Node3D>();
					Node3D R = Bat.GetNode<Node3D>("Roof2");
					Bat.RemoveChild(R);
					R.Position = new Vector3((i-16)*27,R.Position.Y,(j-16)*25);
					AddChild(R);
				}
				else
				{
					Node3D Bat = GD.Load<PackedScene>("res://Ressources/Map/Moderne/Building/Building10.tscn").Instantiate<Node3D>();
					Bat.Position = new Vector3((i-16)*27,0,(j-16)*25);
					AddChild(Bat);
				}
			}
		}
	}
}
