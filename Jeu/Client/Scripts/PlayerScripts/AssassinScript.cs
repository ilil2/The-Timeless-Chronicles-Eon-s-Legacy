using Godot;
using System;
using JeuClient.Scripts.PlayerScripts;
using Lib;

public partial class AssassinScript : ClassScript
{
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
    
    protected void Dash()
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

		if (Input.IsMouseButtonPressed(MouseButton.Left) && AnimationState != 2)
		{
			AnimationState = 2;
			
			AnimationSet(false, false, true, true);
			
			GameManager.InfoJoueur["animation"] = "hit";
		}
		else if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[4].Item2) && forward && AnimationState != 3)
		{
			AnimationState = 3;
			AnimationSet(false, true, false, false);
			GameManager.InfoJoueur["animation"] = "sprint";
		}
		else if (!Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[4].Item2) && (left || right || forward || backward) && AnimationState != 1)
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
		else if (!Input.IsMouseButtonPressed(MouseButton.Left) && !(left || right || forward || backward) && AnimationState != 0)
		{
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
	
	public override void TakeDamage(float damage)
	{
		Heath -= damage;
		if (Heath <= 0)
		{
			IsDead = true;
			AnimationState = -1;
			AnimationSet(false, false, false, false, true);
			GameManager.InfoJoueur["animation"] = "death";
		}
	}
}
