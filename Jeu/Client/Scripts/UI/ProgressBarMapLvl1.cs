using Godot;
using System;

public partial class ProgressBarMapLvl1 : Control
{
	public int Load = 0;
	private ProgressBar _progressBar;
	private Label _label;
	
	private float _screenDefalutWidth = 1152;
	private float _textDefaultSize = 30;
	
	public override void _Ready()
	{
		_progressBar = GetNode<ProgressBar>("ProgressBar");
	}
	
	public void OnResize()
	{
		_label = GetNode<Label>("wait");
        
		_label.LabelSettings.FontSize = (int)(_textDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
	}

	public override void _Process(double delta)
	{
		_progressBar.Value = Load/1.5f;
		if (Load == 2500)
		{
			_label.Text = "En attente des autres joueurs";
		}
		else if (Load >= 150)
		{
			_label.Text = "Chargement de la map : 100%";
		}
		else
		{
			_label.Text = "Chargement de la map : " + (int)_progressBar.Value + "%";
		}
	}
}
