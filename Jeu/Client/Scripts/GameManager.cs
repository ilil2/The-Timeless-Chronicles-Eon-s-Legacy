using Godot;
using System;
using System.IO;

using System.Threading;

using Lib;

using System.Net;
using System.Net.Sockets;
using Godot.Collections;
using JeuClient.Scripts.PlayerScripts;

public partial class GameManager : Node3D
{
	protected static Socket soc;
	private IPEndPoint iep;
	
	protected static Thread th;
	
	private Control connectionMenu;
	
	protected static NetworkStream ns;
	protected static TextReader tr;		//lecture requette serveur principal
	protected static TextWriter tw;		//ecriture requette serveur principal
	protected static TextReader tr2;		//lecture requette serveur secondaire
	protected static TextWriter tw2;		//ecriture requette serveur secondaire
	
	protected static int port_serv_jeu;	//port serveur secondaire
	protected static bool conn2 = true;

	public static string ConnectionError = "";
	public static string IDGame = "";
	public static bool LobbyReset = false;
	public static bool ValidIDGame = false;
	protected static bool OnJoin = false;
	
	protected static DateTime startTime = DateTime.Now;
	protected static TimeSpan timerDuration = TimeSpan.FromSeconds(1);
	protected static DateTime endTime = DateTime.Now;

	public static InputControl InputManger;
	
	protected static Node3D Map;
	
	protected static bool tentative_connection = true;
	
	protected static bool conn = true;					//connexion au serveur principal
	private bool join = false;					//partie rejointe
	
	protected static int state = 0;
	
	protected static Socket soc2;
	protected static IPEndPoint iep2;
	
	protected static NetworkStream ns2;
	protected static Thread th2;
	
	public static Dictionary<string, string> InfoJoueur = new Dictionary<string, string>();
	public static Dictionary<string, string> InfoAutreJoueur = new Dictionary<string, string>();

	protected static bool _loadMap = false;

	protected static int _nbJoueur = 0;
	protected static CharacterBody3D Joueur1;
	protected static CharacterBody3D Joueur2;
	protected static CharacterBody3D Joueur3;
	protected static CharacterBody3D Joueur4;

	protected static Control _chat;

	protected static string IP;
	private static string GetIp()
	{
		StreamReader sr = new StreamReader("Scripts/Save/IP.txt");
		string res = sr.ReadLine();
		sr.Close();
		return res;
	}

	public void Send(int serveur,string s)
	{

		if (serveur == 0)
		{
			tw.WriteLine(s);
			tw.Flush();
		}
		else if (serveur == 1)
		{
			tw2.WriteLine(s);
			tw2.Flush();
		}
		else
		{
			throw new ArgumentException("Erreur de choix du serveur d'envoi");
		}
	}

	public string Receive(int serveur)
	{
		if (serveur == 0)
		{
			return tr.ReadLine();
		}

		if (serveur == 1)
		{
			return tr2.ReadLine();
		}

		throw new ArgumentException("Erreur de choix du serveur de reception");
	}

	protected void Add(Node o)
	{
		AddChild(o);
	}
	
	public override void _Ready()
	{
		IP = GetIp();
		InputManger = new InputControl();
		
		soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);	//creation du socket
		iep = new IPEndPoint(IPAddress.Parse(IP), 9191);						//adresse + port du serveur principal
		soc.Connect(iep);				//conexion
		
		
		ns = new NetworkStream(soc);
		tw = new StreamWriter(ns);	//lecture requette serveur principal
		tr = new StreamReader(ns);	//ecriture requette serveur principal
		
		PackedScene connectionUI = GD.Load<PackedScene>("res://Scenes/ConnectionUI.tscn");
		Control connectionMenu = connectionUI.Instantiate<Control>();
		AddChild(connectionMenu);
		
		PackedScene MapScene = GD.Load<PackedScene>("res://Scenes/MapScenes/Lvl1/MapLvl1.tscn");
		Map = MapScene.Instantiate<Node3D>();
		
		PackedScene ChatSceneUI = GD.Load<PackedScene>("res://Scenes/UI/ChatUI.tscn");
		_chat = ChatSceneUI.Instantiate<Control>();
	}
	
	//process
	public override void _Process(double delta)
	{
		try		//voir si le serveur est en ligne
		{
			if (state == 0)
			{
				State0.State();
			}

			else if (state == 1)
			{
				PackedScene LobbyScene = GD.Load<PackedScene>("res://Scenes/LobbyManager.tscn");
				Control LobbyMenu = LobbyScene.Instantiate<Control>();
				AddChild(LobbyMenu);
				State1.State();
			}
			
			else if (state == 2)
			{
				State2.State();
			}

			else if (state == 3)
			{
				State3.State();
				PackedScene ClassSelectUI = GD.Load<PackedScene>("res://Scenes/UI/ClassSelectUI.tscn");
				Control ClassSelect = ClassSelectUI.Instantiate<Control>();
				AddChild(ClassSelect);
			}
			
			else if (state == 4)
			{
				State4.State();
				
				if (_loadMap)
				{
					AddChild(Map);
					
					_loadMap = false;
				}
			}
			else if (state == 5)
			{
				State5.State();
				
				AddChild(Joueur1);
				switch (_nbJoueur)
				{
					case 1:
						AddChild(Joueur2);
						break;
					case 2:
						AddChild(Joueur2);
						AddChild(Joueur3);
						break;
					case 3:
						AddChild(Joueur2);
						AddChild(Joueur3);
						AddChild(Joueur4);
						break;
				}
				AddChild(_chat);
			}
			
			else if (state == 6)
			{
				State6.State(delta);
			}
			
		}
		catch (Exception e)
		{
			//GD.Print("serveur hors ligne");	//si le serveur est hors ligne
			GD.Print(e);
		}
	}
	
	protected static void Listen()		//premier thread
	{
		while (true)
		{
			string? rep = tr.ReadLine();	//lecture de donnée du serveur
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

	protected static void Listen2()		//deuxieme thread
	{
		while (true)
		{
			string rep = tr2.ReadLine();
			if (rep.Length > 5 && rep.Substring(0,5) == "ready")
			{
				rep = rep.Substring(6);
				string[] InfoReady = rep.Split("/");
				for (int i = 1; i < Conversions.AtoI(InfoReady[0]) * 3 + 1; i += 3)
				{
					if (InfoReady[i] == "")
					{
						i -= 3;
					}
					else
					{
						InfoAutreJoueur[$"id{InfoReady[i]}"] = InfoReady[i];
						InfoAutreJoueur[$"pseudo{InfoReady[i]}"] = InfoReady[i + 1];
						InfoAutreJoueur[$"class{InfoReady[i]}"] = InfoReady[i + 2];
					}
				}
				_nbJoueur = Conversions.AtoI(InfoReady[0]) + 1;

				switch (_nbJoueur)
				{
					case 1:
						InfoJoueur["id"] = "0";
						break;
					case 2:
						if (!InfoAutreJoueur.ContainsKey("id0"))
						{
							InfoJoueur["id"] = "0";
						}
						else
						{
							InfoJoueur["id"] = "1";
						}
						break;
					case 3:
						if (!InfoAutreJoueur.ContainsKey("id0"))
						{
							InfoJoueur["id"] = "0";
						}
						else if (!InfoAutreJoueur.ContainsKey("id1"))
						{
							InfoJoueur["id"] = "1";
						}
						else
						{
							InfoJoueur["id"] = "2";
						}
						break;
					case 4:
						if (!InfoAutreJoueur.ContainsKey("id0"))
						{
							InfoJoueur["id"] = "0";
						}
						else if (!InfoAutreJoueur.ContainsKey("id1"))
						{
							InfoJoueur["id"] = "1";
						}
						else if (!InfoAutreJoueur.ContainsKey("id2"))
						{
							InfoJoueur["id"] = "2";
						}
						else
						{
							InfoJoueur["id"] = "3";
						}
						break;
				}
				
				ClassSelectUI.Supr = true;
				_loadMap = true;
			}
			else if (rep.Length > 2 && rep.Substring(0,2) == "in")
			{
				string line = rep.Substring(3);
				string[] SplitInfo = line.Split('|');
				for (int i = 0; i < 3; i++)
				{
					string[] CoordInfo = SplitInfo[i].Split('/');
					if (CoordInfo[1] != "deco")
					{
						CoordInfo[1] = CoordInfo[1].Substring(3);
						InfoAutreJoueur[$"co{CoordInfo[0]}"] = CoordInfo[1];
					}
					else
					{
						InfoAutreJoueur[$"co{CoordInfo[0]}"] = "0;-3;0";
					}
				}
			}
			
			else if (rep.Length > 4 && rep.Substring(0,4) == "chat")
			{
				rep = rep.Substring(5);
				if (rep.Length > InfoJoueur["pseudo"].Length && rep.Substring(0,InfoJoueur["pseudo"].Length) == InfoJoueur["pseudo"])
				{
					rep = "vous" + rep.Substring(InfoJoueur["pseudo"].Length);
				}
				((ChatUI)_chat).Outputaddtext = rep;
			}
		}
	}
}
