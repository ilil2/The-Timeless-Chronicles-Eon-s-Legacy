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
}