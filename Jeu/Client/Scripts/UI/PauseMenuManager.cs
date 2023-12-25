using Godot;
using System;

public partial class PauseMenuManager : Control
{
    public static bool MainMenu;
    public static bool SettingsMenu;
    public static bool PauseMenu;
    
    private int _menuState;

    public override void _Ready()
    {
        MainMenu = true;
        SettingsMenu = false;
        PauseMenu = true;
        
        PackedScene pauseUI = GD.Load<PackedScene>("res://Scenes/UI/PauseMenuUI.tscn");
        Control pauseMenu = pauseUI.Instantiate<Control>();
        AddChild(pauseMenu);
    }

    public override void _Process(double delta)
    {
        if (!GameManager._pausemode)
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
            QueueFree();
        }
        else if (!PauseMenu)
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
            GameManager._pausemode = false;
            QueueFree();
        }

        if (MainMenu && _menuState != 0)
        {
            _menuState = 0;
            PackedScene pauseUI = GD.Load<PackedScene>("res://Scenes/UI/PauseMenuUI.tscn");
            Control pauseMenu = pauseUI.Instantiate<Control>();
            AddChild(pauseMenu);
        }
        else if (SettingsMenu && _menuState != 1)
        {
            _menuState = 1;
            PackedScene pauseUI = GD.Load<PackedScene>("res://Scenes/UI/SettingsMenuUI.tscn");
            Control pauseMenu = pauseUI.Instantiate<Control>();
            AddChild(pauseMenu);
        }
    }
}
