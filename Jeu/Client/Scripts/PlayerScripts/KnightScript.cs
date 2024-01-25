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
        
        WalkSpeed = 3.6f;
        RunSpeed = 6.8f;
        DashPower = 70.0f;
        
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

        if (Camera.Current && !GameManager._pausemode && !((ChatUI)GameManager._chat).IsOnChat())
        {
            Move(delta);
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
        if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[0].Item2) || Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[1].Item2) || Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[2].Item2) ||
            Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[3].Item2))
        {
            Direction = new Vector3(Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[2].Item2)) - Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[3].Item2)), 0,
                Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[0].Item2)) - Conversions.BtoI(Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[1].Item2)));
            Direction = Direction.Rotated(Vector3.Up, CameraH.Rotation.Y).Normalized();
            IsWalking = true;
            MovementSpeed = WalkSpeed;
            if (AnimationPlayer.CurrentAnimation != "Walk")
            {
                AnimationPlayer.Play("Walk");
            }
        }
        else
        {
            IsWalking = false;
            AnimationPlayer.Play("Init");
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
}
