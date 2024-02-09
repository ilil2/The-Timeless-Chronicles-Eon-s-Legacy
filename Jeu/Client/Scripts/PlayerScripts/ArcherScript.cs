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
		InitPlayer();

		AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
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
		}
		else
		{
			_shootTimer = 0;
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

	private void ShootArrow()
	{
		_shootTimer += 1;
		
		if (Input.IsMouseButtonPressed(MouseButton.Right) && _shootTimer > 30)
		{
			if (!_shootAnimation)
			{
				_shootAnimation = true;
				AnimationPlayer.Play("ArrowShootView");
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
			
			IsShooting = false;
			_shootPower = 1;

			AnimationPlayer.Play("ArrowShootViewReset");
			_shootAnimation = false;
		}
	}
}
