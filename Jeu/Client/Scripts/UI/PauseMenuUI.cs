using Godot;
using System;
using System.Collections.Generic;

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
    private float _titleDefaultSize = 45;
    private float _leaveConfirmDefaultSize = 32;
    private float _buttonDefaultSize = 30;
    
    public override void _Ready()
    {
        _resumeButton = GetNode<Button>("ResumeButton");
        _settingsButton = GetNode<Button>("SettingsButton");
        _leaveButton = GetNode<Button>("LeaveButton");
        
        _leaveConfirm = GetNode<Control>("ConfirmLeave");
        _leaveConfirmButton = GetNode<Button>("ConfirmLeave/ConfirmLeaveButton");
        _leaveCancelButton = GetNode<Button>("ConfirmLeave/CancelLeaveButton");
        
        _leaveConfirm.Visible = false;
        
        Translation();
    }
    
    private void Translation()
    {
        int language = GameManager.SettingsManager.GetAllSettings()["language"];
        Dictionary<string, string> languageDict = GameManager.LanguageManager.GetLanguage(language);
        
        _title.Text = languageDict["pauseMenuTitle"];
        _resumeButtonText.Text = languageDict["pauseMenuResumeButton"];
        _settingsButtonText.Text = languageDict["pauseMenuSettingsButton"];
        _leaveButtonText.Text = languageDict["pauseMenuLeaveButton"];
        _leaveConfirmTitle.Text = languageDict["pauseMenuConfirmLeaveTitle"];
        _leaveConfirmButtonText.Text = languageDict["pauseMenuConfirmLeaveButton"];
        _leaveCancelButtonText.Text = languageDict["pauseMenuCancelLeaveButton"];
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
            GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
        }
        else if (_leaveCancelButton.ButtonPressed)
        {
            _leaveConfirm.Visible = false;
        }
    }
}
