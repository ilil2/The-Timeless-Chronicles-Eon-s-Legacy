using Godot;
using System;

public partial class Portal : Node3D
{
	private AnimationPlayer _animationPortal;
	private AudioStreamPlayer3D _idleSound;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animationPortal = GetNode<AnimationPlayer>("AnimationPlayer");
		_idleSound = GetNode<AudioStreamPlayer3D>("Idle");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_animationPortal.CurrentAnimation == "Idle" && _idleSound.Playing == false)
		{
			_idleSound.Play();
			GD.Print("Play Idle Sound");
		}

		if (!_animationPortal.IsPlaying())
		{
			_idleSound.Stop();
			GD.Print("Stop Idle Sound");
		}
	}
}
