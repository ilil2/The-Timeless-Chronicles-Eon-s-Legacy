using Godot;
using System;
using System.Collections.Generic;

namespace Lib;

public partial class MapTool : Node
{
	public bool DebugMode = false;
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
	public static bool Debug(CharacterBody3D Player)
	{
		Camera3D CameraPlayer;
		if(DebugMode)
		{
			DebugMode = false;
		}
		else
		{
			DebugMode = true;
		}
		return DebugMode;
	}
}
