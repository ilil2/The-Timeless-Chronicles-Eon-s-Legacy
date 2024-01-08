using Godot;

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
            string commandchat = ((ChatUI)_chat).Inputtext;
            if (commandchat != "")
            {
                if (commandchat[0] == '/')
                {
                    commandchat = commandchat.Substring(1);

                    if (Cheat)
                    {
                        if (commandchat == "debug")
                        {
                            DebugMode = !DebugMode;
                        }
                        else if (commandchat == "display")
                        {
                            DebugMode = DebugMode;
                        }
                        else if (commandchat == "help")
                        {
                            if (commandchat.Length == 4)
                            {
                                ((ChatUI)_chat).Outputaddtext = "effectuez \"/help <numero de page>\" pour charger une page ou \"/help <commande>\" pour de l'aide sur une commande précise";
                            }
                            else
                            {
                                commandchat = commandchat.Substring(5);
                                if (commandchat == "1")
                                {
                                    ((ChatUI)_chat).Outputaddtext = "Page 1 : \n\tdebug - passe en camera debug\n\tdisplay - affiche les infos utiles\n\tsetrule - change une règle du jeu";
                                }
                                else if (commandchat == "setrule")
                                {
                                    ((ChatUI)_chat).Outputaddtext = "setrule : \n\tfog <on/off> - active ou désactive le fog";
                                }
                                else
                                {
                                    ((ChatUI)_chat).Outputaddtext = "effectuez \"/help <numero de page>\" pour charger une page ou \"/help <commande>\" pour de l'aide sur une commande précise";
                                }
                            }
                        }
                        else if (commandchat.Length >= 15 && commandchat.Substring(0, 7) == "setrule")
                        {
                            commandchat.Substring(8);

                            if (commandchat.Substring(0,3) == "fog")
                            {
                                commandchat = commandchat.Substring(4);
                                if (commandchat.Substring(0,3) == "off")
                                {
                                    Fog = false;
                                }
                                else if (commandchat.Substring(0,2) == "on")
                                {
                                    Fog = true;
                                }
                            }
                            if (commandchat.Substring(0,2) == "ia")
                            {
                                commandchat = commandchat.Substring(4);
                                if (commandchat.Substring(0,3) == "off")
                                {
                                    DebugMode = DebugMode;
                                }
                                else if (commandchat.Substring(0,2) == "on")
                                {
                                    DebugMode = DebugMode;
                                }
                            }
                        }
                    }
                    else if (commandchat == "cheat on")
                    {
                        Cheat = true;
                        ((ChatUI)_chat).Outputaddtext = "passage en mode triche";

                    }
                    else if (commandchat == "help")
                    {
                        ((ChatUI)_chat).Outputaddtext = "effectuez \"/cheat on\" pour passer en mode triche";
                    }
                }
                else
                {
                    tw2.WriteLine("chat:" + ((ChatUI)_chat).Inputtext);
                    tw2.Flush();
                }
                ((ChatUI)_chat).Inputtext = "";
            }
        }
        else
        {
            _chat.Visible = false;
        }
    }
}