using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using System.Threading;

using Lib;

using System.Net;
using System.Net.Sockets;
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
	
	public static IMap Map;
	protected static Control ProgressBar;
	
	protected static bool tentative_connection = true;
	
	protected static bool conn = true;					//connexion au serveur principal
	private bool join = false;					//partie rejointe
	
	public static int state = -1;

	private static int _xp = 0;
	private static int _qxp = 0;
	public static int xp
	{
		get => _xp;
		set => _qxp=value;
	}

	private static int _gold = 5000;
	private static int _qgold = 5000;

	public static int Gold
	{
		get => _gold;
		set
		{
			GD.Print($"set gold {_gold} => {value}");
			_qgold = value;
		}
	}
	
	public static int levelStart = 0;
	public static int level
	{
		get
		{
			if (xp / 100 > 5)
			{
				return 5;
			}
			else
			{
				return xp / 100;
			}
		}
	}
	
	//Variables de joueur
	public static (string, int)[] Skills = new (string, int)[3];
	public static int Skillnumber = 0;
	public static int MaxHealth = 100;
	public static int Health = 100;
	public static int MaxStamina = 1000;
	public static int CurrentHealth = 100;
	public static int CurrentMaxHealth = 100;
	public static int Stamina = 1000;
	public static int ManaUse = 50;
	public static int Damage = 10;
	public static int CriticalChance = 20;
	public static int ChargeSpeed = 7;
	public static int HealSpeed = 10;
	public static float ShootSpeed = 0.015f;
	public static int NbArrow = 1;
	public static int DashDegat = 0;
	
	public static float WalkSpeed = 4.2f;
	public static float CurrentWalkSpeed = 4.2f;
	public static float RunSpeed = 7.5f;
	public static float CurrentRunSpeed = 7.5f;

	public static Socket soc2;
	public static IPEndPoint iep2;
	
	protected static Thread th2;
	
	public static Dictionary<string, string> InfoJoueur = new Dictionary<string, string>();
	public static Dictionary<string, string> InfoAutreJoueur = new Dictionary<string, string>();

	protected static bool _loadMap = false;

	public static int _nbJoueur = 0;
	public static CharacterBody3D Joueur1;
	public static CharacterBody3D Joueur2;
	public static CharacterBody3D Joueur3;
	public static CharacterBody3D Joueur4;
	public static List<CharacterBody3D> ListJoueur;

	public static Control _chat;
	private Control _GameHud;

	protected static string IP;
	protected static bool MapOnLoad = false;
	public static bool StartMap = false;
	
	public static bool LockCamera = false;
	public static bool _pausemode = false;
	protected static bool thread;

	protected static bool Cheat = false;
	public static bool DebugMode = false;
	public static bool CDisplay = false;
	public static bool Fog = true;
	protected static bool Quit = false; 
	protected static int Seed = 42;
	protected static int AleateSeed = 42;
	
	protected static Queue<IMap> Lvls = new Queue<IMap>();
	
	private static string GetIp()
	{
		StreamReader sr = new StreamReader("Scripts/Save/IP.txt");
		string res = sr.ReadLine();
		sr.Close();
		if (char.IsLetter(res[0]))
		{
			IPAddress[] addresses = Dns.GetHostAddresses(res);
			IPAddress firstAddress = addresses[0];
			res = firstAddress.ToString();
		}
		return res;
	}

	
	public override void _Notification(int what)
	{
		if (what == NotificationWMCloseRequest || what == NotificationCrash)
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
	
	private void EnqueueMap(string path)
	{
		PackedScene MapScene = GD.Load<PackedScene>(path);
		Map = MapScene.Instantiate<IMap>();
		Lvls.Enqueue(Map);
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
		EnqueueMap($"res://Scenes/MapScenes/Shop.tscn");
		for (int i = 1; i <= 3; i++)
		{
			EnqueueMap($"res://Scenes/MapScenes/Lvl{i}/MapLvl{i}.tscn");
			EnqueueMap($"res://Scenes/MapScenes/Lvl{i}/BossScenes/Boss{i}Map.tscn");
			EnqueueMap($"res://Scenes/MapScenes/Shop.tscn");
		}

		Map = Lvls.Dequeue();
		
		PackedScene ChatSceneUI = GD.Load<PackedScene>("res://Scenes/UI/ChatUI.tscn");
		_chat = ChatSceneUI.Instantiate<Control>();
		
		PackedScene ProgressBarMap = GD.Load<PackedScene>("res://Scenes/UI/ProgressBarMapLvl1.tscn");
		ProgressBar = ProgressBarMap.Instantiate<Control>();
		
		PackedScene GameHud = GD.Load<PackedScene>("res://Scenes/HUD/GameHUD.tscn");
		_GameHud = GameHud.Instantiate<Control>();

		PackedScene Music = GD.Load<PackedScene>("res://Scenes/SoundManager.tscn");
		AddChild(Music.Instantiate<Node>());
		
		PackedScene MainScreenScene = GD.Load<PackedScene>("res://Scenes/UI/MainScreen.tscn");
		Control MainScreen = MainScreenScene.Instantiate<Control>();
		AddChild(MainScreen);
	}

	public static void UpdateXpAndGold()
	{
		if(_qxp>_xp)
		{
				_xp++;
			
		}
		else if(_qxp<_xp)
		{
				_xp--;
			
		}
		if(_qgold>_gold)
		{
			if(_qgold-_gold>100)
			{
				_gold+=100;
			}
			else if(_qgold-_gold>10)
			{
				_gold+=10;
			}
			else
			{
				_gold++;
			}
			
		}
		else if(_qgold<_gold)
		{
			if(-(_qgold-_gold)>100)
			{
				_gold-=100;
			}
			else if(-(_qxp-_gold)>10)
			{
				_gold-=10;
			}
			else
			{
				_gold--;
			}
			
		}
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
					AddChild(_chat);
					_chat.Visible = false;
					
					MapOnLoad = true;
					_loadMap = false;
				}
			}
			else if (state == 5)
			{
				State5.State();
				ListJoueur = new List<CharacterBody3D>{
					Joueur1,Joueur2,Joueur3,Joueur4
				};	
				AddChild(Joueur1);
				AddChild(_GameHud);
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
				Joueur1.GetNode<Camera3D>("CameraPlayer/h/v/Camera3D").Current = true;
				
				_chat.Visible = true;
				_GameHud.Visible = true;
			}
			
			else if (state == 6)
			{
				State6.State(delta);
				UpdateXpAndGold();
			}
			
			else if (state == 7)
			{
				if (_loadMap)
				{
					Map.QueueFree();
					RemoveChild(Joueur1);
					switch (_nbJoueur)
					{
						case 2:
							RemoveChild(Joueur2);
							break;
						case 3:
							RemoveChild(Joueur2);
							RemoveChild(Joueur3);
							break;
						case 4:
							RemoveChild(Joueur2);
							RemoveChild(Joueur3);
							RemoveChild(Joueur4);
							break;
					}
					_chat.Visible = false;
					
					PackedScene ProgressBarMap = GD.Load<PackedScene>("res://Scenes/UI/ProgressBarMapLvl1.tscn");
					ProgressBar = ProgressBarMap.Instantiate<Control>();
					AddChild(ProgressBar);
					Map = Lvls.Dequeue();
					Map.SetSeed(Seed,AleateSeed);
					AddChild(Map);
					
					MapOnLoad = true;
					_loadMap = false;
					
				}
				State4.State();
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
