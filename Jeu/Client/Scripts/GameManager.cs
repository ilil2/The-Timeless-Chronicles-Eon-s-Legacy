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
	private bool OnJoin = false;
	
	private DateTime startTime = DateTime.Now;

	private TimeSpan timerDuration = TimeSpan.FromSeconds(1);

	private DateTime endTime = DateTime.Now;
	
	public override void _Ready()
	{
		Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);	//creation du socket
		IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9191);						//adresse + port du serveur principal
		soc.Connect(iep);				//conexion
		
		
		NetworkStream ns = new NetworkStream(soc);
		tw = new StreamWriter(ns);	//lecture requette serveur principal
		tr = new StreamReader(ns);	//ecriture requette serveur principal
		
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
							
							string line = tr.ReadLine();
							if (line == "creation success")
							{
								tentative_connection = false;
								string user_id = ConnectionUI._pseudo;
							}
							else
							{
								ConnectionError = "Pseudo deja existant";
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
					if (LobbyManager.CreateButtonPressed)
					{
						tw.WriteLine("newgame");					//preparation d'envoi au serveur de "requette"
						tw.Flush();								//envoie au serveur
						LobbyReset = true;
						
						string rep = tr.ReadLine();
						if (rep.Substring(0, 7) == "newgame")
						{
							IDGame = rep.Substring(8);
							OnJoin = true;
							th = new Thread(Listen);		//initialisation thread pour la lecture de requette
							th.Start();							//lancement thread
							LobbyReset = true;
						}
					}
					
					else if (LobbyManager.JoinGamePressed)
					{
						tw.WriteLine($"joingame {LobbyManager.IDJoinGame}");					//preparation d'envoi au serveur de "requette"
						tw.Flush();								//envoie au serveur
						LobbyReset = true;
						
						string rep = tr.ReadLine();
						if (rep.Substring(0) == "join")
						{
							ValidIDGame = true; 
							
							IDGame = LobbyManager.IDJoinGame;
							OnJoin = true;
							th = new Thread(Listen);		//initialisation thread pour la lecture de requette
							th.Start();							//lancement thread
							LobbyReset = true;
						}
						else
						{
							LobbyManager.IDError = rep;
						}
						
					}
					if (LobbyManager.BackButtonPressed && OnJoin)
					{
						GD.Print("back");
						LobbyReset = true;
						OnJoin = false;
						th.Interrupt();
						tw.WriteLine("back");
						tw.Flush();
					}
					
					if (OnJoin && DateTime.Now > endTime)
					{
						tw.WriteLine("player");
						tw.Flush();
						startTime = DateTime.Now;
					}
					endTime = startTime.Add(timerDuration);
				}
				else
				{
					LobbyManager.InRunning = false;
					state = 10;
				}
			}

			if (state == 3)
			{
					if (OnJoin)
				{
					th.Interrupt();				//fermeture du thread listen
				}
				ns.Close();
				tw.Close();					//fermeture envoi requette au serveur principal
				tr.Close();					//fermeture recu requette du serveur principal
				soc.Disconnect(false);		//deconnection du socket
				
			}
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
	
	private void Listen()		//premier thread
	{
		while (true)
		{
			string rep = tr.ReadLine();	//lecture de donnée du serveur
			if (rep.Contains(":"))
			{
				if (rep.Substring(0, 7) == "newserv")	//si la requette commence par newserv
				{
					port_serv_jeu = Lib.Conversions.AtoI(rep.Substring(8)); //recuperation du nouveau port
					Console.WriteLine("connexion au serveur de jeu ...");
					conn2 = false;											//deconnextion du serveur principal
					break;
				}
				
				else if (rep.Substring(0,10) == "listplayer")
				{
					string line = rep.Substring(11);
					string[] SplitPseudo = line.Split(';');
					(LobbyManager.NamePlayer[0],LobbyManager.NamePlayer[1],LobbyManager.NamePlayer[2],LobbyManager.NamePlayer[3]) = (SplitPseudo[0],SplitPseudo[1],SplitPseudo[2],SplitPseudo[3]);
				}
			}
			else if (rep == "remove")
			{
				LobbyReset = true;
				LobbyManager.kill = true;
			}
		}
	}

	private void Listen2()		//deuxieme thread
	{
		while (true)
		{
			string rep = tr2.ReadLine();
			Console.WriteLine(rep);
		}
	}
}