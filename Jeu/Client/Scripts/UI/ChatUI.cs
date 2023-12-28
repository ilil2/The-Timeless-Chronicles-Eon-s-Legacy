using Godot;
using System;

public partial class ChatUI : Control
{
	private Label _output;
	private LineEdit _input;
	private ColorRect _colorRect;

	private string _outputtext;
	public string Outputaddtext;
	public string Inputtext;

	private bool _onchat;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_output = GetNode<Label>("Output");
		_input = GetNode<LineEdit>("Input");
		_colorRect = GetNode<ColorRect>("ColorRect");

		Outputaddtext = "";
		_colorRect.Color = new Color(0,0,0,0.6f);
		_onchat = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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
}
