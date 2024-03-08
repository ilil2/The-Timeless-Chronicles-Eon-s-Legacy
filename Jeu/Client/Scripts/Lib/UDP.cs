using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lib;

public class UDP
{
    public static string Receive(Socket soc)
    {
        byte[] receive = new byte[1024];
        EndPoint remoteEp = new IPEndPoint(IPAddress.Any, 0);
        int receivedBytes = soc.ReceiveFrom(receive, ref remoteEp);

        string receivedMessage = Encoding.UTF8.GetString(receive, 0, receivedBytes);
        return receivedMessage;
    }
    
    public static void Send(Socket soc, string message, EndPoint remoteEp)
    {
        byte[] send = Encoding.UTF8.GetBytes(message);
        soc.SendTo(send, remoteEp);
    }
    
    public static (string,EndPoint) FirstReceive(Socket soc)
    {
        byte[] receive = new byte[1024];
        EndPoint remoteEp = new IPEndPoint(IPAddress.Any, 0);
        int receivedBytes = soc.ReceiveFrom(receive, ref remoteEp);

        string receivedMessage = Encoding.UTF8.GetString(receive, 0, receivedBytes);
        return (receivedMessage,remoteEp);
    }

    public static void OneShot(string message)
    {
        Socket soc2 = GameManager.soc2;
        IPEndPoint iep2 = GameManager.iep2;
        Dictionary<string,string> InfoJoueur = GameManager.InfoJoueur;
        Send(soc2,InfoJoueur["id"] + "_" + "on:" + message,iep2);    
    }
}