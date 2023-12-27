using Godot;
using System;

public partial class ProgressBarMapLvl1 : Control
{
	public int Load = 0;
	private ProgressBar _progressBar;
	private Label _label;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_progressBar = GetNode<ProgressBar>("ProgressBar");
		_label = GetNode<Label>("wait");
		_label.Text = "";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_progressBar.Value = Load/1.5f;
		if (Load == 2500)
		{
			_progressBar.ShowPercentage = false;
			_label.Text = "En attente des autres joueurs";
		}
	}
}
