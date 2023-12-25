using System;
using Godot;

public class State5 : GameManager
{
	public State5()
	{
		State();
	}
	
    public void State()
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
					((OtherClassScript)Joueur2).SetID(0);
					((OtherClassScript)Joueur2).SetClasse(InfoAutreJoueur["class0"]);
					AddChild(Joueur2);
							
					PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class2"]}.tscn");
					Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
					Joueur3.Name = "Joueur3";
					InfoAutreJoueur["co2"] = "0;0;0";
					((OtherClassScript)Joueur3).SetID(2);
					((OtherClassScript)Joueur3).SetClasse(InfoAutreJoueur["class2"]);
					AddChild(Joueur3);
				}
				else if (InfoJoueur["id"] == "2")
				{
					PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class0"]}.tscn");
					Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
					Joueur2.Name = "Joueur2";
					InfoAutreJoueur["co0"] = "0;0;0";
					((OtherClassScript)Joueur2).SetID(0);
					((OtherClassScript)Joueur2).SetClasse(InfoAutreJoueur["class0"]);
					AddChild(Joueur2);
							
					PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class1"]}.tscn");
					Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
					Joueur3.Name = "Joueur3";
					InfoAutreJoueur["co1"] = "0;0;0";
					((OtherClassScript)Joueur3).SetID(1);
					((OtherClassScript)Joueur3).SetClasse(InfoAutreJoueur["class1"]);
					AddChild(Joueur3);
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
							
					PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class2"]}.tscn");
					Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
					Joueur3.Name = "Joueur3";
					InfoAutreJoueur["co2"] = "0;0;0";
					((OtherClassScript)Joueur3).SetID(2);
					((OtherClassScript)Joueur3).SetClasse(InfoAutreJoueur["class2"]);
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
				    ((OtherClassScript)Joueur2).SetID(0);
				    ((OtherClassScript)Joueur2).SetClasse(InfoAutreJoueur["class0"]);
				    AddChild(Joueur2);
							
				    PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class2"]}.tscn");
				    Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
				    Joueur3.Name = "Joueur3";
				    InfoAutreJoueur["co2"] = "0;0;0";
				    ((OtherClassScript)Joueur3).SetID(2);
				    ((OtherClassScript)Joueur3).SetClasse(InfoAutreJoueur["class2"]);
				    AddChild(Joueur3);
							
				    PackedScene SceneJoueur4 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class3"]}.tscn");
				    Joueur4 = SceneJoueur4.Instantiate<CharacterBody3D>();
				    Joueur4.Name = "Joueur4";
				    InfoAutreJoueur["co3"] = "0;0;0";
				    ((OtherClassScript)Joueur4).SetID(3);
				    ((OtherClassScript)Joueur4).SetClasse(InfoAutreJoueur["class3"]);
				    AddChild(Joueur4);
			    }
			    else if (InfoJoueur["id"] == "2")
			    {
				    PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class0"]}.tscn"); 
				    Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
				    Joueur2.Name = "Joueur2";
				    InfoAutreJoueur["co0"] = "0;0;0";
				    ((OtherClassScript)Joueur2).SetID(0);
				    ((OtherClassScript)Joueur2).SetClasse(InfoAutreJoueur["class0"]);
				    AddChild(Joueur2);
							
				    PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class1"]}.tscn");
				    Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
				    Joueur3.Name = "Joueur3";
				    InfoAutreJoueur["co1"] = "0;0;0";
				    ((OtherClassScript)Joueur3).SetID(1);
				    ((OtherClassScript)Joueur3).SetClasse(InfoAutreJoueur["class1"]);
				    AddChild(Joueur3);
							
				    PackedScene SceneJoueur4 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class3"]}.tscn");
				    Joueur4 = SceneJoueur4.Instantiate<CharacterBody3D>();
				    Joueur4.Name = "Joueur4";
				    InfoAutreJoueur["co3"] = "0;0;0";
				    ((OtherClassScript)Joueur4).SetID(3);
				    ((OtherClassScript)Joueur4).SetClasse(InfoAutreJoueur["class3"]);
				    AddChild(Joueur4);
			    }
			    else if (InfoJoueur["id"] == "3")
			    {
				    PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class0"]}.tscn");
				    Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
				    Joueur2.Name = "Joueur2";
				    InfoAutreJoueur["co0"] = "0;0;0";
				    ((OtherClassScript)Joueur2).SetID(0);
				    ((OtherClassScript)Joueur2).SetClasse(InfoAutreJoueur["class0"]);
				    AddChild(Joueur2);
							
				    PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class1"]}.tscn");
				    Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
				    Joueur3.Name = "Joueur3";
				    InfoAutreJoueur["co1"] = "0;0;0";
				    ((OtherClassScript)Joueur3).SetID(1);
				    ((OtherClassScript)Joueur3).SetClasse(InfoAutreJoueur["class1"]);
				    AddChild(Joueur3);
							
				    PackedScene SceneJoueur4 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class2"]}.tscn");
				    Joueur4 = SceneJoueur4.Instantiate<CharacterBody3D>();
				    Joueur4.Name = "Joueur4";
				    InfoAutreJoueur["co2"] = "0;0;0";
				    ((OtherClassScript)Joueur4).SetID(2);
				    ((OtherClassScript)Joueur4).SetClasse(InfoAutreJoueur["class2"]);
				    AddChild(Joueur4);
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
							
				    PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class2"]}.tscn");
				    Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
				    Joueur3.Name = "Joueur3";
				    InfoAutreJoueur["co2"] = "0;0;0";
				    ((OtherClassScript)Joueur3).SetID(2);
				    ((OtherClassScript)Joueur3).SetClasse(InfoAutreJoueur["class2"]);
				    AddChild(Joueur3);
							
				    PackedScene SceneJoueur4 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class3"]}.tscn");
				    Joueur4 = SceneJoueur4.Instantiate<CharacterBody3D>();
				    Joueur4.Name = "Joueur4";
				    InfoAutreJoueur["co3"] = "0;0;0";
				    ((OtherClassScript)Joueur4).SetID(3);
				    ((OtherClassScript)Joueur4).SetClasse(InfoAutreJoueur["class3"]);
				    AddChild(Joueur4);
			    }
			    break;
	    }
				
	    AddChild(_chat);
				
	    state = 6;
    }
}