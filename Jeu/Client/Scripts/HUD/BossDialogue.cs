using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public partial class BossDialogue : Area3D
{
	public bool Near = false;
	private IMap Parent;
	//private Camera3D Cam;
	private Control Hud;
	private Label Line;
	private Dictionary<string,Dictionary<string,string>> Dialogue;
	private string TargetText = "";
	private int indexLetter = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Parent = GetParent<IMap>();
		//Cam = GetNode<Camera3D>("Cam");
		Hud = GetNode<Control>("DialogueHud");
		Line = Hud.GetNode<Label>("Line");
		Dialogue = JsonSerializer.Deserialize<Dictionary<string,Dictionary<string,string>>>(File.ReadAllText("Ressources/Dialogue/BossDialogue.json"));
		//ResetData();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CompleteText();
		if (Near)
		{
			GameHUD.IsVisible = false;
		}
		else
		{
			GameHUD.IsVisible = true;
		}
	}
	
	private string GetDialogue()
	{
		(int c1, int c2) = SaveDialogue.Boss;
		c2++;
		if(Dialogue[c1.ToString()][c2.ToString()]=="END")
		{	
			Hud.Visible = false;
			Near = false;
			//Cam.Current = false;
			Parent.CamOnPlayer = true;
			Hud.Visible = false;
			GameHUD.IsVisible = false;
			Parent.GetParent().GetNode<Control>("GameHUD").Visible = true;
			Input.MouseMode = Input.MouseModeEnum.Captured;	
		}
		SaveDialogue.Boss = (c1, c2);
		GD.Print($"{c1} {c2}");
		if (Dialogue.ContainsKey(c1.ToString()))
		{
			if (Dialogue[c1.ToString()].ContainsKey(c2.ToString()))
			{
				return Dialogue[c1.ToString()][c2.ToString()];
			}
		}
		return "Dialogue not found";
	}
	
	private void CompleteText()
	{
		if(Line.Text!=TargetText)
		{
			Line.Text+=TargetText[indexLetter];
			indexLetter++;
		}
	}
}
