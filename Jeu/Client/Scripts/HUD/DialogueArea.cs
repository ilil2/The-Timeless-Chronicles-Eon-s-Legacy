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
	private string TargetText = "";
	private int indexLetter = 0;
	
	public override void _Ready()
	{
		Parent = GetParent<IMap>();
		Cam = GetNode<Camera3D>("Cam");
		Dialogue = JsonSerializer.Deserialize<Dictionary<string,Dictionary<string,string>>>(File.ReadAllText("Ressources/Dialogue/EmaxDialogue.json"));
		//ResetData();
		
	}

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
	
	private void OnResize()
	{
		Hud = GetNode<Control>("DialogueHud");
		Line = Hud.GetNode<Label>("Line");
		Line.LabelSettings.FontSize = (int)(30 * (Hud.GetViewportRect().Size.X / 1152));
	}
	
	private void _on_body_entered(Node3D body)
	{
		if(body is ClassScript)
		{
			GameHUD.IsVisible = false;
			SaveDialogue.Emax = (SaveDialogue.Emax.c1,0);
			
			_on_skip_button_pressed();
			
			Near = true;
			Cam.Current = true;
			Parent.CamOnPlayer = false;
			Hud.Visible = true;
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
		
	}
	
	private void _on_body_exited(Node3D body)
	{
		GameHUD.IsVisible = true;
	}
	private void _on_close_button_pressed()
	{
		Hud.Visible = false;
		Near = false;
		Cam.Current = false;
		Parent.CamOnPlayer = true;
		Hud.Visible = false;
		GameHUD.IsVisible = false;
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
		if (Dialogue.ContainsKey(c1.ToString()))
		{
			if (Dialogue[c1.ToString()].ContainsKey(c2.ToString()))
			{
				return Dialogue[c1.ToString()][c2.ToString()];
			}
		}

		return "Dialogue not found";

	}

	private void _on_skip_button_pressed()
	{
		Line.Text = "";
		indexLetter = 0;
		TargetText = GetDialogue();
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

