using Godot;
using System;

public partial class Laser : Node3D
{
    private RayCast3D _laserRay;
    private MeshInstance3D _laserMesh;
    private StaticBody3D _rangeMax;
    
    private Vector3 _startPoint;

    private float _laserSize = 0.8f;

    private int _laserId = -1;
    
    public override void _Ready()
    {
        _laserRay = GetNode<RayCast3D>("LaserRay");
        _laserMesh = GetNode<MeshInstance3D>("LaserRay/LaserMesh");
        _rangeMax = GetNode<StaticBody3D>("Range");
        
        _startPoint = GlobalPosition;
        
        Visible = false;
    }
    
    public override void _PhysicsProcess(double delta)
    {
        if (_laserRay.IsColliding())
        {
            Vector3 stopPoint = _laserRay.GetCollisionPoint();
            Vector3 middle = new Vector3((_startPoint.X + stopPoint.X) / 2, (_startPoint.Y + stopPoint.Y) / 2, (_startPoint.Z + stopPoint.Z) / 2);
        
            _laserMesh.GlobalPosition = middle;
            _laserMesh.Scale = new Vector3(_laserSize, _startPoint.DistanceTo(middle), _laserSize);
            
            _rangeMax.Visible = false;
        }
        else
        {
            _rangeMax.Visible = true;
        }

        if (_laserId != -1)
        {
            if (GameManager.InfoAutreJoueur[$"attack{_laserId}"] == "stop")
            {
                QueueFree();
            }
        }
        else
        {
            if (GameManager.InfoJoueur["info"] == "stop")
            {
                QueueFree();
            }
        }
    }
    
    public void SetLaserID(int id)
    {
        _laserId = id;
    }
    
    public void TimeOut()
    {
        Visible = true;
    }
}
