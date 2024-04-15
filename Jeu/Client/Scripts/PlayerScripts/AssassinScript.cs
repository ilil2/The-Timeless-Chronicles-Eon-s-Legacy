using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;
using Lib;

public partial class AssassinScript : ClassScript
{
	protected float DashPower = 80.0f;
	protected int DashCount = 3;
	protected bool CanDash = true;
	protected int DashTimer;
	
	public override void _Ready()
	{
		InitPlayer();
		
		AnimationTree = GetNode<AnimationTree>("AnimationTree");
		AnimationTree.Active = true;
		
		WalkSpeed = 5f;
		RunSpeed = 8f;
		DashPower = 120.0f;
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
				Move(delta);
			}
			else
			{
				if (AnimationState != 0)
				{
					AnimationState = 0;
					AnimationSet(false, false, false, true);
					GameManager.InfoJoueur["animation"] = "idle";
				}
				Velocity = new Vector3(0, 0, 0);
			}
		}
	}
	
	protected void Dash()
	{
		if (CanDash && DashCount > 0)
		{
			if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[5].Item2) && UseStamina(300))
			{
				if (!IsWalking)
				{
					Direction = new Vector3(0, 0, 1);
					Direction = Direction.Rotated(Vector3.Up, CameraH.Rotation.Y).Normalized();
				}
				
				HorizontalVelocity = Direction * DashPower;
				DashCount -= 1;
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
		if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[0].Item2) || Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[1].Item2) || Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[2].Item2) ||
			Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[3].Item2))
		{
			Direction = new Vector3(Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[2].Item2)) - Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[3].Item2)), 0,
				Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[0].Item2)) - Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[1].Item2)));
			Direction = Direction.Rotated(Vector3.Up, CameraH.Rotation.Y).Normalized();
			IsWalking = true;
			
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
		
		PlayerMesh.Rotation = new Vector3(0, CameraH.Rotation.Y + (float) Math.PI, 0);
			
		HorizontalVelocity = HorizontalVelocity.Lerp(Direction.Normalized() * MovementSpeed, (float)(Acceleration * delta));
			
		Dash();
		
		Vector3 velocity = Velocity;
		velocity.Z = HorizontalVelocity.Z + VerticalVelocity.Z;
		velocity.X = HorizontalVelocity.X + VerticalVelocity.X;
		velocity.Y = VerticalVelocity.Y;
		
		Velocity = velocity;
		MoveAndSlide();
	}

	private void Animation()
	{
		bool left = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[2].Item2);
		bool right = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[3].Item2);
		bool forward = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[0].Item2);
		bool backward = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[1].Item2);

		(int, int) direction = (Conversions.BtoI(left) - Conversions.BtoI(right), Conversions.BtoI(forward) - Conversions.BtoI(backward));

		if (Input.IsMouseButtonPressed(MouseButton.Left) && AnimationState != 2 && !InteractionShop.OnShop && !GameHUD.OnInventory)
		{
			DirectionControl = (0,0);
			AnimationState = 2;
			AnimationSet(false, false, true, true);
			GameManager.InfoJoueur["animation"] = "hit";
			
			if (Stamina + 100 <= MaxStamina)
			{
				Stamina += 100;
			}
		}
		else if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[4].Item2) && forward && AnimationState != 3)
		{
			DirectionControl = (0,0);
			AnimationState = 3;
			AnimationSet(false, true, false, false);
			GameManager.InfoJoueur["animation"] = "sprint";
		}
		else if (!Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[4].Item2) && (left || right || forward || backward) && direction != DirectionControl)
		{
			AnimationState = 1;
			DirectionControl = direction;
			AnimationTree.Set("parameters/Walk/blend_position", new Vector2(direction.Item1, direction.Item2));
			
			AnimationSet(true, false, false, false);

			if (direction.Item2 != 0)
			{
				GameManager.InfoJoueur["animation"] = "walk";
			}
			else
			{
				GameManager.InfoJoueur["animation"] = "walkside";
			}
		}
		else if (!Input.IsMouseButtonPressed(MouseButton.Left) && (!(left || right || forward || backward) || AnimationState != 1) && (!(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[4].Item2) && forward) || AnimationState != 3) && AnimationState != 0)
		{
			DirectionControl = (0,0);
			AnimationState = 0;
			AnimationSet(false, false, false, true);
			GameManager.InfoJoueur["animation"] = "idle";
		}
	}
	
	private void AnimationSet(bool walk, bool sprint, bool hit, bool idle, bool death = false)
	{
		AnimationTree.Set("parameters/conditions/WhenWalk", walk);
		AnimationTree.Set("parameters/conditions/WhenSprint", sprint);
		AnimationTree.Set("parameters/conditions/WhenHit", hit);
		AnimationTree.Set("parameters/conditions/Idle", idle);
		AnimationTree.Set("parameters/conditions/Death", death);
	}
	
	public override void TakeDamage(int damage)
	{
		Health -= damage;
		if (Health <= 0 && !IsDead)
		{
			IsDead = true;
			AnimationState = -1;
			AnimationSet(false, false, false, false, true);
			GameManager.InfoJoueur["animation"] = "death";
			GetNode<Timer>("DeathTimer").Start();
		}
	}
	
	private void _on_dash_timeout()
	{
		if (DashCount < 3)
		{
			DashCount += 1;
		}
	}

	private void _on_sprint_timeout()
	{
		if (IsRunning)
		{
			if (!UseStamina(15))
			{
				TakeDamage(1);
			}
		}
	}
	
	private void _on_death_timer_timeout()
	{
		Position -= new Vector3(0,10,0);
	}
}
