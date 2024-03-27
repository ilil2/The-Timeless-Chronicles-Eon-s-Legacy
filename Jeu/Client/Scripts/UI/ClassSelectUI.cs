using Godot;
using System;
using System.Collections.Generic;

public partial class ClassSelectUI : Control
{
	public static string ClassChose = "";
	public static bool Supr = false; 
	
	
	//Variable du texte d'attente
	
	
	
	private float _screenDefalutWidth = 1152;
	private float _waitingTextDefaultSize = 27;
	private float _buttonDefaultSize = 20;
	
	private bool _isReady = false;
	private List<string> ClassList = new List<string>{"Knight","Scientist","Assassin","Archer"};
	private int ClassID = 0;
	private int AngleTarget = 0;
	private Node3D Pivot;
	private AnimationPlayer animation;
	private TextMesh ClassText;
	
	public override void _Ready()
	{
		
		animation = GetNode<AnimationPlayer>("AnimationPlayer");
		Pivot = GetNode<Node3D>("ClassSelect3D/Pivot");
		animation.Play("Enter");
		Pivot.GetNode<AnimationPlayer>("Knight/AnimationPlayer").Play("Idle");	
		Pivot.GetNode<AnimationPlayer>("Scientist/AnimationPlayer").Play("Idle");	
		Pivot.GetNode<AnimationPlayer>("Assassin/AnimationPlayer").Play("Idle");	
		Pivot.GetNode<AnimationPlayer>("Archer/AnimationPlayer").Play("Idle");	
		ClassText = (GetNode<MeshInstance3D>("ClassSelect3D/TextMesh").Mesh as TextMesh);
		Translation();
	}
	
	private void Translation()
	{
		int language = GameManager.SettingsManager.GetAllSettings()["language"];
		Dictionary<string, string> languageDict = GameManager.LanguageManager.GetLanguage(language);
	}
	
	public void OnResize()
	{
	
	}
	
	public override void _Process(double delta)
	{
		ClassText.Text = ClassList[ClassID];
		if (Supr)
		{
			QueueFree();
		}
		if(Pivot.RotationDegrees.Y<AngleTarget-2)
		{
			Pivot.RotationDegrees+=new Vector3(0,2,0);
		}
		else if(Pivot.RotationDegrees.Y>AngleTarget+2)
		{
			Pivot.RotationDegrees+=new Vector3(0,-2,0);
		}
		else
		{
			Pivot.RotationDegrees=new Vector3(0,AngleTarget,0);
		}
		
	}
	
	private void _on_ready_pressed()
	{
		_isReady = true;
		ClassChose = ClassList[ClassID];
	}


	private void _on_left_pressed()
	{
		AngleTarget+=90;
		ClassID-=1;
		ClassID%=4;
	}


	private void _on_right_pressed()
	{
		AngleTarget-=90;
		ClassID+=1;
		ClassID%=4;
	}
}

