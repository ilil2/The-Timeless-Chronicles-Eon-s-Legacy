using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;
using Lib;

public partial class KnightScript : ClassScript
{

	public override void _Ready()
	{
		//Initialisation du joueur
		InitPlayer();
		
		WalkSpeed = 6f;
		RunSpeed = 6.8f;
		DashPower = 70.0f;
		
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
		Animation();
	}

	public override void _PhysicsProcess(double delta)
	{
		Pause();
		PhysicsReset();
		Gravity(delta);
		Move(delta);
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

	private bool Attack()
	{
		if (Input.IsMouseButtonPressed(MouseButton.Left))
		{
			GameManager.InfoJoueur["attack"] = "hit";
			return true;
		}
		if (Input.IsMouseButtonPressed(MouseButton.Right))
		{
			GameManager.InfoJoueur["attack"] = "protection";
			return true;
		}

		return false;
	}

	private void Animation()
	{
		bool left = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[2].Item2);
		bool right = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[3].Item2);
		bool forward = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[0].Item2);
		bool backward = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[1].Item2);
		
		if (Input.IsMouseButtonPressed(MouseButton.Left))
		{
			AnimationTree.Set("parameters/conditions/WhenWalk", false);
			AnimationTree.Set("parameters/conditions/WhenBlock", false);
			AnimationTree.Set("parameters/conditions/WhenHit", true);
			AnimationTree.Set("parameters/conditions/Idle", false);
		}
		else if (Input.IsMouseButtonPressed(MouseButton.Right))
		{
			AnimationTree.Set("parameters/conditions/WhenWalk", false);
			AnimationTree.Set("parameters/conditions/WhenBlock", true);
			AnimationTree.Set("parameters/conditions/WhenHit", false);
			AnimationTree.Set("parameters/conditions/Idle", false);
		}
		else if (left || right || forward || backward)
		{
			AnimationTree.Set("parameters/conditions/WhenWalk", true);
			AnimationTree.Set("parameters/conditions/WhenBlock", false);
			AnimationTree.Set("parameters/conditions/WhenHit", false);
			AnimationTree.Set("parameters/conditions/Idle", false);

			if (Conversions.BtoI(left) - Conversions.BtoI(right) != 0)
			{
				GameManager.InfoJoueur["attack"] = "walkside";
			}
			else
			{
				GameManager.InfoJoueur["attack"] = "walk";
			}
		}
		else
		{
			AnimationTree.Set("parameters/conditions/WhenWalk", false);
			AnimationTree.Set("parameters/conditions/WhenBlock", false);
			AnimationTree.Set("parameters/conditions/WhenHit", false);
			AnimationTree.Set("parameters/conditions/Idle", true);
			
			GameManager.InfoJoueur["attack"] = "init";
		}
		
		AnimationTree.Set("parameters/Walk/blend_position", new Vector2(Conversions.BtoI(left) - Conversions.BtoI(right), Conversions.BtoI(forward) - Conversions.BtoI(backward)));
	}
}
