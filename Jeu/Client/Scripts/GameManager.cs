using Godot;
using System;
using System.IO;

using System.Threading;

using System.Net;
using System.Net.Sockets;

public partial class GameManager : Node3D
{
	private TextReader tr;		//lecture requette serveur principal
	private TextWriter tw;		//ecriture requette serveur principal
	private TextReader tr2;		//lecture requette serveur secondaire
	private TextWriter tw2;		//ecriture requette serveur secondaire
	
	private int port_serv_jeu;	//port serveur secondaire
	private bool conn2 = true;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		try		//voir si le serveur est en ligne
		{
			Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);	//creation du socket
			IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9191);						//adresse + port du serveur principal
			soc.Connect(iep);																			//conexion

			NetworkStream ns = new NetworkStream(soc);
			tw = new StreamWriter(ns);	//lecture requette serveur principal
			tr = new StreamReader(ns);	//ecriture requette serveur principal

			Thread th = new Thread(Listen);		//initialisation thread pour la lecture de requette
			th.Start();							//lancement thread
			
			PackedScene connectionUI = GD.Load<PackedScene>("res://Scene/ConnectionUI.tscn");
			Control connectionMenu = connectionUI.Instantiate<Control>();
			AddChild(connectionMenu);

			bool tentative_connection = true;
			while (tentative_connection)
			{
				if (ConnectionUI.ConnectionButton.ButtonPressed)
				{
					if (ConnectionUI._pseudo != "" && ConnectionUI._password != "" &&
						ConnectionUI._pseudo.Length >= 4 && ConnectionUI._pseudo.Length <= 32 &&
						ConnectionUI._password.Length >= 8 && ConnectionUI._password.Length <= 32)
					{
						tw.WriteLine($"conn:{ConnectionUI._pseudo};{ConnectionUI._password}");

						if (tr.ReadLine() == "connection success")
						{
							tentative_connection = false;
							string user_id = ConnectionUI._pseudo;
						}
					}
				}

				if (ConnectionUI.InscriptionButton.ButtonPressed)
				{
					if (ConnectionUI._password == ConnectionUI._confirm_password)
					{
						if (ConnectionUI._pseudo != "" && ConnectionUI._password != "" &&
							ConnectionUI._pseudo.Length >= 4 && ConnectionUI._pseudo.Length <= 32 &&
							ConnectionUI._password.Length >= 8 && ConnectionUI._password.Length <= 32)
						{
							tw.WriteLine($"insc:{ConnectionUI._pseudo};{ConnectionUI._password}");

							if (tr.ReadLine() == "creation success")
							{
								tentative_connection = false;
								string user_id = ConnectionUI._pseudo;
							}
						}
					}
				}
			}
			
			connectionMenu.Free();
			
			bool conn = true;					//connexion au serveur principal
			bool join = false;					//partie rejointe
			while (conn && conn2)
			{
				if (join == false)
				{
					string? requette = Console.ReadLine();	//requette client
					tw.WriteLine(requette);					//preparation d'envoi au serveur de "requette"
					tw.Flush();								//envoie au serveur
					
					try
					{
						if (requette == "start" || requette.Substring(0, 8) == "joingame")	//si partie rejointe
						{
							join = true;
						}
					}
					catch
					{
						Console.Write("");
					}
				}
			}
			
			th.Interrupt();				//fermeture du thread listen
			ns.Close();
			tw.Close();					//fermeture envoi requette au serveur principal
			tr.Close();					//fermeture recu requette du serveur principal
			soc.Disconnect(false);		//deconnection du socket
			
			
			System.Threading.Thread.Sleep(2000);		//wait 2secondes
			
			Socket soc2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);	//nouveau socket
			IPEndPoint iep2 = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port_serv_jeu);				//nouvelle ip
			soc2.Connect(iep2);																			//connexion
			
			NetworkStream ns2 = new NetworkStream(soc2);
			tw2 = new StreamWriter(ns2);					//lecture serveur secondaire
			tr2 = new StreamReader(ns2);					//ecriture serveur secondaire

			Thread th2 = new Thread(Listen2);	//initialisation thread
			th2.Start();						//debut du thread
		}
		catch
		{
			Console.WriteLine("serveur hors ligne");	//si le serveur est hors ligne
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
