using Godot;
using System;
using System.Collections.Generic;

namespace Lib;

public partial class MapTool : Node
{
	public Camera3D DebugCam;
	
	public static double Distance(Node3D Node1, Node3D Node2)
	{
		return Math.Sqrt(Math.Pow(Node1.GlobalPosition.X - Node2.GlobalPosition.X, 2) +
						 Math.Pow(Node1.GlobalPosition.Z - Node2.GlobalPosition.Z, 2));
	}
	public static double Distance(Vector3 Node1, Vector3 Node2)
	{
		return Math.Sqrt(Math.Pow(Node1.X - Node2.X, 2) +
						 Math.Pow(Node1.Z - Node2.Z, 2));
	}
	
	public static bool IsNodeVisible(Node3D node, Camera3D camera)
	{
		// Not Use !
		Vector3 cameraPosition = camera.GlobalTransform.Origin;
		Vector3 nodePosition = node.GlobalTransform.Origin;

		return !camera.IsPositionBehind(nodePosition);
	}
	
	public static bool CheckSleep(List<RigidBody3D> PseudoList)
	{
		for (int i = 0; i<PseudoList.Count;i++)
		{
			if (((RigidBody3D)PseudoList[i]).Sleeping == false)
			{
				return false;
			}
		}
		return true;
	}
	public static void Debug(CharacterBody3D Player, Node3D Map, bool DebugM)
	{
		Camera3D CameraPlayer = Player.GetNode<Camera3D>("CameraPlayer/h/v/Camera3D");
		if(DebugM)
		{
			try
			{
				Camera3D DebugCam = Map.GetNode<Camera3D>("SpecCam");
				DebugM = false;
				(Map as IMap).CamOnPlayer = true;
				DebugCam.Current = false;
				Map.RemoveChild(DebugCam);
			}
			catch
			{
				DebugM = false;
			}
			
		}
		else
		{
			DebugM = true;
			Camera3D DebugCam = GD.Load<PackedScene>("res://Scenes/Debug/SpecCam.tscn").Instantiate<Camera3D>();
			DebugCam.GlobalTransform = CameraPlayer.GlobalTransform;
			Map.AddChild(DebugCam);
			(Map as IMap).CamOnPlayer = false;
			DebugCam.Current = true;
			
			
		}
	}
	
	public static List<RigidBody3D> Concat(List<RigidBody3D> item1, List<RigidBody3D> item2)
	{
		List<RigidBody3D> res = new List<RigidBody3D>();
		foreach (var e in item1)
		{
			res.Add(e);
		}

		foreach (var e in item2)
		{
			res.Add(e);
		}

		return res;
	}
}
