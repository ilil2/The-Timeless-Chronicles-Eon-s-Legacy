using Godot;
using System;

public partial class ChatUI : Control
{
	private Label _output;
	private LineEdit _input;
	private ColorRect _colorRect;
	private Control _chat;

	private string _outputtext;
	public string Outputaddtext;
	public string Inputtext = "";

	private bool _onchat;
	
	private float _screenDefalutWidth = 1152;
	private float _chatDefaultSize = 12;
	
	public override void _Ready()
	{
		_input = GetNode<LineEdit>("Chat/Input");
		_colorRect = GetNode<ColorRect>("Chat/ColorRect");
		_chat = GetNode<Control>("Chat");

		Outputaddtext = "";
		_colorRect.Color = new Color(0,0,0,0.6f);
		_onchat = false;
	}
	
	public void OnResize()
	{
		_output = GetNode<Label>("Chat/Output");
		
		_output.LabelSettings.FontSize = (int)(_chatDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
	}

	public override void _Process(double delta)
	{
		if (GameManager._pausemode)
		{
			_onchat = false;
			_input.ReleaseFocus();
			
			Visible = false;
		}
		else
		{
			Visible = true;
		}

		_output.Text = _outputtext;
			
		if (Outputaddtext != "")
		{
			_outputtext += Outputaddtext + "\n";
			Outputaddtext = "";
		}

		if (_onchat)
		{
			_colorRect.Color = new Color(0,0,0,0.75f);
			if (Input.IsKeyPressed(Key.Enter))
			{
				Inputtext = _input.Text;
				_input.Text = ""; 
				_onchat = false;
				_input.ReleaseFocus();
			}

			if (Input.IsKeyPressed(Key.Escape))
			{
				_onchat = false;
				_input.Text = "";
				_input.ReleaseFocus();
			}
		}
		else
		{
			_colorRect.Color = new Color(0,0,0,0.6f);
			if (Input.IsKeyPressed(Key.T))
			{
				_onchat = true;
				_input.GrabFocus();
			}
		}
	}
	
	public bool IsOnChat()
	{
		return _onchat;
	}
}
