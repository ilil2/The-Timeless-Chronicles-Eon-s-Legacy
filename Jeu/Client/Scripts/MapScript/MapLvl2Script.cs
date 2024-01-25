using Godot;
using System;
using System.Collections.Generic;

public partial class MapLvl2Script : Node3D, IMap
{

	private Random Rand = new Random();
	private PackedScene Wa = GD.Load<PackedScene>("res://Scenes/MapScenes/Lvl2/R.tscn");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		 CreateBorder();

		const int nbtree = 500;
		const int radius = 1;
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

			const int radiusmap = 150;
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

			Sphere.Position = new Vector3((float)x, 20, (float)z);
			AddChild(Sphere);


		}
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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
}
