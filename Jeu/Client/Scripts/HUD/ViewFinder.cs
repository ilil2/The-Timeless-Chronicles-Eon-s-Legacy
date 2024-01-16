using Godot;
using System;

public partial class ViewFinder : Control
{
    private Label _crosshair;
    
    private int _crosshairDefaultSize = 35;
    private int _screenDefalutWidth = 1152;

    public void OnResize()
    {
        _crosshair = GetNode<Label>("Crosshair");
        
        _crosshair.LabelSettings.FontSize = (int)(_crosshairDefaultSize * (GetViewportRect().Size.X / _screenDefalutWidth));
    }

    public override void _Process(double delta)
    {
        if (!ArcherScript.IsShooting && !ScientistScript.IsAiming)
        {
            QueueFree();
        }
    }
}
