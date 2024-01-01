using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Serveur;

public class Serveur
{
    private int ID;
    private int joueur_ready;

    private string[] info = new string[4];
    private string[] chat = {"","","",""};
    private string[][] oneshot = {new string[] {"","","",""}, new string[] {"","","",""}, new string[] {"","","",""}, new string[] {"","","",""}};

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
    
    private void com(object o) //fonction qui gere un client unique
    {
        ClientCom cc = (ClientCom)o;                        //creation de l'objet client
        NetworkStream ns = new NetworkStream(cc.Socket);    //debut de la connection
        TextReader tr = new StreamReader(ns);               //chaine recue
        TextWriter tw = new StreamWriter(ns);               //chaine a envoyer
        
        Console.WriteLine($"nouveau client : {cc.id} ip : {cc.Socket.RemoteEndPoint}");

        cc.pseudo = tr.ReadLine();
        cc.classe = tr.ReadLine();
        joueur_ready++;
        
        Console.WriteLine($"{cc.pseudo} : ready");

        info[cc.id] = $"{cc.id}/{cc.pseudo}/{cc.classe}";
        
        while (joueur_ready < ID) {}
        
        Thread.Sleep(100);

        switch (cc.id)
        {
            case 0:
                tw.WriteLine($"ready:{ID-1}/{info[1]}/{info[2]}/{info[3]}");
                break;
            case 1:
                tw.WriteLine($"ready:{ID-1}/{info[0]}/{info[2]}/{info[3]}");
                break;
            case 2:
                tw.WriteLine($"ready:{ID-1}/{info[0]}/{info[1]}/{info[3]}");
                break;
            case 3:
                tw.WriteLine($"ready:{ID-1}/{info[0]}/{info[1]}/{info[2]}");
                break;
            default:
                tw.WriteLine("marche po//////");
                break;
        }
        tw.Flush();

        joueur_ready = 0;

        Thread.Sleep(100);
        
        string ready;
        do
        {
            ready = tr.ReadLine();
        } while (ready != "load");

        joueur_ready ++;

        Console.WriteLine("ici");
        while (joueur_ready < ID){}
        Console.WriteLine("la");
        
        Thread.Sleep(100);
        
        tw.WriteLine("start");
        tw.Flush();
        
        info = new[] { "-1/co:1;0;1", "-1/co:-1;0;1", "-1/co:1;0;-1", "-1/co:-1;0;1" };
        
        try
        {
            bool connect = true;        //varriable pour la deconnection du client
            while (connect)             //boucle de connection
            {
                string requette = tr.ReadLine();        //recuperation de la chaine
                //Console.WriteLine($"{cc.pseudo} : {requette}");
                if (requette == "quit")
                {
                    Console.WriteLine($"client {cc.id} s'est deconnecté");
                    cc.Socket.Disconnect(false);                //deconnection du client
                    connect = false;                                      //arret de la boucle
                }
                else if (requette.Substring(0,4) == "chat")
                {
                    requette = requette.Substring(5);
                    
                    chat[0] = $"{cc.pseudo}: {requette}";
                    chat[1] = $"{cc.pseudo}: {requette}";
                    chat[2] = $"{cc.pseudo}: {requette}";
                    chat[3] = $"{cc.pseudo}: {requette}";
                    
                    Console.WriteLine(chat);
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

                    /*if (lines[0] == "false" || true)
                    {
                        switch (cc.id)
                        {
                            case 0:
                                tw.WriteLine("in:" + info[1] + "|" + info[2] + "|" + info[3]);
                                //Console.WriteLine("in:" + info[1] + "|" + info[2] + "|" + info[3]);
                                break;
                            case 1:
                                tw.WriteLine("in:" + info[0] + "|" + info[2] + "|" + info[3]);
                                //Console.WriteLine("in:" + info[0] + "|" + info[2] + "|" + info[3]);
                                break;
                            case 2:
                                tw.WriteLine("in:" + info[0] + "|" + info[1] + "|" + info[3]);
                                //Console.WriteLine("in:" + info[0] + "|" + info[1] + "|" + info[3]);
                                break;
                            case 3:
                                tw.WriteLine("in:" + info[0] + "|" + info[1] + "|" + info[2]);
                                //Console.WriteLine("in:" + info[0] + "|" + info[1] + "|" + info[2]);
                                break;
                        }
                        tw.Flush();
                    }
                    else
                    {
                        tw.WriteLine(info[0] + ";" + info[1] + ";" + info[2] + ";" + info[3]);
                        tw.Flush();
                    }
                    */
                }

                switch (cc.id)
                {
                    case 0:
                        tw.WriteLine("in:" + info[1] + "|" + info[2] + "|" + info[3]);
                        //Console.WriteLine("in:" + info[1] + "|" + info[2] + "|" + info[3]);
                        break;
                    case 1:
                        tw.WriteLine("in:" + info[0] + "|" + info[2] + "|" + info[3]);
                        //Console.WriteLine("in:" + info[0] + "|" + info[2] + "|" + info[3]);
                        break;
                    case 2:
                        tw.WriteLine("in:" + info[0] + "|" + info[1] + "|" + info[3]);
                        //Console.WriteLine("in:" + info[0] + "|" + info[1] + "|" + info[3]);
                        break;
                    case 3:
                        tw.WriteLine("in:" + info[0] + "|" + info[1] + "|" + info[2]);
                        //Console.WriteLine("in:" + info[0] + "|" + info[1] + "|" + info[2]);
                        break;
                }
                tw.Flush();
                
                if (chat[cc.id] != "")
                {
                    tw.WriteLine("chat:"+chat[cc.id]);
                    tw.Flush();
                    chat[cc.id] = "";
                }

                string one;
                
                if ((one = oneshot[cc.id][0]) != "")
                {
                    tw.WriteLine("on:" + cc.id + one);
                    tw.Flush();
                    oneshot[cc.id][0] = "";
                }
                if ((one = oneshot[cc.id][1]) != "")
                {
                    tw.WriteLine("on:" + cc.id + one);
                    tw.Flush();
                    oneshot[cc.id][1] = "";
                }
                if ((one = oneshot[cc.id][2]) != "")
                {
                    tw.WriteLine("on:" + cc.id + one);
                    tw.Flush();
                    oneshot[cc.id][2] = "";
                }
                if ((one = oneshot[cc.id][3]) != "")
                {
                    tw.WriteLine("on:" + cc.id + one);
                    tw.Flush();
                    oneshot[cc.id][3] = "";
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