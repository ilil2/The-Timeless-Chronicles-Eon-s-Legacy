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
	
	protected static int port_serv_jeu;	//port serveur secondaire
	protected static bool conn2 = true;

	public static string IDGame = "";
	public static bool LobbyReset = false;
	public static bool ValidIDGame = false;
	protected static bool OnJoin = false;
	
	protected static DateTime startTime = DateTime.Now;
	protected static TimeSpan timerDuration = TimeSpan.FromSeconds(1);
	protected static DateTime endTime = DateTime.Now;

	public static InputControl InputManger;
	public static LanguageControl LanguageManager;
	public static Settings SettingsManager;
	public static FastConnection FastConnectionManager;
	
	protected static IMap Map;
	protected static Control ProgressBar;
	
	protected static bool tentative_connection = true;
	
	protected static bool conn = true;					//connexion au serveur principal
	private bool join = false;					//partie rejointe
	
	protected static int state = 0;
	
	protected static Socket soc2;
	protected static IPEndPoint iep2;
	
	protected static Thread th2;
	
	public static Dictionary<string, string> InfoJoueur = new Dictionary<string, string>();
	public static Dictionary<string, string> InfoAutreJoueur = new Dictionary<string, string>();

	protected static bool _loadMap = false;

	protected static int _nbJoueur = 0;
	public static CharacterBody3D Joueur1;
	public static CharacterBody3D Joueur2;
	public static CharacterBody3D Joueur3;
	public static CharacterBody3D Joueur4;

	public static Control _chat;

	protected static string IP;
	protected static bool MapOnLoad = false;
	public static bool StartMap = false;
	
	public static bool LockCamera = false;
	public static bool _pausemode = false;
	protected static bool thread;

	protected static bool Cheat = false;
	public static bool DebugMode = false;
	public static bool Fog = true;
	protected static bool Quit = false; 
	protected static int Seed = 42;
	protected static int AleateSeed = 42;
	
	private static string GetIp()
	{
		StreamReader sr = new StreamReader("Scripts/Save/IP.txt");
		string res = sr.ReadLine();
		sr.Close();
		return res;
	}

	
	public override void _Notification(int what)
	{
		if (what == NotificationWMCloseRequest)
		{
			OnQuit();
			GetTree().Quit(); // default behavior
		}
	}
	
	private void OnQuit()
	{
		Quit = true;
		try
		{
			UDP.Send(soc2, $"{InfoJoueur["id"]}_deco", iep2);
		}
		catch
		{
			GD.Print("deco");
		}
		Console.WriteLine("quit");
	}
	
	public override void _Ready()
	{
		//GetTree().Connect("window_close_request", new Callable(this, nameof(OnQuit)));
		
		IP = GetIp();
		LanguageManager = new LanguageControl();
		SettingsManager = new Settings();
		InputManger = new InputControl();
		FastConnectionManager = new FastConnection();
		
		soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);	//creation du socke
		iep = new IPEndPoint(IPAddress.Parse(IP), 9191);						//adresse + port du serveur principal
		soc.Connect(iep);				//conexion
		
		
		ns = new NetworkStream(soc);
		tw = new StreamWriter(ns);	//lecture requette serveur principal
		tr = new StreamReader(ns);	//ecriture requette serveur principal
		
		PackedScene connectionUI = GD.Load<PackedScene>("res://Scenes/UI/ConnectionUI.tscn");
		Control connectionMenu = connectionUI.Instantiate<Control>();
		AddChild(connectionMenu);
		const int lvl = 3;
		PackedScene MapScene = GD.Load<PackedScene>($"res://Scenes/MapScenes/Lvl{lvl}/MapLvl{lvl}.tscn");
		Map = MapScene.Instantiate<IMap>();
		
		PackedScene ChatSceneUI = GD.Load<PackedScene>("res://Scenes/UI/ChatUI.tscn");
		_chat = ChatSceneUI.Instantiate<Control>();
		
		PackedScene ProgressBarMap = GD.Load<PackedScene>("res://Scenes/UI/ProgressBarMapLvl1.tscn");
		ProgressBar = ProgressBarMap.Instantiate<Control>();
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
				PackedScene LobbyScene = GD.Load<PackedScene>("res://Scenes/UI/LobbyManager.tscn");
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
					AddChild(ProgressBar);
					AddChild(Map);
					
					MapOnLoad = true;
					_loadMap = false;
				}
			}
			else if (state == 5)
			{
				State5.State();
				
				AddChild(Joueur1);
				switch (_nbJoueur)
				{
					case 2:
						AddChild(Joueur2);
						break;
					case 3:
						AddChild(Joueur2);
						AddChild(Joueur3);
						break;
					case 4:
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
		while (thread)
		{
			try
			{
				Listen1.Listen();
			}
			catch
			{
				thread = false;
			}
		}
	}

	protected static void Listen2()		//deuxieme thread
	{
		while (true)
		{
			global::Listen2.Listen();
		}
	}
}
