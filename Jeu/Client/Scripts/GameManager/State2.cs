using System;
using System.Threading;
using Godot;

public partial class State2 : GameManager
{
	public static void State()
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
					thread = true;
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
					thread = true;
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
				LobbyReset = true;
				OnJoin = false;
				thread = false;
				//th.Interrupt();
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
			state = 3;
		}
	}
}
