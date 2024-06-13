using Godot;
using System;
using Lib;
using System.Collections.Generic;

public partial class PortalExit : Node3D
{
	private Node3D Portal;
	private AnimationPlayer AnimationPortal;
	private bool Open = false;
	[Export] private bool PlayClose = false;
	[Export] private bool OnCam = false;
	private Camera3D Cam;
	private bool PlayerIn = false;
	private Label nb;
	private int NbPlayer = 0;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Portal = GetNode<Node3D>("Portal");
		AnimationPortal = Portal.GetNode<AnimationPlayer>("AnimationPlayer");
		Cam = GetNode<Camera3D>("Cam");
		Cam.Current = OnCam;
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Cam.Current = OnCam;
		if(PlayClose)
		{
			PlayClose = false;
			AnimationPortal.Play("Close");
		}
		if(PlayerIn)
		{
			nb.Text = $"{NbPlayer}/{GameManager._nbJoueur}, En attente des autres joueurs ...";
			OnCam = true;
		}
		double dist = GetMinDist();
		if(dist>10)
		{
			if(Open)
			{
				AnimationPortal.Play("Close");
				Open = false;
			}
			else if(!AnimationPortal.IsPlaying())
			{
				Portal.Visible = false;
			}
		}
		else if((GetParent() as IMap).CanExit)
		{
			if(!Open && SaveDialogue.Emax.c1 != 0 && SaveDialogue.Emax.c1 % 2 == 0)
			{
				Portal.Visible = true;
				AnimationPortal.Play("Open");
				Open = true;
			}
			else if(!AnimationPortal.IsPlaying())
			{
				AnimationPortal.Play("Idle");
			}
		}
		//AnimationPortal.Play("Idle");
	}
	private double GetMinDist()
	{
		if(GameManager.ListJoueur!=null)
		{
			double Min = 99999999999.0;
			for(int i = 0; i<GameManager._nbJoueur;i++)
			{
				double dist = 0.0;
				if(GameManager.ListJoueur[i]!= null && GameManager.ListJoueur[i].IsInsideTree())
				{
					dist = MapTool.Distance(Portal.GlobalPosition,GameManager.ListJoueur[i].GlobalPosition);	
				}
				else
				{
					GD.Print("ERROR: Player Non set (PortalExit)");
					dist = Min+1;
				}
				if(Min>dist)
				{
					Min = dist;
				}
			}
			return Min;
		}
		else
		{
			return 1000.0;
		}
		//return MapTool.Distance(Portal.GlobalPosition,GameManager.ListJoueur[0].GlobalPosition);
	}
	private void _on_ex_body_entered(Node3D body)
	{
		if(Open)
		{
			if(body == GameManager.ListJoueur[0])
			{
				PlaceCam();
				nb = new Label();
				nb.Text = "Test";
				AddChild(nb);
				PlayerIn = true;
				GameManager.SettingsManager.SetSetting("enableChat",0);
			}
			
			body.Visible = false;
			GD.Print($"{body.Name} is Visible {body.Visible}");
			body.Position-=new Vector3(0,3,0);
			GD.Print("Player Enter Portal!");
			NbPlayer++;
		}
	}

	private void PlaceCam()
	{
		Camera3D Cam = GetNode<Camera3D>("Cam");
		Cam.Current = true;
		(GetParent() as IMap).CamOnPlayer = false;
		
	}
}

