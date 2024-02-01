using Godot;
using System;
using System.Collections.Generic;
using Lib;
public partial class MapLvl2Script : IMap
{

	private Random Rand = new Random(42);
	private Random Rand2 = new Random(42);
	public int step = 0;
	private PackedScene Wa = GD.Load<PackedScene>("res://Scenes/MapScenes/Lvl2/R.tscn");
	private List<RigidBody3D> PseudoTreeList = new List<RigidBody3D>();
	private List<Node3D> SpawnPoint = new List<Node3D>();
	private List<Node3D> KeyList = new List<Node3D>();
	public int FrameCount = 0;
	private int StartTimer = 0;
	private Dictionary<int,float> IdToRadius = new Dictionary<int,float>
	{
		{1,2.5f},
		{2,1.5f},
		{3,2f},
		{4,3.5f},
		{5,1f},
	};
	private Dictionary<float,int> RadiusToId = new Dictionary<float,int>
	{
		{2.5f,1},
		{1.5f,2},
		{2f,3},
		{3.5f,4},
		{1f,5},
	};
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CreateBorder();
		PseudoCreateForest();
		CreatePseudoSpawnPoint();
		CreatePseudoKey();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		FrameCount+=1;
		if (!MapReady)
		{
			
			if(MapTool.CheckSleep(PseudoTreeList))
			{
				CreateForest();
				CreateKey();
				RemoveSafeArea();
				MapReady = true;
			}
		
		}
		
	}
	
	public override List<(int, int, int)> GetSpawnLocation()
	{
		List<(int, int, int)> res = new List<(int, int, int)>();
		Node3D Spawn = GetNode<Node3D>("Spawn");
		for(int i = 0; i<Spawn.GetChildCount();i++)
		{
			Vector3 Pos = Spawn.GetChild<Node3D>(i).Position;
			res.Add(new ((int)Pos.X,(int)Pos.Y,(int)Pos.Z));
		}
		return res;
	}

	public override void DebugMode(CharacterBody3D Player, bool DebugMode)
	{
		MapTool.Debug(Player,this,DebugMode);
	}
	
	
	private void CreateBorder()
	{
		const int Rayon = 300;
		const int Pas = 2;
		for (int i = 0; i<360; i+=Pas)
		{
			float rot = Mathf.DegToRad(i);
			Node3D W = Wa.Instantiate<Node3D>();
			Node3D Wall = W.GetNode<Node3D>($"R{Rand.Next(1,5)}");
			W.RemoveChild(Wall);
			double X = Math.Cos(rot);
			double Z = Math.Sin(rot);
			Wall.Position = new Vector3((float)X*Rayon,0,(float)Z*Rayon);
			Wall.Rotation += new Vector3(0,-rot,0);
			AddChild(Wall);
		}
		Node3D RotGate = GetNode<Node3D>("RotationGate");
		float rotG = Mathf.DegToRad(Rand.Next(0,181)*2);
		RotGate.Rotation = new Vector3(0,rotG,0);
	}
	
	
	
	private void PseudoCreateForest()
	{
		const int nbtree = 3000;
		//const float radius = 2.5f;
		for (int i = 0; i < nbtree; i++)
		{
			float radius = IdToRadius[Rand.Next(1,6)];
			RigidBody3D Sphere = new RigidBody3D();
			Sphere.AxisLockLinearY = true;
			SphereShape3D sphereShape = new SphereShape3D();
			CollisionShape3D collisionShape = new CollisionShape3D();
			collisionShape.Name = "Coll";
			
			sphereShape.Radius = radius;
			collisionShape.Shape = sphereShape;
			Sphere.AddChild(collisionShape);

			(double? x, double? z) = GetRandomPos(Rand);

			Sphere.Position = new Vector3((float)x, 0, (float)z);
			PseudoTreeList.Add(Sphere);
			AddChild(Sphere);
		}
	}
	
	private void CreatePseudoSpawnPoint()
	{
		const int nbMob = 750;
		const float radius = 1f;
		for (int i = 0; i < nbMob; i++)
		{
			RigidBody3D Sphere = new RigidBody3D();
			Sphere.AxisLockLinearY = true;
			SphereShape3D sphereShape = new SphereShape3D();
			CollisionShape3D collisionShape = new CollisionShape3D();
			
			MeshInstance3D meshInstance = new MeshInstance3D();
			SphereMesh sphereMesh = new SphereMesh();
			sphereMesh.Radius = radius;
			sphereMesh.Height = radius * 2;
			meshInstance.Mesh = sphereMesh;
			Sphere.AddChild(meshInstance);
			
			sphereShape.Radius = radius;
			collisionShape.Shape = sphereShape;
			Sphere.AddChild(collisionShape);

			(double? x, double? z) = GetRandomPos(Rand2);

			Sphere.Position = new Vector3((float)x, 0, (float)z);
			//PseudoTreeList.Add(Sphere);
			//AddChild(Sphere);
		}
	}
	
	private void CreatePseudoKey()
	{
		const int nbKey = 4;
		const float radius = 1f;
		for (int i = 0; i < nbKey; i++)
		{
			RigidBody3D Sphere = new RigidBody3D();
			Sphere.AxisLockLinearY = true;
			SphereShape3D sphereShape = new SphereShape3D();
			CollisionShape3D collisionShape = new CollisionShape3D();
			
			MeshInstance3D meshInstance = new MeshInstance3D();
			BoxMesh sphereMesh = new BoxMesh();
			sphereMesh.Size = new Vector3(1,2,1);
			meshInstance.Mesh = sphereMesh;
			Sphere.AddChild(meshInstance);
			
			sphereShape.Radius = radius;
			collisionShape.Shape = sphereShape;
			Sphere.AddChild(collisionShape);

			(double? x, double? z) = GetRandomPos(Rand2);

			Sphere.Position = new Vector3((float)x, 0, (float)z);
			KeyList.Add(Sphere);
			AddChild(Sphere);
		}
	}
	
	private (double? x,double? z) GetRandomPos(Random Rando)
	{
		const int radiusmap = 290;
		double t = 2 * Math.PI * Rando.NextDouble();
		double u = Rando.NextDouble() + Rando.NextDouble();
		double? r = null;
		if (u>1)
		{
			r = 2 - u;
		}
		else
		{
			r = u;
		}
		double? x = radiusmap * r * Math.Cos(t);
		double? z = radiusmap * r * Math.Sin(t);
		
		return new (x,z);
	}

	private void CreateForest()
	{
		for (int i = 0; i<PseudoTreeList.Count;i++)
		{
			Vector3 Pos = PseudoTreeList[i].Position;
			float Sp = ((SphereShape3D)PseudoTreeList[i].GetNode<CollisionShape3D>("Coll").Shape).Radius;
			RemoveChild(PseudoTreeList[i]);
			Node3D tree = GD.Load<PackedScene>($"res://Ressources/Map/Global/tre2/Model/Tree{RadiusToId[Sp]}.tscn").Instantiate<Node3D>();
			tree.Position = Pos + new Vector3(0,Rand.Next(-10,1)/10f,0);
			tree.Rotation = new Vector3(0,Mathf.DegToRad(Rand.Next(0,361)),0);
			AddChild(tree);
			
		}
	}
	
	private void CreateKey()
	{
		for (int i = 0; i<KeyList.Count;i++)
		{
			Node3D Key = KeyList[i];
			Node3D RealKey = GD.Load<PackedScene>($"res://Ressources/Map/Global/Object/Key/key.tscn").Instantiate<Node3D>();
			RealKey.Position = Key.Position;
			RemoveChild(Key);
			Key.QueueFree();
			AddChild(RealKey);
		}
	}
	
	private void RemoveSafeArea()
	{
		StaticBody3D SafeArea = GetNode<StaticBody3D>("SafeArea");
		RemoveChild(SafeArea);
		SafeArea.QueueFree();
	}
	
	
}
