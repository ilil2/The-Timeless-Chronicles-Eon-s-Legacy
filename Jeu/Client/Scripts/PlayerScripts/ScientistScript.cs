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
            ShootLaser();
        }
    }
    
    private void ShootLaser()
    {
        if (Input.IsMouseButtonPressed(MouseButton.Left))
        {
            PackedScene laserScene = GD.Load<PackedScene>("res://Scenes/EntityScenes/Laser.tscn");
            Node3D laser = laserScene.Instantiate<Node3D>();
            
            double rotationY = _cameraH.Rotation.Y;
            
            laser.GlobalPosition = new Vector3(_cameraV.GlobalPosition.X + (float)Math.Sin(rotationY)*2, Position.Y + 1, Position.Z + (float)Math.Cos(rotationY)*2);
            laser.Rotation = new Vector3(laser.Rotation.X, (float)rotationY, _cameraV.Rotation.X);
            
            GetTree().Root.AddChild(laser);
        }
    }
}
