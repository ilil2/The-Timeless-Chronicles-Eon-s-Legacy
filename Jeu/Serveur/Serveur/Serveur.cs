using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Serveur;

public class Serveur
{
    private int ID = 0;
    private int joueur_ready = 0;

    private string[] info = new string[4];

    /*public class Prog
    {
        public int n { get; set; }

        public Prog(int n)
        {
            this.n = n;
        }
    }*/

    

    public void MainProgram(int n)
    {
        //Prog pr = (Prog)o;
        Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);
        IPEndPoint iep = new IPEndPoint(IPAddress.Parse("0.0.0.0"), n);
        soc.Bind(iep); //connection depuis n'importe ou
        
        soc.Listen(4); //mise en ecoute du serveur

        Console.WriteLine("Serveur en marche");

        bool inline = true; //variable pour pouvoir desactiver le serveur
        while (inline)
        {
            Console.WriteLine("En attente ...");
            Socket s = soc.Accept();                        //acceptation des nouvelles connection
            ClientCom clicom = new ClientCom(s,ID);         //creation de l'objet client
            ID++;
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

        cc.pseudo = tr.ReadLine();
        cc.classe = tr.ReadLine();
        joueur_ready++;

        while (joueur_ready < ID) {}
        tw.WriteLine("ready");
        tw.Flush();
        
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
                else if (requette.Substring(0,4) == "chat")
                {
                    Console.WriteLine($"{cc.id} : {requette}"); //affichage de la requete recue
                    tw.WriteLine($"vous : {requette}");
                    tw.Flush(); //envoi de la reponse
                }
                else if (requette.Substring(0,2) == "in")
                {
                    string line = requette.Substring(2);
                    string[] lines = line.Split('/');
                    string res = cc.id + "/";
                    
                    foreach (var donnee in lines)
                    {
                        if (donnee.Substring(0,2) == "co")
                        {
                            res += donnee;
                        }
                    }

                    info[cc.id] = res;

                    if (lines[0] == "false")
                    {
                        switch (cc.id)
                        {
                            case 0:
                                tw.WriteLine(info[1] + ";" + info[2] + ";" + info[3]);
                                break;
                            case 1:
                                tw.WriteLine(info[0] + ";" + info[2] + ";" + info[3]);
                                break;
                            case 2:
                                tw.WriteLine(info[0] + ";" + info[1] + ";" + info[3]);
                                break;
                            case 3:
                                tw.WriteLine(info[0] + ";" + info[1] + ";" + info[2]);
                                break;
                        }
                        tw.Flush();
                    }
                    else
                    {
                        tw.WriteLine(info[0] + ";" + info[1] + ";" + info[2] + ";" + info[3]);
                        tw.Flush();
                    }
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
        
        public string pseudo { get; set; }
        
        public string classe { get; set; }

        public ClientCom(Socket s, int id)     //initialisation de l'objet
        {
            this.Socket = s;
            this.id = id;
        }
    }
}