using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Serveur;

class ClientCom         //type de l'objet client
{
    public Socket Socket { get; }      //socket de l'objet
    public int id { get; }             //id de l'objet
        
    public string pseudo { get; set; }
        
    public string classe { get; set; }

    public EndPoint ep { get; set; }
    
    public ClientCom(Socket s, int id,EndPoint ep)     //initialisation de l'objet
    {
        this.Socket = s;
        this.id = id;
        this.ep = ep;
    }
}