using Godot;
using System;

public partial class PauseMenuUI : Control
{
    private Button _resumeButton;
    private Button _settingsButton;
    private Button _leaveButton;
    
    private Label _title;
    private Label _resumeButtonText;
    private Label _settingsButtonText;
    private Label _leaveButtonText;
    
    private float _screenDefalutWidth = 1152;
    private float _titleDefaultSize = 40;
    private float _buttonDefaultSize = 25;
    
    public override void _Ready()
    {
        _resumeButton = GetNode<Button>("ResumeButton");
        _settingsButton = GetNode<Button>("SettingsButton");
        _leaveButton = GetNode<Button>("LeaveButton");
    }
    
    public void OnResize()
    {
        _title = GetNode<Label>("PauseMenuText");
        _resumeButtonText = GetNode<Label>("ResumeButton/ResumeButtonText");
        _settingsButtonText = GetNode<Label>("SettingsButton/SettingsButtonText");
        _leaveButtonText = GetNode<Label>("LeaveButton/LeaveButtonText");
        
        _title.LabelSettings.FontSize = (int)(_titleDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _resumeButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _settingsButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _leaveButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
    }
    
    public override void _Process(double delta)
    {
        if (!PauseMenuManager.MainMenu)
        {
            QueueFree();
        }
        
        if (_resumeButton.ButtonPressed)
        {
            PauseMenuManager.PauseMenu = false;
        }

        if (_settingsButton.ButtonPressed)
        {
            PauseMenuManager.SettingsMenu = true;
            PauseMenuManager.MainMenu = false;
        }

        if (_leaveButton.ButtonPressed)
        {
            GetTree().Quit();
        }
    }
}
