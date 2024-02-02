using Godot;
using System;
using System.IO;
using System.Diagnostics;
using Microsoft;

public partial class ServeurManager : Node3D
{
	private int DateDay = DateTime.Now.Day;
	private int DateDay2 = DateTime.Now.Day;
	
	private int Seed = 42;
	private int AleateSeed = 42;
	
	private Random rand = new Random();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StreamWriter sw = new StreamWriter("port.txt"); 
		Process p1 = new Process();
		//p1.StartInfo.FileName = "bash";
		//p1.StartInfo.Arguments = "exec.sh";
		p1.StartInfo.FileName = "execwin.bat";

		//a executer a la fin du _ready
		Seed = JeuServeur.Random.Rand();
		AleateSeed = rand.Next(0, 1000000);
		Write();
		p1.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		DateDay2 = DateTime.Now.Day;
		if (DateDay2 != DateDay)
		{
			Exec();
			DateDay = DateDay2;
		}
	}

	//a effectuer tout les jours
	private void Exec()
	{
		Seed = JeuServeur.Random.Rand();
		AleateSeed = rand.Next(0, 1000000);
		Write();
	}

	private void Write()
	{
		StreamWriter sw = new StreamWriter("DayInfo.txt");
		sw.Write($"{Seed}*{AleateSeed}");
		sw.Close();
	}
}
