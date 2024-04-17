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
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Parent = GetParent<IMap>();
		Cam = GetNode<Camera3D>("Cam");
		Hud = GetNode<Control>("DialogueHud");
		Line = Hud.GetNode<Label>("Line");
		Dialogue = JsonSerializer.Deserialize<Dictionary<string,Dictionary<string,string>>>(File.ReadAllText("Ressources/Dialogue/EmaxDialogue.json"));
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
			var data = GetData();
			string[] index = data["Emax"].Split(";");
			string Cur1 = index[0];
			string Cur2 = "0";
			data["Emax"] = Cur1+";"+Cur2;
			UpdateJson(data);
			
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
		Near = false;
		Cam.Current = false;
		Parent.CamOnPlayer = true;
		Hud.Visible = false;
		Parent.GetParent().GetNode<Control>("GameHUD").Visible = true;
		Input.MouseMode = Input.MouseModeEnum.Captured;	
	}
	
	private Dictionary<string,string> GetData()
	{
		var data = JsonSerializer.Deserialize<Dictionary<string,string>>(File.ReadAllText("Ressources/Dialogue/SaveDialogue.json"));
		return data;
	}
	private string GetDialogue(Dictionary<string,string> data)
	{
		string[] index = data["Emax"].Split(";");
		string Cur1 = index[0];
		string Cur2 = (int.Parse(index[1])+1).ToString();
		if(Dialogue[Cur1][Cur2]=="END")
		{
			_on_close_button_pressed();
		}
		if(Dialogue[Cur1][Cur2]=="NEXT")
		{
			Cur1 = (int.Parse(index[0])+1).ToString();
			Cur2 = "1";
		}
		data["Emax"] = Cur1+";"+Cur2;
		UpdateJson(data);
		GD.Print(Cur2);
		return Dialogue[Cur1][Cur2];
	}

	private void _on_skip_button_pressed()
	{
		Line.Text = GetDialogue(GetData());
	}
	private void UpdateJson(Dictionary<string,string> obj)
	{
		File.WriteAllText("Ressources/Dialogue/SaveDialogue.json", JsonSerializer.Serialize(obj));
	}
}

