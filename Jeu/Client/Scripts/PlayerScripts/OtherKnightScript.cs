using Godot;
using System;

public partial class OtherKnightScript : OtherClassScript
{
    public override void _Ready()
    {
        InitOtherPlayer();
        AnimationOtherPlayer = GetNode<AnimationPlayer>("AnimationOtherPlayer");
    }
    
    public override void _Process(double delta)
    {
        PseudoManager();
        if (GameManager.InfoAutreJoueur[$"attack{Id}"] == "hit")
        {
            AnimationOtherPlayer.Play("hit");
            
            GameManager.InfoAutreJoueur[$"attack{Id}"] = "";
        }
    }
    
    public override void _PhysicsProcess(double delta)
    {
        SetPosition();
        SetRotation();
    }
}
