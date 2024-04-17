using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public partial class DialogueArea : Area3D
{
	public bool Near = false;
	private IMap Parent;
	private Camera3D Cam;
	private Control Hud;
	private Label Line;
	private Dictionary<string,Dictionary<string,string>> Dialogue;
	private AnimationPlayer Ani;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Parent = GetParent<IMap>();
		Cam = GetNode<Camera3D>("Cam");
		Hud = GetNode<Control>("DialogueHud");
		Line = Hud.GetNode<Label>("Line");
		Dialogue = JsonSerializer.Deserialize<Dictionary<string,Dictionary<string,string>>>(File.ReadAllText("Ressources/Dialogue/EmaxDialogue.json"));
		Ani = GetNode<AnimationPlayer>("AnimationPlayer");
		//ResetData();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
	private void _on_body_entered(Node3D body)
	{
		if(body is ClassScript)
		{
			Ani.Play("Open");
			SaveDialogue.Emax = (SaveDialogue.Emax.c1,0);
			
			_on_skip_button_pressed();
			
			Near = true;
			Cam.Current = true;
			Parent.CamOnPlayer = false;
			Hud.Visible = true;
			Parent.GetParent().GetNode<Control>("GameHUD").Visible = false;
			Input.MouseMode = Input.MouseModeEnum.Visible;	
			GD.Print("Enter");
		}
		
	}
	
	private void _on_body_exited(Node3D body)
	{
		GD.Print("Exit");
	}
	private void _on_close_button_pressed()
	{
		Ani.Play("Close");
		Near = false;
		Cam.Current = false;
		Parent.CamOnPlayer = true;
		Hud.Visible = false;
		Parent.GetParent().GetNode<Control>("GameHUD").Visible = true;
		Input.MouseMode = Input.MouseModeEnum.Captured;	
	}
	
	private string GetDialogue()
	{
		(int c1, int c2) = SaveDialogue.Emax;
		c2++;
		if(Dialogue[c1.ToString()][c2.ToString()]=="END")
		{
			_on_close_button_pressed();
		}
		if(Dialogue[c1.ToString()][c2.ToString()]=="NEXT")
		{
			c1++;
			c2 = 1;
		}

		SaveDialogue.Emax = (c1, c2);
		GD.Print($"{c1} {c2}");
		return Dialogue[c1.ToString()][c2.ToString()];
	}

	private void _on_skip_button_pressed()
	{
		Line.Text = GetDialogue();
	}
}

