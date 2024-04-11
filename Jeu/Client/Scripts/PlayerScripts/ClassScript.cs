using System;
using Godot;
using Lib;

namespace JeuClient.Scripts.PlayerScripts;

public abstract partial class ClassScript : PlayerScript
{
	//Variable de base du joueur
	protected int Id;
	public string Pseudo;
	public string Classe;
	
	protected int MaxHealth = 100;
	protected int Heath = 100;
	protected int MaxStamina = 1000;
	protected int Stamina = 1000;
	public int Gold = 5000;
	
	//Variable des objets
	public bool PlayerIsHere = false;
	protected Node3D CameraH;
	protected SpringArm3D CameraV;
	public Camera3D Camera;
	protected Node3D CameraPlayer;
	protected Node3D PlayerMesh;
	protected AnimationPlayer AnimationPlayer;
	protected AnimationTree AnimationTree;
	protected int AnimationState = -1;
	
	//Variable de camera
	private float _fovMax = 80;
	private float _fovMin = 40;
	
	//Variable de mouvement
	protected float GravityValue = 9.8f;
	protected float WalkSpeed = 4.2f;
	protected float RunSpeed = 7.5f;
	
	protected (int, int) DirectionControl = (0, 0);

	protected bool IsWalking;
	protected bool IsRunning;
	
	protected Vector3 Direction;
	protected Vector3 HorizontalVelocity;
	protected Vector3 Movement;
	protected Vector3 VerticalVelocity;
	protected float MovementSpeed;
	protected float AngularAcceleration = 10;
	protected int Acceleration = 15;
	
	protected int _uiTimer;
	
	public int GetId()
	{
		return Id;
	}
	
	public SpringArm3D GetCameraVect()
	{
		return CameraV;
	}
	
	public Node3D GetCameraH()
	{
		return CameraH;
	}
	
	public Node3D GetCamera()
	{
		return CameraPlayer;
	}
	
	protected void InitPlayer()
	{
		Id = Conversions.AtoI(GameManager.InfoJoueur["id"]);
		Pseudo = GameManager.InfoJoueur["pseudo"];
		Classe = GameManager.InfoJoueur["class"];
		
		CameraPlayer = GetNode<Node3D>("CameraPlayer");
		Camera = GetNode<Camera3D>("CameraPlayer/h/v/Camera3D");
		CameraV = GetNode<SpringArm3D>("CameraPlayer/h/v");
		CameraH = GetNode<Node3D>("CameraPlayer/h");
		Direction = Vector3.Back.Rotated(Vector3.Up, CameraH.GlobalTransform.Basis.GetEuler().Y);
		
		PlayerMesh = GetNode<Node3D>("Player");
	}

	protected void Zoom(InputEvent @event)
	{
		if (@event.AsText() == "Mouse Wheel Down")
		{
			if (Camera.Fov <= _fovMax)
			{
				Camera.Fov  += 2;
			}
		}
		else if (@event.AsText() == "Mouse Wheel Up")
		{
			if (Camera.Fov >= _fovMin)
			{
				Camera.Fov -= 2;
			}
		}
	}
	
	protected void SendPosition()
	{
		GameManager.InfoJoueur["co"] = $"{Position.X};{Position.Y};{Position.Z}";
		GameManager.InfoJoueur["orientation"] = $"{PlayerMesh.Rotation.X};{PlayerMesh.Rotation.Y};{PlayerMesh.Rotation.Z}";
		GameManager.InfoJoueur["hp"] = $"{Heath}";
		GameManager.InfoJoueur["mp"] = $"{Stamina}";
		PlayerIsHere = true;

	}

	protected void Pause()
	{
		if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[16].Item2) && !GameManager._pausemode && _uiTimer > 20)
		{
			GameManager.InfoJoueur["animation"] = "idle";
			CameraV.SpringLength = -4;
			_uiTimer = 0;
			GameManager._pausemode = true;
			Input.MouseMode = Input.MouseModeEnum.Visible;
			PackedScene pauseUI = GD.Load<PackedScene>("res://Scenes/UI/PauseMenuManager.tscn");
			Control pauseMenu = pauseUI.Instantiate<Control>();
			AddChild(pauseMenu);
		}
		else if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[16].Item2) && GameManager._pausemode && _uiTimer > 20)
		{
			_uiTimer = 0;
			GameManager._pausemode = false;
		}
	}
	
	protected void Inventory()
	{
		if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[12].Item2) && !GameHUD.OnInventory && _uiTimer > 20)
		{
			GameManager.InfoJoueur["animation"] = "idle";
			CameraV.SpringLength = -4;
			_uiTimer = 0;
			GameHUD.OnInventory = true;
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
		else if (Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[12].Item2) && GameHUD.OnInventory && _uiTimer > 20)
		{
			_uiTimer = 0;
			GameHUD.OnInventory = false;
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}
	}

	protected void PhysicsReset()
	{
		MovementSpeed = 0f;
	}
	
	protected void Gravity(double delta)
	{
		if (!IsOnFloor())
		{
			VerticalVelocity += Vector3.Down * GravityValue * 2 * (float)delta;
		}
		else
		{
			VerticalVelocity = Vector3.Down * GravityValue / 10 * (float)delta;
		}
	}
	
	protected abstract void Move(double delta);
	
	public abstract void TakeDamage(int damage);
	
	public bool UseStamina(int stamina)
	{
		if (Stamina >= stamina)
		{
			Stamina -= stamina;
			return true;
		}

		return false;
	}
	
	public int GetHealth()
	{
		return Heath;
	}
	public void SetHealth(int health)
	{
		if(health>MaxHealth)
		{
			Heath = MaxHealth;
		}
		else
		{
			Heath = health;
		}
	}
	
	public int GetMaxHealth()
	{
		return MaxHealth;
	}
	
	public int GetStamina()
	{
		return Stamina;
	}
	
	public void SetStamina(int stamina)
	{
		if(stamina>MaxStamina)
		{
			stamina = MaxStamina;
		}
	}
	
	public int GetMaxStamina()
	{
		return MaxStamina;
	}
}
