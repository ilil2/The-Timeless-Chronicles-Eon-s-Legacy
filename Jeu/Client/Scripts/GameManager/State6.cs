public partial class State6 : GameManager
{
    public static void State(double delta)
    {
        tw2.WriteLine("in:co:" + InfoJoueur["co"] + "/" + InfoJoueur["orientation"]);
        tw2.Flush();
        ((MapLvl1Script)Map).DebugMode(delta, Joueur1);

        if (!_pausemode)
        {
            _chat.Visible = true;
            if (((ChatUI)_chat).Inputtext != "")
            {
                tw2.WriteLine("chat:" + ((ChatUI)_chat).Inputtext);
                tw2.Flush();
                ((ChatUI)_chat).Inputtext = "";
            }
        }
        else
        {
            _chat.Visible = false;
        }
    }
}