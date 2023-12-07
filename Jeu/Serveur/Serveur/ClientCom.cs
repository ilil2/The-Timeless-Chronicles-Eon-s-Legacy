using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Serveur;

public class ClientCom         //type de l'objet client
{
    public Socket Socket { get; }      //socket de l'objet
    public int id { get; }             //id de l'objet
        
    public string pseudo { get; set; }
        
    public string classe { get; set; }
    
    public IPAddress IP { get; set; }
    
    public EndPoint Rep { get; set; }
    
    public string requette { get; set; }

    public ClientCom(Socket soc,IPAddress ip, EndPoint rep ,int id, string s)     //initialisation de l'objet
    {
        this.Socket = soc;
        this.id = id;
        this.IP = ip;
        this.Rep = rep;
        this.requette = s;
    }
}