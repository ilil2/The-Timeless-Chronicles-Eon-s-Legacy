using Godot;
using System;

public partial class SettingsMenuUI : Control
{
    private Button _backButton;
    private ItemList _inputList;
    
    private Label _title;
    private Label _backButtonText;
    private ColorRect _backgroundColor;
    private Label _gameSettingsText;
    private Label _audioSettingsText;
    private Label _videoSettingsText;
    private Label _inputSettingsText;
    
    private float _screenDefalutWidth = 1152;
    private float _titleDefaultSize = 40;
    private float _buttonDefaultSize = 20;
    
    private bool _isChangingInput;
    
    public override void _Ready()
    {
        _backButton = GetNode<Button>("BackButton");
        _inputList = GetNode<ItemList>("InputSettings/InputList");

        foreach (var (key, value) in GameManager.InputManger.GetAllControl())
        {
            _inputList.AddItem($"{key.ToUpper()} : {value}");
        }
    }
    
    public void OnResize()
    {
        _title = GetNode<Label>("SettingsTextMenu");
        _backButtonText = GetNode<Label>("BackButton/BackButtonText");
        
        _gameSettingsText = GetNode<Label>("GameSettingsButton/GameSettingsText");
        _audioSettingsText = GetNode<Label>("AudioSettingsButton/AudioSettingsText");
        _videoSettingsText = GetNode<Label>("VideoSettingsButton/VideoSettingsText");
        _inputSettingsText = GetNode<Label>("InputSettingsButton/InputSettingsText");
        
        _title.LabelSettings.FontSize = (int)(_titleDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _backButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        
        _gameSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _audioSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _videoSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _inputSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey eventKey)
        {
            if (eventKey.Pressed && _inputList.GetSelectedItems().Length > 0)
            {
                GameManager.InputManger.SetControl(_inputList.GetSelectedItems()[0], eventKey.Keycode);
                _inputList.SetItemText(_inputList.GetSelectedItems()[0], $"{_inputList.GetItemText(_inputList.GetSelectedItems()[0]).Split(":")[0]}: {eventKey.Keycode}");
            }
        }
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
