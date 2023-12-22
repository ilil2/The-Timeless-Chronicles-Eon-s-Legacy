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
	private Socket soc;
	private IPEndPoint iep;
	
	private Thread th;
	
	private Control connectionMenu;
	
	private static NetworkStream ns;
	private static TextReader tr;		//lecture requette serveur principal
	private static TextWriter tw;		//ecriture requette serveur principal
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

	public static InputControl InputManger;
	
	private Node3D Map;
	
	private bool tentative_connection = true;
	
	private bool conn = true;					//connexion au serveur principal
	private bool join = false;					//partie rejointe
	
	private int state = 0;
	
	private Socket soc2;
	private IPEndPoint iep2;
	
	private NetworkStream ns2;
	private Thread th2;
	
	public static Dictionary<string, string> InfoJoueur = new Dictionary<string, string>();
	public static Dictionary<string, string> InfoAutreJoueur = new Dictionary<string, string>();

	private bool _loadMap = false;

	private int _nbJoueur = 0;
	private CharacterBody3D Joueur1;
	private CharacterBody3D Joueur2;
	private CharacterBody3D Joueur3;
	private CharacterBody3D Joueur4;

	private string IP;
	private static string GetIp()
	{
		StreamReader sr = new StreamReader("Scripts/Save/IP.txt");
		string res = sr.ReadLine();
		sr.Close();
		return res;
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
	}
	
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
								InfoJoueur["pseudo"] = ConnectionUI._pseudo;
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
								InfoJoueur["pseudo"] = ConnectionUI._pseudo;
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

			else if (state == 1)
			{
				PackedScene LobbyScene = GD.Load<PackedScene>("res://Scenes/LobbyManager.tscn");
				Control LobbyMenu = LobbyScene.Instantiate<Control>();
				AddChild(LobbyMenu);
				state = 2;
			}
			
			else if (state == 2)
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
					else if (LobbyManager.StartGame)
					{
						LobbyReset = true;
						tw.WriteLine("start");					//preparation d'envoi au serveur de "requette"
						tw.Flush();								//envoie au serveur	
						Thread.Sleep(2000);
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
					state = 3;
				}
			}

			else if (state == 3)
			{
				if (OnJoin)
				{
					th.Interrupt();				//fermeture du thread listen
				}
				ns.Close();
				tw.Close();					//fermeture envoi requette au serveur principal
				tr.Close();					//fermeture recu requette du serveur principal
				soc.Disconnect(false);		//deconnection du socket
				state = 4;
				
				soc2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);	//nouveau socket
				iep2 = new IPEndPoint(IPAddress.Parse(IP), port_serv_jeu);				//nouvelle ip
				soc2.Connect(iep2);																			//connexion
			
				ns2 = new NetworkStream(soc2);
				tw2 = new StreamWriter(ns2);					//lecture serveur secondaire
				tr2 = new StreamReader(ns2);					//ecriture serveur secondaire
				
				tw2.WriteLine(InfoJoueur["pseudo"]);
				tw2.Flush();
				
				PackedScene ClassSelectUI = GD.Load<PackedScene>("res://Scenes/UI/ClassSelectUI.tscn");
				Control ClassSelect = ClassSelectUI.Instantiate<Control>();
				AddChild(ClassSelect);

				th2 = new Thread(Listen2);	//initialisation thread
				th2.Start();						//debut du thread
			}
			
			else if (state == 4)
			{
				if (ClassSelectUI.ClassChose != "")
				{
					InfoJoueur["class"] = ClassSelectUI.ClassChose;
					ClassSelectUI.ClassChose = "";
					tw2.WriteLine(InfoJoueur["class"]);
					tw2.Flush();
				}
				else if (_loadMap)
				{
					AddChild(Map);
					
					_loadMap = false;
				}
				else if (((MapLvl1Script)Map).MapIsReady())
				{
					state = 5;
				}
			}
			else if (state == 5)
			{
				(int x, int z) = ((MapLvl1Script)Map).GetSpawnLocation();
				Random rand = new Random();
				
				PackedScene SceneJoueur1 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/{InfoJoueur["class"]}.tscn");
				Joueur1 = SceneJoueur1.Instantiate<CharacterBody3D>();
				Joueur1.Name = "Joueur1";
				Joueur1.Position = new Vector3(x + rand.Next(-6,6),0,z + rand.Next(-6,6));
				InfoJoueur["co"] = "0;0;0";
				AddChild(Joueur1);
				
				switch (_nbJoueur)
				{
					case 2:
						if (InfoJoueur["id"] == "1")
						{
							PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class0"]}.tscn");
							Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
							Joueur2.Name = "Joueur2";
							InfoAutreJoueur["co0"] = "0;0;0";
							((OtherClassScript)Joueur2).SetID(0);
							((OtherClassScript)Joueur2).SetClasse(InfoAutreJoueur["class0"]);
							AddChild(Joueur2);
						}
						else
						{
							PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class1"]}.tscn");
							Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
							Joueur2.Name = "Joueur2";
							InfoAutreJoueur["co1"] = "0;0;0";
							((OtherClassScript)Joueur2).SetID(1);
							((OtherClassScript)Joueur2).SetClasse(InfoAutreJoueur["class1"]);
							AddChild(Joueur2);
						}
						break;
					case 3:
						if (InfoJoueur["id"] == "1")
						{
							PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class0"]}.tscn");
							Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
							Joueur2.Name = "Joueur2";
							InfoAutreJoueur["co0"] = "0;0;0";
							AddChild(Joueur2);
							
							PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class2"]}.tscn");
							Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
							Joueur3.Name = "Joueur3";
							InfoAutreJoueur["co2"] = "0;0;0";
							AddChild(Joueur3);
						}
						else if (InfoJoueur["id"] == "2")
						{
							PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class0"]}.tscn");
							Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
							Joueur2.Name = "Joueur2";
							InfoAutreJoueur["co0"] = "0;0;0";
							AddChild(Joueur2);
							
							PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class1"]}.tscn");
							Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
							Joueur3.Name = "Joueur3";
							InfoAutreJoueur["co1"] = "0;0;0";
							AddChild(Joueur3);
						}
						else
						{
							PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class1"]}.tscn");
							Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
							Joueur2.Name = "Joueur2";
							InfoAutreJoueur["co1"] = "0;0;0";
							AddChild(Joueur2);
							
							PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class2"]}.tscn");
							Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
							Joueur3.Name = "Joueur3";
							InfoAutreJoueur["co2"] = "0;0;0";
							AddChild(Joueur3);
						}
						break;
					case 4:
						if (InfoJoueur["id"] == "1")
						{
							PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class0"]}.tscn");
							Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
							Joueur2.Name = "Joueur2";
							InfoAutreJoueur["co0"] = "0;0;0";
							AddChild(Joueur2);
							
							PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class2"]}.tscn");
							Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
							Joueur3.Name = "Joueur3";
							InfoAutreJoueur["co2"] = "0;0;0";
							AddChild(Joueur3);
							
							PackedScene SceneJoueur4 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class3"]}.tscn");
							Joueur4 = SceneJoueur4.Instantiate<CharacterBody3D>();
							Joueur4.Name = "Joueur4";
							InfoAutreJoueur["co3"] = "0;0;0";
							AddChild(Joueur4);
						}
						else if (InfoJoueur["id"] == "2")
						{
							PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class0"]}.tscn"); 
							Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
							Joueur2.Name = "Joueur2";
							InfoAutreJoueur["co0"] = "0;0;0";
							AddChild(Joueur2);
							
							PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class1"]}.tscn");
							Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
							Joueur3.Name = "Joueur3";
							InfoAutreJoueur["co1"] = "0;0;0";
							AddChild(Joueur3);
							
							PackedScene SceneJoueur4 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class3"]}.tscn");
							Joueur4 = SceneJoueur4.Instantiate<CharacterBody3D>();
							Joueur4.Name = "Joueur4";
							InfoAutreJoueur["co3"] = "0;0;0";
							AddChild(Joueur4);
						}
						else if (InfoJoueur["id"] == "3")
						{
							PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class0"]}.tscn");
							Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
							Joueur2.Name = "Joueur2";
							InfoAutreJoueur["co0"] = "0;0;0";
							AddChild(Joueur2);
							
							PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class1"]}.tscn");
							Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
							Joueur3.Name = "Joueur3";
							InfoAutreJoueur["co1"] = "0;0;0";
							AddChild(Joueur3);
							
							PackedScene SceneJoueur4 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class2"]}.tscn");
							Joueur4 = SceneJoueur4.Instantiate<CharacterBody3D>();
							Joueur4.Name = "Joueur4";
							InfoAutreJoueur["co2"] = "0;0;0";
							AddChild(Joueur4);
						}
						else
						{
							PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class1"]}.tscn");
							Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
							Joueur2.Name = "Joueur2";
							InfoAutreJoueur["co1"] = "0;0;0";
							AddChild(Joueur2);
							
							PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class2"]}.tscn");
							Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
							Joueur3.Name = "Joueur3";
							InfoAutreJoueur["co2"] = "0;0;0";
							AddChild(Joueur3);
							
							PackedScene SceneJoueur4 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class3"]}.tscn");
							Joueur4 = SceneJoueur4.Instantiate<CharacterBody3D>();
							Joueur4.Name = "Joueur4";
							InfoAutreJoueur["co3"] = "0;0;0";
							AddChild(Joueur4);
						}
						break;
				}
				
				state = 6;
			}
			
			else if (state == 6)
			{
				tw2.WriteLine("in:co:" + InfoJoueur["co"]);
				tw2.Flush();
			}
			
		}
		catch (Exception e)
		{
			//GD.Print("serveur hors ligne");	//si le serveur est hors ligne
			GD.Print(e);
		}
	}
	
	private void Listen()		//premier thread
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

	private void Listen2()		//deuxieme thread
	{
		while (true)
		{
			string rep = tr2.ReadLine();
			if (rep.Substring(0,5) == "ready")
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
			else if (rep.Substring(0,2) == "in")
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
		}
	}
}
