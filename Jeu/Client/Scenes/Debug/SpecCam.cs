using System;
using Godot;

public partial class SpecCam : Camera3D
{
	private float _shiftMultiplier = 2.5f;
	private float _altMultiplier = 1.0f / 2.5f; 
	private float sensitivity = 0.25f;

	private Vector2 _mousePosition = new Vector2(0.0f, 0.0f);
	private float _totalPitch = 0.0f;

	private Vector3 _direction = new Vector3(0.0f, 0.0f, 0.0f);
	private Vector3 _velocity = new Vector3(0.0f, 0.0f, 0.0f);
	private int _acceleration = 30;
	private int _deceleration = -10;
	private int _velMultiplier = 4;

	private bool _z = false;
	private bool _s = false;
	private bool _q = false;
	private bool _d = false;
	private bool _a = false;
	private bool _e = false;
	private bool _shift = false;
	private bool _alt = false;

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
		UpdateLabel(delta);
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
	private void UpdateLabel(double delta)
	{
		Label FPS = GetNode<Label>("FPS");
		Label POS = GetNode<Label>("POS");
		
		FPS.Text = $"FPS: {(int)(1/delta)}";
		POS.Text = $"X: {MathF.Round(Position.X,2)}  Y: {MathF.Round(Position.Y,2)}  Z: {MathF.Round(Position.Z,2)}";
	}
}
