using Godot;
using System;
using System.IO;
using System.Diagnostics;

public partial class ServeurManager : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StreamWriter sw = new StreamWriter("port.txt"); 
		Process p1 = new Process();
		//p1.StartInfo.FileName = "bash";
		//p1.StartInfo.Arguments = "exec.sh";
		p1.StartInfo.FileName = "execwin.bat";

		p1.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
