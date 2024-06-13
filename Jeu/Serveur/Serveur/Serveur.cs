using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using Lib;

namespace Serveur;

public class Serveur
{
    private static int ID = 0;
    private int joueur_ready = 0;

    protected static string[] info = new string[4];
    private static ClientCom[] clients = new ClientCom[4];
    
    private static string? _seed = "42*42";
    private static Random _rand = new Random();
    private static int _seed2 = _rand.Next(0, 1000000);

    public static string GetInfo()
    {
        return $"in:{info[0]}|{info[1]}|{info[2]}|{info[3]}";
    }

    public static void SendAll(Socket soc, string s)
    {
        for (int i = 0; i < ID; i++)
        {
            UDP.Send(soc, s, clients[i].ep);
        }
    }
    
    public static void TCPAllSend(Socket soc, string s)
    {
        for (int i = 0; i < ID; i++)
        {
            UDP.TCPSend(soc, s, clients[i].ep);
        }
    }
    
    private static void GetSeed()
    {
        StreamReader sr = new StreamReader("../Godot/DayInfo.txt");
        _seed = sr.ReadLine();
        sr.Close();
    }

    public void MainProgram(int n,int nbr)
    {
        GetSeed();
        //Prog pr = (Prog)o;
        Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,ProtocolType.Udp);
        IPEndPoint iep = new IPEndPoint(IPAddress.Any, n);
        soc.Bind(iep); //connection depuis n'importe ou
        
        Console.WriteLine("Serveur en marche");
        Console.WriteLine(nbr);
        Console.WriteLine(ID);
        while (joueur_ready < ID || ID < nbr)
        {
            Console.WriteLine(ID);
            (string s,EndPoint ep) = UDP.FirstReceive(soc);
            if (s == "connect")
            {
                ClientCom clicom = new ClientCom(soc,ID,ep);         //creation de l'objet client
                clients[ID] = clicom;
                
                UDP.Send(soc, ID.ToString() + $"{_seed}*{_seed2}", ep);        //envoie de l'ID au client
                Console.WriteLine("Client connecté : " + ID);
                ID++;
            }
            else
            {
                string[] s2 = s.Split("/");
                Console.WriteLine(s2[1]);
                if (s2[1] == "Archer" || s2[1] == "Scientist" || s2[1] == "Knight" || s2[1] == "Assassin")
                {
                    Console.WriteLine("Client prêt : " + s2[1]);
                    clients[Lib.Conversion.AtoI(s2[0])].classe = s2[1];
                    info[Lib.Conversion.AtoI(s2[0])] = $"{Lib.Conversion.AtoI(s2[0])}/{clients[Lib.Conversion.AtoI(s2[0])].pseudo}/{s2[1]}";
                    joueur_ready++;
                }
                else
                {
                    clients[Lib.Conversion.AtoI(s2[0])].pseudo = s2[1];
                }
            }
        }

        Console.WriteLine("Tout les joueurs sont prêts");
        
        SendAll(soc, $"ready:{ID-1}/{info[0]}/{info[1]}/{info[2]}/{info[3]}");
        
        info = new[] { "-1/co:1;0;1/0;0;0/0/0", "-1/co:-1;0;1/0;0;0/0/0", "-1/co:1;0;-1/0;0;0/0/0", "-1/co:-1;0;1/0;0;0/0/0" };
        
        Wait(soc);
        
        Console.WriteLine("Tout les joueurs sont chargés");
        
        SendAll(soc, "start");

        Thread th = new Thread(GameServeur);
        th.Start(soc);
    }

    private void GameServeur(object o)
    {
        Socket soc = (Socket)o;
        while (ID > 0)
        {
            try
            {
                string s = UDP.Receive(soc);
                string[] s2 = s.Split("_");
                int id = Conversion.AtoI(s2[0]);

                /*if (s2[1].Substring(0, 4) == "tcp:")
            {
                s2[1] = s2[1].Substring(4);
                UDP.Send(soc, "re:" + s2[1], clients[Lib.Conversion.AtoI(s2[0])].ep);
            }*/
                
                string sub = s2[1].Substring(0, 2);
                if (sub == "in")
                {
                    info[id] = s2[0] + "/" + s2[1].Substring(3);
                }
                else if (sub == "on")
                {
                    s2[1] = s2[1].Substring(3);
                    SendAll(soc, $"on:{s2[0]}|{s2[1]}");
                    if (s2[1] == "next")
                    {
                        Wait(soc);
                    }
                    SendAll(soc,"start");
                }
                else if (sub == "an")
                {
                    s2[1] = s2[1].Substring(3);
                    SendAll(soc, $"an:{s2[0]}|{s2[1]}");
                }
                else if (sub == "ia")
                {
                    s2[1] = s2[1].Substring(3);
                    SendAll(soc, $"ia:{s2[0]}|{s2[1]}");
                }
                else if (sub == "bo")
                {
                    s2[1] = s2[1].Substring(3);
                    SendAll(soc, $"bo:{s2[0]}|{s2[1]}");
                }
                else if (s2[1].Substring(0, 4) == "chat")
                {
                    s2[1] = s2[1].Substring(5);
                    SendAll(soc, $"chat:{clients[id].pseudo}: {s2[1]}");

                    Console.WriteLine($"chat:{clients[id].pseudo}: {s2[1]}");
                }
                else if (s2[1] == "deco")
                {
                    Console.WriteLine("deco");
                    info[id] = "-1/co:-1;0;1/0;0;0";

                    switch (id)
                    {
                        case 0:
                            clients[0] = clients[1];
                            clients[1] = clients[2];
                            clients[2] = clients[3];
                            break;
                        case 1:
                            clients[1] = clients[2];
                            clients[2] = clients[3];
                            break;
                        case 2:
                            clients[2] = clients[3];
                            break;
                    }
                    ID -= 1;
                    
                    SendAll(soc, $"deco:{id}");

                    if (ID == 0)
                    {
                        Console.WriteLine("Tout les joueurs sont déconnectés");
                        throw new StopException();
                    }
                }
                else
                {
                    Console.WriteLine(s);
                }

                SendAll(soc, GetInfo());
            }
            catch (StopException)
            {
                Console.WriteLine("fin");
                break;
            }
            catch 
            {
                Console.WriteLine("erreur");
            }
        }
    }

    private void Wait(Socket soc)
    {
        joueur_ready = 0;
        while (joueur_ready < ID)
        {
            string s = UDP.Receive(soc);
            if (s == "load")
            {
                joueur_ready++;
            }
        }
    }
}