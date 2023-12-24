using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class KnightScript : ClassScript
{

    public override void _Ready()
    {
        //Initialisation du joueur
        InitPlayer();
        
        _walkSpeed = 3.6f;
        _runSpeed = 6.8f;
        _dashPower = 70.0f;
        
        _animationPlayer = GetNode<AnimationPlayer>("PlayerBody/Sword/AnimationPlayer");
    }

    public override void _Input(InputEvent @event)
    {
        Zoom(@event);
    }

    public override void _Process(double delta)
    {
        SendPosition();
    }

    public override void _PhysicsProcess(double delta)
    {
        PhysicsReset();
        Gravity(delta);

        if (_camera.Current)
        {
            Dash();
            Move(delta);
            AnimationPlayer();
        }
    }
    
    private void AnimationPlayer()
    {
        //Animation du joueur
        if (_isRunning)
        {
            _animationPlayer.Play("run");
        }
        else if (_animationPlayer.CurrentAnimation == "run")
        {
            _animationPlayer.Stop();
        }
			
        if (Input.IsMouseButtonPressed(MouseButton.Left) && _animationPlayer.CurrentAnimation != "run")
        {
            _animationPlayer.Play("hit");
        }
    }
}
