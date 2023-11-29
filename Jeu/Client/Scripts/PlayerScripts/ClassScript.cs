using Godot;

namespace JeuClient.Scripts.PlayerScripts;

public class ClassScript
{
    //Variables du Fov du joueur
    private float _fovMax = 120;
    private float _fovMin = 30;
    
    // Variables d'une classe
    public int id { get; set; }
    public int pseudo { get; set; }
    public string classe { get; set; }

    public ClassScript(int id, int pseudo, string classe)
    {
        this.id = id;
        this.pseudo = pseudo;
        this.classe = classe;
    }

    public Vector3 GeneralGravity(double delta, float gravity)
    {
        return Vector3.Down * gravity * 2 * (float)delta;
    }

    public Vector3 FloorGravity(double delta, float gravity)
    {
        return Vector3.Down * gravity / 10 * (float)delta;
    }

    public Vector3 Dash(Vector3 direction, float dashPower)
    {
        return direction * dashPower;
    }

    public void Zoom(Camera3D camera)
    {
        if (camera.Fov >= _fovMin)
        {
            camera.Fov -= 1;
        }
    }
    
    public void DeZoom(Camera3D camera)
    {
        if (camera.Fov <= _fovMax)
        {
            camera.Fov  += 1;
        }
    }
    
    
}