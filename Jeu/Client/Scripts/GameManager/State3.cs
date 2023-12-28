using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using Godot;
using System.Threading;

public partial class State3 : GameManager
{
    public static void State()
    {
        if (ScanPorts(IP,port_serv_jeu))
        {
            if (OnJoin)
            {
                th.Interrupt();				//fermeture du thread listen
            }
            ns.Close();
            tw.Close();					//fermeture envoi requette au serveur principal
            tr.Close();					//fermeture recu requette du serveur principal
            soc.Disconnect(false);		//deconnection du socket
            state = 4;
				
            soc2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);	//nouveau socket
            iep2 = new IPEndPoint(IPAddress.Parse(IP), port_serv_jeu);				//nouvelle ip
            soc2.Connect(iep2);																			//connexion
			
            ns2 = new NetworkStream(soc2);
            tw2 = new StreamWriter(ns2);					//lecture serveur secondaire
            tr2 = new StreamReader(ns2);					//ecriture serveur secondaire
				
            tw2.WriteLine(InfoJoueur["pseudo"]);
            tw2.Flush();

            th2 = new Thread(Listen2);	//initialisation thread
            th2.Start();						//debut du thread
        }
    }

    public static bool ScanPorts(string ipAddress, int port)
    {
        bool b = false;
        TcpListener tcpListener = null;

        try
        {
            tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
            tcpListener.Start();
            b = true;
        }
        finally
        {
            tcpListener?.Stop();
        }

        return b;
    }
}