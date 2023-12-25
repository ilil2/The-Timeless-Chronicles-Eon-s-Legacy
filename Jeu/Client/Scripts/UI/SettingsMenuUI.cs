using Godot;
using System;

public partial class SettingsMenuUI : Control
{
    //General
    private Button _backButton;
    
    private Label _title;
    private Label _backButtonText;
    
    private float _screenDefalutWidth = 1152;
    private float _titleDefaultSize = 40;
    private float _buttonDefaultSize = 25;
    
    private bool _isChangingInput;
    
    //Game
    private Control _gameSettings;
    private Button _gameSettingsButton;
    
    private Label _gameSettingsText;
    
    //Audio
    private Control _audioSettings;
    private Button _audioSettingsButton;
    
    private Label _audioSettingsText;
    
    //Video
    private Control _videoSettings;
    private Button _videoSettingsButton;
    
    private Label _videoSettingsText;
    
    //Input
    private Control _inputSettings;
    private Button _inputSettingsButton;
    private ItemList _inputList;
    private Button _resetInputButton;
    private Button _saveInputButton;
    
    private Label _inputSettingsText;
    private Label _resetInputText;
    private Label _saveInputText;
    
    public override void _Ready()
    {
        //Game
        _gameSettings = GetNode<Control>("GameSettings");
        _gameSettingsButton = GetNode<Button>("GameSettingsButton");
        
        //Audio
        _audioSettings = GetNode<Control>("AudioSettings");
        _audioSettingsButton = GetNode<Button>("AudioSettingsButton");
        
        //Video
        _videoSettings = GetNode<Control>("VideoSettings");
        _videoSettingsButton = GetNode<Button>("VideoSettingsButton");
        
        //Input
        _inputSettings = GetNode<Control>("InputSettings");
        _inputSettingsButton = GetNode<Button>("InputSettingsButton");
        _inputList = GetNode<ItemList>("InputSettings/InputList");
        _resetInputButton = GetNode<Button>("InputSettings/ResetInputButton");
        _saveInputButton = GetNode<Button>("InputSettings/SaveInputButton");
        
        //General
        _backButton = GetNode<Button>("BackButton");

        foreach (var (key, value) in GameManager.InputManger.GetAllControl())
        {
            _inputList.AddItem($"{key.ToUpper()} : {value}");
        }
        
        _gameSettings.Visible = true;
        _audioSettings.Visible = false;
        _videoSettings.Visible = false;
        _inputSettings.Visible = false;
    }
    
    public void OnResize()
    {
        //General
        _title = GetNode<Label>("SettingsTextMenu");
        _backButtonText = GetNode<Label>("BackButton/BackButtonText");
        
        _title.LabelSettings.FontSize = (int)(_titleDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _backButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        
        //Game
        _gameSettingsText = GetNode<Label>("GameSettingsButton/GameSettingsText");
        
        _gameSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        
        //Audio
        _audioSettingsText = GetNode<Label>("AudioSettingsButton/AudioSettingsText");
        
        _audioSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        
        //Video
        _videoSettingsText = GetNode<Label>("VideoSettingsButton/VideoSettingsText");
        
        _videoSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        
        //Input
        _inputSettingsText = GetNode<Label>("InputSettingsButton/InputSettingsText");
        _resetInputText = GetNode<Label>("InputSettings/ResetInputButton/ResetInputText");
        _saveInputText = GetNode<Label>("InputSettings/SaveInputButton/SaveInputText");
        
        _inputSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _resetInputText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _saveInputText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
    }

    public override void _Input(InputEvent @event)
    {
        if (_inputSettings.Visible)
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
    }

    public override void _Process(double delta)
    {
        //General
        if (!PauseMenuManager.SettingsMenu)
        {
            QueueFree();
        }
        
        if (_backButton.ButtonPressed)
        {
            PauseMenuManager.MainMenu = true;
            PauseMenuManager.SettingsMenu = false;
        }

        if (_gameSettingsButton.ButtonPressed)
        {
            _gameSettings.Visible = true;
            _audioSettings.Visible = false;
            _videoSettings.Visible = false;
            _inputSettings.Visible = false;
        } 
        else if (_audioSettingsButton.ButtonPressed)
        {
            _gameSettings.Visible = false;
            _audioSettings.Visible = true;
            _videoSettings.Visible = false;
            _inputSettings.Visible = false;
        }
        else if (_videoSettingsButton.ButtonPressed)
        {
            _gameSettings.Visible = false;
            _audioSettings.Visible = false;
            _videoSettings.Visible = true;
            _inputSettings.Visible = false;
        }
        else if (_inputSettingsButton.ButtonPressed)
        {
            _gameSettings.Visible = false;
            _audioSettings.Visible = false;
            _videoSettings.Visible = false;
            _inputSettings.Visible = true;
        }
        
        //Game
        
        //Audio
        
        //Video
        
        //Input
        if (_resetInputButton.ButtonPressed)
        {
            GameManager.InputManger.ResetControl();
            _inputList.Clear();
            foreach (var (key, value) in GameManager.InputManger.GetAllControl())
            {
                _inputList.AddItem($"{key.ToUpper()} : {value}");
            }
        }
        else if (_saveInputButton.ButtonPressed)
        {
            GameManager.InputManger.SaveControl();
        }
        
    }
}
