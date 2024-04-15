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
		HeathPlayer();
	}
	
	public override void _PhysicsProcess(double delta)
	{
		_uiTimer += 1;
		
		Pause();
		PhysicsReset();
		Gravity(delta);
		
		if (!IsDead)
		{
			if (Camera.Current && !GameManager._pausemode && !((ChatUI)GameManager._chat).IsOnChat())
			{
				Inventory();
				Animation();
				ShootArrow();
				Move(delta);
			}
			else
			{
				if (AnimationState != 0)
				{
					AnimationState = 0;
					AnimationSet(false, false, false, false, false, true);
					GameManager.InfoJoueur["animation"] = "idle";
				}
				_shootTimer = 0;
				Velocity = new Vector3(0, 0, 0);
			}
		}
	}

	protected override void Move(double delta)
	{
		if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[0].Item2) || Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[1].Item2) || Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[2].Item2) ||
			Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[3].Item2))
		{
			int left = Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[2].Item2));
			int right = Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[3].Item2));
			int forward = Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[0].Item2));
			int backward = Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[1].Item2));
					
			Direction = new Vector3(left - right, 0, forward - backward);
			Direction = Direction.Rotated(Vector3.Up, CameraH.Rotation.Y).Normalized();
			IsWalking = true;
			MovementSpeed = WalkSpeed;
		}
			
		//Calcul de la rotation du joueur
		PlayerMesh.Rotation = new Vector3(0, CameraH.Rotation.Y + (float) Math.PI, 0);
			
		HorizontalVelocity = HorizontalVelocity.Lerp(Direction.Normalized() * MovementSpeed, (float)(Acceleration * delta));
		
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
		
		if (!_isAiming && IsShooting && UseStamina(50))
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
		
		(int, int) direction = (Conversions.BtoI(left) - Conversions.BtoI(right), Conversions.BtoI(forward) - Conversions.BtoI(backward));
		
		if (Input.IsMouseButtonPressed(MouseButton.Left) && AnimationState != 6 && !_isAiming && !InteractionShop.OnShop && !GameHUD.OnInventory && UseStamina(50))
		{
			DirectionControl = (0,0);
			AnimationState = 6;
			AnimationSet(false, false, false, false, true, true);
			GameManager.InfoJoueur["animation"] = "hitbow";
		}
		else if (!_isAiming && IsShooting && AnimationState != 3 && !InteractionShop.OnShop && !GameHUD.OnInventory)
		{
			DirectionControl = (0,0);
			AnimationState = 3;
			AnimationSet(false, false, false, true, false, false);
			GameManager.InfoJoueur["animation"] = "shoot";
			IsShooting = false;
		}
		else if (_isAiming)
		{
			if ((left || right || forward || backward) && direction != DirectionControl)
			{
				AnimationState = 4;
				DirectionControl = direction;
				AnimationSet(false, true, false, false, false, false);
				
				if (direction.Item2 != 0)
				{
					GameManager.InfoJoueur["animation"] = "aimwalk";
				}
				else
				{
					GameManager.InfoJoueur["animation"] = "aimwalkside";
				}
				
				AnimationTree.Set("parameters/AimWalk/blend_position", new Vector2(direction.Item1, direction.Item2));
			}
			else if (AnimationState != 1)
			{
				DirectionControl = (0,0);
				AnimationState = 1;
				AnimationSet(false, false, true, false, false, false);
				GameManager.InfoJoueur["animation"] = "aim";
			}
		}
		else if ((left || right || forward || backward) && direction != DirectionControl)
		{
			AnimationState = 4;
			if (_isAiming)
			{
				DirectionControl = direction;
				AnimationSet(false, true, false, false, false, false);
				
				if (direction.Item2 != 0)
				{
					GameManager.InfoJoueur["animation"] = "aimwalk";
				}
				else
				{
					GameManager.InfoJoueur["animation"] = "aimwalkside";
				}
				
				AnimationTree.Set("parameters/AimWalk/blend_position", new Vector2(direction.Item1, direction.Item2));
			}
			else
			{
				DirectionControl = direction;

				if (direction.Item2 != 0)
				{
					GameManager.InfoJoueur["animation"] = "walk";
				}
				else
				{
					GameManager.InfoJoueur["animation"] = "walkside";
				}
				
				AnimationTree.Set("parameters/Walk/blend_position", new Vector2(direction.Item1, direction.Item2));
				AnimationSet(true, false, false, false, false, false);
			}
		}
		else if ((_isAiming || !IsShooting) && !_isAiming && (!(left || right || forward || backward) || AnimationState != 4) && AnimationState != 0)
		{
			DirectionControl = (0,0);
			AnimationState = 0;
			AnimationSet(false, false, false, false, false, true);
			GameManager.InfoJoueur["animation"] = "idle";
		}
	}
	
	private void AnimationSet(bool walk, bool aimwalk, bool aim, bool shoot, bool hit, bool idle, bool death = false)
	{
		AnimationTree.Set("parameters/conditions/WhenWalk", walk);
		AnimationTree.Set("parameters/conditions/WhenAimWalk", aimwalk);
		AnimationTree.Set("parameters/conditions/WhenAim", aim);
		AnimationTree.Set("parameters/conditions/WhenShoot", shoot);
		AnimationTree.Set("parameters/conditions/WhenHitBow", hit);
		AnimationTree.Set("parameters/conditions/Idle", idle);
		AnimationTree.Set("parameters/conditions/Death", death);
	}
	
	public override void TakeDamage(int damage)
	{
		Heath -= damage;
		if (Heath <= 0 && !IsDead)
		{
			IsDead = true;
			AnimationState = -1;
			AnimationSet(false, false, false, false, false, false, true);
			GameManager.InfoJoueur["animation"] = "death";
			GetNode<Timer>("DeathTimer").Start();
		}
	}
	
	private void _on_stamina_timeout()
	{
		if (Stamina + 5 <= MaxStamina)
		{
			Stamina += 5;
		}
	}
	private void _on_death_timer_timeout()
	{
		Position -= new Vector3(0,10,0);
	}
}
