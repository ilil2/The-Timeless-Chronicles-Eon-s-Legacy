using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class AssassinScript : ClassScript
{
    public override void _Ready()
    {
        InitPlayer();
        
        _walkSpeed = 4.6f;
        _runSpeed = 7.9f;
        _dashPower = 100.0f;
    }

    public override void _Input(InputEvent @event)
    {
        if (_camera.Current && !GameManager._pausemode)
        {
            Zoom(@event);
        }
    }

    public override void _Process(double delta)
    {
        SendPosition();
        Pause();
    }

    public override void _PhysicsProcess(double delta)
    {
        PhysicsReset();
        Gravity(delta);

        if (_camera.Current && !GameManager._pausemode && !((ChatUI)GameManager._chat).IsOnChat())
        {
            Move(delta);
        }
    }
}
