using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public partial class BossDialogue : Control
{
	public bool Near = false;
	private IMap Parent;
	private Control Hud;
	private Label Line;
	private Dictionary<string,Dictionary<string,string>> Dialogue;
	private string TargetText = "";
	private int indexLetter = 0;
	[Export] public bool _next = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		(int c1, int c2) = SaveDialogue.Boss;
		c1++;
		SaveDialogue.Boss = (c1, c2);
		Parent = GetParent<IMap>();
		Line = GetNode<Label>("Line");
		Dialogue = JsonSerializer.Deserialize<Dictionary<string,Dictionary<string,string>>>(File.ReadAllText("Ressources/Dialogue/BossDialogue.json"));
		//ResetData();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CompleteText();
		if(_next)
		{
			Next();
			_next = false;
		}
	}
	
	private string GetDialogue()
	{
		(int c1, int c2) = SaveDialogue.Boss;
		c2++;
		if(Dialogue[c1.ToString()][c2.ToString()]=="END")
		{	
			c1++;
			return "";
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
	
	public void Next()
	{
		indexLetter = 0;
		Line.Text = "";
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
