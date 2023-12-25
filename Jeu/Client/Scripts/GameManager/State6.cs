public class State6 : GameManager
{
    public State6(double delta)
    {
        State(delta);
    }

    public void State(double delta)
    {
        tw2.WriteLine("in:co:" + InfoJoueur["co"]);
        tw2.Flush();
        ((MapLvl1Script)Map).DebugMode(delta, Joueur1);
        if (((ChatUI)_chat).Inputtext != "")
        {
            tw2.WriteLine("chat:" + ((ChatUI)_chat).Inputtext);
            tw2.Flush();
            ((ChatUI)_chat).Inputtext = "";
        }
    }
}