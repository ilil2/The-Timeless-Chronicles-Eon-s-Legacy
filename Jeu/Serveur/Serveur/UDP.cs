using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Serveur;

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
        byte[] receive = new byte[8096];
        EndPoint remoteEp = new IPEndPoint(IPAddress.Any, 0);
        int receivedBytes = soc.ReceiveFrom(receive, ref remoteEp);

        string receivedMessage = Encoding.UTF8.GetString(receive, 0, receivedBytes);
        return (receivedMessage,remoteEp);
    }
    
    public static void TCPSend(Socket soc, string message, EndPoint remoteEp)
    {
        message = $"tcp:{message}";
        byte[] receive = new byte[256];
        do
        {
            byte[] send = Encoding.UTF8.GetBytes(message);
            soc.SendTo(send, remoteEp);
            soc.ReceiveFrom(receive, ref remoteEp);
        } while (receive != Encoding.UTF8.GetBytes("ACK"));
        
        OKSend(soc, remoteEp);
    }
    
    public static void ACKSend(Socket soc, EndPoint remoteEp)
    {
        byte[] receive = new byte[256];
        do
        {
            byte[] send = Encoding.UTF8.GetBytes("ACK");
            soc.SendTo(send, remoteEp);
            soc.ReceiveFrom(receive, ref remoteEp);
        } while (receive != Encoding.UTF8.GetBytes("OK"));
    }
    
    public static void OKSend(Socket soc, EndPoint remoteEp)
    {
        byte[] receive = new byte[256];
        soc.ReceiveTimeout = 200;
        try
        {
            while (true)
            {
                byte[] send = Encoding.UTF8.GetBytes("OK");
                soc.SendTo(send, remoteEp);
                soc.ReceiveFrom(receive, ref remoteEp);
            }
        }
        catch
        {
            soc.ReceiveTimeout = 60000;
        }
    }
}