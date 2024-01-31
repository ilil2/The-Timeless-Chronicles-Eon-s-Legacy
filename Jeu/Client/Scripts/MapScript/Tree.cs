using Godot;
using System;
using Lib;

public partial class Tree : Node3D
{
	private CharacterBody3D Player = new CharacterBody3D();
	private Vector3 Me = new Vector3();
	private Vector3 PlayerPos = new Vector3();
	private bool PlayerSet = false;
	private const int RenderDist = 100;
	public override void _Ready()
	{
	
	}

	
	public override void _Process(double delta)
	{
		if(!PlayerSet && GameManager.Joueur1!=null)
		{
			Player = GameManager.Joueur1;
			PlayerSet = true;
			
		}
		else
		{
			Me = Position;
			PlayerPos = Player.Position;
			double dist = MapTool.Distance(Me,PlayerPos);
			if(dist>RenderDist)
			{
				Visible = false;
			}
			else
			{
				Visible = true;
			}
		}
	}
}
