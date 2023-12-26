using System;

public class Listen1 : GameManager
{
    public static void Listen()
    {
        string? rep = tr.ReadLine();	//lecture de donnée du serveur
        if (rep.Contains(":"))
        {
            if (rep.Substring(0, 7) == "newserv")	//si la requette commence par newserv
            {
                port_serv_jeu = Lib.Conversions.AtoI(rep.Substring(8)); //recuperation du nouveau port
                Console.WriteLine("connexion au serveur de jeu ...");
                conn2 = false;											//deconnextion du serveur principal
            }
				
            else if (rep.Substring(0,10) == "listplayer")
            {
                string line = rep.Substring(11);
                string[] SplitPseudo = line.Split(';');
                (LobbyManager.NamePlayer[0],LobbyManager.NamePlayer[1],LobbyManager.NamePlayer[2],LobbyManager.NamePlayer[3]) = (SplitPseudo[0],SplitPseudo[1],SplitPseudo[2],SplitPseudo[3]);
            }
        }
        else if (rep == "remove")
        {
            LobbyReset = true;
            LobbyManager.kill = true;
        }
    }
}