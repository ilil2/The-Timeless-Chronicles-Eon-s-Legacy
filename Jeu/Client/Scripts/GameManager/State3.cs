using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using Godot;
using System.Threading;
using Serveur;

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
            
        UDP.Send(soc2,InfoJoueur["pseudo"],iep2);    //envoie du pseudo au serveur secondaire
        

        th2 = new Thread(Listen2);    //initialisation thread
        th2.Start();                        //debut du thread
        state = 4;
        
    }
}