using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;


namespace Serveur;

public class MainServeur
{
    private int ID = 0;
    private int[] ports = new[] {4242,4002};
    private int nbr_serveur = 0;
    private int nbr_joueur = 0;
    public void MainProgram()
    {
        Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);
        IPEndPoint iep = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 9191);
        soc.Bind(iep); //connection depuis n'importe ou
        
        soc.Listen(); //mise en ecoute du serveur

        Console.WriteLine("Serveur en marche");

        bool inline = true; //variable pour pouvoir desactiver le serveur
        while (inline)
        {
            Console.WriteLine("En attente ...");
            Socket s = soc.Accept();                        //acceptation des nouvelles connection
            ID++;
            ClientCom clicom = new ClientCom(s, ID);        //creation de l'objet client
            Thread th = new Thread(com);                    //mise en place de la connection
            th.Start(clicom);                               //demarage de la connection
        }
    }
    
    public void com(object o) //fonction qui gere un client unique
    {
        ClientCom cc = (ClientCom)o;                        //creation de l'objet client
        NetworkStream ns = new NetworkStream(cc.Socket);    //debut de la connection
        TextReader tr = new StreamReader(ns);               //chaine recue
        TextWriter tw = new StreamWriter(ns);               //chaine a envoyer
        
        Console.WriteLine($"nouveau client : {cc.id} ip : {cc.Socket.RemoteEndPoint}");
        tw.WriteLine($"bien conecté a {cc.Socket.LocalEndPoint}");
        tw.Flush();     //envoi des données
        
        try
        {
            nbr_joueur++;
            while (true)
            {
                if (nbr_joueur == 4)
                {
                    tw.WriteLine($"newserv:{ports[nbr_serveur]}");
                    
                    nbr_serveur++;
                    nbr_serveur = 0;
                    
                    tw.Flush();
                }
            }
        }
        catch
        {
            Console.WriteLine($"client {cc.id} deconnecté de force");   //si le client s'est deconnecté de force
        }
    }

    class ClientCom         //type de l'objet client
    {
        public Socket Socket { get; set; }      //socket de l'objet
        public int id { get; set; }             //id de l'objet
        
        public int serv_id { get; set; }        //id du serveur

        public ClientCom(Socket s, int num)     //initialisation de l'objet
        {
            this.Socket = s;
            this.id = num;
        }
    }
}