using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using System.Net.NetworkInformation;

using Lib;

namespace Serveur;

public class MainServeur
{
    private int ID = 0;
    private List<int> ports_total = new List<int>() {4242,4002,1919,6969,1984};
    private List<int> ports = new List<int>() {4242,4002,1919,6969,1984};
    
    private List<string> id_games = new List<string> {};
    private Dictionary<string,string[]> player_games = new Dictionary<string, string[]>();
    private string start_game = "aaa";

    private List<string> user_ids_csv = new List<string>();
    private List<string> user_passwords_csv = new List<string>();

    private void CSV()
    {
        StreamReader sr = new StreamReader("comptes.csv");

        string? line = sr.ReadLine();
        while (line != null)
        {
            try
            {
                user_ids_csv.Add(line.Substring(0,line.IndexOf(';')));
                user_passwords_csv.Add(line.Substring(line.IndexOf(';')+1));
            }
            catch
            {
                Console.Write("");
            }
            line = sr.ReadLine();
        }
        
        sr.Close();
    }
    
    public void MainProgram()
    {
        Thread ping = new Thread(ping_serv);
        ping.Start();
        
        CSV();
        
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
            ClientCom clicom = new ClientCom(s, ID.ToString(),"");        //creation de l'objet client
            Thread th = new Thread(com);                    //mise en place de la connection
            th.Start(clicom); //demarage de la connection
            
            Thread th2 = new Thread(lunch_game);                    
            th2.Start(clicom);
        }
    }
    
    private void com(object o) //fonction qui gere un client unique
    {
        ClientCom cc = (ClientCom)o;                        //creation de l'objet client
        NetworkStream ns = new NetworkStream(cc.Socket);    //debut de la connection
        TextReader tr = new StreamReader(ns);               //chaine recue
        TextWriter tw = new StreamWriter(ns);               //chaine a envoyer
        
        Console.WriteLine($"nouveau client : {cc.id} ip : {cc.Socket.RemoteEndPoint}");

        string line;
        string? user_id_csv = "";
        string? user_password_csv = "";
        string? new_id_csv = "";
        string? new_password_csv = "";

        bool incorect_conn = true;
        
        while (incorect_conn)
        {
            try
            {
                line = tr.ReadLine();
                Console.WriteLine(line);
                bool error = false;
                if (line.Substring(0, 4) == "conn")
                {
                    if (user_ids_csv.Contains(line.Substring(5, line.IndexOf(';') - 5)))
                    {
                        user_id_csv = line.Substring(5, line.IndexOf(';') - 5);
                    }
                    else
                    {
                        error = true;
                    }

                    if (!error && user_passwords_csv[user_ids_csv.IndexOf(user_id_csv)] == line.Substring(line.IndexOf(';') + 1))
                    {
                        user_password_csv = line.Substring(line.IndexOf(';') + 1);
                    }
                    else
                    {
                        error = true;
                    }

                    if (error)
                    {
                        tw.WriteLine("Pseudo ou mot de passe incorrect");
                        tw.Flush();
                    }
                    else
                    {
                        cc.id = user_id_csv;
                        tw.WriteLine("connection success");
                        tw.Flush();
                        incorect_conn = false;
                        Console.WriteLine("connection effectuée");
                    }
                }
                else
                {
                    new_id_csv = "!";
                    if (user_ids_csv.Contains(line.Substring(5, line.IndexOf(';') - 5)) == false)
                    {
                        new_id_csv = line.Substring(5, line.IndexOf(';') - 5);
                        new_password_csv = line.Substring(line.IndexOf(';') + 1);
                        tw.WriteLine("creation success");
                        tw.Flush();
                        incorect_conn = false;
                        Console.WriteLine("creation effectuée");
                        
                        StreamWriter sw_conn = new StreamWriter("comptes.csv", true);
                        sw_conn.WriteLine($"{new_id_csv};{new_password_csv}");
                        sw_conn.Close();

                        cc.id = new_id_csv;
                        
                        user_ids_csv.Add(new_id_csv);
                        user_passwords_csv.Add(new_password_csv);
                    }
                    else
                    {
                        tw.WriteLine("Compte déjà existant");
                        tw.Flush();
                    }
                }
                tw.Flush();
            }
            catch
            {
                Console.WriteLine("deconnecté pendant connexion");
                incorect_conn = false;
            }
            
        }
        
        
        try
        {
            bool master = false;
            bool join = false;
            bool new_player = false;
            while (true)
            {
                string requette = tr.ReadLine(); 
                if (requette == "start" && master)
                {
                    master = false;
                    
                    StreamWriter sw = new StreamWriter("port.txt"); 
                    Process p1 = new Process();
                    p1.StartInfo.FileName = "bash";
                    p1.StartInfo.Arguments = "exec.sh";
                    //p1.StartInfo.FileName = "execwin.bat";
                    
                    sw.Write(ports[0]);
                    sw.Close();
                    p1.Start(); /* Cette instruction ouvre un invite de commande n°2 */
                    
                    id_games.Remove(cc.game_id);
                    
                    start_game = cc.game_id;
                    join = false;
                    
                    Thread.Sleep(100);
                    
                    ports.Remove(ports[0]);
                }
                else if (requette == "newgame" && join == false)
                {
                    cc.game_id = IDGames.LetterID();
                    id_games.Add(cc.game_id);

                    string[] player_list = new string[] {cc.id,"","",""};
                    player_games.Add(cc.game_id,player_list);
                    cc.in_my_game = player_list;
                    
                    master = true;
                    join = true;
                    new_player = true;
                    
                    Console.WriteLine($"nouvelle game : {cc.game_id} par : {cc.id}");
                    tw.WriteLine($"newgame:{cc.game_id}");
                }

                else if (requette.Contains(' '))
                {
                    if (requette.Substring(0,8) == "joingame" && join == false && id_games.Contains(requette.Substring(9)))
                    {
                        cc.game_id = requette.Substring(9);
                        Console.WriteLine($"{cc.id} a rejoint : {cc.game_id}");
                        tw.WriteLine($"join");

                        //ListManupulation.PrintListOfList(player_games);
                        
                        //int index = ListManupulation.ListofListIndexOf(player_games, 0, cc.game_id);
                        string[] player_list = player_games[cc.game_id];
                        for (int i = 0; i < 4; i++)
                        {
                            if (player_list[i] == "")
                            {
                                player_list[i] = cc.id;
                                break;
                            }
                        }

                        player_games[cc.game_id] = player_list;
                        cc.in_my_game = new string[4];
                        
                        //ListManupulation.PrintListOfList(player_games);
                        
                        join = true;
                        new_player = true;
                    }
                    else
                    {
                        Console.WriteLine($"{cc.id} a tente de rejoindre : {requette.Substring(9)} mais elle n'existe pas");
                        tw.WriteLine("ID inconnu");
                    }
                }
                
                else if (requette == "back")
                {
                    string[] player_list = player_games[cc.game_id];
                    if (player_list[0] == cc.id)
                    {
                        player_games.Remove(cc.id);
                    }
                    else
                    {
                        if (player_list[1] == cc.id)
                        {
                            player_list[1] = player_list[2];
                            player_list[2] = player_list[3];

                        }
                        else if (player_list[2] == cc.id)
                        {
                            player_list[2] = player_list[3];
                        }

                        player_list[3] = "";
                        player_games[cc.game_id] = player_list;
                        Console.WriteLine(player_list[0] + " " + player_list[1]);
                    }

                    master = false;
                    join = false;
                    new_player = false;
                    cc.game_id = "";
                    cc.in_my_game = new string[4];
                }
                
                tw.Flush();
                
                if (requette == "player") //!join && ListManupulation.ListofListContain(player_games,0,cc.game_id)
                {
                    /*for (int i = 0; i < 4; i++)
                    {
                        /*if (ListManupulation.ListofListExist(player_games, ListManupulation.ListofListIndexOf(player_games, 0, cc.game_id), i + 1))
                        {
                            if (cc.in_my_game[i] == player_games[ListManupulation.ListofListIndexOf(player_games, 0, cc.game_id)][i + 1])
                            {
                                cc.in_my_game[i] = player_games[ListManupulation.ListofListIndexOf(player_games, 0, cc.game_id)][i + 1];
                                new_player = true;
                                Console.WriteLine(cc.in_my_game[i]);
                            }
                        }
                        
                    }*/
                    cc.in_my_game = player_games[cc.game_id];
                    new_player = true;
                }
                else
                {
                    Console.WriteLine($"{requette} : {cc.id}");
                }
                
                if (player_games.ContainsKey(cc.game_id) == false)
                {
                    tw.WriteLine("remove");
                    tw.Flush();
                    
                    master = false;
                    join = false;
                    new_player = false;
                    cc.game_id = "";
                    cc.in_my_game = new string[4];
                }
                
                if (new_player)
                {
                    tw.WriteLine($"listplayer:{cc.in_my_game[0]};{cc.in_my_game[1]};{cc.in_my_game[2]};{cc.in_my_game[3]}");
                    new_player = false;
                    tw.Flush();
                }
                
                tw.Flush();
            }
        }
        catch
        {
            Console.WriteLine($"client {cc.id} deconnecté");   //si le client s'est deconnecté de force
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
                tw.WriteLine($"newserv:{ports[0]}");
                    
                Thread.Sleep(3000);
                    
                tw.Flush();
                break;
            }
        }
    }

    public void ping_serv()
    {
        while (true)
        {
            foreach (var portNumber in ports_total)
            {
                TcpListener tcpListener = null;

                try
                {
                    tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), portNumber);
                    tcpListener.Start();
                    if (!ports.Contains(portNumber))
                    {
                        ports.Add(portNumber);
                    }
                }
                finally
                {
                    tcpListener?.Stop();
                }
            }   
            Thread.Sleep(60000);
        }
        
    }

    class ClientCom         //type de l'objet client
    {
        public Socket Socket { get; set; }      //socket de l'objet
        public string id { get; set; }             //id de l'objet
        
        public string game_id { get; set; }        //id du serveur
        
        public string[] in_my_game { get; set; }

        public ClientCom(Socket s, string num,string game_id)     //initialisation de l'objet
        {
            this.Socket = s;
            this.id = num;
            this.game_id = game_id;
        }
    }
}