using Godot;
using System;
using System.Collections.Generic;
using JeuClient.Scripts.PlayerScripts;

public partial class SettingsMenuUI : Control
{
    //General
    private Button _backButton;
    
    private Label _title;
    private Label _backButtonText;
    
    private float _screenDefalutWidth = 1152;
    private float _screenDefalutHight = 648;
    private float _titleDefaultSize = 40;
    private float _buttonDefaultSize = 25;
    private float _textDefaultSize = 20;
    private float _checkButtonDefaultSize = 2.5f;
    
    private float _checkButtonSizeX = 44;
    private float _checkButtonSizeY = 24;
    
    private bool _isChangingInput;
    
    //Game
    private Control _gameSettings;
    private Button _gameSettingsButton;
    private OptionButton _languageChooseButton;
    private ScrollBar _mouseSensibilityBar;
    private CheckButton _fullScreenButton;
    private CheckButton _enableChatButton;
    private OptionButton _chatSizeButton;
    private Button _resetGameSettingsButton;
    private Button _saveGameSettingsButton;
    
    private Label _gameSettingsText;
    private Label _languageChooseText;
    private Label _mouseSensibilityText;
    private Label _fullScreenText;
    private Label _enableChatText;
    private Label _chatSizeText;
    private Label _resetGameSettingsText;
    private Label _saveGameSettingsText;
    
    private bool _isFullScreen = false;
    private bool _isChatEnable = true;
    
    private float _checkButtonEnableChatXPosition = 920;
    private float _checkButtonEnableChatYPosition = 224;
    private float _checkButtonFullScreenXPosition = 440;
    private float _checkButtonFullScreenYPosition = 400;
    
    //Audio
    private Control _audioSettings;
    private Button _audioSettingsButton;
    private Button _resetAudioSettingsButton;
    private Button _saveAudioSettingsButton;
    
    private Label _audioSettingsText;
    private Label _resetAudioSettingsText;
    private Label _saveAudioSettingsText;
    
    //Video
    private Control _videoSettings;
    private Button _videoSettingsButton;
    private Button _resetVideoSettingsButton;
    private Button _saveVideoSettingsButton;
    
    private Label _videoSettingsText;
    private Label _resetVideoSettingsText;
    private Label _saveVideoSettingsText;
    
    //Input
    private Control _inputSettings;
    private Button _inputSettingsButton;
    private ItemList _inputList;
    private Button _resetInputButton;
    private Button _saveInputButton;
    
    private Label _inputSettingsText;
    private Label _resetInputText;
    private Label _saveInputText;
    
    //Language
    private List<Dictionary<string, string>> _allLanguages;
    private int _language;
    private Dictionary<string, string> _languageDict;
    
    public override void _Ready()
    {
        Dictionary<string, int> Settings = GameManager.SettingsManager.GetAllSettings();
        
        //Language
        _allLanguages = GameManager.LanguageManager.GetAllLanguages();
        _language = Settings["language"];
        _languageDict = GameManager.LanguageManager.GetLanguage(_language);
        
        Translation();
        
        //Game
        _gameSettings = GetNode<Control>("GameSettings");
        _gameSettingsButton = GetNode<Button>("GameSettingsButton");
        _mouseSensibilityBar = GetNode<ScrollBar>("GameSettings/MouseSensibilityBar");
        _resetGameSettingsButton = GetNode<Button>("GameSettings/ResetGameSettingsButton");
        _saveGameSettingsButton = GetNode<Button>("GameSettings/SaveGameSettingsButton");

        foreach (var language in _allLanguages)
        {
            _languageChooseButton.AddItem(language["languageName"]);
        }
        _languageChooseButton.Selected = _language;
        
        _mouseSensibilityBar.Value = Settings["mouseSensibility"];

        if (Settings["enableChat"] == 1)
        {
            _enableChatButton.ButtonPressed = true;
        }
        else
        {
            _enableChatButton.ButtonPressed = false;
        }
        
        
        _fullScreenButton.Size = new Vector2(_checkButtonSizeX, _checkButtonSizeY);
        _fullScreenButton.Position = new Vector2(_checkButtonFullScreenXPosition, _checkButtonFullScreenYPosition);
        
        _enableChatButton.Size = new Vector2(_checkButtonSizeX, _checkButtonSizeY);
        _enableChatButton.Position = new Vector2(_checkButtonEnableChatXPosition, _checkButtonEnableChatYPosition);
        
        //Audio
        _audioSettings = GetNode<Control>("AudioSettings");
        _audioSettingsButton = GetNode<Button>("AudioSettingsButton");
        _resetAudioSettingsButton = GetNode<Button>("AudioSettings/ResetAudioSettingsButton");
        _saveAudioSettingsButton = GetNode<Button>("AudioSettings/SaveAudioSettingsButton");
        
        //Video
        _videoSettings = GetNode<Control>("VideoSettings");
        _videoSettingsButton = GetNode<Button>("VideoSettingsButton");
        _resetVideoSettingsButton = GetNode<Button>("VideoSettings/ResetVideoSettingsButton");
        _saveVideoSettingsButton = GetNode<Button>("VideoSettings/SaveVideoSettingsButton");
        
        //Input
        _inputSettings = GetNode<Control>("InputSettings");
        _inputSettingsButton = GetNode<Button>("InputSettingsButton");
        _resetInputButton = GetNode<Button>("InputSettings/ResetInputButton");
        _saveInputButton = GetNode<Button>("InputSettings/SaveInputButton");
        
        foreach (var (key, value) in GameManager.InputManger.GetAllControl())
        {
            _inputList.AddItem($"{key.ToUpper()} : {value}");
        }
        
        //General
        _backButton = GetNode<Button>("BackButton");
        
        _gameSettings.Visible = true;
        _audioSettings.Visible = false;
        _videoSettings.Visible = false;
        _inputSettings.Visible = false;
    }
    
    private void Translation()
    {
        //Settings Menu
        _title.Text = _languageDict["settingsMenuTitle"];
        _backButtonText.Text = _languageDict["settingsMenuBackButton"];
        
        //Settings Menu - Game
        _gameSettingsText.Text = _languageDict["settingsMenuGameButton"];
        _resetGameSettingsText.Text = _languageDict["settingsMenuResetButton"];
        _saveGameSettingsText.Text = _languageDict["settingsMenuSaveButton"];
        
        _languageChooseText.Text = _languageDict["settingsMenuGameLanguage"];
        _mouseSensibilityText.Text = _languageDict["settingsMenuGameMouseSensibility"];
        _fullScreenText.Text = _languageDict["settingsMenuGameFullScreen"];
        _enableChatText.Text = _languageDict["settingsMenuGameEnableChat"];
        _chatSizeText.Text = _languageDict["settingsMenuGameChatSize"];
        //_chatSizeButton.SetItemText(0, _languageDict["settingsMenuGameChatSizeSmall"]);
        //_chatSizeButton.SetItemText(1, _languageDict["settingsMenuGameChatSizeMedium"]);
        //_chatSizeButton.SetItemText(2, _languageDict["settingsMenuGameChatSizeLarge"]);
        
        //Settings Menu - Audio
        _audioSettingsText.Text = _languageDict["settingsMenuAudioButton"];
        _resetAudioSettingsText.Text = _languageDict["settingsMenuResetButton"];
        _saveAudioSettingsText.Text = _languageDict["settingsMenuSaveButton"];
        
        
        //Settings Menu - Video
        _videoSettingsText.Text = _languageDict["settingsMenuVideoButton"];
        _resetVideoSettingsText.Text = _languageDict["settingsMenuResetButton"];
        _saveVideoSettingsText.Text = _languageDict["settingsMenuSaveButton"];
        
        
        //Settings Menu - Controls
        _inputSettingsText.Text = _languageDict["settingsMenuControlsButton"];
        _resetInputText.Text = _languageDict["settingsMenuResetButton"];
        _saveInputText.Text = _languageDict["settingsMenuSaveButton"];
        
        GameManager.InputManger.SetControlName(0, _languageDict["settingsMenuControlsMoveForward"]);
        GameManager.InputManger.SetControlName(1, _languageDict["settingsMenuControlsMoveBackward"]);
        GameManager.InputManger.SetControlName(2, _languageDict["settingsMenuControlsMoveLeft"]);
        GameManager.InputManger.SetControlName(3, _languageDict["settingsMenuControlsMoveRight"]);
        GameManager.InputManger.SetControlName(4, _languageDict["settingsMenuControlsSprint"]);
        GameManager.InputManger.SetControlName(5, _languageDict["settingsMenuControlsDash"]);
        GameManager.InputManger.SetControlName(6, _languageDict["settingsMenuControlsCapacity1"]);
        GameManager.InputManger.SetControlName(7, _languageDict["settingsMenuControlsCapacity2"]);
        GameManager.InputManger.SetControlName(8, _languageDict["settingsMenuControlsCapacity3"]);
        GameManager.InputManger.SetControlName(9, _languageDict["settingsMenuControlsItem1"]);
        GameManager.InputManger.SetControlName(10, _languageDict["settingsMenuControlsItem2"]);
        GameManager.InputManger.SetControlName(11, _languageDict["settingsMenuControlsItem3"]);
        GameManager.InputManger.SetControlName(12, _languageDict["settingsMenuControlsInventory"]);
        GameManager.InputManger.SetControlName(13, _languageDict["settingsMenuControlsReload"]);
        GameManager.InputManger.SetControlName(14, _languageDict["settingsMenuControlsChat"]);
        GameManager.InputManger.SetControlName(15, _languageDict["settingsMenuControlsPause"]);
    }
    
    public void OnResize()
    {
        //General
        _title = GetNode<Label>("SettingsTextMenu");
        _backButtonText = GetNode<Label>("BackButton/BackButtonText");
        
        _title.LabelSettings.FontSize = (int)(_titleDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _backButtonText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        
        //Game
        
        //Button
        _languageChooseButton = GetNode<OptionButton>("GameSettings/LanguageChooseButton");
        _chatSizeButton = GetNode<OptionButton>("GameSettings/ChatSizeButton");
        _fullScreenButton = GetNode<CheckButton>("GameSettings/FullScreenButton");
        _enableChatButton = GetNode<CheckButton>("GameSettings/EnableChatButton");
        
        //Label
        _gameSettingsText = GetNode<Label>("GameSettingsButton/GameSettingsText");
        _languageChooseText = GetNode<Label>("GameSettings/LanguageChooseText");
        _mouseSensibilityText = GetNode<Label>("GameSettings/MouseSensibilityText");
        _fullScreenText = GetNode<Label>("GameSettings/FullScreenText");
        _enableChatText = GetNode<Label>("GameSettings/EnableChatText");
        _chatSizeText = GetNode<Label>("GameSettings/ChatSizeText");
        _resetGameSettingsText = GetNode<Label>("GameSettings/ResetGameSettingsButton/ResetGameSettingsText");
        _saveGameSettingsText = GetNode<Label>("GameSettings/SaveGameSettingsButton/SaveGameSettingsText");
        
        //Button Size
        _languageChooseButton.AddThemeFontSizeOverride("font_size", (int)(_textDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth)));
        _chatSizeButton.AddThemeFontSizeOverride("font_size", (int)(_textDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth)));
        
        _fullScreenButton.Scale = new Vector2(_checkButtonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth), _checkButtonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _fullScreenButton.Size = new Vector2(_checkButtonSizeX, _checkButtonSizeY);
        _fullScreenButton.Position = new Vector2(_checkButtonFullScreenXPosition * (GetViewportRect().Size.X / _screenDefalutWidth), _checkButtonFullScreenYPosition * (GetViewportRect().Size.Y / _screenDefalutHight));
        
        _enableChatButton.Scale = new Vector2(_checkButtonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth), _checkButtonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _enableChatButton.Size = new Vector2(_checkButtonSizeX, _checkButtonSizeY);
        _enableChatButton.Position = new Vector2(_checkButtonEnableChatXPosition * (GetViewportRect().Size.X / _screenDefalutWidth), _checkButtonEnableChatYPosition * (GetViewportRect().Size.Y / _screenDefalutHight));
        
        //Label Size
        _gameSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _languageChooseText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _mouseSensibilityText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _fullScreenText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _enableChatText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _chatSizeText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _resetGameSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _saveGameSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        
        //Audio
        
        //Label
        _audioSettingsText = GetNode<Label>("AudioSettingsButton/AudioSettingsText");
        _resetAudioSettingsText = GetNode<Label>("AudioSettings/ResetAudioSettingsButton/ResetAudioSettingsText");
        _saveAudioSettingsText = GetNode<Label>("AudioSettings/SaveAudioSettingsButton/SaveAudioSettingsText");
        
        //Label Size
        _audioSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _resetAudioSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _saveAudioSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        
        //Video
        
        //Label
        _videoSettingsText = GetNode<Label>("VideoSettingsButton/VideoSettingsText");
        _resetVideoSettingsText = GetNode<Label>("VideoSettings/ResetVideoSettingsButton/ResetVideoSettingsText");
        _saveVideoSettingsText = GetNode<Label>("VideoSettings/SaveVideoSettingsButton/SaveVideoSettingsText");
        
        //Label Size
        _videoSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _resetVideoSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _saveVideoSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        
        //Input
        
        //ItemList
        _inputList = GetNode<ItemList>("InputSettings/InputList");
        
        //Label
        _inputSettingsText = GetNode<Label>("InputSettingsButton/InputSettingsText");
        _resetInputText = GetNode<Label>("InputSettings/ResetInputButton/ResetInputText");
        _saveInputText = GetNode<Label>("InputSettings/SaveInputButton/SaveInputText");
        
        //ItemList Size
        _inputList.AddThemeFontSizeOverride("font_size", (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth)));
        
        //Label Size
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

    public override void _PhysicsProcess(double delta)
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
        else if (_gameSettingsButton.ButtonPressed)
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
        if (_languageChooseButton.Selected != _language)
        {
            _language = _languageChooseButton.Selected;
            _languageDict = GameManager.LanguageManager.GetLanguage(_language);
            Translation();
            GameManager.SettingsManager.SetSetting("language", _language);
            
            _inputList.Clear();
            foreach (var (key, value) in GameManager.InputManger.GetAllControl())
            {
                _inputList.AddItem($"{key.ToUpper()} : {value}");
            }
        } 
        else if (_resetGameSettingsButton.ButtonPressed)
        {
            GameManager.SettingsManager.ResetSettings();
            Dictionary<string, int> Settings = GameManager.SettingsManager.GetAllSettings();
            _languageChooseButton.Selected = Settings["language"];
            _mouseSensibilityBar.Value = Settings["mouseSensibility"];
        }
        else if (_saveGameSettingsButton.ButtonPressed)
        {
            Dictionary<string, int> Settings = GameManager.SettingsManager.GetAllSettings();
            
            //Mouse Sensibility
            Settings["mouseSensibility"] = (int)_mouseSensibilityBar.Value;
            ((CameraPlayer)((ClassScript)GameManager.Joueur1).GetCamera()).ChangeSensibility((int)_mouseSensibilityBar.Value);
            
            //Chat Size
            Settings["chatSize"] = _chatSizeButton.Selected;
                
            //Save
            GameManager.SettingsManager.SaveSettings();
        }
        else if (!_enableChatButton.ButtonPressed && _isChatEnable)
        {
            _isChatEnable = false;
            
            GameManager.SettingsManager.SetSetting("enableChat", 0);
        }
        else if (_enableChatButton.ButtonPressed && !_isChatEnable)
        {
            _isChatEnable = true;
            
            GameManager.SettingsManager.SetSetting("enableChat", 1);
        }
        else if (!_fullScreenButton.ButtonPressed && _isFullScreen)
        {
            _isFullScreen = false;
            
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
        }
        else if (_fullScreenButton.ButtonPressed && !_isFullScreen)
        {
            _isFullScreen = true;
            
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
        }
        
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
