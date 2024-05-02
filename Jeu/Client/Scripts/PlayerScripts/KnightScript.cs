using Godot;
using JeuClient.Scripts.PlayerScripts;
using Lib;

public partial class KnightScript : ClassScript
{
	private bool _isBlocking;

	public override void _Ready()
	{
		InitPlayer();
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
				if (_isBlocking)
				{
					Velocity = new Vector3(0, 0, 0);
				}
				else
				{
					Move(delta);
				}
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

	private void Animation()
	{
		bool left = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[2].Item2);
		bool right = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[3].Item2);
		bool forward = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[0].Item2);
		bool backward = Input.IsKeyPressed(GameManager.InputManger.GetAllControl()[1].Item2);
		
		(int, int) direction = (Conversions.BtoI(left) - Conversions.BtoI(right), Conversions.BtoI(forward) - Conversions.BtoI(backward));
		
		if (Input.IsMouseButtonPressed(MouseButton.Left) && AnimationState != 3 && !InteractionShop.OnShop && !GameHUD.OnInventory  && AnimationState != -2 && UseStamina(ManaUse))
		{
			DirectionControl = (0,0);
			AnimationState = 3;
			
			AnimationSet(false, false, true, true);
			
			GameManager.InfoJoueur["animation"] = "hit";
		}
		else if (Input.IsMouseButtonPressed(MouseButton.Right) && AnimationState != 2 && !InteractionShop.OnShop && !GameHUD.OnInventory && AnimationState != 1 && AnimationState != -2)
		{
			DirectionControl = (0,0);
			AnimationState = 2;
			
			AnimationSet(false, true, false, false);
			
			GameManager.InfoJoueur["animation"] = "protection";
		}
		else if ((left || right || forward || backward) && direction != DirectionControl && AnimationState != 2 && AnimationState != -2)
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
		else if (!Input.IsMouseButtonPressed(MouseButton.Right) && !Input.IsMouseButtonPressed(MouseButton.Left) && (!(left || right || forward || backward) || AnimationState != 1) && AnimationState != 0 && AnimationState != -2)
		{
			DirectionControl = (0,0);
			AnimationState = 0;
			AnimationSet(false, false, false, true);
			GameManager.InfoJoueur["animation"] = "idle";
		}
	}
	
	private void AnimationSet(bool walk, bool block, bool hit, bool idle, bool damage = false, bool damageblock = false, bool death = false)
	{
		AnimationTree.Set("parameters/conditions/WhenWalk", walk);
		AnimationTree.Set("parameters/conditions/WhenBlock", block);
		AnimationTree.Set("parameters/conditions/WhenHit", hit);
		AnimationTree.Set("parameters/conditions/Idle", idle);
		AnimationTree.Set("parameters/conditions/Death", death);
		AnimationTree.Set("parameters/conditions/Damage", damage);
		AnimationTree.Set("parameters/conditions/DamageBlock", damageblock);

		_isBlocking = block;
	}
	
	public override void TakeDamage(int damage)
	{
		if (!_isBlocking)
		{
			Health -= damage;
			if (Health <= 0 && !IsDead)
			{
				IsDead = true;
				AnimationState = -1;
				AnimationSet(false, false, false, false, false, false, true);
				GameManager.InfoJoueur["animation"] = "death";
				GetNode<Timer>("DeathTimer").Start();
			}
			else
			{
				AnimationState = -2;
				AnimationSet(false, false, false, false, true);
				GameManager.InfoJoueur["animation"] = "damage";
				DamageTimer.Start();
			}
		}
		else
		{
			if (!UseStamina(damage*20))
			{
				Health -= damage;
				if (Health <= 0 && !IsDead)
				{
					IsDead = true;
					AnimationState = -1;
					AnimationSet(false, false, false, false, false, false, true);
					GameManager.InfoJoueur["animation"] = "death";
					GetNode<Timer>("DeathTimer").Start();
				}
				else
				{
					AnimationState = -2;
					AnimationSet(false, false, false, false, false, true);
					GameManager.InfoJoueur["animation"] = "damageblock";
					DamageTimer.Start();
				}
			}
			else
			{
				AnimationState = -2;
				AnimationSet(false, false, false, false, false, true);
				GameManager.InfoJoueur["animation"] = "damageblock";
				DamageTimer.Start();
			}
		}
	}
	
	private void _on_death_timeout()
	{
		Position -= new Vector3(0,10,0);
	}
	
	private void _on_stamina_timeout()
	{
		if (!_isBlocking)
		{
			if (Stamina + ChargeSpeed <= MaxStamina)
			{
				Stamina += ChargeSpeed;
			}
		}
	}
	
	private void _on_damage_timer_timeout()
	{
		AnimationState = -3;
	}
}

