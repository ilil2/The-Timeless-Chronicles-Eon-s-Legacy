using Godot;
using System;
using System.Collections.Generic;

public partial class MapLvl2Script : Node3D, IMap
{

	private Random Rand = new Random();
	private int state = 0;
	private PackedScene Wa = GD.Load<PackedScene>("res://Scenes/MapScenes/Lvl2/R.tscn");
	private List<RigidBody3D> PseudoTreeList = new List<RigidBody3D>();
	private List<Node3D> TreeList = new List<Node3D>();
	public int FrameCount = 0;
	private int StartTimer = 0;
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
			if ((FrameCount-StartTimer>1) && CheckSleep())
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

	public (int,int) GetSpawnLocation()
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
	
	private void CreateBorder()
	{
		const int Rayon = 150;
		const int Pas = 4;
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
		const int nbtree = 500;
		const float radius = 2.5f;
		for (int i = 0; i < nbtree; i++)
		{
			RigidBody3D Sphere = new RigidBody3D();
			SphereShape3D sphereShape = new SphereShape3D();
			SphereMesh sphereMesh = new SphereMesh();
			CollisionShape3D collisionShape = new CollisionShape3D();
			MeshInstance3D meshInstance = new MeshInstance3D();
			sphereMesh.Radius = radius;
			sphereMesh.Height = radius * 2;
			meshInstance.Mesh = sphereMesh;
			sphereShape.Radius = radius;
			collisionShape.Shape = sphereShape;
			Sphere.AddChild(meshInstance);
			Sphere.AddChild(collisionShape);

			const int radiusmap = 140;
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
			RemoveChild(PseudoTreeList[i]);
			Node3D tree = GD.Load<PackedScene>("res://Ressources/Map/Global/tre2/Model/Tree1.tscn").Instantiate<Node3D>();
			tree.Position = Pos - new Vector3(0,2,0);
			tree.Rotation = new Vector3(0,Mathf.DegToRad(Rand.Next(0,361)),0);
			AddChild(tree);
			TreeList.Add(tree);
			
		}
	}
	
	private bool CheckSleep()
	{
		for (int i = 0; i<PseudoTreeList.Count;i++)
		{
			if (PseudoTreeList[i].Sleeping == false)
			{
				return false;
			}
		}
		return true;
	}
	private void Render(Camera3D Cam)
	{
		Node3D Player = (Node3D)Cam;
		for (int i = 0; i<TreeList.Count;i++)
		{
			
			Node3D tree = TreeList[i];
			Node3D LOD = tree.GetNode<Node3D>("LOD");
			Node3D HD = tree.GetNode<Node3D>("HD");
			double dist = Distance(Player,tree);
			if (dist>50)
			{
				HD.Visible = false;
				LOD.Visible = true;
			}
			else
			{
				HD.Visible = true;
				LOD.Visible = false;
			}
		}
		
	}
	
	private double Distance(Node3D Room1, Node3D Room2)
	{
		return Math.Sqrt(Math.Pow(Room1.GlobalPosition.X - Room2.GlobalPosition.X, 2) +
						 Math.Pow(Room1.GlobalPosition.Z - Room2.GlobalPosition.Z, 2));
	}
}
