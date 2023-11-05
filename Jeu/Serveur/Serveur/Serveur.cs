using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Serveur;

public class Serveur
{
    private int ID = 0;

    /*public class Prog
    {
        public int n { get; set; }

        public Prog(int n)
        {
            this.n = n;
        }
    }*/

    public static int AtoI(string? s)
    {
        int res = 0;

        foreach (var c in s)
        {
            if (char.IsNumber(c))
            {
                res += (int)c - 48;
                res *= 10;
            }
            else
            {
                throw new InvalidCastException("veuillez rentrer un nombre en base 10");
            }
        }

        res /= 10;
        return res;
    }

    public void MainProgram()
    {
        //Prog pr = (Prog)o;
        int pr = AtoI(Console.ReadLine());
        Console.WriteLine(pr);
        Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);
        IPEndPoint iep = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 4242);
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
        tw.Flush();     //envoie des données
        
        try
        {
            bool connect = true;        //varriable pour la deconnection du client
            while (connect)             //boucle de connection
            {
                string requette = tr.ReadLine();        //recuperation de la chaine
                if (requette == "quit")
                {
                    Console.WriteLine($"client {cc.id} s'est deconnecté");
                    cc.Socket.Disconnect(false);                //deconnection du client
                    connect = false;                                      //arret de la boucle
                }
                else
                {
                    Console.WriteLine($"{cc.id} : {requette}");         //affichage de la requette recue
                    tw.WriteLine($"vous : {requette}");
                    tw.Flush();                                         //envoie de la reponse
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

        public ClientCom(Socket s, int num)     //initialisation de l'objet
        {
            this.Socket = s;
            this.id = num;
        }
    }
}