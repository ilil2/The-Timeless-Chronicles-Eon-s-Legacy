using Godot;
using System;

public partial class SettingsMenuUI : Control
{
    private Button _backButton;
    
    private Label _title;
    private Label _backButtonText;
    private ColorRect _backgroundColor;
    
    private float _screenDefalutWidth = 1152;
    private float _titleDefaultSize = 40;
    private float _buttonDefaultSize = 20;
    
    public override void _Ready()
    {
        _backButton = GetNode<Button>("BackButton");
    }
    
    public void OnResize()
    {
        _title = GetNode<Label>("SettingsTextMenu");
        _backButtonText = GetNode<Label>("BackButton/BackButtonText");
        _backgroundColor = GetNode<ColorRect>("BackgroundColor");
        
        _title.LabelSettings.FontSize = (int)(_titleDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _backButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _backgroundColor.Size = GetViewportRect().Size;
    }

    public override void _Process(double delta)
    {
        if (!PauseMenuManager.SettingsMenu)
        {
            QueueFree();
        }
        
        if (_backButton.ButtonPressed)
        {
            PauseMenuManager.MainMenu = true;
            PauseMenuManager.SettingsMenu = false;
        }
    }
}
