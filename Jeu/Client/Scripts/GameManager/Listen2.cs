using System;
using Lib;
using Serveur;

public partial class Listen2 : GameManager
{
    public static void Listen()
    {
        string rep = UDP.Receive(soc2);
        if (rep.Length > 2 && rep.Substring(0,2) == "in")
	    {
		    string line = rep.Substring(3);
		    string[] SplitInfo = line.Split('|');
		    for (int i = 0; i < 4; i++)
		    {
			    string[] CoordInfo = SplitInfo[i].Split('/');
			    if (CoordInfo[1] != "deco")
			    {
				    if (CoordInfo[0] != InfoJoueur["id"])
				    {
					    CoordInfo[1] = CoordInfo[1].Substring(3);
					    InfoAutreJoueur[$"co{CoordInfo[0]}"] = CoordInfo[1];
					    InfoAutreJoueur[$"orientation{CoordInfo[0]}"] = CoordInfo[2];
				    }
			    }
			    else
			    {
				    InfoAutreJoueur[$"co{CoordInfo[0]}"] = "0;-3;0";
			    }
		    }
	    }
        
        else if (rep.Length > 2 && rep.Substring(0,2) == "on")
        {
	        rep = rep.Substring(3);
	        string id = rep.Split('|')[0];

	        if (InfoJoueur["id"] != id)
	        {
		        InfoAutreJoueur[$"attack{id}"] = rep.Split('|')[1];
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