using Godot;
using System;
using System.Collections.Generic;

public partial class PortalEnter : Node3D
{
	private Node3D Portal;
	private AnimationPlayer AnimationPortal;
	private AnimationPlayer Animationn;
	[Export] private bool PlayOpen = false;
	private bool open = false;
	private List<Node3D> AnimationSpawn = new List<Node3D>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach(Node3D s in GetNode<Node3D>("AnimationSpawn").GetChildren())
		{
			AnimationSpawn.Add(s);
		}
		AnimationPortal = GetNode<AnimationPlayer>("Portal/AnimationPlayer");
		Animationn = GetNode<AnimationPlayer>("AnimationPlayer");
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(PlayOpen)
		{
			AnimationPortal.Play("Open");
			Animationn.Play("Enter");
			PlayOpen = false;
			open = true;
		}
		if(!AnimationPortal.IsPlaying() && open)
		{
			AnimationPortal.Play("Idle");
		}
		if(Animationn.CurrentAnimation == "Enter")
		{
			for(int i = 0; i<GameManager._nbJoueur;i++)
			{
				if(GameManager.ListJoueur[i]!=null && GameManager.ListJoueur[i].IsInsideTree())
				{
					GameManager.ListJoueur[i].GlobalPosition = AnimationSpawn[i].GlobalPosition;
				}
			}
		}
	}
}
