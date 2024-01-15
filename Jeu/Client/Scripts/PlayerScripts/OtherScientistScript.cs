using Godot;
using System;

public partial class OtherScientistScript : OtherClassScript
{
    public override void _Ready()
    {
        InitOtherPlayer();
    }
    
    public override void _Process(double delta)
    {
        PseudoManager();
    }
    
    public override void _PhysicsProcess(double delta)
    {
        SetPosition();
        SetRotation();
    }
}
