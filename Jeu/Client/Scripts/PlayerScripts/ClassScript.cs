using Godot;
using Lib;

namespace JeuClient.Scripts.PlayerScripts;

public class ClassScript
{
    //Variables du Fov du joueur
    private float _fovMax = 75;
    private float _fovMin = 40;
    
    // Variables d'une classe
    public int id { get; set; }
    public string pseudo { get; set; }
    public string classe { get; set; }

    public ClassScript(int id, string pseudo, string classe)
    {
        this.id = id;
        this.pseudo = pseudo;
        this.classe = classe;
    }

    public Vector3 Gravity(double delta, float gravity, bool floor)
    {
        if (floor)
        {
            return Vector3.Down * gravity / 10 * (float)delta;
        }
        return Vector3.Down * gravity * 2 * (float)delta;
    }

    public Vector3 MoveDirection(bool forward, bool backward, bool right, bool left, Node3D h)
    {
        Vector3 direction = new Vector3(Conversions.BtoI(right) - Conversions.BtoI(left), 0,
            Conversions.BtoI(forward) - Conversions.BtoI(backward));
        direction = direction.Rotated(Vector3.Up, h.GlobalTransform.Basis.GetEuler().Y).Normalized();
        return direction;
    }

    public Vector3 Dash(Vector3 direction, float dashPower)
    {
        return direction * dashPower;
    }
    
    

    public void Zoom(Camera3D camera)
    {
        if (camera.Fov >= _fovMin)
        {
            camera.Fov -= 2;
        }
    }
    
    public void DeZoom(Camera3D camera)
    {
        if (camera.Fov <= _fovMax)
        {
            camera.Fov  += 2;
        }
    }
}