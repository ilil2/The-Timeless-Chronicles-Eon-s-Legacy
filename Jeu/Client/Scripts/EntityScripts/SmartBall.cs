using Godot;
using System;
using Lib;
using System.Collections.Generic;

public partial class SmartBall : Node3D
{
	private Vector3 TargetPosition = new Vector3(0,0,0);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private CharacterBody3D GetPlayer()
	{
		double distmin = 99999999999.0;
		CharacterBody3D res = null;
		for(int i = 0; i<GameManager.ListJoueur.Count;i++)
		{
			if(GameManager.ListJoueur[i]!=null)
			{
				double dist = MapTool.Distance(this.GlobalPosition,GameManager.ListJoueur[i].GlobalPosition);
				if(dist<distmin)
				{
					distmin = dist;
					res = GameManager.ListJoueur[i];
				}
			}
		}
		return res;
	}
}
