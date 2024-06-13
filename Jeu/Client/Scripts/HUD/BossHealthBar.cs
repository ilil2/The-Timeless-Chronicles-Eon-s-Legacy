using Godot;
using System;

public partial class BossHealthBar : Control
{
	
	[Export] public int Max = 100;
	[Export] public int Value = 50;
	
	private ProgressBar Hp;
	private ProgressBar SubBar;
	public Label Name;
	[Export] public string _name = "";
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hp = GetNode<ProgressBar>("Bar");
		SubBar = GetNode<ProgressBar>("SubBar");
		Name = GetNode<Label>("Name");
		Hp.MaxValue = Max;
		SubBar.MaxValue = Max;
		Hp.Value = Max;
		SubBar.Value = Max;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void Process(double delta)
	{
		if(Name.Text!=_name)
		{
			Name.Text = _name;
		}
		if(Hp.Value!=Value)
		{
			Hp.Value = Value;
		}
		if(Hp.Value!=SubBar.Value)
		{
			SubBar.Value-=0.5;
		}
	}
}
