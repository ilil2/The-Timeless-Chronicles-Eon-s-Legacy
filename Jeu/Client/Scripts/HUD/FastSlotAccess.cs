using Godot;

namespace JeuClient.Scripts.HUD;

public abstract partial class FastSlotAccess : Panel
{
    public static Potion[] Access = new Potion[8];
    public static TextureRect[] FastSlot = new TextureRect[8];
    
    public static void UpdateSlot()
    {
        for(int i = 0; i<3 ; i++)
        {
            if(Access[i]!=null)
            {
                (FastSlot[i] as TextureRect).Texture = GD.Load<Texture2D>(Access[i].img);
            }
            else
            {
                (FastSlot[i] as TextureRect).Texture = null;
            }
        }
    }
}