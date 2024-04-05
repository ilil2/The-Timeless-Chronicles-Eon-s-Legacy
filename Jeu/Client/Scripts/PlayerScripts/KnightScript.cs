using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;
using Lib;

public partial class KnightScript : ClassScript
{

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
	}

	public override void _PhysicsProcess(double delta)
	{
		Pause();
		PhysicsReset();
		Gravity(delta);
		if (!IsDead)
		{
			if (Camera.Current && !GameManager._pausemode && !((ChatUI)GameManager._chat).IsOnChat())
			{
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

	private void Animation()
	{
		bool left = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[2].Item2);
		bool right = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[3].Item2);
		bool forward = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[0].Item2);
		bool backward = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[1].Item2);
		
		if (Input.IsMouseButtonPressed(MouseButton.Left) && AnimationState != 3 && UseStamina(50))
		{
			AnimationState = 3;
			
			AnimationSet(false, false, true, true);
			
			GameManager.InfoJoueur["animation"] = "hit";
		}
		else if (Input.IsMouseButtonPressed(MouseButton.Right) && AnimationState != 2 && AnimationState != 1)
		{
			AnimationState = 2;
			AnimationSet(false, true, false, false);
			GameManager.InfoJoueur["animation"] = "protection";
		}
		else if ((left || right || forward || backward) && AnimationState != 1 && AnimationState != 2)
		{
			AnimationState = 1;
			AnimationTree.Set("parameters/Walk/blend_position", new Vector2(Conversions.BtoI(left) - Conversions.BtoI(right), Conversions.BtoI(forward) - Conversions.BtoI(backward)));
			AnimationSet(true, false, false, false);

			if (Conversions.BtoI(left) - Conversions.BtoI(right) != 0)
			{
				GameManager.InfoJoueur["animation"] = "walkside";
			}
			else
			{
				GameManager.InfoJoueur["animation"] = "walk";
			}
		}
		else if (!Input.IsMouseButtonPressed(MouseButton.Right) && !Input.IsMouseButtonPressed(MouseButton.Left) && !(left || right || forward || backward) && AnimationState != 0)
		{
			AnimationState = 0;
			AnimationSet(false, false, false, true);
			GameManager.InfoJoueur["animation"] = "idle";
		}
	}
	
	private void AnimationSet(bool walk, bool block, bool hit, bool idle, bool death = false)
	{
		AnimationTree.Set("parameters/conditions/WhenWalk", walk);
		AnimationTree.Set("parameters/conditions/WhenBlock", block);
		AnimationTree.Set("parameters/conditions/WhenHit", hit);
		AnimationTree.Set("parameters/conditions/Idle", idle);
		AnimationTree.Set("parameters/conditions/Death", death);
		
	}
	
	
	public override void TakeDamage(int damage)
	{
		Heath -= damage;
		if (Heath <= 0)
		{
			IsDead = true;
			AnimationState = -1;
			AnimationSet(false, false, false, false, true);
			GameManager.InfoJoueur["animation"] = "death";
			GetNode<Timer>("Timer").Start();
		}
		Stamina = 1000;
	}
	
	private void _on_timer_timeout()
	{
		Position -= new Vector3(0,10,0);
	}
}

