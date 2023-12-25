using Godot;

public class State1 : GameManager
{
    public State1()
    {
        State();
    }

    public void State()
    {
        PackedScene LobbyScene = GD.Load<PackedScene>("res://Scenes/LobbyManager.tscn");
        Control LobbyMenu = LobbyScene.Instantiate<Control>();
        AddChild(LobbyMenu);
        state = 2;
    }
}