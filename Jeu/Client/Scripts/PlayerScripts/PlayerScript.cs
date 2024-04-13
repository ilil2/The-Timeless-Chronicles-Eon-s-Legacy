using Godot;

namespace JeuClient.Scripts.PlayerScripts;

public abstract partial class PlayerScript : CharacterBody3D
{
	public int Id;
	public string Pseudo;
	public string Classe;
	public bool IsDead = false;
	public virtual void Revive()
	{
		Position+= new Vector3(0,10,0);
	}
	public int GetId()
	{
		return Id;
	}
}
