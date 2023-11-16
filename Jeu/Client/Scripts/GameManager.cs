using Godot;
using System;
using System.IO;

using System.Threading;

using Lib;

using System.Net;
using System.Net.Sockets;

public partial class GameManager : Node3D
{
	private Socket soc;
	private IPEndPoint iep;
	
	private Thread th;
	
	private Control connectionMenu;
	
	private NetworkStream ns;
	private TextReader tr;		//lecture requette serveur principal
	private TextWriter tw;		//ecriture requette serveur principal
	private TextReader tr2;		//lecture requette serveur secondaire
	private TextWriter tw2;		//ecriture requette serveur secondaire
	
	private int port_serv_jeu;	//port serveur secondaire
	private bool conn2 = true;


	public static string ConnectionError = "";
	public static string IDGame = "";
	public static bool LobbyReset = false;
	public static bool ValidIDGame = false;
	
	public override void _Ready()
	{
		Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);	//creation du socket
		IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9191);						//adresse + port du serveur principal
		soc.Connect(iep);				//conexion
		
		
		NetworkStream ns = new NetworkStream(soc);
		tw = new StreamWriter(ns);	//lecture requette serveur principal
		tr = new StreamReader(ns);	//ecriture requette serveur principal
		
		Thread th = new Thread(Listen);		//initialisation thread pour la lecture de requette
		th.Start();							//lancement thread
		
		PackedScene connectionUI = GD.Load<PackedScene>("res://Scenes/ConnectionUI.tscn");
		Control connectionMenu = connectionUI.Instantiate<Control>();
		AddChild(connectionMenu);
	}
	
	private bool tentative_connection = true;
	
	private bool conn = true;					//connexion au serveur principal
	private bool join = false;					//partie rejointe
	
	private int state = 0;
	
	//process
	public override void _Process(double delta)
	{
		try		//voir si le serveur est en ligne
		{
			if (state == 0)
			{
				if (tentative_connection)
				{
					if (ConnectionUI.ConnectionButton.ButtonPressed)
					{
						if (ConnectionUI._pseudo.Length >= 4 && ConnectionUI._pseudo.Length <= 16 &&
							ConnectionUI._password.Length >= 8 && ConnectionUI._password.Length <= 32)
						{
							tw.WriteLine($"conn:{ConnectionUI._pseudo};{Hashing.ToSHA256(ConnectionUI._password)}");
							tw.Flush();
							
							string? line = tr.ReadLine();
							if (line == "connection success")
							{
								tentative_connection = false;
								string user_id = ConnectionUI._pseudo;
							}
							else
							{
								ConnectionError = line;
							}
						}
						else
						{
							ConnectionError = "Pseudo ou mot de passe incorrect";
						}
					}

					else if (ConnectionUI.InscriptionButton.ButtonPressed)
					{
						if (ConnectionUI._pseudo.Length >= 4 && ConnectionUI._pseudo.Length <= 32 &&
						    ConnectionUI._password.Length >= 8 && ConnectionUI._password.Length <= 32)
						{
							tw.WriteLine($"insc:{ConnectionUI._pseudo};{Hashing.ToSHA256(ConnectionUI._password)}");
							tw.Flush();
								
							string? line = tr.ReadLine();
							if (line == "creation success")
							{
								tentative_connection = false;
								string user_id = ConnectionUI._pseudo;
							}
							else
							{
								ConnectionError = line;
							}
						}
						else
						{
							ConnectionError = "Pseudo ou mot de passe incorrect";
						}
					}
				}
				
				else
				{
					ConnectionUI.in_connection = false;
					state = 1;
				}
			}

			if (state == 1)
			{
				PackedScene LobbyScene = GD.Load<PackedScene>("res://Scenes/LobbyManager.tscn");
				Control LobbyMenu = LobbyScene.Instantiate<Control>();
				AddChild(LobbyMenu);
				state = 2;
			}
			
			if (state == 2)
			{
				if (conn && conn2)
				{
					if (LobbyManager.CreateButtonPressed == true)
					{
						tw.WriteLine("newgame");					//preparation d'envoi au serveur de "requette"
						tw.Flush();								//envoie au serveur
						LobbyReset = true;
						
						string rep = tr.ReadLine();
						if (rep.Substring(0, 7) == "newgame")
						{
							IDGame = rep.Substring(8);
						}
					}
					
					else if (LobbyManager.JoinGamePressed == true)
					{
						tw.WriteLine($"joingame {LobbyManager.IDJoinGame}");					//preparation d'envoi au serveur de "requette"
						tw.Flush();								//envoie au serveur
						LobbyReset = true;
						
						string rep = tr.ReadLine();
						if (rep.Substring(0) == "join")
						{
							ValidIDGame = true;
						}
					}
				}
				else
				{
					state = 10;
				}
			}
			
			th.Interrupt();				//fermeture du thread listen
			ns.Close();
			tw.Close();					//fermeture envoi requette au serveur principal
			tr.Close();					//fermeture recu requette du serveur principal
			soc.Disconnect(false);		//deconnection du socket
			
			/*
			System.Threading.Thread.Sleep(2000);		//wait 2secondes
			
			Socket soc2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);	//nouveau socket
			IPEndPoint iep2 = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port_serv_jeu);				//nouvelle ip
			soc2.Connect(iep2);																			//connexion
			
			NetworkStream ns2 = new NetworkStream(soc2);
			tw2 = new StreamWriter(ns2);					//lecture serveur secondaire
			tr2 = new StreamReader(ns2);					//ecriture serveur secondaire

			Thread th2 = new Thread(Listen2);	//initialisation thread
			th2.Start();						//debut du thread
			*/
		}
		catch
		{
			//GD.Print("serveur hors ligne");	//si le serveur est hors ligne
			int n = 0;
		}
	}
	
	public void Listen()		//premier thread
	{
		while (true)
		{
			string rep = tr.ReadLine();	//lecture de donnée du serveur
			Console.WriteLine(rep);		//ecriture de la donnee
			if (rep.Contains(":"))
			{
				if (rep.Substring(0, 7) == "newserv")	//si la requette commence par newserv
				{
					port_serv_jeu = Lib.Conversions.AtoI(rep.Substring(8)); //recuperation du nouveau port
					Console.WriteLine("connexion au serveur de jeu ...");
					conn2 = false;											//deconnextion du serveur principal
					break;
				}
			}
		}
	}

	public void Listen2()		//deuxieme thread
	{
		while (true)
		{
			string rep = tr2.ReadLine();
			Console.WriteLine(rep);
		}
	}
}
