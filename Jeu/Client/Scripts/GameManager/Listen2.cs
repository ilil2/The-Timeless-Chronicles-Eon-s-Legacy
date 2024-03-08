using Lib;

public partial class Listen2 : GameManager
{
	public static void Listen()
	{
		string rep = UDP.Receive(soc2);
		if (rep.Length > 2 && rep.Substring(0,2) == "in")
		{
			string line = rep.Substring(3);
			string[] SplitInfo = line.Split('|');
			for (int i = 0; i < _nbJoueur; i++)
			{
				string[] CoordInfo = SplitInfo[i].Split('/');
				CoordInfo[1] = CoordInfo[1].Substring(3); 
				InfoAutreJoueur[$"co{CoordInfo[0]}"] = CoordInfo[1];
				InfoAutreJoueur[$"orientation{CoordInfo[0]}"] = CoordInfo[2];
			}
		}
		
		else if (rep.Length > 2 && rep.Substring(0,2) == "on")
		{
			rep = rep.Substring(3);
			string id = rep.Split('|')[0];
			rep = rep.Split('|')[1];

			if (rep == "next")
			{
				state = 7;
				_loadMap = true;
				StartMap = false;
			}
			else if (InfoJoueur["id"] != id)
			{
				InfoAutreJoueur[$"attack{id}"] = rep;
			}
		}
		
		else if (rep.Length > 2 && rep.Substring(0,2) == "an")
		{
			rep = rep.Substring(3);
			string id = rep.Split('|')[0];
			rep = rep.Split('|')[1];
			
			if (InfoJoueur["id"] != id)
			{
				InfoAutreJoueur[$"animation{id}"] = rep;
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

		else if (rep.Substring(0,4) == "deco")
		{
			int id = Conversions.AtoI(rep.Substring(5));
			
			if (id == ((OtherClassScript)Joueur2).GetID())
			{
				Joueur2.QueueFree();
			}
			else if (id == ((OtherClassScript)Joueur3).GetID())
			{
				Joueur3.QueueFree();
			}
			else
			{
				Joueur4.QueueFree();
			}
		}
			
		else if (rep.Length > 4 && rep.Substring(0,5) == "start")
		{
			StartMap = true;
		}
		
		else if (rep.Length > 5 && rep.Substring(0,5) == "ready")
		{
			rep = rep.Substring(6);
			string[] InfoReady = rep.Split("/");
			for (int i = 1; i < Conversions.AtoI(InfoReady[0]) * 3 + 4; i += 3)
			{
				if (InfoReady[i] == "")
				{
					i -= 3;
				}
				else if (InfoReady[i] == InfoJoueur["id"])
				{
					InfoJoueur["pseudo"] = InfoReady[i + 1];
					InfoJoueur["class"] = InfoReady[i + 2];
				}
				else
				{
					InfoAutreJoueur[$"id{InfoReady[i]}"] = InfoReady[i];
					InfoAutreJoueur[$"pseudo{InfoReady[i]}"] = InfoReady[i + 1];
					InfoAutreJoueur[$"class{InfoReady[i]}"] = InfoReady[i + 2];
				}
			}
			_nbJoueur = Conversions.AtoI(InfoReady[0]) + 1;
				
			ClassSelectUI.Supr = true;
			_loadMap = true;
		}
	}
}
