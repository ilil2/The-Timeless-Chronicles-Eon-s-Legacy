using Godot;
using System;
using System.Collections.Generic;

namespace Lib;

public partial class MapTool : Node
{
	public static bool DebugM = false;
	public Camera3D DebugCam;
	
	public static double Distance(Node3D Node1, Node3D Node2)
	{
		return Math.Sqrt(Math.Pow(Node1.GlobalPosition.X - Node2.GlobalPosition.X, 2) +
						 Math.Pow(Node1.GlobalPosition.Z - Node2.GlobalPosition.Z, 2));
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
	public static bool Debug(CharacterBody3D Player, Node3D Map)
	{
		Camera3D CameraPlayer = Player.GetNode<Camera3D>("CameraPlayer/h/v/Camera3D");
		if(DebugM)
		{
			Camera3D DebugCam = Map.GetNode<Camera3D>("SpecCam");
			DebugM = false;
			CameraPlayer.Current = true;
			DebugCam.Current = false;
			Map.RemoveChild(DebugCam);
			
		}
		else
		{
			DebugM = true;
			Camera3D DebugCam = GD.Load<PackedScene>("res://Scenes/Debug/SpecCam.tscn").Instantiate<Camera3D>();
			DebugCam.GlobalTransform = CameraPlayer.GlobalTransform;
			Map.AddChild(DebugCam);
			CameraPlayer.Current = false;
			DebugCam.Current = true;
			
			
		}
		return DebugM;
	}
}
