namespace JeuClient.Scripts.PlayerScripts;

public class ClassScript
{
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
}