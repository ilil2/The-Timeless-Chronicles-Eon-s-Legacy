using Godot;
using System;

public partial class Laser : Node3D
{
    private RayCast3D _laserRay;
    private MeshInstance3D _laserMesh;
    
    private Vector3 _startPoint;
    
    private int _laserTimer;
    
    public override void _Ready()
    {
        _laserRay = GetNode<RayCast3D>("LaserRay");
        _laserMesh = GetNode<MeshInstance3D>("LaserRay/LaserMesh");
        
        _startPoint = GlobalPosition;
    }
    
    public override void _Process(double delta)
    {
        if (_laserRay.IsColliding())
        {
            Vector3 stopPoint = _laserRay.GetCollisionPoint();
            Vector3 middle = new Vector3((_startPoint.X + stopPoint.X) / 2, (_startPoint.Y + stopPoint.Y) / 2, (_startPoint.Z + stopPoint.Z) / 2);
        
            _laserMesh.GlobalPosition = middle;
            _laserMesh.Scale = new Vector3(1f, _startPoint.DistanceTo(middle), 1f);
            Visible = true;
        }
        else
        {
            Visible = false;
        }
        
        _laserTimer += 1;

        if (_laserTimer >= 500)
        {
            QueueFree();
        }
    }
}
