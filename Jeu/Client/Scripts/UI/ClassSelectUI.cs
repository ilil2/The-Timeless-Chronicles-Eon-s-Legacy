using Godot;
using System;
using System.Collections.Generic;

public partial class ClassSelectUI : Control
{
	public static string ClassChose = "";
	public static bool Supr = false; 
	
	private bool _isReady = false;
	private List<string> _classList = new List<string>{"Knight","Scientist","Assassin","Archer"};
	private int _classID = 0;
	private int _angleTarget = 0;
	
	private Node3D _pivot;
	private Node3D _buttonReady;
	private Node3D _buttonLeft;
	private Node3D _buttonRight;
	private AnimationPlayer _animation;
	private TextMesh _classText;
	private TextMesh _readyText;
	private TextMesh _waitingText;

	private Dictionary<string, string> _languageDict;
	
	public override void _Ready()
	{
		_animation = GetNode<AnimationPlayer>("AnimationPlayer");
		_pivot = GetNode<Node3D>("ClassSelect3D/Pivot");
		_animation.Play("Enter");
		_pivot.GetNode<AnimationPlayer>("Knight/AnimationPlayer").Play("Idle");	
		_pivot.GetNode<AnimationPlayer>("Scientist/AnimationPlayer").Play("Idle");	
		_pivot.GetNode<AnimationPlayer>("Assassin/AnimationPlayer").Play("Idle");	
		_pivot.GetNode<AnimationPlayer>("Archer/AnimationPlayer").Play("Idle");
		
		_classText = GetNode<MeshInstance3D>("ClassSelect3D/TextMesh").Mesh as TextMesh;
		_readyText = GetNode<MeshInstance3D>("ClassSelect3D/ButtonReady/TextMesh").Mesh as TextMesh;
		_waitingText = GetNode<MeshInstance3D>("ClassSelect3D/WaitingText").Mesh as TextMesh;
		
		_buttonReady = GetNode<Node3D>("ClassSelect3D/ButtonReady");
		_buttonLeft = GetNode<Node3D>("ClassSelect3D/ButtonLeft");
		_buttonRight = GetNode<Node3D>("ClassSelect3D/ButtonRight");
		
		Translation();
	}
	
	private void Translation()
	{
		int language = GameManager.SettingsManager.GetAllSettings()["language"];
		_languageDict = GameManager.LanguageManager.GetLanguage(language);
		
		_readyText.Text = _languageDict["selectClassMenuReadyButton"];
	}
	
	public override void _Process(double delta)
	{
		_classText.Text = _languageDict[_classList[_classID]];
		
		if (Supr)
		{
			QueueFree();
		}
		
		if(_pivot.RotationDegrees.Y < _angleTarget-2)
		{
			_pivot.RotationDegrees += new Vector3(0,2,0);
		}
		else if(_pivot.RotationDegrees.Y > _angleTarget+2)
		{
			_pivot.RotationDegrees += new Vector3(0,-2,0);
		}
		else
		{
			_pivot.RotationDegrees = new Vector3(0,_angleTarget,0);
		}
		
	}
	
	private void _on_ready_pressed()
	{
		if (!_isReady)
		{
			_animation.Play("Ready");
			_isReady = true;
			ClassChose = _classList[_classID];
			_waitingText.Text = _languageDict["selectClassMenuWaitingText"];
		}
	}


	private void _on_left_pressed()
	{
		if (!_isReady)
		{
			_animation.Play("Left");
			_angleTarget += 90;
			_classID -= 1;
			if (_classID < 0)
			{
				_classID += 4;
			}
		}
	}


	private void _on_right_pressed()
	{
		if (!_isReady)
		{
			_animation.Play("Right");
			_angleTarget -= 90;
			_classID += 1;
			_classID %= 4;
		}
	}
}

