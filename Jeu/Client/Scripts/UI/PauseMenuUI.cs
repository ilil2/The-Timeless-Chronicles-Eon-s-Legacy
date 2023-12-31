using Godot;
using System;

public partial class PauseMenuUI : Control
{
    private Button _resumeButton;
    private Button _settingsButton;
    private Button _leaveButton;
    private Button _leaveConfirmButton;
    private Button _leaveCancelButton;
    
    private Control _leaveConfirm;
    
    private Label _title;
    private Label _resumeButtonText;
    private Label _settingsButtonText;
    private Label _leaveButtonText;
    private Label _leaveConfirmTitle;
    private Label _leaveConfirmButtonText;
    private Label _leaveCancelButtonText;
    
    private float _screenDefalutWidth = 1152;
    private float _titleDefaultSize = 40;
    private float _leaveConfirmDefaultSize = 30;
    private float _buttonDefaultSize = 25;
    
    public override void _Ready()
    {
        _resumeButton = GetNode<Button>("ResumeButton");
        _settingsButton = GetNode<Button>("SettingsButton");
        _leaveButton = GetNode<Button>("LeaveButton");
        
        _leaveConfirm = GetNode<Control>("ConfirmLeave");
        _leaveConfirmButton = GetNode<Button>("ConfirmLeave/ConfirmLeaveButton");
        _leaveCancelButton = GetNode<Button>("ConfirmLeave/CancelLeaveButton");
        
        _leaveConfirm.Visible = false;
    }
    
    public void OnResize()
    {
        _title = GetNode<Label>("PauseMenuText");
        _resumeButtonText = GetNode<Label>("ResumeButton/ResumeButtonText");
        _settingsButtonText = GetNode<Label>("SettingsButton/SettingsButtonText");
        _leaveButtonText = GetNode<Label>("LeaveButton/LeaveButtonText");
        _leaveConfirmTitle = GetNode<Label>("ConfirmLeave/ConfirmLeaveTitle");
        _leaveConfirmButtonText = GetNode<Label>("ConfirmLeave/ConfirmLeaveButton/ConfirmLeaveText");
        _leaveCancelButtonText = GetNode<Label>("ConfirmLeave/CancelLeaveButton/CancelLeaveText");
        
        _title.LabelSettings.FontSize = (int)(_titleDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _resumeButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _settingsButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _leaveButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _leaveConfirmTitle.LabelSettings.FontSize = (int)(_leaveConfirmDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _leaveConfirmButtonText.LabelSettings.FontSize = (int)(_leaveConfirmDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _leaveCancelButtonText.LabelSettings.FontSize = (int)(_leaveConfirmDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
    }
    
    public override void _Process(double delta)
    {
        if (!PauseMenuManager.MainMenu)
        {
            QueueFree();
        }
        
        if (_resumeButton.ButtonPressed && !_leaveConfirm.Visible)
        {
            PauseMenuManager.PauseMenu = false;
        }
        else if (_settingsButton.ButtonPressed && !_leaveConfirm.Visible)
        {
            PauseMenuManager.SettingsMenu = true;
            PauseMenuManager.MainMenu = false;
        }
        else if (_leaveButton.ButtonPressed && !_leaveConfirm.Visible)
        {
            _leaveConfirm.Visible = true;
        }
        
        if (_leaveConfirmButton.ButtonPressed)
        {
            GetTree().Quit();
        }
        else if (_leaveCancelButton.ButtonPressed)
        {
            _leaveConfirm.Visible = false;
        }
    }
}
