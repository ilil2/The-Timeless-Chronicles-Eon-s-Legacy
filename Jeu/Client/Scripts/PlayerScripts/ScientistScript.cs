using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;
using Lib;

public partial class ScientistScript : ClassScript
{
    //Varibale du tir
    private int _shootCooldown;
    private bool _isShooting;
    private bool _shootAnimation;
    
    private int _shootCooldownValue = 100;
    
    
    public static bool IsAiming;
    
    public override void _Ready()
    {
        InitPlayer();
        
        AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        _shootCooldown = _shootCooldownValue - 50;
    }

    public override void _Input(InputEvent @event)
    {
        if (Camera.Current && !GameManager._pausemode)
        {
            Zoom(@event);
        }
    }

    public override void _Process(double delta)
    {
        SendPosition();
    }

    public override void _PhysicsProcess(double delta)
    {
        Pause();
        PhysicsReset();
        Gravity(delta);
        if (!_isShooting)
        {
            Move(delta);
        }

        if (Camera.Current && !GameManager._pausemode && !((ChatUI)GameManager._chat).IsOnChat())
        {
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
    
    protected override void Dash()
    {
        if (CanDash)
        {
            if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[5].Item2))
            {
                if (!IsWalking)
                {
                    Direction = new Vector3(0, 0, 1);
                    Direction = Direction.Rotated(Vector3.Up, CameraH.Rotation.Y).Normalized();
                }
			    
                HorizontalVelocity = Direction * DashPower;
                CanDash = false;
            }
        }
        else
        {
            DashTimer += 1;
            if (DashTimer % 20 == 0)
            {
                CanDash = true;
                DashTimer = 0;
            }
        }
    }
    
    protected override void Move(double delta)
    {
        if (Camera.Current && !GameManager._pausemode && !((ChatUI)GameManager._chat).IsOnChat())
        {
            if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[0].Item2) || Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[1].Item2) || Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[2].Item2) ||
                Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[3].Item2))
            {
                Direction = new Vector3(Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[2].Item2)) - Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[3].Item2)), 0,
                    Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[0].Item2)) - Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[1].Item2)));
                Direction = Direction.Rotated(Vector3.Up, CameraH.Rotation.Y).Normalized();
                IsWalking = true;
                MovementSpeed = WalkSpeed;
            }
            else
            {
                IsWalking = false;
            }
	        
            //Calcul de la rotation du joueur
            PlayerMesh.Rotation = new Vector3(0, CameraH.Rotation.Y + (float) Math.PI, 0);
		    
            HorizontalVelocity = HorizontalVelocity.Lerp(Direction.Normalized() * MovementSpeed, (float)(Acceleration * delta));
	        
            Dash();
        }
	    
        //Calcul du movement du joueur
        Vector3 velocity = Velocity;
        velocity.Z = HorizontalVelocity.Z + VerticalVelocity.Z;
        velocity.X = HorizontalVelocity.X + VerticalVelocity.X;
        velocity.Y = VerticalVelocity.Y;
		
        //Application du mouvement au joueur
        Velocity = velocity;
        MoveAndSlide();
    }
    
    private void ShootLaser()
    {
        _shootCooldown += 1;
        
        if (Input.IsMouseButtonPressed(MouseButton.Right))
        {
            if (!_shootAnimation)
            {
                _shootAnimation = true;
                AnimationPlayer.Play("LaserShootView");
            }
            IsAiming = true;
			
            PackedScene crossHairScene = GD.Load<PackedScene>("res://Scenes/HUD/ViewFinder.tscn");
            Control crossHair = crossHairScene.Instantiate<Control>();
            AddChild(crossHair);
        }
        else if (IsAiming && !_isShooting)
        {
            IsAiming = false;
            AnimationPlayer.Play("LaserShootViewReset");
            _shootAnimation = false;
            
        }
        
        if (Input.IsMouseButtonPressed(MouseButton.Left) && _shootCooldown > _shootCooldownValue && IsAiming && !_isShooting)
        {
            _isShooting = true;
            PackedScene laserScene = GD.Load<PackedScene>("res://Scenes/EntityScenes/Laser.tscn");
            Node3D laser = laserScene.Instantiate<Node3D>();
            
            double rotationY = CameraH.Rotation.Y;
            Vector3 laserPosition = new Vector3(CameraV.GlobalPosition.X + (float)Math.Sin(rotationY), Position.Y + 1.2f, Position.Z + (float)Math.Cos(rotationY));
            laser.GlobalPosition = new Vector3((laserPosition.X + GlobalPosition.X) / 2, laserPosition.Y, (laserPosition.Z + GlobalPosition.Z) / 2);
            laser.Rotation = new Vector3(CameraV.Rotation.X + 0.15f, (float)rotationY, CameraV.Rotation.X + 0.15f);
            GameManager.InfoJoueur["attack"] = $"{laser.Position.X};{laser.Position.Y};{laser.Position.Z};{laser.Rotation.X};{laser.Rotation.Y};{laser.Rotation.Z}";
            GetTree().Root.AddChild(laser);
            
            _shootCooldown = 0;
            GameManager.LockCamera = true;
        }
        
        if (!Input.IsMouseButtonPressed(MouseButton.Left) && _isShooting)
        {
            GameManager.InfoJoueur["attack"] = "stop";
            _isShooting = false;
            GameManager.LockCamera = false;
        }
    }
}
