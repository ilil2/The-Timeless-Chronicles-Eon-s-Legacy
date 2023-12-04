using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.IO.Compression;
using System.Text;

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

    private void Send(string s,object o)
    {
        ClientCom client = (ClientCom)o;
        byte[] data = Encoding.UTF8.GetBytes(s);
        client.Socket.Send(data, 0, data.Length, SocketFlags.None);
    }

    private string Receive(object o,int i = 1024)
    {
        ClientCom client = (ClientCom)o;
        byte[] buffer = new byte[i];
        int bytesRead = client.Socket.Receive(buffer);
        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        return receivedData;
    }

    public void MainProgram(int n)
    {
        //Prog pr = (Prog)o;
        Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);
        IPEndPoint iep = new IPEndPoint(IPAddress.Parse("0.0.0.0"), n);
        soc.Bind(iep); //connection depuis n'importe ou
        
        soc.Listen(); //mise en ecoute du serveur

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

            if (ID == 4)
            {
                inline = false;
            }
        }
    }
    
    private void com(object o) //fonction qui gere un client unique
    {
        ClientCom cc = (ClientCom)o;                        //creation de l'objet client
        NetworkStream ns = new NetworkStream(cc.Socket);    //debut de la connection
        TextReader tr = new StreamReader(ns);               //chaine recue
        TextWriter tw = new StreamWriter(ns);               //chaine a envoyer
        
        Console.WriteLine($"nouveau client : {cc.id} ip : {cc.Socket.RemoteEndPoint}");

        cc.pseudo = Receive(cc);
        cc.classe = Receive(cc);
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
                string requette = Receive(cc);        //recuperation de la chaine
                //Console.WriteLine($"{cc.pseudo} : {requette}");
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
                        tw.WriteLine(info[0] + ";" + info[1] + ";" + info[2] + ";" + info[3]);
                        tw.Flush();
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
            if (joueur_ready == 0)
            {
                throw new Exception("fermeture du serveur");
            }
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