using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class NoMansLand : Area3D
{
	private IMap Map;
	private StaticBody3D[] GraveArray = new StaticBody3D[4];
	private CharacterBody3D[] GhostArray = new CharacterBody3D[4];
	private PackedScene Grave = GD.Load<PackedScene>("res://Ressources/Map/Global/Object/Gravestone/Gravestone.tscn");

	private PackedScene Gh = GD.Load<PackedScene>("res://Scenes/PlayerScenes/Ghost.tscn");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Map = GetParent<IMap>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_body_entered(Node3D body)
	{
		if (body is PlayerScript player)
		{
			StaticBody3D Stone = Grave.Instantiate<StaticBody3D>();
			CharacterBody3D ghost = Gh.Instantiate<CharacterBody3D>();
			Stone.Name = $"{player.GetId()}";
			(Stone as Gravestone).ID = player.GetId();
			Stone.Position = new Vector3(player.Position.X,0,player.Position.Z);
			ghost.Position = new Vector3(player.Position.X,1,player.Position.Z);
			AddChild(Stone);
			GraveArray[player.GetId()] = Stone;
			GhostArray[player.GetId()] = ghost;
			Stone.Rotation = player.GetNode<Node3D>("Player").Rotation;
			(Stone as Gravestone).Pseudo.Text = player.Pseudo;

			if (player is ClassScript)
			{
				AddChild(ghost);
				//Stone.GetNode<Camera3D>("Camera3D").Current = true;
				(ghost as Ghost).Camera.Current = true;
				Map.CamOnPlayer = false;
			}
		}
	}


	private void _on_body_exited(Node3D body)
	{
		if (body is PlayerScript player)
		{
			GraveArray[player.GetId()].QueueFree();
			GhostArray[player.GetId()].QueueFree();
			Map.CamOnPlayer = true;
		}
	
	}
}

