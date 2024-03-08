using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;
using Lib;

public partial class ArcherScript : ClassScript
{
	//Varibale du tir
	private int _shootTimer;
	private float _shootPower = 0.25f;
	private bool _isAiming;
	private bool _shootAnimation;
	
	public static bool IsShooting;
	
	public override void _Ready()
	{
		//Soutenance
		WalkSpeed = 6f;
		RunSpeed = 6.8f;
		DashPower = 70.0f;
		
		InitPlayer();

		AnimationTree = GetNode<AnimationTree>("AnimationTree");
		AnimationTree.Active = true;
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
		Move(delta);

		if (Camera.Current && !GameManager._pausemode && !((ChatUI)GameManager._chat).IsOnChat())
		{
			ShootArrow();
			Animation();
		}
		else
		{
			_shootTimer = 0;
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
			    
				//Changement de la vitesse du joueur si il sprint
				if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[4].Item2) && IsWalking)
				{ 
					MovementSpeed = RunSpeed;
					IsRunning = true;
				}
				else
				{
					MovementSpeed = WalkSpeed;
					IsRunning = false;
				}
			}
			else
			{
				IsRunning = false;
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

	private void ShootArrow()
	{
		_shootTimer += 1;

		if (CameraV.SpringLength > -4 && !_isAiming)
		{
			CameraV.SpringLength -= 0.1f;
		}
		
		if (Input.IsMouseButtonPressed(MouseButton.Right) && _shootTimer > 30)
		{
			if (CameraV.SpringLength <= 0)
			{
				CameraV.SpringLength += 0.1f;
			}
			
			IsShooting = true;
			_isAiming = true;
			
			if (_shootPower <= 3)
			{
				_shootPower += 0.015f;
			}
			
			PackedScene crossHairScene = GD.Load<PackedScene>("res://Scenes/HUD/ViewFinder.tscn");
			Control crossHair = crossHairScene.Instantiate<Control>();
			AddChild(crossHair);
		}
		else if (IsShooting)
		{
			_isAiming = false;
			_shootTimer = 0;
		}
		
		if (!_isAiming && IsShooting)
		{
			PackedScene arrowScene = GD.Load<PackedScene>("res://Scenes/EntityScenes/Arrow.tscn");
			RigidBody3D arrow = arrowScene.Instantiate<RigidBody3D>();

			double rotationY = CameraH.Rotation.Y;
			Vector3 arrowPosition = new Vector3(CameraV.GlobalPosition.X + (float)Math.Sin(rotationY)*2, Position.Y + 1, Position.Z + (float)Math.Cos(rotationY)*2);
			
			arrow.Position = new Vector3((arrowPosition.X + GlobalPosition.X) / 2, arrowPosition.Y, (arrowPosition.Z + GlobalPosition.Z) / 2);
			arrow.Rotation = new Vector3(arrow.Rotation.X, (float)(rotationY + Math.PI / 2f), CameraV.Rotation.X);
			arrow.LinearVelocity = new Vector3((float)(Math.Sin(rotationY)*10), -Mathf.RadToDeg(CameraV.Rotation.X) / 5, (float)(Math.Cos(rotationY)*10)) * _shootPower;
			
			GameManager.InfoJoueur["attack"] = $"{arrow.Position.X};{arrow.Position.Y};{arrow.Position.Z};{arrow.Rotation.X};{arrow.Rotation.Y};{arrow.Rotation.Z};{arrow.LinearVelocity.X};{arrow.LinearVelocity.Y};{arrow.LinearVelocity.Z}";
			GetTree().Root.AddChild(arrow);
			
			_shootPower = 1;
		}
	}
	
	private void Animation()
	{
		bool left = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[2].Item2);
		bool right = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[3].Item2);
		bool forward = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[0].Item2);
		bool backward = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[1].Item2);

		if (Input.IsMouseButtonPressed(MouseButton.Left) && AnimationState != 6 && !_isAiming)
		{
			AnimationState = 6;
			
			AnimationTree.Set("parameters/conditions/WhenWalk", false);
			AnimationTree.Set("parameters/conditions/WhenAimWalk", false);
			AnimationTree.Set("parameters/conditions/WhenAim", false);
			AnimationTree.Set("parameters/conditions/WhenShoot", false);
			AnimationTree.Set("parameters/conditions/WhenHitBow", true);
			AnimationTree.Set("parameters/conditions/Idle", true);
			
			GameManager.InfoJoueur["animation"] = "hitbow";
		}
		else if (!_isAiming && IsShooting && AnimationState != 3)
		{
			AnimationState = 3;
			
			AnimationTree.Set("parameters/conditions/WhenWalk", false);
			AnimationTree.Set("parameters/conditions/WhenAimWalk", false);
			AnimationTree.Set("parameters/conditions/WhenAim", false);
			AnimationTree.Set("parameters/conditions/WhenShoot", true);
			AnimationTree.Set("parameters/conditions/WhenHitBow", false);
			AnimationTree.Set("parameters/conditions/Idle", false);
			
			GameManager.InfoJoueur["animation"] = "shoot";
			IsShooting = false;
		}
		else if (_isAiming)
		{
			if ((left || right || forward || backward) && AnimationState != 2)
			{
				AnimationState = 2;
				
				AnimationTree.Set("parameters/conditions/WhenWalk", false);
				AnimationTree.Set("parameters/conditions/WhenAimWalk", true);
				AnimationTree.Set("parameters/conditions/WhenAim", false);
				AnimationTree.Set("parameters/conditions/WhenShoot", false);
				AnimationTree.Set("parameters/conditions/WhenHitBow", false);
				AnimationTree.Set("parameters/conditions/Idle", false);
				
				if (Conversions.BtoI(left) - Conversions.BtoI(right) != 0)
				{
					GameManager.InfoJoueur["animation"] = "aimwalkside";
				}
				else
				{
					GameManager.InfoJoueur["animation"] = "aimwalk";
				}
				
				AnimationTree.Set("parameters/AimWalk/blend_position", new Vector2(Conversions.BtoI(left) - Conversions.BtoI(right), Conversions.BtoI(forward) - Conversions.BtoI(backward)));
			}
			else if (AnimationState != 1)
			{
				AnimationState = 1;
				
				AnimationTree.Set("parameters/conditions/WhenWalk", false);
				AnimationTree.Set("parameters/conditions/WhenAimWalk", false);
				AnimationTree.Set("parameters/conditions/WhenAim", true);
				AnimationTree.Set("parameters/conditions/WhenShoot", false);
				AnimationTree.Set("parameters/conditions/WhenHitBow", false);
				AnimationTree.Set("parameters/conditions/Idle", false);
				
				GameManager.InfoJoueur["animation"] = "aim";
			}
		}
		else if (left || right || forward || backward)
		{
			if (_isAiming && AnimationState != 4)
			{
				AnimationState = 4;
				
				AnimationTree.Set("parameters/conditions/WhenWalk", false);
				AnimationTree.Set("parameters/conditions/WhenAimWalk", true);
				AnimationTree.Set("parameters/conditions/WhenAim", false);
				AnimationTree.Set("parameters/conditions/WhenShoot", false);
				AnimationTree.Set("parameters/conditions/WhenHitBow", false);
				AnimationTree.Set("parameters/conditions/Idle", false);
				
				if (Conversions.BtoI(left) - Conversions.BtoI(right) != 0)
				{
					GameManager.InfoJoueur["animation"] = "aimwalkside";
				}
				else
				{
					GameManager.InfoJoueur["animation"] = "aimwalk";
				}
				
				AnimationTree.Set("parameters/AimWalk/blend_position", new Vector2(Conversions.BtoI(left) - Conversions.BtoI(right), Conversions.BtoI(forward) - Conversions.BtoI(backward)));
			}
			else if (AnimationState != 5)
			{
				AnimationState = 5;
				
				AnimationTree.Set("parameters/conditions/WhenWalk", true);
				AnimationTree.Set("parameters/conditions/WhenAimWalk", false);
				AnimationTree.Set("parameters/conditions/WhenAim", false);
				AnimationTree.Set("parameters/conditions/WhenShoot", false);
				AnimationTree.Set("parameters/conditions/WhenHitBow", false);
				AnimationTree.Set("parameters/conditions/Idle", false);

				if (Conversions.BtoI(left) - Conversions.BtoI(right) != 0)
				{
					GameManager.InfoJoueur["animation"] = "walkside";
				}
				else
				{
					GameManager.InfoJoueur["animation"] = "walk";
				}
				
				AnimationTree.Set("parameters/Walk/blend_position", new Vector2(Conversions.BtoI(left) - Conversions.BtoI(right), Conversions.BtoI(forward) - Conversions.BtoI(backward)));
			}
		}
		else if ((_isAiming || !IsShooting) && !_isAiming && AnimationState != 0)
		{
			AnimationState = 0;
			
			AnimationTree.Set("parameters/conditions/WhenWalk", false);
			AnimationTree.Set("parameters/conditions/WhenAimWalk", false);
			AnimationTree.Set("parameters/conditions/WhenAim", false);
			AnimationTree.Set("parameters/conditions/WhenShoot", false);
			AnimationTree.Set("parameters/conditions/WhenHitBow", false);
			AnimationTree.Set("parameters/conditions/Idle", true);
			
			GameManager.InfoJoueur["animation"] = "idle";
		}
	}
}
