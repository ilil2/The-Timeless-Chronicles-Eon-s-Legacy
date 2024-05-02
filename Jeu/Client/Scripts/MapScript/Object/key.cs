using Godot;
using System;
using System.Xml.Serialization;
using JeuClient.Scripts.PlayerScripts;

public partial class key : IRender
{
	private Node3D Model;
	private int FrameCount = 0;
	private Vector3 InitPos;
	private const int LinearY = 5;
	private const float SpeedRotation = 1f;
	private const int SpeedLinearY = 3;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Model = GetNode<Node3D>("Mesh");
		InitPos = Model.Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if(RenderSetup())
		{
			Render();
		}
		FrameCount+=SpeedLinearY;
		//GD.Print(Math.Sin(Mathf.DegToRad(FrameCount)));
		Model.Position = InitPos + new Vector3(0,(float)Math.Sin(Mathf.DegToRad(FrameCount))/LinearY,0);
		Model.RotationDegrees += new Vector3(0,SpeedRotation,0);
	}
	
	private void _on_area_3d_body_entered(Node3D body)
	{
		if(body is PlayerScript)
		{
			GD.Print($"Key find by {(body as ClassScript).Pseudo}");
			GameManager.InfoJoueur["attack"] = "key";
			MapLvl2Script map = ((MapLvl2Script)GameManager.Map);
			map.NbrKey += 1;
			if (map.NbrKey == 3)
			{
				map.CanExit = true;
			}
		}
	}
}


