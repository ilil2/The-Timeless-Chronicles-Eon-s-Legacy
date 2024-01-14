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
    private float _titleDefaultSize = 40;
    private float _buttonDefaultSize = 25;
    
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
        //Language
        _allLanguages = GameManager.LanguageManager.GetAllLanguages();
        _language = GameManager.SettingsManager.GetAllSettings()["language"];
        _languageDict = GameManager.LanguageManager.GetLanguage(_language);
        
        Translation();
        
        //Game
        _gameSettings = GetNode<Control>("GameSettings");
        _gameSettingsButton = GetNode<Button>("GameSettingsButton");
        _languageChooseButton = GetNode<OptionButton>("GameSettings/LanguageChooseButton");
        _mouseSensibilityBar = GetNode<ScrollBar>("GameSettings/MouseSensibilityBar");
        _fullScreenButton = GetNode<CheckButton>("GameSettings/FullScreenButton");
        _enableChatButton = GetNode<CheckButton>("GameSettings/EnableChatButton");
        _chatSizeButton = GetNode<OptionButton>("GameSettings/ChatSizeButton");
        _resetGameSettingsButton = GetNode<Button>("GameSettings/ResetGameSettingsButton");
        _saveGameSettingsButton = GetNode<Button>("GameSettings/SaveGameSettingsButton");

        foreach (var language in _allLanguages)
        {
            _languageChooseButton.AddItem(language["languageName"]);
        }
        _languageChooseButton.Selected = _language;
        
        _mouseSensibilityBar.Value = GameManager.SettingsManager.GetAllSettings()["mouseSensibility"];
        
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
        _inputList = GetNode<ItemList>("InputSettings/InputList");
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
        _gameSettingsText = GetNode<Label>("GameSettingsButton/GameSettingsText");
        _languageChooseText = GetNode<Label>("GameSettings/LanguageChooseText");
        _mouseSensibilityText = GetNode<Label>("GameSettings/MouseSensibilityText");
        _fullScreenText = GetNode<Label>("GameSettings/FullScreenText");
        _enableChatText = GetNode<Label>("GameSettings/EnableChatText");
        _chatSizeText = GetNode<Label>("GameSettings/ChatSizeText");
        _resetGameSettingsText = GetNode<Label>("GameSettings/ResetGameSettingsButton/ResetGameSettingsText");
        _saveGameSettingsText = GetNode<Label>("GameSettings/SaveGameSettingsButton/SaveGameSettingsText");
        
        _gameSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _languageChooseText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _mouseSensibilityText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _fullScreenText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _enableChatText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _chatSizeText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _resetGameSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _saveGameSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        
        //Audio
        _audioSettingsText = GetNode<Label>("AudioSettingsButton/AudioSettingsText");
        _resetAudioSettingsText = GetNode<Label>("AudioSettings/ResetAudioSettingsButton/ResetAudioSettingsText");
        _saveAudioSettingsText = GetNode<Label>("AudioSettings/SaveAudioSettingsButton/SaveAudioSettingsText");
        
        _audioSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _resetAudioSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _saveAudioSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        
        //Video
        _videoSettingsText = GetNode<Label>("VideoSettingsButton/VideoSettingsText");
        _resetVideoSettingsText = GetNode<Label>("VideoSettings/ResetVideoSettingsButton/ResetVideoSettingsText");
        _saveVideoSettingsText = GetNode<Label>("VideoSettings/SaveVideoSettingsButton/SaveVideoSettingsText");
        
        _videoSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _resetVideoSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        _saveVideoSettingsText.LabelSettings.FontSize = (int)(_buttonDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
        
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
        if (_resetGameSettingsButton.ButtonPressed)
        {
        }
        else if (_saveGameSettingsButton.ButtonPressed)
        {
            if ((int)_mouseSensibilityBar.Value != GameManager.SettingsManager.GetAllSettings()["mouseSensibility"])
            {
                //Mouse Sensibility
                GameManager.SettingsManager.GetAllSettings()["mouseSensibility"] = (int)_mouseSensibilityBar.Value;
                ((CameraPlayer)((ClassScript)GameManager.Joueur1).GetCamera()).ChangeSensibility((int)_mouseSensibilityBar.Value);
                
                if (_languageChooseButton.Selected != GameManager.SettingsManager.GetAllSettings()["language"])
                {
                    //Language
                    GameManager.SettingsManager.GetAllSettings()["language"] = _languageChooseButton.Selected;
                    _language = GameManager.SettingsManager.GetAllSettings()["language"];
                    _languageDict = GameManager.LanguageManager.GetLanguage(_language);
                    Translation();
                    
                    _inputList.Clear();
                    foreach (var (key, value) in GameManager.InputManger.GetAllControl())
                    {
                        _inputList.AddItem($"{key.ToUpper()} : {value}");
                    }
                }
                
                //Save
                GameManager.SettingsManager.SaveSettings();
            }
            else if (_languageChooseButton.Selected != GameManager.SettingsManager.GetAllSettings()["language"])
            {
                //Language
                GameManager.SettingsManager.GetAllSettings()["language"] = _languageChooseButton.Selected;
                _language = GameManager.SettingsManager.GetAllSettings()["language"];
                _languageDict = GameManager.LanguageManager.GetLanguage(_language);
                Translation();
                
                _inputList.Clear();
                foreach (var (key, value) in GameManager.InputManger.GetAllControl())
                {
                    _inputList.AddItem($"{key.ToUpper()} : {value}");
                }
                
                //Save
                GameManager.SettingsManager.SaveSettings();
            }
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
