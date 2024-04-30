using Godot;
using System;

public partial class Ghost : Camera3D
{
	private float _shiftMultiplier = 2.5f;
	private float _altMultiplier = 1.0f / 2.5f; 
	private float sensitivity = 0.25f;

	private Vector2 _mousePosition;
	private float _totalPitch;

	private Vector3 _direction;
	private Vector3 _lastDirection;
	private Vector3 _velocity;
	private int _acceleration = 30;
	private int _deceleration = -10;
	private int _velMultiplier = 4;

	private bool _z;
	private bool _s;
	private bool _q;
	private bool _d;
	private bool _a;
	private bool _e;
	private bool _shift;
	private bool _alt;

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion eventMouseMotion)
		{
			_mousePosition = eventMouseMotion.Relative;
		}

		if (@event is InputEventMouseButton eventMouseButton)
		{
			switch (eventMouseButton.ButtonIndex)
			{
				case MouseButton.Right:
					if (eventMouseButton.Pressed)
					{
						Input.MouseMode = Input.MouseModeEnum.Captured;
					}
					else
					{
						Input.MouseMode = Input.MouseModeEnum.Visible;
					}
					break;
				case MouseButton.WheelUp:
					_velMultiplier = (int)Mathf.Clamp(_velMultiplier + 1.1f, 0.2f, 20f);
					break;
				case MouseButton.WheelDown:
					_velMultiplier = (int)Mathf.Clamp(_velMultiplier / 1.1f, 0.2f, 20f);
					break;
			}
		}

		if (@event is InputEventKey eventInputKey)
		{
			switch (eventInputKey.Keycode)
			{
				case Key.Z:
					_z = eventInputKey.Pressed;
					break;
				case Key.S:
					_s = eventInputKey.Pressed;
					break;
				case Key.Q:
					_q = eventInputKey.Pressed;
					break;
				case Key.D:
					_d = eventInputKey.Pressed;
					break;
				case Key.Ctrl:
					_a = eventInputKey.Pressed;
					break;
				case Key.Space:
					_e = eventInputKey.Pressed;
					break;
			}
		}
	}

	public override void _Process(double delta)
	{
		_updateMouseLook();
		_updateMovement(delta);
	}
	
	private float _booltofloat(bool b)
	{
		return b ? 1.0f : 0.0f;
	}

	private void _updateMovement(double delta)
	{
		_direction = new Vector3(_booltofloat(_d) - _booltofloat(_q), _booltofloat(_e) - _booltofloat(_a), _booltofloat(_s) - _booltofloat(_z));
		
		Vector3 offset = _direction.Normalized() * _acceleration * (float)delta * _velMultiplier;

		float speedMulti = 1;

		if (_shift)
		{
			speedMulti *= _shiftMultiplier;
		}

		if (_alt)
		{
			speedMulti *= _altMultiplier;
		}

		if (_direction == Vector3.Zero)
		{
			_velocity = Vector3.Zero;
		}
		else if(_lastDirection != _direction)
		{
			_velocity = Vector3.Zero;
			_velocity.X = Mathf.Clamp(_velocity.X + offset.X, -_velMultiplier, _velMultiplier);
			_velocity.Y = Mathf.Clamp(_velocity.Y + offset.Y, -_velMultiplier, _velMultiplier);
			_velocity.Z = Mathf.Clamp(_velocity.Z + offset.Z, -_velMultiplier, _velMultiplier);
			
			Translate(_velocity * (float)delta * speedMulti);

			_lastDirection = _direction;
		}
		else
		{
			_velocity.X = Mathf.Clamp(_velocity.X + offset.X, -_velMultiplier, _velMultiplier);
			_velocity.Y = Mathf.Clamp(_velocity.Y + offset.Y, -_velMultiplier, _velMultiplier);
			_velocity.Z = Mathf.Clamp(_velocity.Z + offset.Z, -_velMultiplier, _velMultiplier);
			
			Translate(_velocity * (float)delta * speedMulti);
		}
	}

	private void _updateMouseLook()
	{
		if (Input.MouseMode == Input.MouseModeEnum.Captured)
		{
			_mousePosition *= sensitivity;
			float yaw = _mousePosition.X;
			float pitch = _mousePosition.Y;
			_mousePosition = Vector2.Zero;
			
			pitch = Mathf.Clamp(pitch, -90.0f - _totalPitch, 90.0f - _totalPitch);
			_totalPitch += pitch;
			
			RotateY(Mathf.DegToRad(-yaw));
			RotateObjectLocal(new Vector3(1f,0f,0f), Mathf.DegToRad(-pitch));
			
		}
	}
}
