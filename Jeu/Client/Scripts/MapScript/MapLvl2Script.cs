using Godot;
using System;
using System.Collections.Generic;
using Lib;
public partial class MapLvl2Script : Node3D, IMap
{

	private Random Rand = new Random(42);
	private int state = 0;
	private PackedScene Wa = GD.Load<PackedScene>("res://Scenes/MapScenes/Lvl2/R.tscn");
	private List<RigidBody3D> PseudoTreeList = new List<RigidBody3D>();
	private List<Node3D> TreeList = new List<Node3D>();
	private List<Node3D> SpawnPoint = new List<Node3D>();
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
		CreateForest0();
		state = 1;
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		FrameCount+=1;
		//Debug
		Camera3D DebugCam = GetNode<Camera3D>("SpecCam");
		Label S = DebugCam.GetNode<Label>("State");
		S.Text = $"State {state}";
		Render(DebugCam);
		//Debug
		if(state==1)
		{
			if (FrameCount-StartTimer>1)
			{
				CreateForest1();
				state = 2;
				StartTimer = FrameCount;
			}
		}
		else if(state==2)
		{
			if ((FrameCount-StartTimer>1) && MapTool.CheckSleep(PseudoTreeList))
			{
				state = 3;
				StartTimer = FrameCount;
			}
		}
		else if(state==3)
		{
			CreateForest2();
			state = 4;
			StartTimer = FrameCount;
		}
	}
	public void SetSeed(int seed)
	{
		Rand = new Random(seed)
		GD.Print($"Seed set : {seed}");
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
	}
	private void CreateForest0()
	{
		const int nbtree = 3000;
		//const float radius = 2.5f;
		for (int i = 0; i < nbtree; i++)
		{
			float radius = IdToRadius[Rand.Next(1,6)];
			RigidBody3D Sphere = new RigidBody3D();
			SphereShape3D sphereShape = new SphereShape3D();
			SphereMesh sphereMesh = new SphereMesh();
			CollisionShape3D collisionShape = new CollisionShape3D();
			MeshInstance3D meshInstance = new MeshInstance3D();
			meshInstance.Name = "Mesh";
			sphereMesh.Radius = radius;
			sphereMesh.Height = radius * 2;
			meshInstance.Mesh = sphereMesh;
			sphereShape.Radius = radius;
			collisionShape.Shape = sphereShape;
			Sphere.AddChild(meshInstance);
			Sphere.AddChild(collisionShape);

			const int radiusmap = 290;
			double t = 2 * Math.PI * Rand.NextDouble();
			double u = Rand.NextDouble() + Rand.NextDouble();
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

			Sphere.Position = new Vector3((float)x, 10, (float)z);
			PseudoTreeList.Add(Sphere);
			AddChild(Sphere);


		}
	}

	private void CreateForest1()
	{
		for (int i = 0; i<PseudoTreeList.Count;i++)
		{
			((PhysicsBody3D)PseudoTreeList[i]).AxisLockLinearX = true;
			((PhysicsBody3D)PseudoTreeList[i]).AxisLockLinearZ = true;
			((PhysicsBody3D)PseudoTreeList[i]).AxisLockAngularX = true;
			((PhysicsBody3D)PseudoTreeList[i]).AxisLockAngularY = true;
			((PhysicsBody3D)PseudoTreeList[i]).AxisLockAngularZ = true;
		}
	}
	private void CreateForest2()
	{
		for (int i = 0; i<PseudoTreeList.Count;i++)
		{
			Vector3 Pos = PseudoTreeList[i].Position;
			float Sp = ((SphereMesh)PseudoTreeList[i].GetNode<MeshInstance3D>("Mesh").Mesh).Radius;
			RemoveChild(PseudoTreeList[i]);
			Node3D tree = GD.Load<PackedScene>($"res://Ressources/Map/Global/tre2/Model/Tree{RadiusToId[Sp]}.tscn").Instantiate<Node3D>();
			tree.Position = Pos - new Vector3(0,3,0);
			tree.Rotation = new Vector3(0,Mathf.DegToRad(Rand.Next(0,361)),0);
			AddChild(tree);
			TreeList.Add(tree);
			
		}
	}
	
	private void Render(Camera3D Cam)
	{
		Node3D Player = (Node3D)Cam;
		for (int i = 0; i<TreeList.Count;i++)
		{
			
			Node3D tree = TreeList[i];
			Node3D LOD = tree.GetNode<Node3D>("LOD");
			Node3D HD = tree.GetNode<Node3D>("HD");
			double dist = MapTool.Distance(Player,tree);
			if (dist>150)
			{
				HD.Visible = false;
				LOD.Visible = false;
			}
			else
			{
				HD.Visible = false;
				LOD.Visible = true;
			}
			//pas assez opti pour le moment
			//tree.Visible = MapTool.IsNodeVisible(tree, Cam);
		}
		
	}

	public List<(int, int, int)> GetSpawnLocation()
	{
		throw new NotImplementedException();
	}

	public bool MapIsReady()
	{
		throw new NotImplementedException();
	}

	public void DebugMode(double delta, CharacterBody3D Player)
	{
		throw new NotImplementedException();
	}
	
}
