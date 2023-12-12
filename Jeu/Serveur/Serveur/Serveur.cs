using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Text;

namespace Serveur;

public class Serveur
{
    private int ID;
    private int joueur_ready;

    private string[] info = new string[4];
    private List<ClientCom> clients = new List<ClientCom>();

    /*public class Prog
    {
        public int n { get; set; }

        public Prog(int n)
        {
            this.n = n;
        }
    }*/

    public static bool ListOfObjectContainIP(List<ClientCom> list,IPAddress ip)
    {
        foreach (var cc in list)
        {
            ClientCom client = (ClientCom)cc;
            if (client.IP.Equals(ip))
            {
                return true;
            }
        }

        return false;
    }

    private static (EndPoint,IPAddress,string) Receive(Socket soc)
    {
        byte[] buffer = new byte[1024];
        EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0); // Stockera l'adresse du client qui envoie le message
        
        // Recevoir des données
        int bytesRead = soc.ReceiveFrom(buffer, ref remoteEndPoint);
        
        IPEndPoint clientIPEndPoint = (IPEndPoint)remoteEndPoint;
        IPAddress clientIPAddress = clientIPEndPoint.Address;

        // Convertir les données en chaîne
        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        return (remoteEndPoint,clientIPAddress,receivedData);
    }

    private static void Send(string message, ClientCom cc)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);

        // Envoyer les données au serveur
        cc.Socket.SendTo(data, cc.Rep);
    }

    private static ClientCom Client(List<ClientCom> clients,IPAddress ip)
    {
        switch (clients.Count)
        {
            case 1:
                return clients[0];
            case 2:
                if (clients[0].IP.Equals(ip))
                {
                    return clients[0];
                }

                return clients[1];
            case 3:
                if (clients[0].IP.Equals(ip))
                {
                    return clients[0];
                }
                if (clients[2].IP.Equals(ip))
                {
                    return clients[2];
                }

                return clients[1];
            case 4:
                if (clients[0].IP.Equals(ip))
                {
                    return clients[0];
                }
                if (clients[2].IP.Equals(ip))
                {
                    return clients[2];
                }
                if (clients[3].IP.Equals(ip))
                {
                    return clients[3];
                }

                return clients[1];
            default:
                throw new ArgumentException("Error List");
        }
    }

    public void MainProgram(int n)
    {
        //Prog pr = (Prog)o;
        Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,ProtocolType.Udp);
        IPEndPoint iep = new IPEndPoint(IPAddress.Any, n);
        soc.Bind(iep); //connection depuis n'importe ou
        
        //soc.Listen(4); //mise en ecoute du serveur

        Console.WriteLine("Serveur en marche");

        bool inline = true; //variable pour pouvoir desactiver le serveur
        while (inline)
        {
            //Console.WriteLine("En attente ...");
            //Socket s = soc.Accept();                        //acceptation des nouvelles connection

            try
            {
                (EndPoint rep, IPAddress ip, string s) = Receive(soc);
                if (!ListOfObjectContainIP(clients, ip) && clients.Count < 4)
                {
                    ClientCom clicom = new ClientCom(soc, ip, rep, ID, s); //creation de l'objet client
                    clients.Add(clicom);
                    ID++;
                    Thread th = new Thread(com); //mise en place de la connection
                    th.Start(clicom); //demarage de la connection
                }
                else if (ListOfObjectContainIP(clients, ip))
                {
                    ClientCom client = Client(clients, ip);
                    client.requette = s;
                }
            }
            catch
            {
                Console.WriteLine("Erreur Main Program");
            }
        }
    }
    
    private void com(object o) //fonction qui gere un client unique
    {
        ClientCom cc = (ClientCom)o;                        //creation de l'objet client
        //NetworkStream ns = new NetworkStream(cc.Socket);    //debut de la connection
        //TextReader tr = new StreamReader(ns);               //chaine recue
        //TextWriter tw = new StreamWriter(ns);               //chaine a envoyer
        
        Console.WriteLine($"nouveau client : {cc.id} ip : {cc.IP.Address}");

        while (cc.requette == "") {}
        cc.pseudo = cc.requette; 
        Console.WriteLine(cc.requette);
        cc.requette = "";
        
        while (cc.requette == "") {}
        cc.classe = cc.requette;
        Console.WriteLine(cc.requette);
        cc.requette = "";
        
        joueur_ready++;
        
        Console.WriteLine($"{cc.pseudo} : ready");

        info[cc.id] = $"{cc.id}/{cc.pseudo}/{cc.classe}";
        
        while (joueur_ready < ID) {}
        
        Thread.Sleep(100);

        switch (cc.id)
        {
            case 0:
                Send($"ready:{ID-1}/{info[1]}/{info[2]}/{info[3]}",cc);
                break;
            case 1:
                Send($"ready:{ID-1}/{info[0]}/{info[2]}/{info[3]}",cc);
                break;
            case 2:
                Send($"ready:{ID-1}/{info[0]}/{info[1]}/{info[3]}",cc);
                break;
            case 3:
                Send($"ready:{ID-1}/{info[0]}/{info[1]}/{info[2]}",cc);
                break;
            default:
                Send("marche po//////",cc);
                break;
        }

        Thread.Sleep(100);
        
        info = new[] { "-1/co:1;0;1", "-1/co:-1;0;1", "-1/co:1;0;-1", "-1/co:-1;0;1" };
        
        try
        {
            bool connect = true;        //varriable pour la deconnection du client
            while (connect)             //boucle de connection
            {
                string requette = cc.requette;        //recuperation de la chaine
                //Console.WriteLine($"{cc.pseudo} : {requette}");
                if (requette == "quit")
                {
                    Console.WriteLine($"client {cc.id} s'est deconnecté");
                    cc.Socket.Disconnect(false);                //deconnection du client
                    connect = false;                                      //arret de la boucle
                }
                else if (requette.Length > 3 && requette.Substring(0,4) == "chat")
                {
                    Console.WriteLine($"{cc.id} : {requette}"); //affichage de la requete recue
                    Send($"vous : {requette}",cc);
                }
                else if (requette.Length > 1 && requette.Substring(0,2) == "in")
                {
                    string line = requette.Substring(3);
                    string[] lines = line.Split('/');
                    string res = cc.id + "/" + line;
                    
                    /*foreach (var donnee in lines)
                    {
                        if (donnee.Substring(0,2) == "co")
                        {
                            res += donnee;
                            string don = donnee.Substring(3);
                            res += don.Substring(0,don.IndexOf(";"));
                            don = donnee.Substring(3);
                            res += don.Substring(0,don.IndexOf(";"));
                            don = donnee.Substring(3);
                            res += don.Substring(0,don.IndexOf(";"));
                        }

                        res += "/";
                    }*/

                    info[cc.id] = res;  

                    if (lines[0] == "false" || true)
                    {
                        switch (cc.id)
                        {
                            case 0:
                                Send("in:" + info[1] + "|" + info[2] + "|" + info[3],cc);
                                //Console.WriteLine("in:" + info[1] + "|" + info[2] + "|" + info[3]);
                                break;
                            case 1:
                                Send("in:" + info[0] + "|" + info[2] + "|" + info[3],cc);
                                //Console.WriteLine("in:" + info[0] + "|" + info[2] + "|" + info[3]);
                                break;
                            case 2:
                                Send("in:" + info[0] + "|" + info[1] + "|" + info[3],cc);
                                //Console.WriteLine("in:" + info[0] + "|" + info[1] + "|" + info[3]);
                                break;
                            case 3:
                                Send("in:" + info[0] + "|" + info[1] + "|" + info[2],cc);
                                //Console.WriteLine("in:" + info[0] + "|" + info[1] + "|" + info[2]);
                                break;
                        }
                    }
                    else
                    {
                        Send(info[0] + ";" + info[1] + ";" + info[2] + ";" + info[3],cc);
                    }
                }
            }
        }
        catch
        {
            //Console.WriteLine(e);
            //throw new Exception();
            Console.WriteLine($"client {cc.id} deconnecté de force");   //si le client s'est deconnecté de force
            joueur_ready -= 1;
            info[cc.id] = $"{cc.id}/deco";
            if (joueur_ready == 0)
            {
                throw new Exception("fermeture du serveur");
            }
        }
    }
}