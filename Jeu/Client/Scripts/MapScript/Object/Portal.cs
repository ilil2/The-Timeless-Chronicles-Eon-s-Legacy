using Godot;
using System;

public partial class Portal : Node3D
{
	private AnimationPlayer _animationPortal;
	private AudioStreamPlayer3D _idleSound;
	private AudioStreamPlayer3D _openSound;
	private AudioStreamPlayer3D _closeSound;

	private bool _isOpen = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animationPortal = GetNode<AnimationPlayer>("AnimationPlayer");
		_idleSound = GetNode<AudioStreamPlayer3D>("Idle");
		_openSound = GetNode<AudioStreamPlayer3D>("Open");
		_closeSound = GetNode<AudioStreamPlayer3D>("Close");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_animationPortal.CurrentAnimation == "Idle" && _idleSound.Playing == false)
		{
			_idleSound.Play();
		}
		else if (_animationPortal.CurrentAnimation == "Open" && !_isOpen)
		{
			_openSound.Play();
			_isOpen = true;
		}
		if (_animationPortal.CurrentAnimation == "Close" && _isOpen)
		{
			_idleSound.Stop();
			_closeSound.Play();
			_isOpen = false;
		}
	}
}
