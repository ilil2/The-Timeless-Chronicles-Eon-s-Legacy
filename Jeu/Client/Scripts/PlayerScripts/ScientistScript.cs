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
        
        AnimationTree = GetNode<AnimationTree>("AnimationTree");
        AnimationTree.Active = true;

        _shootCooldown = _shootCooldownValue - 50;
        
        //Soutenance
        WalkSpeed = 6f;
        RunSpeed = 6.8f;
        DashPower = 70.0f;
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
	        Animation();
        }
        else
        {
            if (_shootCooldown >= _shootCooldownValue)
            {
                _shootCooldown = _shootCooldownValue - 20;
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
        
        if (CameraV.SpringLength > -4 && !IsAiming)
        {
	        CameraV.SpringLength -= 0.1f;
        }
        
        if (Input.IsMouseButtonPressed(MouseButton.Right))
        {
	        if (CameraV.SpringLength <= 0)
	        {
		        CameraV.SpringLength += 0.1f;
	        }
	        
            IsAiming = true;
			
            PackedScene crossHairScene = GD.Load<PackedScene>("res://Scenes/HUD/ViewFinder.tscn");
            Control crossHair = crossHairScene.Instantiate<Control>();
            AddChild(crossHair);
        }
        else if (IsAiming && !_isShooting)
        {
            IsAiming = false;
        }
        
        if (Input.IsMouseButtonPressed(MouseButton.Left) && _shootCooldown > _shootCooldownValue && IsAiming && !_isShooting)
        {
            PackedScene laserScene = GD.Load<PackedScene>("res://Scenes/EntityScenes/Laser.tscn");
            Node3D laser = laserScene.Instantiate<Node3D>();
            
            double rotationY = CameraH.Rotation.Y;
            Vector3 laserPosition = new Vector3(CameraV.GlobalPosition.X + (float)Math.Sin(rotationY), Position.Y + 1.2f, Position.Z + (float)Math.Cos(rotationY));
            laser.GlobalPosition = new Vector3((laserPosition.X + GlobalPosition.X) / 2, laserPosition.Y, (laserPosition.Z + GlobalPosition.Z) / 2);
            laser.Rotation = new Vector3(CameraV.Rotation.X + 0.15f, (float)rotationY, CameraV.Rotation.X + 0.15f);
            UDP.OneShot($"{laser.Position.X};{laser.Position.Y};{laser.Position.Z};{laser.Rotation.X};{laser.Rotation.Y};{laser.Rotation.Z}");
            GetTree().Root.AddChild(laser);
            
            GameManager.LockCamera = true;
        }
        
        if (!Input.IsMouseButtonPressed(MouseButton.Left) && _isShooting)
        {
            GameManager.InfoJoueur["info"] = "stop";
            GameManager.InfoJoueur["animation"] = "stop";
            _isShooting = false;
            GameManager.LockCamera = false;
        }
    }
    
    private void Animation()
	{
		bool left = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[2].Item2);
		bool right = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[3].Item2);
		bool forward = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[0].Item2);
		bool backward = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[1].Item2);
		
		if (Input.IsMouseButtonPressed(MouseButton.Left) && _shootCooldown > _shootCooldownValue && IsAiming && !_isShooting && AnimationState != 2)
		{
			AnimationState = 2;
			
			AnimationTree.Set("parameters/conditions/WhenWalk", false);
			AnimationTree.Set("parameters/conditions/WhenShoot", true);
			AnimationTree.Set("parameters/conditions/Idle", false);
			
			GameManager.InfoJoueur["animation"] = "shoot";
			
			_shootCooldown = 0;
			_isShooting = true;
		}
		else if ((left || right || forward || backward) && AnimationState != 1)
		{
			AnimationState = 1;
			
			AnimationTree.Set("parameters/Walk/blend_position", new Vector2(Conversions.BtoI(left) - Conversions.BtoI(right), Conversions.BtoI(forward) - Conversions.BtoI(backward)));
			
			AnimationTree.Set("parameters/conditions/WhenWalk", true);
			AnimationTree.Set("parameters/conditions/WhenShoot", false);
			AnimationTree.Set("parameters/conditions/Idle", false);

			if (Conversions.BtoI(left) - Conversions.BtoI(right) != 0)
			{
				GameManager.InfoJoueur["animation"] = "walkside";
			}
			else
			{
				GameManager.InfoJoueur["animation"] = "walk";
			}
		}
		else if (!(Input.IsMouseButtonPressed(MouseButton.Left) && IsAiming) && !(left || right || forward || backward) && AnimationState != 0)
		{
			AnimationState = 0;
			
			AnimationTree.Set("parameters/conditions/WhenWalk", false);
			AnimationTree.Set("parameters/conditions/WhenShoot", false);
			AnimationTree.Set("parameters/conditions/Idle", true);
			
			GameManager.InfoJoueur["animation"] = "idle";
		}
	}
}
