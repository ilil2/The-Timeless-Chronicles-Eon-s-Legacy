using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class ScientistScript : ClassScript
{
    public override void _Ready()
    {
        InitPlayer();
    }

    public override void _Input(InputEvent @event)
    {
        Zoom(@event);
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

        if (_camera.Current)
        {
            Dash();
            Move(delta);
        }
    }
}
