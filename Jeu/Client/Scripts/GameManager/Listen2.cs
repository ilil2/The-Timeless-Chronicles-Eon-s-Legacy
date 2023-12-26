using Lib;

public class Listen2 : GameManager
{
    public static void Listen()
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