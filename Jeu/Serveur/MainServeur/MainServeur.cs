using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Lib;

namespace Serveur;

public class MainServeur
{
    private int ID = 0;
    private int[] ports = new[] {4242,4002};
    private int nbr_serveur = 0;
    private int nbr_joueur = 0;
    
    private Random rand = new Random();
    private List<int> id_games = new List<int> {};
    private int start_game = -1;
    
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
            ClientCom clicom = new ClientCom(s, ID,0);        //creation de l'objet client
            Thread th = new Thread(com);                    //mise en place de la connection
            th.Start(clicom); //demarage de la connection
            
            Thread th2 = new Thread(lunch_game);                    
            th2.Start(clicom);
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
            bool master = false;
            bool join = false;
            nbr_joueur++;
            while (true)
            {
                string requette = tr.ReadLine(); 
                
                if (requette == "start" && master)
                {
                    tw.WriteLine($"newserv:{ports[nbr_serveur]}");
                    
                    StreamWriter sw = new StreamWriter("port.txt"); 
                    sw.Write(ports[nbr_serveur]);
                    sw.Close();
    
                    Process p1 = new Process();
                    p1.StartInfo.FileName = "bash";
                    p1.StartInfo.Arguments = "exec.sh";
                    p1.Start(); /* Cette instruction ouvre un invite de commande n°2 */
                    
                    Thread.Sleep(5000);
                    
                    start_game = cc.game_id;
                    join = false;
                    id_games.Remove(start_game);
                    tw.Flush();
                    
                    Thread.Sleep(2000);
                    
                    nbr_serveur++;
                    nbr_joueur = 0;
                }
                else if (requette == "newgame" && join == false)
                {
                    cc.game_id = rand.Next(1,1001);
                    id_games.Add(cc.game_id);
                    master = true;
                    Console.WriteLine($"nouvelle game : {cc.game_id} par : {cc.id}");
                    tw.WriteLine($"l'ID de la game : {cc.game_id}");
                    join = false;
                }

                else if (requette.Contains(' '))
                {
                    if (requette.Substring(0,8) == "joingame" && join == false && id_games.Contains(Conversion.AtoI(requette.Substring(9))))
                    {
                        cc.game_id = Conversion.AtoI(requette.Substring(9));
                        Console.WriteLine($"{cc.id} a rejoint : {cc.game_id}");
                        tw.WriteLine($"{cc.game_id} rejoint");
                        join = false;
                    }
                }
                tw.Flush();
            }
        }
        catch
        {
            Console.WriteLine($"client {cc.id} deconnecté de force");   //si le client s'est deconnecté de force
        }
    }

    public void lunch_game(object o)
    {
        ClientCom cc = (ClientCom)o;                        //creation de l'objet client
        NetworkStream ns = new NetworkStream(cc.Socket);    //debut de la connection
        TextWriter tw = new StreamWriter(ns);

        while (true)
        {
            if (cc.game_id == start_game)
            {
                tw.WriteLine($"newserv:{ports[nbr_serveur]}");
                    
                Thread.Sleep(3000);
                    
                tw.Flush();
                break;
            }
        }
    }

    class ClientCom         //type de l'objet client
    {
        public Socket Socket { get; set; }      //socket de l'objet
        public int id { get; set; }             //id de l'objet
        
        public int game_id { get; set; }        //id du serveur

        public ClientCom(Socket s, int num,int game_id)     //initialisation de l'objet
        {
            this.Socket = s;
            this.id = num;
            this.game_id = game_id;
        }
    }
}