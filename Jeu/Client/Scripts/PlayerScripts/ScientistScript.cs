using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;

public partial class ScientistScript : ClassScript
{
    //Varibale du tir
    private int _shootTimer;
    private int _shootCooldown;
    private bool _isShooting;
    private bool _shootAnimation;
    
    private int _shootCooldownValue = 10;
    private int _shootTimerValue = 500;
    
    
    public static bool IsAiming;
    
    public override void _Ready()
    {
        InitPlayer();
        
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        _shootCooldown = _shootCooldownValue - 50;
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
            if (!_isShooting)
            {
                Move(delta);
            }
            ShootLaser();
        }
        else
        {
            if (_shootCooldown >= _shootCooldownValue)
            {
                _shootCooldown = _shootCooldownValue - 20;
            }
        }
    }
    
    private void ShootLaser()
    {
        _shootCooldown += 1;
        
        if (_shootTimer >= _shootTimerValue)
        {
            _shootTimer = 0;
            _isShooting = false;
        }
        
        if (_isShooting)
        {
            _shootTimer += 1;
        }
        
        if (Input.IsMouseButtonPressed(MouseButton.Right))
        {
            if (!_shootAnimation)
            {
                _shootAnimation = true;
                _animationPlayer.Play("LaserShootView");
            }
            IsAiming = true;
			
            PackedScene crossHairScene = GD.Load<PackedScene>("res://Scenes/HUD/ViewFinder.tscn");
            Control crossHair = crossHairScene.Instantiate<Control>();
            AddChild(crossHair);
        }
        else if (IsAiming /*&& !_isShooting*/)
        {
            IsAiming = false;
            _animationPlayer.Play("LaserShootViewReset");
            _shootAnimation = false;
        }
        
        if (Input.IsMouseButtonPressed(MouseButton.Left) && _shootCooldown > _shootCooldownValue && IsAiming && !_isShooting)
        {
            _isShooting = true;
            PackedScene laserScene = GD.Load<PackedScene>("res://Scenes/EntityScenes/Laser.tscn");
            Node3D laser = laserScene.Instantiate<Node3D>();
            
            double rotationY = _cameraH.Rotation.Y;
            
            laser.GlobalPosition = new Vector3(_cameraV.GlobalPosition.X + (float)Math.Sin(rotationY), Position.Y + 1, Position.Z + (float)Math.Cos(rotationY));
            laser.Rotation = new Vector3(_cameraV.Rotation.X + 0.15f, (float)rotationY, _cameraV.Rotation.X + 0.15f);
            
            GetTree().Root.AddChild(laser);
            
            _shootCooldown = 0;
        }
    }
}
