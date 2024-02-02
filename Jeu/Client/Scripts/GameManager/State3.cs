using System.Net.Sockets;
using System.Net;
using System.Threading;
using Lib;

public partial class State3 : GameManager
{
    public static void State()
    {
        if (OnJoin)
        {
            th.Interrupt();                //fermeture du thread listen
            thread = false;
        }
        ns.Close();
        tw.Close();                    //fermeture envoi requette au serveur principal
        tr.Close();                    //fermeture recu requette du serveur principal
        soc.Disconnect(false);        //deconnection du socket

        soc2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);    //nouveau socket
        soc2.ReceiveTimeout = 60000;
        iep2 = new IPEndPoint(IPAddress.Parse(IP), port_serv_jeu);                //nouvelle ip
        
        UDP.Send(soc2,"connect",iep2);    //envoie requette de connection au serveur secondaire
        InfoJoueur["id"] = UDP.Receive(soc2);    //reception de l'ID du serveur secondaire
        Seed = Conversions.AtoI(InfoJoueur["id"].Split('*')[0].Substring(1));
        AleateSeed = Conversions.AtoI(InfoJoueur["id"].Split('*')[1]);
        ((IMap)Map).SetSeed(Seed,AleateSeed);
        InfoJoueur["id"] = InfoJoueur["id"].Substring(0,1);
        UDP.Send(soc2,$"{InfoJoueur["id"]}/{InfoJoueur["pseudo"]}",iep2);    //envoie du pseudo au serveur secondaire
        

        th2 = new Thread(Listen2);    //initialisation thread
        th2.Start();                        //debut du thread
        state = 4;
        
    }
}