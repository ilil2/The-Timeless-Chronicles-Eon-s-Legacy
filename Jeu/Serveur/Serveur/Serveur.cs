using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Serveur;

public class Serveur
{
    private int ID = 0;
    private int joueur_ready = 0;

    protected static string[] info = new string[4];
    private string[] chat = {"","","",""};
    private string[][] oneshot = {new string[] {"","","",""}, new string[] {"","","",""}, new string[] {"","","",""}, new string[] {"","","",""}};
    
    private static ClientCom[] clients = new ClientCom[4];

    public static string GetInfo()
    {
        return $"in:{info[0]}|{info[1]}|{info[2]}|{info[3]}";
    }
    

    public void MainProgram(int n)
    {
        //Prog pr = (Prog)o;
        Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,ProtocolType.Udp);
        IPEndPoint iep = new IPEndPoint(IPAddress.Any, n);
        soc.Bind(iep); //connection depuis n'importe ou
        
        Console.WriteLine("Serveur en marche");

        while (joueur_ready < ID || clients[0] == null)
        {
            (string s,EndPoint ep) = UDP.FirstReceive(soc);
            if (s == "connect" && ID < 4)
            {
                ClientCom clicom = new ClientCom(soc,ID,ep);         //creation de l'objet client
                clients[ID] = clicom;
                
                UDP.Send(soc, ID.ToString(), ep);        //envoie de l'ID au client
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
        for (int i = 0; i < ID; i++)
        {
            UDP.Send(soc, $"ready:{ID-1}/{info[0]}/{info[1]}/{info[2]}/{info[3]}", clients[i].ep);
        }
        
        info = new[] { "-1/co:1;0;1/0;0;0", "-1/co:-1;0;1/0;0;0", "-1/co:1;0;-1/0;0;0", "-1/co:-1;0;1/0;0;0" };
        
        joueur_ready = 0;

        while (joueur_ready < ID)
        {
            string s = UDP.Receive(soc);
            if (s == "load")
            {
                joueur_ready++;
            }
        }
        
        Console.WriteLine("Tout les joueurs sont chargés");
        
        for (int i = 0; i < ID; i++)
        {
            UDP.Send(soc, "start", clients[i].ep);
        }
        
        while (true)
        {
            string s = UDP.Receive(soc);
            string[] s2 = s.Split("_");
            int id = Lib.Conversion.AtoI(s2[0]);

            /*if (s2[1].Substring(0, 4) == "tcp:")
            {
                s2[1] = s2[1].Substring(4);
                UDP.Send(soc, "re:" + s2[1], clients[Lib.Conversion.AtoI(s2[0])].ep);
            }*/
            
            if (s2[1].Substring(0, 2) == "in")
            {
                info[id] = s2[0] + "/" + s2[1].Substring(3);
            }
            else if (s.Substring(0,4) == "chat")
            {
                s = s.Substring(5);
                    
                chat[0] = $"{clients[id].pseudo}: {s}";
                chat[1] = $"{clients[id].pseudo}: {s}";
                chat[2] = $"{clients[id].pseudo}: {s}";
                chat[3] = $"{clients[id].pseudo}: {s}";
                    
                Console.WriteLine(chat);
            }
            else if (s.Substring(0,2) == "on")
            { 
                string res = s.Substring(3);;

                oneshot[0][id] = res;
                oneshot[1][id] = res;
                oneshot[2][id] = res;
                oneshot[3][id] = res;
                oneshot[id][id] = "";
            }

            for (int i = 0; i < ID; i++)
            {
                UDP.Send(soc,GetInfo(),clients[i].ep);
            }
            
            if (chat[id] != "")
            {
                UDP.Send(soc,"chat:"+chat[id],clients[id].ep);
                chat[id] = "";
            }
            
            string one;
                
            if ((one = oneshot[id][0]) != "")
            {
                UDP.Send(soc,"on:" + 0 + "|" + one,clients[id].ep);
                oneshot[id][0] = "";
            }
            if ((one = oneshot[id][1]) != "")
            {
                UDP.Send(soc,"on:" + 1 + "|" + one,clients[id].ep);
                oneshot[id][1] = "";
            }
            if ((one = oneshot[id][2]) != "")
            {
                UDP.Send(soc,"on:" + 2 + "|" + one,clients[id].ep);
                oneshot[id][2] = "";
            }
            if ((one = oneshot[id][3]) != "")
            {
                UDP.Send(soc,"on:" + 3 + "|" + one,clients[id].ep);
                oneshot[id][3] = "";
            }
        }
    }
}