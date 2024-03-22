using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;
using Lib;

public partial class Exit : Area3D
{
	private int PlayerIn = 0;
	private IMap Map;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Map = (IMap)GetParent();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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
				UDP.OneShot("next");
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

