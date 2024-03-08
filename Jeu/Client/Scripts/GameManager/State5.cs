using System;
using System.Collections.Generic;
using Godot;
using Lib;

public partial class State5 : GameManager
{
	public static void State()
	{
		List<(int, int, int)> SpawnLocation = Map.GetSpawnLocation();
		
		PackedScene SceneJoueur1 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/{InfoJoueur["class"]}.tscn");
		Joueur1 = SceneJoueur1.Instantiate<CharacterBody3D>(); 
		Joueur1.Name = "Joueur1"; 
		Joueur1.Position = new Vector3(SpawnLocation[Conversions.AtoI(InfoJoueur["id"])].Item1,SpawnLocation[Conversions.AtoI(InfoJoueur["id"])].Item2,SpawnLocation[Conversions.AtoI(InfoJoueur["id"])].Item3); 
		InfoJoueur["co"] = "0;0;0";
		InfoJoueur["orientation"] = "0;0;0";
		InfoJoueur["attack"] = "";
		InfoJoueur["info"] = "";
				
		switch (_nbJoueur)
		{
			case 2:
				if (InfoJoueur["id"] == "1")
				{
					PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class0"]}.tscn");
					Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
					Joueur2.Name = "Joueur2";
					InitJoueur(0);
					((OtherClassScript)Joueur2).SetID(0);
					((OtherClassScript)Joueur2).SetClasse(InfoAutreJoueur["class0"]);
				}
				else
				{
					PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class1"]}.tscn");
					Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
					Joueur2.Name = "Joueur2";
					InitJoueur(1);
					((OtherClassScript)Joueur2).SetID(1);
					((OtherClassScript)Joueur2).SetClasse(InfoAutreJoueur["class1"]);
				}
				break;
			case 3:
				if (InfoJoueur["id"] == "1")
				{
					PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class0"]}.tscn");
					Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
					Joueur2.Name = "Joueur2";
					InitJoueur(0);
					((OtherClassScript)Joueur2).SetID(0);
					((OtherClassScript)Joueur2).SetClasse(InfoAutreJoueur["class0"]);
							
					PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class2"]}.tscn");
					Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
					Joueur3.Name = "Joueur3";
					InitJoueur(2);
					((OtherClassScript)Joueur3).SetID(2);
					((OtherClassScript)Joueur3).SetClasse(InfoAutreJoueur["class2"]);
				}
				else if (InfoJoueur["id"] == "2")
				{
					PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class0"]}.tscn");
					Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
					Joueur2.Name = "Joueur2";
					InitJoueur(0);
					((OtherClassScript)Joueur2).SetID(0);
					((OtherClassScript)Joueur2).SetClasse(InfoAutreJoueur["class0"]);
							
					PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class1"]}.tscn");
					Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
					Joueur3.Name = "Joueur3";
					InitJoueur(1);
					((OtherClassScript)Joueur3).SetID(1);
					((OtherClassScript)Joueur3).SetClasse(InfoAutreJoueur["class1"]);
				}
				else
				{
					PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class1"]}.tscn");
					Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
					Joueur2.Name = "Joueur2";
					InitJoueur(1);
					((OtherClassScript)Joueur2).SetID(1);
					((OtherClassScript)Joueur2).SetClasse(InfoAutreJoueur["class1"]);
							
					PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class2"]}.tscn");
					Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
					Joueur3.Name = "Joueur3";
					InitJoueur(2);
					((OtherClassScript)Joueur3).SetID(2);
					((OtherClassScript)Joueur3).SetClasse(InfoAutreJoueur["class2"]);
				}
				break;
			case 4: 
				if (InfoJoueur["id"] == "1")
				{ 
					PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class0"]}.tscn");
					Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
					Joueur2.Name = "Joueur2";
					InitJoueur(0);
					((OtherClassScript)Joueur2).SetID(0);
					((OtherClassScript)Joueur2).SetClasse(InfoAutreJoueur["class0"]);
							
					PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class2"]}.tscn");
					Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
					Joueur3.Name = "Joueur3";
					InitJoueur(2);
					((OtherClassScript)Joueur3).SetID(2);
					((OtherClassScript)Joueur3).SetClasse(InfoAutreJoueur["class2"]);
							
					PackedScene SceneJoueur4 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class3"]}.tscn");
					Joueur4 = SceneJoueur4.Instantiate<CharacterBody3D>();
					Joueur4.Name = "Joueur4";
					InitJoueur(3);
					((OtherClassScript)Joueur4).SetID(3);
					((OtherClassScript)Joueur4).SetClasse(InfoAutreJoueur["class3"]);
				}
				else if (InfoJoueur["id"] == "2")
				{
					PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class0"]}.tscn"); 
					Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
					Joueur2.Name = "Joueur2";
					InitJoueur(0);
					((OtherClassScript)Joueur2).SetID(0);
					((OtherClassScript)Joueur2).SetClasse(InfoAutreJoueur["class0"]);
							
					PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class1"]}.tscn");
					Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
					Joueur3.Name = "Joueur3";
					InitJoueur(1);
					((OtherClassScript)Joueur3).SetID(1);
					((OtherClassScript)Joueur3).SetClasse(InfoAutreJoueur["class1"]);
							
					PackedScene SceneJoueur4 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class3"]}.tscn");
					Joueur4 = SceneJoueur4.Instantiate<CharacterBody3D>();
					Joueur4.Name = "Joueur4";
					InitJoueur(3);
					((OtherClassScript)Joueur4).SetID(3);
					((OtherClassScript)Joueur4).SetClasse(InfoAutreJoueur["class3"]);
				}
				else if (InfoJoueur["id"] == "3")
				{
					PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class0"]}.tscn");
					Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
					Joueur2.Name = "Joueur2";
					InitJoueur(0);
					((OtherClassScript)Joueur2).SetID(0);
					((OtherClassScript)Joueur2).SetClasse(InfoAutreJoueur["class0"]);
							
					PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class1"]}.tscn");
					Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
					Joueur3.Name = "Joueur3";
					InitJoueur(1);
					((OtherClassScript)Joueur3).SetID(1);
					((OtherClassScript)Joueur3).SetClasse(InfoAutreJoueur["class1"]);
							
					PackedScene SceneJoueur4 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class2"]}.tscn");
					Joueur4 = SceneJoueur4.Instantiate<CharacterBody3D>();
					Joueur4.Name = "Joueur4";
					InitJoueur(2);
					((OtherClassScript)Joueur4).SetID(2);
					((OtherClassScript)Joueur4).SetClasse(InfoAutreJoueur["class2"]);
				}
				else
				{
					PackedScene SceneJoueur2 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class1"]}.tscn");
					Joueur2 = SceneJoueur2.Instantiate<CharacterBody3D>();
					Joueur2.Name = "Joueur2";
					InitJoueur(1);
					((OtherClassScript)Joueur2).SetID(1);
					((OtherClassScript)Joueur2).SetClasse(InfoAutreJoueur["class1"]);
							
					PackedScene SceneJoueur3 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class2"]}.tscn");
					Joueur3 = SceneJoueur3.Instantiate<CharacterBody3D>();
					Joueur3.Name = "Joueur3";
					InitJoueur(2);
					((OtherClassScript)Joueur3).SetID(2);
					((OtherClassScript)Joueur3).SetClasse(InfoAutreJoueur["class2"]);
							
					PackedScene SceneJoueur4 = GD.Load<PackedScene>($"res://Scenes/PlayerScenes/Other{InfoAutreJoueur["class3"]}.tscn");
					Joueur4 = SceneJoueur4.Instantiate<CharacterBody3D>();
					Joueur4.Name = "Joueur4";
					InitJoueur(3);
					((OtherClassScript)Joueur4).SetID(3);
					((OtherClassScript)Joueur4).SetClasse(InfoAutreJoueur["class3"]);
				}
				break;
		}
				
		state = 6;
	}

	private static void InitJoueur(int id)
	{
		InfoAutreJoueur[$"co{id}"] = "0;0;0";
		InfoAutreJoueur[$"orientation{id}"] = "0;0;0";
		InfoAutreJoueur[$"attack{id}"] = "";
	}
}
