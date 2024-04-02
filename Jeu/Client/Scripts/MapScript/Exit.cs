using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;
using Lib;
[Tool]
public partial class Exit : Area3D
{
	private int PlayerIn = 0;
	private IMap Map;
	[Export] public bool exit = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Map = (IMap)GetParent();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(exit)
		{
			UDP.OneShot("next");
			GD.Print("Next Actif ---------------------------------------------------------");
			exit = false;
		}
	}
	
	public bool AllInside()
	{
		return (PlayerIn == (GameManager._nbJoueur) && (Map as IMap).CanExit);
	}
	
	private void _on_body_entered(Node3D body)
	{
		if(body is ClassScript || body is OtherClassScript)
		{
			PlayerIn++;
			if(AllInside())
			{
				
				Map.Ani.Play("Exit");
				
			}
		}
	}
	private void _on_body_exited(Node3D body)
	{
		if(body is ClassScript || body is OtherClassScript)
		{
			PlayerIn--;
		}
	}
}

