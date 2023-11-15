using Godot;
using System;

public partial class pauseUI : Control
{
	public void Reprendre()
	{
		//Supprimer le menu de pause
		Input.MouseMode = Input.MouseModeEnum.Captured;
		GameManager._pausemode = false;
		QueueFree();
	}
	
	public void Quitter()
	{
		//Quitter le jeu
		GetTree().Quit();
	}
}
