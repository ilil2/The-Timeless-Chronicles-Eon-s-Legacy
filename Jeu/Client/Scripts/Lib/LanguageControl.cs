using System.Collections.Generic;

namespace Lib;

public class LanguageControl
{
    private List<Dictionary<string, string>> _listLanguage = new ()
    {
        new Dictionary<string, string>
        {
            {"languageName", "English"},
            
            //Connection Menu
            {"connectionMenuTitle", "Connection"},
            {"connectionMenuPseudoText", "Pseudo"},
            {"connectionMenuPasswordText", "Password"},
            {"connectionMenuConnectionButton", "Connection"},
            {"connectionMenuNoAccountButton", "No account ?"},
            {"connectionMenuFastConnectionButton", "Fast Connection"},
            {"connectionMenuErrorUsernameOrPasswordText", "Incorrect username or password"},
            {"connectionMenuErrorFastConnectionText", "Error in fast connection"},
            {"connectionMenuErrorNoFastConnectionText", "No fast connections registered"},
            {"connectionMenuFastConnectionSaveButton", "Save username and password locally."},
            {"connectionMenuFastConnectionSaveWarningText", "/!\\ Warning :\nSaving your username and password locally makes it easier to log in, but may entail security risks. If your device is compromised, your information could be accessible."},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Inscription"},
            {"inscriptionMenuPseudoText", "Pseudo"},
            {"inscriptionMenuPasswordText", "Password"},
            {"inscriptionMenuPasswordConfirmText", "Confirm password"},
            {"inscriptionMenuInscriptionButton", "Inscription"},
            {"inscriptionMenuAlreadyAccountButton", "Already have an account ?"},
            {"inscriptionMenuErrorIncorrectConfirmText", "Incorrect confirmation password"},
            {"inscriptionMenuErrorAlreadyExistText", "Username already exist"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Lobby"},
            {"lobbyMenuCreateGame", "Create Game"},
            {"lobbyMenuJoinGame", "Join Game"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Create Game"},
            {"createGameMenuBackButton", "Back =>"},
            {"createGameMenuStartGame", "Start Game"},
            {"createGameMenuID", "Game ID: "},
            {"createGameMenuPlayer1", "Player 1: "},
            {"createGameMenuPlayer2", "Player 2: "},
            {"createGameMenuPlayer3", "Player 3: "},
            {"createGameMenuPlayer4", "Player 4: "},
            
            //Join Game Menu
            {"joinGameMenuTitle", "Join Game"},
            {"joinGameMenuBackButton", "<= Back"},
            {"joinGameMenuGameID", "ID of the Game"},
            
            //Select Class Menu
            {"Archer", "Archer"},
            {"Knight", "Knight"},
            {"Scientist", "Scientist"},
            {"Assassin", "Assassin"},
            {"selectClassMenuReadyButton", "Ready"},
            {"selectClassMenuWaitingText", "Waiting for the other players..."},
            
            //Map loading
            {"gameLoadingMapText", "Loading the map : "},
            {"gameLoadingMapWaitingText", "Waiting for the other players"},
            
            //Pause Menu
            {"pauseMenuTitle", "Pause Menu"},
            {"pauseMenuResumeButton", "Resume"},
            {"pauseMenuSettingsButton", "Settings"},
            {"pauseMenuLeaveButton", "Leave"},
            {"pauseMenuConfirmLeaveTitle", "Are you sure you want to leave the game ?"},
            {"pauseMenuConfirmLeaveButton", "Leave"},
            {"pauseMenuCancelLeaveButton", "Cancel"},
            
            //Settings Menu
            {"settingsMenuTitle", "Settings"},
            {"settingsMenuBackButton", "<= Back"},
            {"settingsMenuResetButton", "Reset"},
            {"settingsMenuSaveButton", "Save"},
            
            //Settings Menu - Game
            {"settingsMenuGameButton", "Game"},
            {"settingsMenuGameLanguage", "Language :"},
            {"settingsMenuGameMouseSensibility", "Mouse Sensibility :"},
            {"settingsMenuGameFullScreen", "Full Screen :"},
            {"settingsMenuGameEnableChat", "Enable Chat :"},
            {"settingsMenuGameChatSize", "Chat Size :"},
            {"settingsMenuGameChatSizeSmall", "Small"},
            {"settingsMenuGameChatSizeMedium", "Medium"},
            {"settingsMenuGameChatSizeLarge", "Large"},
            
            //Settings Menu - Audio
            {"settingsMenuAudioButton", "Audio"},
            
            //Settings Menu - Video
            {"settingsMenuVideoButton", "Video"},
            
            //Settings Menu - Controls
            {"settingsMenuControlsButton", "Controls"},
            {"settingsMenuControlsMoveForward", "Forward"},
            {"settingsMenuControlsMoveBackward", "Backward"},
            {"settingsMenuControlsMoveLeft", "Left"},
            {"settingsMenuControlsMoveRight", "Right"},
            {"settingsMenuControlsSprint", "Sprint"},
            {"settingsMenuControlsDash", "Dash"},
            {"settingsMenuControlsCapacity1", "Capacity 1"},
            {"settingsMenuControlsCapacity2", "Capacity 2"},
            {"settingsMenuControlsCapacity3", "Capacity 3"},
            {"settingsMenuControlsItem1", "Item 1"},
            {"settingsMenuControlsItem2", "Item 2"},
            {"settingsMenuControlsItem3", "Item 3"},
            {"settingsMenuControlsInventory", "Inventory"},
            {"settingsMenuControlsReload", "Reload"},
            {"settingsMenuControlsChat", "Chat"},
            {"settingsMenuControlsEnableChat", "Enable Chat"},
            {"settingsMenuControlsPause", "Pause"},
            
            //Skill Description
            {"skillDescriptionSpeed", "Increases the character's movement speed.\nSpeed: "},
            {"skillDescriptionHealth", "Boosts the character's maximum health points.\nHeath: "},
            {"skillDescriptionDamage", "Enhances the damage dealt by the character's attacks.\nDamage: "},
            {"skillDescriptionStamina", "Increases the character's stamina for more sustained actions.\nStamina: "},
            {"skillDescriptionReload", "Decreases the time required to reload stamina.\nReload Stamina: "},
            {"skillDescriptionStaminaUse", "Reduces the stamina consumed by actions.\nStamina Use:"},
            {"skillDescriptionArrow", "Adds an extra arrow when the archer shoots (maximum of 3 arrows).\nArrows: +1"},
            {"skillDescriptionShootSpeed", "Increases the speed at which arrows are shot.\nShoot Speed: "},
            {"skillDescriptionArrowPoison", "Arrows are imbued with poison, causing damage over time.\nPoison Time: 5s"},
            {"skillDescriptionArrowGel", "Arrows are coated with gel, possibly causing slow effects and stunning enemies for a few seconds.\nGel Time: 5s"},
            {"skillDescriptionHealSpeed", "Enhances the speed at which healing occurs.\nHeal Speed per second: "},
            {"skillDescriptionLaserMove", "Allows the character to move the laser while shooting it."},
            {"skillDescriptionRevive", "Enables the character to revive fallen allies."},
            {"skillDescriptionVampire", "Grants the ability to drain health from enemies.\nDrain Health: 10% of damage"},
            {"skillDescriptionReviveAll", "This skill can be used only once in the entire game. It revives all fallen allies and teleports them to the character's location."},
            {"skillDescriptionReloadProtection", "Provides protection during the reload phase.\nDamage Reduction: 50%"},
            {"skillDescriptionCrit", "Increases the chance of landing critical hits.\nCritical Chance: "},
            {"skillDescriptionRange", "Extends the range of the character's attacks.\nRange: +20%"},
            {"skillDescriptionAgro", "Allows the character to attract the aggro of all nearby mobs."},
            {"skillDescriptionSpike", "Reflects a portion of the damage back to the attacker.\nDamage Reflection: 20%"},
            {"skillDescriptionAbsorption", "Absorbs the damage taken by other players within a certain radius.\nRadius: 5m"},
            {"skillDescriptionInvincibility", "Grants temporary invincibility. \nInvincibility Time: 5s"},
            {"skillDescriptionEscalibur", "Bestows the power of the legendary sword, Excalibur, with a 1 in 5 chance to freeze enemies.\nFreeze Time: 5s"},
            {"skillDescriptionDashDegat", "Deals damage to enemies during a dash attack.\nDamage: 30%"},
            {"skillDescriptionDague", "Allows the character to throw daggers."},
            {"skillDescriptionInvisibility", "Allows the character to become invisible for a short period.\nInvisibility Time: 10s"},
            {"skillDescriptionDoubleAttack", "Enables the character to perform double attacks."},
            {"skillDescriptionPoison", "Imbues attacks with poison, causing damage over time.\nPoison Time: 5s"},
            {"skillChooseTitle", "Choose your new skill"},
            
            //Potion
            {"PotionBuy", "Buy"},
            {"PotionPrice", "Price: "},
            {"PotionTitle0", "Health Potion"},
            {"PotionDescription0", "Restores 40% of the player's health."},
            {"PotionTitle1", "Stamina Potion"},
            {"PotionDescription1", "Restores 60% of the player's stamina"},
            {"PotionTitle2", "Speed Potion"},
            {"PotionDescription2", "Multiplies speed by 1.5 for 20 seconds "},
            {"PotionTitle3", "Resistance Potion"},
            {"PotionDescription3", "Divides damage by 2 for 20 seconds "},
            {"PotionTitle4", "Resurrection potion"},
            {"PotionDescription4", "Revives a nearby player (at 50% health)"},
            {"PotionTitle5", "Amnesia potion"},
            {"PotionDescription5", "Removes a skill acquired by the player"},
        },
        
        new Dictionary<string, string>
        {
            {"languageName", "Français"},
            
            //Connection Menu
            {"connectionMenuTitle", "Connexion"},
            {"connectionMenuPseudoText", "Pseudonyme"},
            {"connectionMenuPasswordText", "Mot de passe"},
            {"connectionMenuConnectionButton", "Connexion"},
            {"connectionMenuNoAccountButton", "Pas de compte ?"},
            {"connectionMenuFastConnectionButton", "Connexion Rapide"},
            {"connectionMenuErrorUsernameOrPasswordText", "Pseudo ou mot de passe incorrect"},
            {"connectionMenuErrorFastConnectionText", "Erreur dans la connexion rapide"},
            {"connectionMenuErrorNoFastConnectionText", "Aucune connexion rapide enregistrée"},
            {"connectionMenuFastConnectionSaveButton", "Enregistrer le pseudo et le mot de passe localement."},
            {"connectionMenuFastConnectionSaveWarningText", "/!\\ Attention :\nEnregistrer votre pseudo et votre mot de passe localement facilite la connexion, mais peut comporter des risques de sécurité. Si votre appareil est compromis, vos informations pourraient être accessibles."},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Inscription"},
            {"inscriptionMenuPseudoText", "Pseudonyme"},
            {"inscriptionMenuPasswordText", "Mot de passe"},
            {"inscriptionMenuPasswordConfirmText", "Confirmer le mot de passe"},
            {"inscriptionMenuInscriptionButton", "Inscription"},
            {"inscriptionMenuAlreadyAccountButton", "Déjà un compte ?"},
            {"inscriptionMenuErrorIncorrectConfirmText", "Confirmation du mot de passe incorrect"},
            {"inscriptionMenuErrorAlreadyExistText", "Pseudo déjà existant"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Lobby"},
            {"lobbyMenuCreateGame", "Créer une partie"},
            {"lobbyMenuJoinGame", "Rejoindre une partie"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Créer une partie"},
            {"createGameMenuBackButton", "Retour =>"},
            {"createGameMenuStartGame", "Démarrer la partie"},
            {"createGameMenuID", "ID de la partie : "},
            {"createGameMenuPlayer1", "Joueur 1 : "},
            {"createGameMenuPlayer2", "Joueur 2 : "},
            {"createGameMenuPlayer3", "Joueur 3 : "},
            {"createGameMenuPlayer4", "Joueur 4 : "},
            
            //Join Game Menu
            {"joinGameMenuTitle", "Rejoindre une partie"},
            {"joinGameMenuBackButton", "<= Retour"},
            {"joinGameMenuGameID", "ID de la partie"},
            
            //Select Class Menu
            {"Archer", "Archer"},
            {"Knight", "Chevalier"},
            {"Scientist", "Scientifique"},
            {"Assassin", "Assassin"},
            {"selectClassMenuReadyButton", "Prêt"},
            {"selectClassMenuWaitingText", "En attente des autres joueurs..."},
            
            //Map loading
            {"gameLoadingMapText", "Chargement de la map : "},
            {"gameLoadingMapWaitingText", "En attente des autres joueurs"},
            
            //Pause Menu
            {"pauseMenuTitle", "Menu Pause"},
            {"pauseMenuResumeButton", "Reprendre"},
            {"pauseMenuSettingsButton", "Paramètres"},
            {"pauseMenuLeaveButton", "Quitter"},
            {"pauseMenuConfirmLeaveTitle", "Êtes-vous sûr de vouloir quitter le jeu ?"},
            {"pauseMenuConfirmLeaveButton", "Quitter"},
            {"pauseMenuCancelLeaveButton", "Annuler"},
            
            //Settings Menu
            {"settingsMenuTitle", "Paramètres"},
            {"settingsMenuBackButton", "<= Retour"},
            {"settingsMenuResetButton", "Réinitialiser"},
            {"settingsMenuSaveButton", "Sauvegarder"},
            
            //Settings Menu - Game
            {"settingsMenuGameButton", "Jeu"},
            {"settingsMenuGameLanguage", "Langue :"},
            {"settingsMenuGameMouseSensibility", "Sensibilité de la souris :"},
            {"settingsMenuGameFullScreen", "Plein écran :"},
            {"settingsMenuGameEnableChat", "Activer le chat :"},
            {"settingsMenuGameChatSize", "Taille du chat :"},
            {"settingsMenuGameChatSizeSmall", "Petit"},
            {"settingsMenuGameChatSizeMedium", "Moyen"},
            {"settingsMenuGameChatSizeLarge", "Grand"},
            
            //Settings Menu - Audio
            {"settingsMenuAudioButton", "Audio"},
            
            //Settings Menu - Video
            {"settingsMenuVideoButton", "Vidéo"},
            
            //Settings Menu - Controls
            {"settingsMenuControlsButton", "Contrôles"},
            {"settingsMenuControlsMoveForward", "Avancer"},
            {"settingsMenuControlsMoveBackward", "Reculer"},
            {"settingsMenuControlsMoveLeft", "Gauche"},
            {"settingsMenuControlsMoveRight", "Droite"},
            {"settingsMenuControlsSprint", "Sprint"},
            {"settingsMenuControlsDash", "Dash"},
            {"settingsMenuControlsCapacity1", "Capacité 1"},
            {"settingsMenuControlsCapacity2", "Capacité 2"},
            {"settingsMenuControlsCapacity3", "Capacité 3"},
            {"settingsMenuControlsItem1", "Objet 1"},
            {"settingsMenuControlsItem2", "Objet 2"},
            {"settingsMenuControlsItem3", "Objet 3"},
            {"settingsMenuControlsInventory", "Inventaire"},
            {"settingsMenuControlsReload", "Recharger"},
            {"settingsMenuControlsChat", "Tchat"},
            {"settingsMenuControlsEnableChat", "Activer le tchat"},
            {"settingsMenuControlsPause", "Pause"},
            
            //Skill Description
            {"skillDescriptionSpeed", "Augmente la vitesse de déplacement du personnage.\nVitesse : "},
            {"skillDescriptionHealth", "Augmente les points de vie maximum du personnage.\nVie : "},
            {"skillDescriptionDamage", "Augmente les dégâts infligés par les attaques du personnage.\nDégâts : "},
            {"skillDescriptionStamina", "Augmente l'endurance du personnage pour des actions plus soutenues.\nEndurance : "},
            {"skillDescriptionReload", "Diminue le temps nécessaire pour recharger l'endurance.\nRechargement de l'endurance : "},
            {"skillDescriptionStaminaUse", "Réduit l'endurance consommée par les actions.\nUtilisation de l'endurance : "},
            {"skillDescriptionArrow", "Ajoute une flèche supplémentaire lorsque l'archer tire (maximum de 3 flèches).\nFlèches : +1"},
            {"skillDescriptionShootSpeed", "Augmente la vitesse à laquelle les flèches sont tirées.\nVitesse de tir : "},
            {"skillDescriptionArrowPoison", "Les flèches sont imprégnées de poison, infligeant des dégâts sur la durée.\nDurée du poison : 5s"},
            {"skillDescriptionArrowGel", "Les flèches sont enrobées de gel, provoquant éventuellement des effets de ralentissement et étourdissant les ennemis pendant quelques secondes.\nDurée du gel : 5s"},
            {"skillDescriptionHealSpeed", "Augmente la vitesse à laquelle la guérison se produit.\nVitesse de guérison par seconde : "},
            {"skillDescriptionLaserMove", "Permet au personnage de déplacer le laser tout en le tirant."},
            {"skillDescriptionRevive", "Permet au personnage de ressusciter les alliés tombés."},
            {"skillDescriptionVampire", "Accorde la capacité de drainer la santé des ennemis.\nDrain de santé : 10% des dégâts"},
            {"skillDescriptionReviveAll", "Cette compétence ne peut être utilisée qu'une seule fois dans toute la partie. Elle ressuscite tous les alliés tombés et les téléporte à l'emplacement du personnage."},
            {"skillDescriptionReloadProtection", "Fournit une protection pendant la phase de rechargement.\nRéduction des dégâts : 50%"},
            {"skillDescriptionCrit", "Augmente la chance de réaliser des coups critiques.\nChance de critique : "},
            {"skillDescriptionRange", "Étend la portée des attaques du personnage.\nPortée : +20%"},
            {"skillDescriptionAgro", "Permet au personnage d'attirer l'aggro de tous les ennemis proches."},
            {"skillDescriptionSpike", "Reflette une partie des dégâts infligés à l'attaquant.\nRéflexion des dégâts : 20%"},
            {"skillDescriptionAbsorption", "Absorbe les dégâts subis par les autres joueurs dans un certain rayon.\nRayon : 5m"},
            {"skillDescriptionInvincibility", "Accorde une invincibilité temporaire. \nTemps d'invincibilité : 5s"},
            {"skillDescriptionEscalibur", "Confère le pouvoir de l'épée légendaire, Excalibur, avec 1 chance sur 5 de geler les ennemis.\nTemps de gel : 5s"},
            {"skillDescriptionDashDegat", "Inflige des dégâts aux ennemis lors d'une attaque en dash.\nDégâts : 30%"},
            {"skillDescriptionDague", "Permet au personnage de lancer des dagues."},
            {"skillDescriptionInvisibility", "Permet au personnage de devenir invisible pendant une courte période.\nTemps d'invisibilité : 10s"},
            {"skillDescriptionDoubleAttack", "Permet au personnage d'effectuer des attaques doubles."},
            {"skillDescriptionPoison", "Imprègne les attaques de poison, infligeant des dégâts sur la durée.\nDurée du poison : 5s"},
            {"skillChooseTitle", "Choisissez votre nouvelle compétence"},
            
            //Potion
            {"PotionBuy", "Acheter"},
            {"PotionPrice", "Prix : "},
            {"PotionTitle0", "Potion de Vie"},
            {"PotionDescription0", "Restaure 40% de la vie du joueur."},
            {"PotionTitle1", "Potion d'Endurance"},
            {"PotionDescription1", "Restaure 60% de l'endurance du joueur"},
            {"PotionTitle2", "Potion de Vitesse"},
            {"PotionDescription2", "Multiplie la vitesse par 1.5 pendant 20 secondes "},
            {"PotionTitle3", "Potion de Résistance"},
            {"PotionDescription3", "Divise les dégâts par 2 pendant 20 secondes "},
            {"PotionTitle4", "Potion de Résurrection"},
            {"PotionDescription4", "Ressuscite un joueur proche (à 50% de vie)"},
            {"PotionTitle5", "Potion d'Amnésie"},
            {"PotionDescription5", "Supprime une compétence acquise par le joueur"},
        },
        
        new Dictionary<string, string>
        {
            {"languageName", "Breizhek (Beta)"},
            
            //Connection Menu
            {"connectionMenuTitle", "Kenstabl"},
            {"connectionMenuPseudoText", "Pseudo"},
            {"connectionMenuPasswordText", "Ger-tremen"},
            {"connectionMenuConnectionButton", "Kenstabl"},
            {"connectionMenuNoAccountButton", "N'eo ket gant ur c'hemennadenn ?"},
            {"connectionMenuFastConnectionButton", "Kenstabl Gwisk"},
            {"connectionMenuErrorUsernameOrPasswordText", "Pseudo pe ger-tremen fall"},
            {"connectionMenuErrorFastConnectionText", "Fazi e kenstabl gwisk"},
            {"connectionMenuErrorNoFastConnectionText", "N'eo ket kenstabl gwisk en em enrollaet"},
            {"connectionMenuFastConnectionSaveButton", "Enrolliñ ar pseudo ha ger-tremen war ar c'hard."},
            {"connectionMenuFastConnectionSaveWarningText", "/!\\ Diwallit :\nEnrolliñ ho pseudo ha ho ger-tremen war ar c'hard a vefe graet gantoc'h evit enrolliñ, met a c'hall bezañ ur raktres sekurite. Ma'z eo ho meziant kaset, e c'hallfe bezañ posupl da dremen ho titouroù."},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "En em enskrivañ"},
            {"inscriptionMenuPseudoText", "Pseudo"},
            {"inscriptionMenuPasswordText", "Ger-tremen"},
            {"inscriptionMenuPasswordConfirmText", "Kadarnaat ar ger-tremen"},
            {"inscriptionMenuInscriptionButton", "En em enskrivañ"},
            {"inscriptionMenuAlreadyAccountButton", "Ur c'hemennadenn zo ganeoc'h ?"},
            {"inscriptionMenuErrorIncorrectConfirmText", "Kadarnaat ar ger-tremen fall"},
            {"inscriptionMenuErrorAlreadyExistText", "Pseudo dija enrollaet"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Lobby"},
            {"lobbyMenuCreateGame", "Krouiñ ur gemennadenn"},
            {"lobbyMenuJoinGame", "Ouzhpennañ ur gemennadenn"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Krouiñ ur gemennadenn"},
            {"createGameMenuBackButton", "Distreiñ =>"},
            {"createGameMenuStartGame", "Kregiñ ar gemennadenn"},
            {"createGameMenuID", "ID ar gemennadenn : "},
            {"createGameMenuPlayer1", "Mestr 1 : "},
            {"createGameMenuPlayer2", "Mestr 2 : "},
            {"createGameMenuPlayer3", "Mestr 3 : "},
            {"createGameMenuPlayer4", "Mestr 4 : "},
            
            //Join Game Menu
            {"joinGameMenuTitle", "Ouzhpennañ ur gemennadenn"},
            {"joinGameMenuBackButton", "<= Distreiñ"},
            {"joinGameMenuGameID", "ID ar gemennadenn"},
            
            //Select Class Menu
            {"Archer", "Sagittaire"},
            {"Knight", "Kavaler"},
            {"Scientist", "Skiantour"},
            {"Assassin", "Lec'helour"},
            {"selectClassMenuReadyButton", "Gwir"},
            {"selectClassMenuWaitingText", "Ouzhpennet gant ar c'hemmennadennoù all..."},
            
            //Map loading
            {"gameLoadingMapText", "Ouzhpennañ ar c'hartenn : "},
            {"gameLoadingMapWaitingText", "Ouzhpennet gant ar c'hemmennadennoù all"},
            
            //Pause Menu
            {"pauseMenuTitle", "Meneger"},
            {"pauseMenuResumeButton", "Adkregiñ"},
            {"pauseMenuSettingsButton", "Arventennoù"},
            {"pauseMenuLeaveButton", "Distroiñ"},
            {"pauseMenuConfirmLeaveTitle", "Ha c'hoant hoc'h eus distreiñ ar gemennadenn ?"},
            {"pauseMenuConfirmLeaveButton", "Distroiñ"},
            {"pauseMenuCancelLeaveButton", "Nullañ"},
            
            //Settings Menu
            {"settingsMenuTitle", "Arventennoù"},
            {"settingsMenuBackButton", "<= Distreiñ"},
            {"settingsMenuResetButton", "Adsevel"},
            {"settingsMenuSaveButton", "Enrollañ"},
            
            //Settings Menu - Game
            {"settingsMenuGameButton", "Gemennadenn"},
            {"settingsMenuGameLanguage", "Yezhioù :"},
            {"settingsMenuGameMouseSensibility", "Sensibilite ar rodell :"},
            {"settingsMenuGameFullScreen", "Plein skramm :"},
            {"settingsMenuGameEnableChat", "Gweredekaat ar c'hat :"},
            {"settingsMenuGameChatSize", "Ment ar c'hat :"},
            {"settingsMenuGameChatSizeSmall", "Berr"},
            {"settingsMenuGameChatSizeMedium", "Medi"},
            {"settingsMenuGameChatSizeLarge", "Hir"},
            
            //Settings Menu - Audio
            {"settingsMenuAudioButton", "Sonerezh"},
            
            //Settings Menu - Video
            {"settingsMenuVideoButton", "Skeudenn"},
            
            //Settings Menu - Controls
            {"settingsMenuControlsButton", "Kontrollerezhioù"},
            {"settingsMenuControlsMoveForward", "Aheñvel"},
            {"settingsMenuControlsMoveBackward", "Distreiñ"},
            {"settingsMenuControlsMoveLeft", "Kleiz"},
            {"settingsMenuControlsMoveRight", "Dehou"},
            {"settingsMenuControlsSprint", "Sprint"},
            {"settingsMenuControlsDash", "Dash"},
            {"settingsMenuControlsCapacity1", "Kapac'h 1"},
            {"settingsMenuControlsCapacity2", "Kapac'h 2"},
            {"settingsMenuControlsCapacity3", "Kapac'h 3"},
            {"settingsMenuControlsItem1", "Danvez 1"},
            {"settingsMenuControlsItem2", "Danvez 2"},
            {"settingsMenuControlsItem3", "Danvez 3"},
            {"settingsMenuControlsInventory", "Inventori"},
            {"settingsMenuControlsReload", "Adsevel"},
            {"settingsMenuControlsChat", "C'hat"},
            {"settingsMenuControlsEnableChat", "Gweredekaat ar c'hat"},
            {"settingsMenuControlsPause", "Meneger"},
            
            //Skill Description
            {"skillDescriptionSpeed", "Ouzhpennañ vitesse an dudenn.\nVitesse : "},
            {"skillDescriptionHealth", "Ouzhpennañ poentioù buhez an dudenn.\nBuhez : "},
            {"skillDescriptionDamage", "Ouzhpennañ an dazont a zo bet graet gant ar c'hemennadenn.\nDazont : "},
            {"skillDescriptionStamina", "Ouzhpennañ an endurans an dudenn evit desachañ a bep seurt.\nEndurans : "},
            {"skillDescriptionReload", "Dimeziñ an amzer ret evit adsevel an endurans.\nAdsevel an endurans : "},
            {"skillDescriptionStaminaUse", "Klask ar c'hemennadenn da zegas endurans d'ar c'hemennadenn.\nEndurans : "},
            {"skillDescriptionArrow", "Ouzhpennañ ur flac'henn ouzhpenn pa vez lakaet ar c'hemennadenn da ziskouez (maksimum 3 flac'henn).\nFlac'henn : +1"},
            {"skillDescriptionShootSpeed", "Ouzhpennañ vitesse ar c'hemennadenn da ziskouez ar flac'henn.\nVitesse ar c'hemennadenn : "},
            {"skillDescriptionArrowPoison", "Ar flac'henn a vez lakaet gant gwez, a zegas dazont war ar c'hemennadenn.\nAmzer gwez : 5s"},
            {"skillDescriptionArrowGel", "Ar flac'henn a vez lakaet gant gel, a zegas efedoù arz hag a ziskouez ar c'hemennadenn evit un nebeud eilenn.\nAmzer gel : 5s"},
            {"skillDescriptionHealSpeed", "Ouzhpennañ vitesse ar c'hemennadenn da ziskouez ar gwerzenn.\nVitesse gwerzenn evit eilenn : "},
            {"skillDescriptionLaserMove", "Aotren d'ar c'hemennadenn da moullañ ar laser pa vez lakaet."},
            {"skillDescriptionRevive", "Aotren d'ar c'hemennadenn da ziskouez ar c'hemennadenn dremenet."},
            {"skillDescriptionVampire", "Aotren d'ar c'hemennadenn da drenanat buhez d'ar c'hemennadenn.\nDrenanat buhez : 10% an dazont"},
            {"skillDescriptionReviveAll", "N'eo ket posupl implijout ar c'hem en un nebeud eilenn. A ziskouez ar c'hemennadenn dremenet hag a zegas anezho da lec'h ar c'hemennadenn."},
            {"skillDescriptionReloadProtection", "Aotren ul lec'h war ar c'hemennadenn pa vez adsevet.\nDiwallit an dazont : 50%"},
            {"skillDescriptionCrit", "Ouzhpennañ an tu d'en em gavout gant ur c'hemennadenn.\nTu d'en em gavout : "},
            {"skillDescriptionRange", "Ouzhpennañ ar c'hentelioù a zo graet gant ar c'hemennadenn.\nKentelioù : +20%"},
            {"skillDescriptionAgro", "Aotren d'ar c'hemennadenn da zegas ar c'hargoù a zo war ar c'hemennadenn."},
            {"skillDescriptionSpike", "Lakaat ur pennad a bep seurt an dazont gant ar c'hemennadenn.\nDazont : 20%"},
            {"skillDescriptionAbsorption", "Absorbiñ an dazont a vez bet graet gant ar c'hemennadenn all a bep seurt.\nDazont : 5m"},
            {"skillDescriptionInvincibility", "Aotren ur c'hemennadenn da vezañ a ziskouez. \nAmzer a ziskouez : 5s"},
            {"skillDescriptionEscalibur", "Aotren gant nerzh ar gourc'hemenadenn, Excalibur, gant ur c'hans war 5 eus bep seurt da ziskouez ar c'hemennadenn.\nAmzer a ziskouez : 5s"},
            {"skillDescriptionDashDegat", "Dazont d'ar c'hemennadenn da ziskouez dazont dremenet pa vez ur atakenn en dash.\nDazont : 30%"},
            {"skillDescriptionDague", "Aotren d'ar c'hemennadenn da lanzell dageoù."},
            {"skillDescriptionInvisibility", "Aotren d'ar c'hemennadenn da vezañ dremenet evit un nebeud eilenn.\nAmzer dremenet : 10s"},
            {"skillDescriptionDoubleAttack", "Aotren d'ar c'hemennadenn da gomz eilenn."},
            {"skillDescriptionPoison", "Ar c'hemennadenn a vez lakaet gant gwez, a zegas dazont war ar c'hemennadenn.\nAmzer gwez : 5s"},
            {"skillChooseTitle", "Choazit ho kempennadenn nevez"},
            
            //Potion
            {"PotionBuy", "Prañ"},
            {"PotionPrice", "Priz : "},
            {"PotionTitle0", "Potion Buhez"},
            {"PotionDescription0", "Adsevel 40% eus buhez ar c'hemennadenn."},
            {"PotionTitle1", "Potion Endurans"},
            {"PotionDescription1", "Adsevel 60% eus endurans ar c'hemennadenn"},
            {"PotionTitle2", "Potion Vitesse"},
            {"PotionDescription2", "Gwiriañ an vitesse gant 1.5 evit 20 eilenn "},
            {"PotionTitle3", "Potion Digeriad"},
            {"PotionDescription3", "Rannan an dazont gant 2 evit 20 eilenn "},
            {"PotionTitle4", "Potion Adsevel"},
            {"PotionDescription4", "Adsevel ur c'hemennadenn a zo a bep seurt (da 50% eus buhez)"},
            {"PotionTitle5", "Potion Memoria"},
            {"PotionDescription5", "Lakaat da benn ur c'hemennadenn bet graet"},
        },
        
        new Dictionary<string, string>
        {
            {"languageName", "Duetsch"},
            
            //Connection Menu
            {"connectionMenuTitle", "Verbindung"},
            {"connectionMenuPseudoText", "Pseudonym"},
            {"connectionMenuPasswordText", "Passwort"},
            {"connectionMenuConnectionButton", "Verbindung"},
            {"connectionMenuNoAccountButton", "Kein Konto?"},
            {"connectionMenuFastConnectionButton", "Schnelle Verbindung"},
            {"connectionMenuErrorUsernameOrPasswordText", "Falscher Benutzername oder falsches Passwort"},
            {"connectionMenuErrorFastConnectionText", "Fehler bei der schnellen Verbindung"},
            {"connectionMenuErrorNoFastConnectionText", "Keine schnellen Verbindungen registriert"},
            {"connectionMenuFastConnectionSaveButton", "Speichern Sie den Benutzernamen und das Passwort lokal."},
            {"connectionMenuFastConnectionSaveWarningText", "/!\\ Warnung :\nDas Speichern Ihres Benutzernamens und Passworts lokal erleichtert das Einloggen, kann aber Sicherheitsrisiken mit sich bringen. Wenn Ihr Gerät kompromittiert ist, können Ihre Informationen zugänglich sein."},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Einschreibung"},
            {"inscriptionMenuPseudoText", "Pseudonym"},
            {"inscriptionMenuPasswordText", "Passwort"},
            {"inscriptionMenuPasswordConfirmText", "Passwort bestätigen"},
            {"inscriptionMenuInscriptionButton", "Einschreibung"},
            {"inscriptionMenuAlreadyAccountButton", "Bereits ein Konto?"},
            {"inscriptionMenuErrorIncorrectConfirmText", "Falsches Bestätigungspasswort"},
            {"inscriptionMenuErrorAlreadyExistText", "Benutzername existiert bereits"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Lobby"},
            {"lobbyMenuCreateGame", "Spiel erstellen"},
            {"lobbyMenuJoinGame", "Spiel beitreten"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Spiel erstellen"},
            {"createGameMenuBackButton", "Zurück =>"},
            {"createGameMenuStartGame", "Spiel starten"},
            {"createGameMenuID", "Spiel-ID : "},
            {"createGameMenuPlayer1", "Spieler 1 : "},
            {"createGameMenuPlayer2", "Spieler 2 : "},
            {"createGameMenuPlayer3", "Spieler 3 : "},
            {"createGameMenuPlayer4", "Spieler 4 : "},
            
            //Join Game Menu
            {"joinGameMenuTitle", "Spiel beitreten"},
            {"joinGameMenuBackButton", "<= Zurück"},
            {"joinGameMenuGameID", "Spiel-ID"},
            
            //Select Class Menu
            {"Archer", "Bogenschütze"},
            {"Knight", "Ritter"},
            {"Scientist", "Wissenschaftler"},
            {"Assassin", "Attentäter"},
            {"selectClassMenuReadyButton", "Bereit"},
            {"selectClassMenuWaitingText", "Warten auf die anderen Spieler..."},
            
            //Map loading
            {"gameLoadingMapText", "Laden der Karte : "},
            {"gameLoadingMapWaitingText", "Warten auf die anderen Spieler"},
            
            //Pause Menu
            {"pauseMenuTitle", "Pause Menü"},
            {"pauseMenuResumeButton", "Fortsetzen"},
            {"pauseMenuSettingsButton", "Einstellungen"},
            {"pauseMenuLeaveButton", "Verlassen"},
            {"pauseMenuConfirmLeaveTitle", "Bist du sicher, dass du das Spiel verlassen willst?"},
            {"pauseMenuConfirmLeaveButton", "Verlassen"},
            {"pauseMenuCancelLeaveButton", "Abbrechen"},
            
            //Settings Menu
            {"settingsMenuTitle", "Einstellungen"},
            {"settingsMenuBackButton", "<= Zurück"},
            {"settingsMenuResetButton", "Zurücksetzen"},
            {"settingsMenuSaveButton", "Speichern"},
            
            //Settings Menu - Game
            {"settingsMenuGameButton", "Spiel"},
            {"settingsMenuGameLanguage", "Sprache :"},
            {"settingsMenuGameMouseSensibility", "Mausempfindlichkeit :"},
            {"settingsMenuGameFullScreen", "Vollbild :"},
            {"settingsMenuGameEnableChat", "Chat aktivieren :"},
            {"settingsMenuGameChatSize", "Chatgröße :"},
            {"settingsMenuGameChatSizeSmall", "Klein"},
            {"settingsMenuGameChatSizeMedium", "Mittel"},
            {"settingsMenuGameChatSizeLarge", "Groß"},
            
            //Settings Menu - Audio
            {"settingsMenuAudioButton", "Audio"},
            
            //Settings Menu - Video
            {"settingsMenuVideoButton", "Video"},
            
            //Settings Menu - Controls
            {"settingsMenuControlsButton", "Steuerung"},
            {"settingsMenuControlsMoveForward", "Vorwärts"},
            {"settingsMenuControlsMoveBackward", "Rückwärts"},
            {"settingsMenuControlsMoveLeft", "Links"},
            {"settingsMenuControlsMoveRight", "Rechts"},
            {"settingsMenuControlsSprint", "Sprinten"},
            {"settingsMenuControlsDash", "Dash"},
            {"settingsMenuControlsCapacity1", "Fähigkeit 1"},
            {"settingsMenuControlsCapacity2", "Fähigkeit 2"},
            {"settingsMenuControlsCapacity3", "Fähigkeit 3"},
            {"settingsMenuControlsItem1", "Gegenstand 1"},
            {"settingsMenuControlsItem2", "Gegenstand 2"},
            {"settingsMenuControlsItem3", "Gegenstand 3"},
            {"settingsMenuControlsInventory", "Inventar"},
            {"settingsMenuControlsReload", "Nachladen"},
            {"settingsMenuControlsChat", "Chat"},
            {"settingsMenuControlsEnableChat", "Chat aktivieren"},
            {"settingsMenuControlsPause", "Pause"},
            
            //Skill Description
            {"skillDescriptionSpeed", "Erhöht die Bewegungsgeschwindigkeit des Charakters.\nGeschwindigkeit : "},
            {"skillDescriptionHealth", "Erhöht die maximalen Lebenspunkte des Charakters.\nLeben : "},
            {"skillDescriptionDamage", "Erhöht den Schaden, den der Charakter verursacht.\nSchaden : "},
            {"skillDescriptionStamina", "Erhöht die Ausdauer des Charakters für anhaltende Aktionen.\nAusdauer : "},
            {"skillDescriptionReload", "Verringert die Zeit, die zum Nachladen der Ausdauer benötigt wird.\nAusdauer nachladen : "},
            {"skillDescriptionStaminaUse", "Verringert die vom Charakter verbrauchte Ausdauer für Aktionen.\nAusdauerverbrauch : "},
            {"skillDescriptionArrow", "Fügt dem Bogenschützen beim Abschießen einen zusätzlichen Pfeil hinzu (maximal 3 Pfeile).\nPfeile : +1"},
            {"skillDescriptionShootSpeed", "Erhöht die Geschwindigkeit, mit der die Pfeile abgeschossen werden.\nSchussgeschwindigkeit : "},
            {"skillDescriptionArrowPoison", "Die Pfeile sind mit Gift getränkt, das über einen bestimmten Zeitraum Schaden verursacht.\nGiftzeit : 5s"},
            {"skillDescriptionArrowGel", "Die Pfeile sind mit Gel umhüllt, das möglicherweise Verlangsamungseffekte verursacht und die Feinde für einige Sekunden betäubt.\nGelzeit : 5s"},
            {"skillDescriptionHealSpeed", "Erhöht die Geschwindigkeit, mit der die Heilung erfolgt.\nHeilgeschwindigkeit pro Sekunde : "},
            {"skillDescriptionLaserMove", "Ermöglicht es dem Charakter, den Laser zu bewegen, während er ihn abfeuert."},
            {"skillDescriptionRevive", "Ermöglicht es dem Charakter, gefallene Verbündete wiederzubeleben."},
            {"skillDescriptionVampire", "Ermöglicht es dem Charakter, die Gesundheit der Feinde abzuziehen.\nGesundheitsabzug : 10% des Schadens"},
            {"skillDescriptionReviveAll", "Diese Fähigkeit kann nur einmal im gesamten Spiel verwendet werden. Sie belebt alle gefallenen Verbündeten wieder und teleportiert sie an die Stelle des Charakters."},
            {"skillDescriptionReloadProtection", "Bietet Schutz während des Nachladens.\nSchadensreduktion : 50%"},
            {"skillDescriptionCrit", "Erhöht die Wahrscheinlichkeit, kritische Treffer zu erzielen.\nKritische Trefferchance : "},
            {"skillDescriptionRange", "Erweitert die Reichweite der Angriffe des Charakters.\nReichweite : +20%"},
            {"skillDescriptionAgro", "Ermöglicht es dem Charakter, die Aggro aller Feinde in der Nähe zu ziehen."},
            {"skillDescriptionSpike", "Reflektiert einen Teil des Schadens, der dem Angreifer zugefügt wird.\nSchadensreflexion : 20%"},
            {"skillDescriptionAbsorption", "Absorbiert den Schaden, den andere Spieler in einem bestimmten Radius erleiden.\nRadius : 5m"},
            {"skillDescriptionInvincibility", "Gewährt eine temporäre Unverwundbarkeit. \nUnverwundbarkeitszeit : 5s"},
            {"skillDescriptionEscalibur", "Gibt dem Charakter die Macht des legendären Schwertes Excalibur, mit einer Chance von 1 zu 5, die Feinde einzufrieren.\nGelzeit : 5s"},
            {"skillDescriptionDashDegat", "Fügt den Feinden Schaden zu, wenn der Charakter einen Angriff im Dash ausführt.\nSchaden : 30%"},
            {"skillDescriptionDague", "Ermöglicht es dem Charakter, Dolche zu werfen."},
            {"skillDescriptionInvisibility", "Ermöglicht es dem Charakter, für kurze Zeit unsichtbar zu werden.\nUnsichtbarkeitszeit : 10s"},
            {"skillDescriptionDoubleAttack", "Ermöglicht es dem Charakter, Doppelangriffe auszuführen."},
            {"skillDescriptionPoison", "Tränkt die Angriffe mit Gift, das über einen bestimmten Zeitraum Schaden verursacht.\nGiftzeit : 5s"},
            {"skillChooseTitle", "Wählen Sie Ihre neue Fähigkeit"},
            
            //Potion
            {"PotionBuy", "Kaufen"},
            {"PotionPrice", "Preis : "},
            {"PotionTitle0", "Lebenspotion"},
            {"PotionDescription0", "Stellt 40% der Lebenspunkte des Spielers wieder her."},
            {"PotionTitle1", "Ausdauerpotion"},
            {"PotionDescription1", "Stellt 60% der Ausdauer des Spielers wieder her"},
            {"PotionTitle2", "Geschwindigkeitspotion"},
            {"PotionDescription2", "Multipliziert die Geschwindigkeit um 1,5 für 20 Sekunden "},
            {"PotionTitle3", "Widerstandspotion"},
            {"PotionDescription3", "Teilt den Schaden für 20 Sekunden durch 2 "},
            {"PotionTitle4", "Wiederbelebungspotion"},
            {"PotionDescription4", "Belebt einen nahegelegenen Spieler wieder (auf 50% Leben)"},
            {"PotionTitle5", "Amnesiepotion"},
            {"PotionDescription5", "Löscht eine vom Spieler erworbene Fähigkeit"},
        },
        
        new Dictionary<string, string>
        {
            {"languageName", "Español"},
            
            //Connection Menu
            {"connectionMenuTitle", "Conexión"},
            {"connectionMenuPseudoText", "Seudónimo"},
            {"connectionMenuPasswordText", "Contraseña"},
            {"connectionMenuConnectionButton", "Conexión"},
            {"connectionMenuNoAccountButton", "¿No tienes una cuenta?"},
            {"connectionMenuFastConnectionButton", "Conexión Rápida"},
            {"connectionMenuErrorUsernameOrPasswordText", "Nombre de usuario o contraseña incorrectos"},
            {"connectionMenuErrorFastConnectionText", "Error en la conexión rápida"},
            {"connectionMenuErrorNoFastConnectionText", "No hay conexiones rápidas registradas"},
            {"connectionMenuFastConnectionSaveButton", "Guardar el nombre de usuario y la contraseña localmente."},
            {"connectionMenuFastConnectionSaveWarningText", "/!\\ Advertencia :\nGuardar su nombre de usuario y contraseña localmente facilita el inicio de sesión, pero puede conllevar riesgos de seguridad. Si su dispositivo está comprometido, su información podría ser accesible."},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Inscripción"},
            {"inscriptionMenuPseudoText", "Seudónimo"},
            {"inscriptionMenuPasswordText", "Contraseña"},
            {"inscriptionMenuPasswordConfirmText", "Confirmar contraseña"},
            {"inscriptionMenuInscriptionButton", "Inscripción"},
            {"inscriptionMenuAlreadyAccountButton", "¿Ya tienes una cuenta?"},
            {"inscriptionMenuErrorIncorrectConfirmText", "Contraseña de confirmación incorrecta"},
            {"inscriptionMenuErrorAlreadyExistText", "Nombre de usuario ya existente"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Lobby"},
            {"lobbyMenuCreateGame", "Crear juego"},
            {"lobbyMenuJoinGame", "Unirse al juego"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Crear juego"},
            {"createGameMenuBackButton", "Regreso =>"},
            {"createGameMenuStartGame", "Comenzar el juego"},
            {"createGameMenuID", "ID del juego : "},
            {"createGameMenuPlayer1", "Jugador 1 : "},
            {"createGameMenuPlayer2", "Jugador 2 : "},
            {"createGameMenuPlayer3", "Jugador 3 : "},
            {"createGameMenuPlayer4", "Jugador 4 : "},
            
            //Join Game Menu
            {"joinGameMenuTitle", "Unirse al juego"},
            {"joinGameMenuBackButton", "<= Regreso"},
            {"joinGameMenuGameID", "ID del juego"},
            
            //Select Class Menu
            {"Archer", "Arquero"},
            {"Knight", "Caballero"},
            {"Scientist", "Científico"},
            {"Assassin", "Asesino"},
            {"selectClassMenuReadyButton", "Listo"},
            {"selectClassMenuWaitingText", "Esperando a los otros jugadores..."},
            
            //Map loading
            {"gameLoadingMapText", "Cargando el mapa : "},
            {"gameLoadingMapWaitingText", "Esperando a los otros jugadores"},
            
            //Pause Menu
            {"pauseMenuTitle", "Menú de pausa"},
            {"pauseMenuResumeButton", "Reanudar"},
            {"pauseMenuSettingsButton", "Ajustes"},
            {"pauseMenuLeaveButton", "Dejar"},
            {"pauseMenuConfirmLeaveTitle", "¿Estás seguro de que quieres dejar el juego?"},
            {"pauseMenuConfirmLeaveButton", "Dejar"},
            {"pauseMenuCancelLeaveButton", "Cancelar"},
            
            //Settings Menu
            {"settingsMenuTitle", "Ajustes"},
            {"settingsMenuBackButton", "<= Regreso"},
            {"settingsMenuResetButton", "Reiniciar"},
            {"settingsMenuSaveButton", "Guardar"},
            
            //Settings Menu - Game
            {"settingsMenuGameButton", "Juego"},
            {"settingsMenuGameLanguage", "Idioma :"},
            {"settingsMenuGameMouseSensibility", "Sensibilidad del ratón :"},
            {"settingsMenuGameFullScreen", "Pantalla completa :"},
            {"settingsMenuGameEnableChat", "Activar el chat :"},
            {"settingsMenuGameChatSize", "Tamaño del chat :"},
            {"settingsMenuGameChatSizeSmall", "Pequeño"},
            {"settingsMenuGameChatSizeMedium", "Medio"},
            {"settingsMenuGameChatSizeLarge", "Grande"},
            
            //Settings Menu - Audio
            {"settingsMenuAudioButton", "Audio"},
            
            //Settings Menu - Video
            {"settingsMenuVideoButton", "Video"},
            
            //Settings Menu - Controls
            {"settingsMenuControlsButton", "Controles"},
            {"settingsMenuControlsMoveForward", "Adelante"},
            {"settingsMenuControlsMoveBackward", "Atrás"},
            {"settingsMenuControlsMoveLeft", "Izquierda"},
            {"settingsMenuControlsMoveRight", "Derecha"},
            {"settingsMenuControlsSprint", "Sprint"},
            {"settingsMenuControlsDash", "Dash"},
            {"settingsMenuControlsCapacity1", "Capacidad 1"},
            {"settingsMenuControlsCapacity2", "Capacidad 2"},
            {"settingsMenuControlsCapacity3", "Capacidad 3"},
            {"settingsMenuControlsItem1", "Objeto 1"},
            {"settingsMenuControlsItem2", "Objeto 2"},
            {"settingsMenuControlsItem3", "Objeto 3"},
            {"settingsMenuControlsInventory", "Inventario"},
            {"settingsMenuControlsReload", "Recargar"},
            {"settingsMenuControlsChat", "Chat"},
            {"settingsMenuControlsEnableChat", "Activar el chat"},
            {"settingsMenuControlsPause", "Pausa"},
            
            //Skill Description
            {"skillDescriptionSpeed", "Aumenta la velocidad de movimiento del personaje.\nVelocidad : "},
            {"skillDescriptionHealth", "Aumenta los puntos de vida máximos del personaje.\nVida : "},
            {"skillDescriptionDamage", "Aumenta el daño que inflige el personaje.\nDaño : "},
            {"skillDescriptionStamina", "Aumenta la resistencia del personaje para acciones continuas.\nResistencia : "},
            {"skillDescriptionReload", "Reduce el tiempo necesario para recargar la resistencia.\nRecargar resistencia : "},
            {"skillDescriptionStaminaUse", "Reduce la resistencia consumida por el personaje para acciones.\nConsumo de resistencia : "},
            {"skillDescriptionArrow", "Añade una flecha adicional al arquero al disparar (máximo 3 flechas).\nFlechas : +1"},
            {"skillDescriptionShootSpeed", "Aumenta la velocidad a la que se disparan las flechas.\nVelocidad de disparo : "},
            {"skillDescriptionArrowPoison", "Las flechas están impregnadas de veneno, que inflige daño durante un período de tiempo.\nTiempo de veneno : 5s"},
            {"skillDescriptionArrowGel", "Las flechas están envueltas en gel, que puede causar efectos de ralentización y aturdir a los enemigos durante unos segundos.\nTiempo de gel : 5s"},
            {"skillDescriptionHealSpeed", "Aumenta la velocidad a la que se cura.\nVelocidad de curación por segundo : "},
            {"skillDescriptionLaserMove", "Permite al personaje mover el láser mientras lo dispara."},
            {"skillDescriptionRevive", "Permite al personaje revivir a los aliados caídos."},
            {"skillDescriptionVampire", "Permite al personaje drenar la vida de los enemigos.\nDrenaje de vida : 10% del daño"},
            {"skillDescriptionReviveAll", "Esta habilidad solo se puede usar una vez en todo el juego. Revive a todos los aliados caídos y los teletransporta a la posición del personaje."},
            {"skillDescriptionReloadProtection", "Proporciona protección durante la recarga.\nReducción de daño : 50%"},
            {"skillDescriptionCrit", "Aumenta la probabilidad de infligir golpes críticos.\nProbabilidad de golpe crítico : "},
            {"skillDescriptionRange", "Amplía el rango de los ataques del personaje.\nRango : +20%"},
            {"skillDescriptionAgro", "Permite al personaje atraer a todos los enemigos cercanos."},
            {"skillDescriptionSpike", "Refleja una parte del daño infligido al atacante.\nReflejo de daño : 20%"},
            {"skillDescriptionAbsorption", "Absorbe el daño que otros jugadores sufren en un radio determinado.\nRadio : 5m"},
            {"skillDescriptionInvincibility", "Concede una invencibilidad temporal. \nTiempo de invencibilidad : 5s"},
            {"skillDescriptionEscalibur", "Otorga al personaje el poder de la legendaria espada Excalibur, con una probabilidad de 1 entre 5 de congelar a los enemigos.\nTiempo de congelación : 5s"},
            {"skillDescriptionDashDegat", "Inflige daño a los enemigos cuando el personaje realiza un ataque en el Dash.\nDaño : 30%"},
            {"skillDescriptionDague", "Permite al personaje lanzar dagas."},
            {"skillDescriptionInvisibility", "Permite al personaje volverse invisible por un corto período de tiempo.\nTiempo de invisibilidad : 10s"},
            {"skillDescriptionDoubleAttack", "Permite al personaje realizar ataques dobles."},
            {"skillDescriptionPoison", "Impregna los ataques con veneno, que inflige daño durante un período de tiempo.\nTiempo de veneno : 5s"},
            {"skillChooseTitle", "Elige tu nueva habilidad"},
            
            //Potion
            {"PotionBuy", "Comprar"},
            {"PotionPrice", "Precio : "},
            {"PotionTitle0", "Poción de vida"},
            {"PotionDescription0", "Restaura el 40% de la vida del personaje."},
            {"PotionTitle1", "Poción de resistencia"},
            {"PotionDescription1", "Restaura el 60% de la resistencia del personaje"},
            {"PotionTitle2", "Poción de velocidad"},
            {"PotionDescription2", "Multiplica la velocidad por 1,5 durante 20 segundos "},
            {"PotionTitle3", "Poción de resistencia"},
            {"PotionDescription3", "Reduce el daño a la mitad durante 20 segundos "},
            {"PotionTitle4", "Poción de resurrección"},
            {"PotionDescription4", "Revive a un jugador cercano (al 50% de vida)"},
            {"PotionTitle5", "Poción de amnesia"},
            {"PotionDescription5", "Elimina una habilidad adquirida por el jugador"},
        },
        
        new Dictionary<string, string>
        {
            {"languageName", "Esperanto"}, 
            
            //Connection Menu
            {"connectionMenuTitle", "Konekto"},
            {"connectionMenuPseudoText", "Pseŭdonomo"},
            {"connectionMenuPasswordText", "Pasvorto"},
            {"connectionMenuConnectionButton", "Konekto"},
            {"connectionMenuNoAccountButton", "Ĉu vi ne havas konton?"},
            {"connectionMenuFastConnectionButton", "Rapida Konekto"},
            {"connectionMenuErrorUsernameOrPasswordText", "Malĝusta uzantnomo aŭ pasvorto"},
            {"connectionMenuErrorFastConnectionText", "Eraro en rapida konekto"},
            {"connectionMenuErrorNoFastConnectionText", "Neniu rapida konekto registrita"},
            {"connectionMenuFastConnectionSaveButton", "Konservu la uzantnomon kaj la pasvorton lokale."},
            {"connectionMenuFastConnectionSaveWarningText", "/!\\ Averto :\nKonservi vian uzantnomon kaj pasvorton lokale faciligas ensaluton, sed povas impliki sekurecajn riskojn. Se via aparato estas kompromitita, via informo povus esti alirebla."},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Inskribo"},
            {"inscriptionMenuPseudoText", "Pseŭdonomo"},
            {"inscriptionMenuPasswordText", "Pasvorto"},
            {"inscriptionMenuPasswordConfirmText", "Konfirmu pasvorton"},
            {"inscriptionMenuInscriptionButton", "Inskribo"},
            {"inscriptionMenuAlreadyAccountButton", "Ĉu vi jam havas konton?"},
            {"inscriptionMenuErrorIncorrectConfirmText", "Malĝusta konfirmo de pasvorto"},
            {"inscriptionMenuErrorAlreadyExistText", "Uzantnomo jam ekzistas"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Ludejo"},
            {"lobbyMenuCreateGame", "Krei ludon"},
            {"lobbyMenuJoinGame", "Aliĝi al ludo"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Krei ludon"},
            {"createGameMenuBackButton", "Reen =>"},
            {"createGameMenuStartGame", "Komenci la ludon"},
            {"createGameMenuID", "Ludo-ID : "},
            {"createGameMenuPlayer1", "Ludanto 1 : "},
            {"createGameMenuPlayer2", "Ludanto 2 : "},
            {"createGameMenuPlayer3", "Ludanto 3 : "},
            {"createGameMenuPlayer4", "Ludanto 4 : "},
            
            //Join Game Menu
            {"joinGameMenuTitle", "Aliĝi al ludo"},
            {"joinGameMenuBackButton", "<= Reen"},
            {"joinGameMenuGameID", "Ludo-ID"},
            
            //Select Class Menu
            {"Archer", "Arĉisto"},
            {"Knight", "Kavaliro"},
            {"Scientist", "Sciencaĵisto"},
            {"Assassin", "Asasino"},
            {"selectClassMenuReadyButton", "Preparita"},
            {"selectClassMenuWaitingText", "Atendante la aliajn ludantojn..."},
            
            //Map loading
            {"gameLoadingMapText", "Ŝarĝante la mapon : "},
            {"gameLoadingMapWaitingText", "Atendante la aliajn ludantojn"},
            
            //Pause Menu
            {"pauseMenuTitle", "Paŭzo Menuo"},
            {"pauseMenuResumeButton", "Daŭrigi"},
            {"pauseMenuSettingsButton", "Agordoj"},
            {"pauseMenuLeaveButton", "Forlasi"},
            {"pauseMenuConfirmLeaveTitle", "Ĉu vi certas, ke vi volas forlasi la ludon?"},
            {"pauseMenuConfirmLeaveButton", "Forlasi"},
            {"pauseMenuCancelLeaveButton", "Nuligi"},
            
            //Settings Menu
            {"settingsMenuTitle", "Agordoj"},
            {"settingsMenuBackButton", "<= Reen"},
            {"settingsMenuResetButton", "Rekomenci"},
            {"settingsMenuSaveButton", "Konservi"},
            
            //Settings Menu - Game
            {"settingsMenuGameButton", "Ludo"},
            {"settingsMenuGameLanguage", "Lingvo :"},
            {"settingsMenuGameMouseSensibility", "Musmovo :"},
            {"settingsMenuGameFullScreen", "Plena ekrano :"},
            {"settingsMenuGameEnableChat", "Aktivigi babilejon :"},
            {"settingsMenuGameChatSize", "Babileja grandeco :"},
            {"settingsMenuGameChatSizeSmall", "Malgranda"},
            {"settingsMenuGameChatSizeMedium", "Meza"},
            {"settingsMenuGameChatSizeLarge", "Granda"},
            
            //Settings Menu - Audio
            {"settingsMenuAudioButton", "Aŭdo"},
            
            //Settings Menu - Video
            {"settingsMenuVideoButton", "Video"},
            
            //Settings Menu - Controls
            {"settingsMenuControlsButton", "Regado"},
            {"settingsMenuControlsMoveForward", "Antaŭen"},
            {"settingsMenuControlsMoveBackward", "Malantaŭen"},
            {"settingsMenuControlsMoveLeft", "Maldekstren"},
            {"settingsMenuControlsMoveRight", "Dekstren"},
            {"settingsMenuControlsSprint", "Sprinto"},
            {"settingsMenuControlsDash", "Dasko"},
            {"settingsMenuControlsCapacity1", "Kapablo 1"},
            {"settingsMenuControlsCapacity2", "Kapablo 2"},
            {"settingsMenuControlsCapacity3", "Kapablo 3"},
            {"settingsMenuControlsItem1", "Elemento 1"},
            {"settingsMenuControlsItem2", "Elemento 2"},
            {"settingsMenuControlsItem3", "Elemento 3"},
            {"settingsMenuControlsInventory", "Enportaĵo"},
            {"settingsMenuControlsReload", "Reŝarĝi"},
            {"settingsMenuControlsChat", "Babilejo"},
            {"settingsMenuControlsEnableChat", "Aktivigi babilejon"},
            {"settingsMenuControlsPause", "Paŭzo"},
            
            //Skill Description
            {"skillDescriptionSpeed", "Pligrandigas la moviĝan rapidecon de la karaktero.\nRapideco : "},
            {"skillDescriptionHealth", "Pligrandigas la maksimuman vivpunktojn de la karaktero.\nVivo : "},
            {"skillDescriptionDamage", "Pligrandigas la damaĝon kaŭzitan de la karaktero.\nDamaĝo : "},
            {"skillDescriptionStamina", "Pligrandigas la karakteran rezistecon por daŭraj agoj.\nRezisto : "},
            {"skillDescriptionReload", "Malpliigas la tempon necesan por reŝarĝi la reziston.\nReŝarĝi reziston : "},
            {"skillDescriptionStaminaUse", "Malpliigas la reziston konsumitan de la karaktero por agoj.\nRezisto konsumo : "},
            {"skillDescriptionArrow", "Aldonas al la pafisto plian sagon dum pafado (maksimume 3 sagoj).\nSagoj : +1"},
            {"skillDescriptionShootSpeed", "Pligrandigas la rapidecon, kun kiu la sagoj estas pafitaj.\nPafrapideco : "},
            {"skillDescriptionArrowPoison", "La sagoj estas impregnitaj per veneno, kiu kaŭzas damaĝon dum difinita tempo.\nVeneno tempo : 5s"},
            {"skillDescriptionArrowGel", "La sagoj estas envolvitaj per gelo, kiu povas kaŭzi malrapidigajn efikojn kaj aturigi la malamikojn dum kelkaj sekundoj.\nGelo tempo : 5s"},
            {"skillDescriptionHealSpeed", "Pligrandigas la rapidecon, kun kiu la resanigo okazas.\nResanigo rapideco por sekundo : "},
            {"skillDescriptionLaserMove", "Permesas al la karaktero movi la laseron dum pafado."},
            {"skillDescriptionRevive", "Permesas al la karaktero revivi falintajn aliancanojn."},
            {"skillDescriptionVampire", "Permesas al la karaktero elverŝi la vivon de la malamikoj.\nVivodrenado : 10% de la damaĝo"},
            {"skillDescriptionReviveAll", "Tiu kapablo povas esti uzata nur unufoje en la tuta ludo. Ĝi revivas ĉiujn falintajn aliancanojn kaj teleportas ilin al la loko de la karaktero."},
            {"skillDescriptionReloadProtection", "Provizi protekton dum la reŝarĝo.\nDamaĝredukto : 50%"},
            {"skillDescriptionCrit", "Pligrandigas la verŝajnecon de kritikaj trafoj.\nKritikaj trafoj : "},
            {"skillDescriptionRange", "Etendas la distancon de la karakteraj atakoj.\nDistanco : +20%"},
            {"skillDescriptionAgro", "Permesas al la karaktero altiri la atenton de ĉiuj malamikoj en la proksimeco."},
            {"skillDescriptionSpike", "Rifletas parton de la damaĝo kaŭzita al la atakanto.\nDamaĝo reflekto : 20%"},
            {"skillDescriptionAbsorption", "Absorbas la damaĝon, kiun aliaj ludantoj suferas en difinita radio.\nRadio : 5m"},
            {"skillDescriptionInvincibility", "Koncedas provizoran senmortiĝon. \nSenmortiĝo tempo : 5s"},
            {"skillDescriptionEscalibur", "Donas al la karaktero la potencon de la legenda glavo Excalibur, kun ŝanco de 1 ĝis 5 konfuzi la malamikojn.\nGelo tempo : 5s"},
            {"skillDescriptionDashDegat", "Kaŭzas damaĝon al la malamikoj, kiam la karaktero faras atakon dum la Dasko.\nDamaĝo : 30%"},
            {"skillDescriptionDague", "Permesas al la karaktero ĵeti dolojn."},
            {"skillDescriptionInvisibility", "Permesas al la karaktero malaperi dum mallonga tempo.\nMalaperi tempo : 10s"},
            {"skillDescriptionDoubleAttack", "Permesas al la karaktero fari duoblajn atakojn."},
            {"skillDescriptionPoison", "Impregnas la atakojn per veneno, kiu kaŭzas damaĝon dum difinita tempo.\nVeneno tempo : 5s"},
            {"skillChooseTitle", "Elektu vian novan kapablon"},
            
            //Potion
            {"PotionBuy", "Aĉeti"},
            {"PotionPrice", "Prezo : "},
            {"PotionTitle0", "Vivpocio"},
            {"PotionDescription0", "Restarigas 40% de la vivpunktoj de la ludanto."},
            {"PotionTitle1", "Rezistopocio"},
            {"PotionDescription1", "Restarigas 60% de la rezisto de la ludanto"},
            {"PotionTitle2", "Rapidopocio"},
            {"PotionDescription2", "Multiplikas la rapidon per 1,5 dum 20 sekundoj "},
            {"PotionTitle3", "Rezistopocio"},
            {"PotionDescription3", "Duobligas la damaĝon dum 20 sekundoj "},
            {"PotionTitle4", "Revivopocio"},
            {"PotionDescription4", "Revivas proksiman ludanton (ĉe 50% de vivo)"},
            {"PotionTitle5", "Amnezopocio"},
            {"PotionDescription5", "Forigas akiritan kapablon de la ludanto"},
        },
        
        new Dictionary<string, string>
        {
            {"languageName", "Latinus"},
            
            //Connection Menu
            {"connectionMenuTitle", "Connexio"},
            {"connectionMenuPseudoText", "Pseudonym"},
            {"connectionMenuPasswordText", "Password"},
            {"connectionMenuConnectionButton", "Connexio"},
            {"connectionMenuNoAccountButton", "Non habes rationem?"},
            {"connectionMenuFastConnectionButton", "Connexio celeriter"},
            {"connectionMenuErrorUsernameOrPasswordText", "Pseudonym vel password falsum"},
            {"connectionMenuErrorFastConnectionText", "Error in connexione celeriter"},
            {"connectionMenuErrorNoFastConnectionText", "Nulla connexio celeriter registrata"},
            {"connectionMenuFastConnectionSaveButton", "Serva pseudonym et password localiter."},
            {"connectionMenuFastConnectionSaveWarningText", "/!\\ Cave :\nServare pseudonym et password localiter facilius est in ingressu, sed potest periculum securitatis habere. Si instrumentum tuum compromissum est, tuae informationes possunt esse accessibiles."},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Inscriptio"},
            {"inscriptionMenuPseudoText", "Pseudonym"},
            {"inscriptionMenuPasswordText", "Password"},
            {"inscriptionMenuPasswordConfirmText", "Password confirmare"},
            {"inscriptionMenuInscriptionButton", "Inscriptio"},
            {"inscriptionMenuAlreadyAccountButton", "Habes rationem?"},
            {"inscriptionMenuErrorIncorrectConfirmText", "Password confirmare falsum"},
            {"inscriptionMenuErrorAlreadyExistText", "Pseudonym iam existit"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Lobby"},
            {"lobbyMenuCreateGame", "Ludum creare"},
            {"lobbyMenuJoinGame", "Ludum iungere"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Ludum creare"},
            {"createGameMenuBackButton", "Retro =>"},
            {"createGameMenuStartGame", "Ludum incipere"},
            {"createGameMenuID", "Ludum ID : "},
            {"createGameMenuPlayer1", "Ludens 1 : "},
            {"createGameMenuPlayer2", "Ludens 2 : "},
            {"createGameMenuPlayer3", "Ludens 3 : "},
            {"createGameMenuPlayer4", "Ludens 4 : "},
            
            //Join Game Menu
            {"joinGameMenuTitle", "Ludum iungere"},
            {"joinGameMenuBackButton", "<= Retro"},
            {"joinGameMenuGameID", "Ludum ID"},
            
            //Select Class Menu
            {"Archer", "Sagittarius"},
            {"Knight", "Eques"},
            {"Scientist", "Scientista"},
            {"Assassin", "Assassinus"},
            {"selectClassMenuReadyButton", "Paratus"},
            {"selectClassMenuWaitingText", "Expectans alii ludum..."},
            
            //Map loading
            {"gameLoadingMapText", "Ludum onerans : "},
            {"gameLoadingMapWaitingText", "Expectans alii ludum"},
            
            //Pause Menu
            {"pauseMenuTitle", "Pause Menü"},
            {"pauseMenuResumeButton", "Resumere"},
            {"pauseMenuSettingsButton", "Constitutiones"},
            {"pauseMenuLeaveButton", "Relinquo"},
            {"pauseMenuConfirmLeaveTitle", "Esne certus vos volo relinquere ludum?"},
            {"pauseMenuConfirmLeaveButton", "Relinquo"},
            {"pauseMenuCancelLeaveButton", "Cancellare"},
            
            //Settings Menu
            {"settingsMenuTitle", "Constitutiones"},
            {"settingsMenuBackButton", "<= Retro"},
            {"settingsMenuResetButton", "Reset"},
            {"settingsMenuSaveButton", "Servare"},
            
            //Settings Menu - Game
            {"settingsMenuGameButton", "Ludum"},
            {"settingsMenuGameLanguage", "Lingua :"},
            {"settingsMenuGameMouseSensibility", "Sensibilitas murem :"},
            {"settingsMenuGameFullScreen", "Plena screen :"},
            {"settingsMenuGameEnableChat", "Chat activare :"},
            {"settingsMenuGameChatSize", "Chat magnitudo :"},
            {"settingsMenuGameChatSizeSmall", "Parvus"},
            {"settingsMenuGameChatSizeMedium", "Medius"},
            {"settingsMenuGameChatSizeLarge", "Magnus"},
            
            //Settings Menu - Audio
            {"settingsMenuAudioButton", "Audio"},
            
            //Settings Menu - Video
            {"settingsMenuVideoButton", "Video"},
            
            //Settings Menu - Controls
            {"settingsMenuControlsButton", "Gubernatio"},
            {"settingsMenuControlsMoveForward", "Ante"},
            {"settingsMenuControlsMoveBackward", "Retro"},
            {"settingsMenuControlsMoveLeft", "Sinistra"},
            {"settingsMenuControlsMoveRight", "Dextra"},
            {"settingsMenuControlsSprint", "Currit"},
            {"settingsMenuControlsDash", "Dash"},
            {"settingsMenuControlsCapacity1", "Capacitas 1"},
            {"settingsMenuControlsCapacity2", "Capacitas 2"},
            {"settingsMenuControlsCapacity3", "Capacitas 3"},
            {"settingsMenuControlsItem1", "Res 1"},
            {"settingsMenuControlsItem2", "Res 2"},
            {"settingsMenuControlsItem3", "Res 3"},
            {"settingsMenuControlsInventory", "Inventarium"},
            {"settingsMenuControlsReload", "Reload"},
            {"settingsMenuControlsChat", "Chat"},
            {"settingsMenuControlsEnableChat", "Chat activare"},
            {"settingsMenuControlsPause", "Pause"},
            
            //Skill Description
            {"skillDescriptionSpeed", "Augeat velocitatem motus characteris.\nVelocitas : "},
            {"skillDescriptionHealth", "Augeat maximos vitam characteris points.\nVita : "},
            {"skillDescriptionDamage", "Augeat damnum characteris infligit.\nDamnum : "},
            {"skillDescriptionStamina", "Augeat characteris resistance ad actiones continuas.\nResistentia : "},
            {"skillDescriptionReload", "Tempus necessarium ad reload resistance minuit.\nReload resistance : "},
            {"skillDescriptionStaminaUse", "Minuit characteris resistance consumptum actiones.\nConsumptio resistance : "},
            {"skillDescriptionArrow", "Addebat sagittam ad sagittarium ad sagittandum (maxime 3 sagittas).\nSagittae : +1"},
            {"skillDescriptionShootSpeed", "Augeat velocitatem sagittarum iaculandi.\nVelocitas sagittarum : "},
            {"skillDescriptionArrowPoison", "Sagittae impregnantur veneno, quod damnum inferre tempus.\nTempus veneni : 5s"},
            {"skillDescriptionArrowGel", "Sagittae involutae gel, quod retardationem effectus et stuporem hostes potest causare per paucos secundos.\nTempus gel : 5s"},
            {"skillDescriptionHealSpeed", "Augeat velocitatem sanandi.\nVelocitas sanandi per secundo : "},
            {"skillDescriptionLaserMove", "Permittebat characteri movere laser dum iaculandum."},
            {"skillDescriptionRevive", "Permittebat characteri revivere collapsos socios."},
            {"skillDescriptionVampire", "Permittebat characteri sugere vitam hostium.\nVitae sugere : 10% damni"},
            {"skillDescriptionReviveAll", "Hoc talentum semel in toto ludo potest uti. Revivit omnes collapsos socios et teleportat eos ad locum characteris."},
            {"skillDescriptionReloadProtection", "Praebet protectionem dum reload.\nDamni reductio : 50%"},
            {"skillDescriptionCrit", "Augeat probabilitatem ictus criticorum.\nProbabilitas ictus criticorum : "},
            {"skillDescriptionRange", "Amplificat characteris ictus range.\nRange : +20%"},
            {"skillDescriptionAgro", "Permittebat characteri attrahere omnes hostes proximos."},
            {"skillDescriptionSpike", "Reflectit partem damni infligi atacante.\nReflectio damni : 20%"},
            {"skillDescriptionAbsorption", "Absorbat damnum alii lusores patiuntur in definitum radio.\nRadius : 5m"},
            {"skillDescriptionInvincibility", "Concedit temporaneam invincibilitatem. \nInvincibilitas tempus : 5s"},
            {"skillDescriptionEscalibur", "Dabit characteri potentiam legendarii gladii Excalibur, cum probabilitate 1 ad 5 congelare hostes.\nCongelatio tempus : 5s"},
            {"skillDescriptionDashDegat", "Inferat damnum hostibus, cum characteri facit ictum in Dash.\nDamnum : 30%"},
            {"skillDescriptionDague", "Permittebat characteri iacere pugiones."},
            {"skillDescriptionInvisibility", "Permittebat characteri evanescere per breve tempus.\nInvisibilitas tempus : 10s"},
            {"skillDescriptionDoubleAttack", "Permittebat characteri facere ictus duplices."},
            {"skillDescriptionPoison", "Impregnat ictus veneno, quod damnum inferre tempus.\nTempus veneni : 5s"},
            {"skillChooseTitle", "Elige tu nueva habilidad"},
            
            //Potion
            {"PotionBuy", "Emere"},
            {"PotionPrice", "Pretium : "},
            {"PotionTitle0", "Vita Potio"},
            {"PotionDescription0", "Restaurat 40% vitam characteris."},
            {"PotionTitle1", "Resistentia Potio"},
            {"PotionDescription1", "Restaurat 60% characteris resistentiam"},
            {"PotionTitle2", "Velocitas Potio"},
            {"PotionDescription2", "Multiplicat velocitatem per 1,5 per 20 secundis "},
            {"PotionTitle3", "Resistentia Potio"},
            {"PotionDescription3", "Reducit damnum ad dimidium per 20 secundis "},
            {"PotionTitle4", "Revivificatio Potio"},
            {"PotionDescription4", "Revivificat proximum ludantem (ad 50% vitam)"},
            {"PotionTitle5", "Amnesia Potio"},
            {"PotionDescription5", "Delet unam acquisitam abilitatem a ludante"},
        },
        
        new Dictionary<string, string>
        {
            {"languageName", "Zulu"},
            
            //Connection Menu
            {"connectionMenuTitle", "Ukuxhumana"},
            {"connectionMenuPseudoText", "I-pseudo"},
            {"connectionMenuPasswordText", "Iphasiwedi"},
            {"connectionMenuConnectionButton", "Ukuxhumana"},
            {"connectionMenuNoAccountButton", "Awunayo i-akhawunti?"},
            {"connectionMenuFastConnectionButton", "Ukuxhumana okukhulu"},
            {"connectionMenuErrorUsernameOrPasswordText", "I-pseudo noma iphasiwedi elingenayo"},
            {"connectionMenuErrorFastConnectionText", "Iphutha ekuxhumaneni okukhulu"},
            {"connectionMenuErrorNoFastConnectionText", "Awukho ukuxhumana okukhulu okuqashisayo"},
            {"connectionMenuFastConnectionSaveButton", "Londoloza i-pseudo noma iphasiwedi ekhaya."},
            {"connectionMenuFastConnectionSaveWarningText", "/!\\ Iseluleko :\nUkuphathwa kwe-pseudo noma iphasiwedi ekhaya kuyafaka kahle ukuxhumana, kodwa kungaba nezimvo zokuphepha. Uma isayithi sakho sibuyekezwe, izifundo zakho zingakwazi ukufinyelela."},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Ukubhalisa"},
            {"inscriptionMenuPseudoText", "I-pseudo"},
            {"inscriptionMenuPasswordText", "Iphasiwedi"},
            {"inscriptionMenuPasswordConfirmText", "Yaqinisa iphasiwedi"},
            {"inscriptionMenuInscriptionButton", "Ukubhalisa"},
            {"inscriptionMenuAlreadyAccountButton", "Unayo i-akhawunti?"},
            {"inscriptionMenuErrorIncorrectConfirmText", "Yaqinisa iphasiwedi elingenayo"},
            {"inscriptionMenuErrorAlreadyExistText", "I-pseudo elingenayo"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Lobby"},
            {"lobbyMenuCreateGame", "Yakha umdlalo"},
            {"lobbyMenuJoinGame", "Ukungena umdlalo"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Yakha umdlalo"},
            {"createGameMenuBackButton", "Emuva =>"},
            {"createGameMenuStartGame", "Qala umdlalo"},
            {"createGameMenuID", "ID umdlalo : "},
            {"createGameMenuPlayer1", "Umshayeli 1 : "},
            {"createGameMenuPlayer2", "Umshayeli 2 : "},
            {"createGameMenuPlayer3", "Umshayeli 3 : "},
            {"createGameMenuPlayer4", "Umshayeli 4 : "},
            
            //Join Game Menu
            {"joinGameMenuTitle", "Ukungena umdlalo"},
            {"joinGameMenuBackButton", "<= Emuva"},
            {"joinGameMenuGameID", "ID umdlalo"},
            
            //Select Class Menu
            {"Archer", "Umbuyiseli"},
            {"Knight", "Umkhosi"},
            {"Scientist", "Umlungisi"},
            {"Assassin", "Umlilo"},
            {"selectClassMenuReadyButton", "Kuyaqhubeka"},
            {"selectClassMenuWaitingText", "Ukungathintana kwezinye izinhlangano..."},
            
            //Map loading
            {"gameLoadingMapText", "Ukungena umdlalo : "},
            {"gameLoadingMapWaitingText", "Ukungathintana kwezinye izinhlangano"},
            
            //Pause Menu
            {"pauseMenuTitle", "I-Menyu yokuqeda"},
            {"pauseMenuResumeButton", "Qhubeka"},
            {"pauseMenuSettingsButton", "Izilungiselelo"},
            {"pauseMenuLeaveButton", "Phuma"},
            {"pauseMenuConfirmLeaveTitle", "Uyakholwa ukuthi ufuna ukuphumela umdlalo?"},
            {"pauseMenuConfirmLeaveButton", "Phuma"},
            {"pauseMenuCancelLeaveButton", "Khansela"},
            
            //Settings Menu
            {"settingsMenuTitle", "Izilungiselelo"},
            {"settingsMenuBackButton", "<= Emuva"},
            {"settingsMenuResetButton", "Qalisa"},
            {"settingsMenuSaveButton", "Londoloza"},
            
            //Settings Menu - Game
            {"settingsMenuGameButton", "Umdlalo"},
            {"settingsMenuGameLanguage", "Ulimi :"},
            {"settingsMenuGameMouseSensibility", "Ukuzwa kwe-mouse :"},
            {"settingsMenuGameFullScreen", "I-Fullscreen :"},
            {"settingsMenuGameEnableChat", "Vumela i-chat :"},
            {"settingsMenuGameChatSize", "Usayizi lwe-chat :"},
            {"settingsMenuGameChatSizeSmall", "Okuncane"},
            {"settingsMenuGameChatSizeMedium", "Okwesibili"},
            {"settingsMenuGameChatSizeLarge", "Okukhulu"},
            
            //Settings Menu - Audio
            {"settingsMenuAudioButton", "I-Audio"},
            
            //Settings Menu - Video
            {"settingsMenuVideoButton", "I-Video"},
            
            //Settings Menu - Controls
            {"settingsMenuControlsButton", "Izilawuli"},
            {"settingsMenuControlsMoveForward", "Phambili"},
            {"settingsMenuControlsMoveBackward", "Emuva"},
            {"settingsMenuControlsMoveLeft", "Kwesokudla"},
            {"settingsMenuControlsMoveRight", "Kwesokunxele"},
            {"settingsMenuControlsSprint", "Sprint"},
            {"settingsMenuControlsDash", "Dash"},
            {"settingsMenuControlsCapacity1", "Ukusebenza 1"},
            {"settingsMenuControlsCapacity2", "Ukusebenza 2"},
            {"settingsMenuControlsCapacity3", "Ukusebenza 3"},
            {"settingsMenuControlsItem1", "Ithuluzi 1"},
            {"settingsMenuControlsItem2", "Ithuluzi 2"},
            {"settingsMenuControlsItem3", "Ithuluzi 3"},
            {"settingsMenuControlsInventory", "I-inventory"},
            {"settingsMenuControlsReload", "Qalisa"},
            {"settingsMenuControlsChat", "I-chat"},
            {"settingsMenuControlsEnableChat", "Vumela i-chat"},
            {"settingsMenuControlsPause", "I-Menyu yokuqeda"},
            
            //Skill Description
            {"skillDescriptionSpeed", "Iphumeza ukusinda kwemoto yomuntu.\nIsindiso : "},
            {"skillDescriptionHealth", "Iphumeza imali yezinkinga ezikhona zomuntu.\nImali : "},
            {"skillDescriptionDamage", "Iphumeza umsebenzi omuntu ukwenza.\nUmsebenzi : "},
            {"skillDescriptionStamina", "Iphumeza umuntu ukukhathazeka kwezinto ezinjalo.\nUkukhathazeka : "},
            {"skillDescriptionReload", "Ukushintsha isikhathi esidingekile sokuphinda ukwenza.\nUkuphinda : "},
            {"skillDescriptionStaminaUse", "Ukushintsha ukukhathazeka okwenziwe ngumuntu.\nUkukhathazeka : "},
            {"skillDescriptionArrow", "Ukufaka isagidi okufaneleka okufakwe ngumuntu (kunye nezinye zisagidi ezintathu).\nIsagidi : +1"},
            {"skillDescriptionShootSpeed", "Iphumeza ukusinda kwesagidi ekufakweni.\nIsindiso esagidi : "},
            {"skillDescriptionArrowPoison", "Izisagidi zifakwe ngezinja, ezinjalo zenza umsebenzi.\nIsikhathi sezinja : 5s"},
            {"skillDescriptionArrowGel", "Izisagidi zifakwe ngegeli, okufaneleka okufakwe ngumuntu ezinzima kanye nokufakwa kwezinja ngokwesikhathi esiningi.\nIsikhathi segeli : 5s"},
            {"skillDescriptionHealSpeed", "Iphumeza ukusinda kwesikhathi sokukhathazeka.\nIsikhathi sokukhathazeka ngosuku : "},
            {"skillDescriptionLaserMove", "Izimisele ukumisa ilaser ekufakweni."},
            {"skillDescriptionRevive", "Izimisele ukumisa abantu abathintana."},
            {"skillDescriptionVampire", "Izimisele ukudla umoya wabanye abantu.\nUkudla : 10% umsebenzi"},
            {"skillDescriptionReviveAll", "Lokhu okusebenza kuphela ngosuku lonke. Ukumisa bonke abantu abathintana kanye nokuthuthukisa kubo ekukhishweni kwemuntu."},
            {"skillDescriptionReloadProtection", "Ukunikeza ulondoloza ekuphinda.\nUkukhathazeka okuphinda : 50%"},
            {"skillDescriptionCrit", "Iphumeza ukusinda kwemfutho wemfutho.\nUkusinda kwemfutho : "},
            {"skillDescriptionRange", "Iphumeza ukusinda kwemfutho ezithintekayo.\nUkusinda : +20%"},
            {"skillDescriptionAgro", "Izimisele ukumisa abantu abathintana bonke kwesikhathi."},
            {"skillDescriptionSpike", "Izimisele ukulandela isikhathi esikhona sokusebenza.\nIsikhathi sokusebenza : 20%"},
            {"skillDescriptionAbsorption", "Izimisele ukukhathazeka okwenziwe ngabanye abantu ekhondweni elisetshenziswe.\nIkhondweni : 5m"},
            {"skillDescriptionInvincibility", "Izimisele ukulondoloza ekuphinda. \nEkuphinda isikhathi : 5s"},
            {"skillDescriptionEscalibur", "Izimisele ukunika umuntu umoya wezulu Excalibur, okufaneleka okufaneleka 1 kuya 5 kuthintana.\nIsikhathi sezulu : 5s"},
            {"skillDescriptionDashDegat", "Izimisele ukwenza umsebenzi abantu, uma umuntu ekwenza isikhathi ekwenzeni.\nUmsebenzi : 30%"},
            {"skillDescriptionDague", "Izimisele ukunika umuntu izinto ezimbalwa."},
            {"skillDescriptionInvisibility", "Izimisele ukufihla umuntu ekuseni.\nIsikhathi sokufihla : 10s"},
            {"skillDescriptionDoubleAttack", "Izimisele ukwenza izinto ezimbalwa."},
            {"skillDescriptionPoison", "Izisagidi zifakwe ngezinja, ezinjalo zenza umsebenzi.\nIsikhathi sezinja : 5s"},
            {"skillChooseTitle", "Khetha umsebenzi wakho omtsha"},
            
            //Potion
            {"PotionBuy", "Thenga"},
            {"PotionPrice", "Isilinganiso : "},
            {"PotionTitle0", "I-Potion ye-Vita"},
            {"PotionDescription0", "Iphindisela 40% imali yezinkinga yomuntu."},
            {"PotionTitle1", "I-Potion ye-Resist"},
            {"PotionDescription1", "Iphindisela 60% imali yezinkinga yomuntu"},
            {"PotionTitle2", "I-Potion ye-Velocitas"},
            {"PotionDescription2", "Iphindisela 1,5 imali yezinkinga yomuntu ngosuku olulandelayo "},
            {"PotionTitle3", "I-Potion ye-Resist"},
            {"PotionDescription3", "Iphindisela 50% imali yezinkinga yomuntu"},
            {"PotionTitle4", "I-Potion ye-Revive"},
            {"PotionDescription4", "Iphindisela umuntu ophakathi (nangaphandle kwe-50% imali)"},
            {"PotionTitle5", "I-Potion ye-Amnesia"},
            {"PotionDescription5", "Iphindisela umsebenzi omuntu omnye ophakathi"},
        },
        
        new Dictionary<string, string>
        {
            {"languageName", "中文"},
            
            //Connection Menu
            {"connectionMenuTitle", "连接"},
            {"connectionMenuPseudoText", "伪"},
            {"connectionMenuPasswordText", "密码"},
            {"connectionMenuConnectionButton", "连接"},
            {"connectionMenuNoAccountButton", "没有帐户？"},
            {"connectionMenuFastConnectionButton", "快速连接"},
            {"connectionMenuErrorUsernameOrPasswordText", "用户名或密码不正确"},
            {"connectionMenuErrorFastConnectionText", "快速连接错误"},
            {"connectionMenuErrorNoFastConnectionText", "没有注册快速连接"},
            {"connectionMenuFastConnectionSaveButton", "本地保存用户名和密码。"},
            {"connectionMenuFastConnectionSaveWarningText", "/!\\ 警告 :\n本地保存用户名和密码可以方便登录，但可能存在安全风险。如果您的设备被入侵，您的信息可能会被访问。"},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "注册"},
            {"inscriptionMenuPseudoText", "伪"},
            {"inscriptionMenuPasswordText", "密码"},
            {"inscriptionMenuPasswordConfirmText", "确认密码"},
            {"inscriptionMenuInscriptionButton", "注册"},
            {"inscriptionMenuAlreadyAccountButton", "已经有一个帐户？"},
            {"inscriptionMenuErrorIncorrectConfirmText", "密码确认不正确"},
            {"inscriptionMenuErrorAlreadyExistText", "用户名已经存在"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "大厅"},
            {"lobbyMenuCreateGame", "创建游戏"},
            {"lobbyMenuJoinGame", "加入游戏"},
            
            //Create Game Menu
            {"createGameMenuTitle", "创建游戏"},
            {"createGameMenuBackButton", "返回 =>"},
            {"createGameMenuStartGame", "开始游戏"},
            {"createGameMenuID", "游戏ID : "},
            {"createGameMenuPlayer1", "玩家 1 : "},
            {"createGameMenuPlayer2", "玩家 2 : "},
            {"createGameMenuPlayer3", "玩家 3 : "},
            {"createGameMenuPlayer4", "玩家 4 : "},
            
            //Join Game Menu
            {"joinGameMenuTitle", "加入游戏"},
            {"joinGameMenuBackButton", "<= 返回"},
            {"joinGameMenuGameID", "游戏ID"},
            
            //Select Class Menu
            {"Archer", "弓箭手"},
            {"Knight", "骑士"},
            {"Scientist", "科学家"},
            {"Assassin", "刺客"},
            {"selectClassMenuReadyButton", "准备好了"},
            {"selectClassMenuWaitingText", "等待其他玩家..."},
            
            //Map loading
            {"gameLoadingMapText", "加载地图 : "},
            {"gameLoadingMapWaitingText", "等待其他玩家"},
            
            //Pause Menu
            {"pauseMenuTitle", "暂停菜单"},
            {"pauseMenuResumeButton", "恢复"},
            {"pauseMenuSettingsButton", "设置"},
            {"pauseMenuLeaveButton", "离开"},
            {"pauseMenuConfirmLeaveTitle", "你确定要离开游戏吗？"},
            {"pauseMenuConfirmLeaveButton", "离开"},
            {"pauseMenuCancelLeaveButton", "取消"},
            
            //Settings Menu
            {"settingsMenuTitle", "设置"},
            {"settingsMenuBackButton", "<= 返回"},
            {"settingsMenuResetButton", "重启"},
            {"settingsMenuSaveButton", "保存"},
            
            //Settings Menu - Game
            {"settingsMenuGameButton", "游戏"},
            {"settingsMenuGameLanguage", "语言 :"},
            {"settingsMenuGameMouseSensibility", "鼠标灵敏度 :"},
            {"settingsMenuGameFullScreen", "全屏 :"},
            {"settingsMenuGameEnableChat", "启用聊天 :"},
            {"settingsMenuGameChatSize", "聊天大小 :"},
            {"settingsMenuGameChatSizeSmall", "小"},
            {"settingsMenuGameChatSizeMedium", "中"},
            {"settingsMenuGameChatSizeLarge", "大"},
            
            //Settings Menu - Audio
            {"settingsMenuAudioButton", "音频"},
            
            //Settings Menu - Video
            {"settingsMenuVideoButton", "视频"},
            
            //Settings Menu - Controls
            {"settingsMenuControlsButton", "控制"},
            {"settingsMenuControlsMoveForward", "向前"},
            {"settingsMenuControlsMoveBackward", "向后"},
            {"settingsMenuControlsMoveLeft", "左"},
            {"settingsMenuControlsMoveRight", "右"},
            {"settingsMenuControlsSprint", "冲刺"},
            {"settingsMenuControlsDash", "冲刺"},
            {"settingsMenuControlsCapacity1", "能力 1"},
            {"settingsMenuControlsCapacity2", "能力 2"},
            {"settingsMenuControlsCapacity3", "能力 3"},
            {"settingsMenuControlsItem1", "项目 1"},
            {"settingsMenuControlsItem2", "项目 2"},
            {"settingsMenuControlsItem3", "项目 3"},
            {"settingsMenuControlsInventory", "库存"},
            {"settingsMenuControlsReload", "重新加载"},
            {"settingsMenuControlsChat", "聊天"},
            {"settingsMenuControlsEnableChat", "启用聊天"},
            {"settingsMenuControlsPause", "暂停"},
            
            //Skill Description
            {"skillDescriptionSpeed", "增加角色的移动速度。\n速度 : "},
            {"skillDescriptionHealth", "增加角色的最大生命值。\n生命 : "},
            {"skillDescriptionDamage", "增加角色造成的伤害。\n伤害 : "},
            {"skillDescriptionStamina", "增加角色对连续动作的抵抗力。\n抵抗力 : "},
            {"skillDescriptionReload", "减少重新加载所需的时间。\n重新加载 : "},
            {"skillDescriptionStaminaUse", "减少角色连续动作的消耗。\n消耗抵抗力 : "},
            {"skillDescriptionArrow", "添加箭矢以供角色射击（最多3支箭矢）。\n箭矢 : +1"},
            {"skillDescriptionShootSpeed", "增加箭矢射击速度。\n箭矢速度 : "},
            {"skillDescriptionArrowPoison", "箭矢浸入毒液，可在一段时间内造成伤害。\n毒液时间 : 5s"},
            {"skillDescriptionArrowGel", "箭矢包裹凝胶，可在几秒钟内造成效果减缓和使敌人昏迷。\n凝胶时间 : 5s"},
            {"skillDescriptionHealSpeed", "增加治疗速度。\n每秒治疗速度 : "},
            {"skillDescriptionLaserMove", "允许角色在射击时移动激光。"},
            {"skillDescriptionRevive", "允许角色复活倒下的队友。"},
            {"skillDescriptionVampire", "允许角色吸收其他玩家的生命。\n吸血 : 10% 伤害"},
            {"skillDescriptionReviveAll", "此技能在整个游戏中只能使用一次。复活所有倒下的队友并将他们传送到角色的位置。"},
            {"skillDescriptionReloadProtection", "在重新加载时提供保护。\n伤害减少 : 50%"},
            {"skillDescriptionCrit", "增加暴击率。\n暴击率 : "},
            {"skillDescriptionRange", "增加角色的攻击范围。\n范围 : +20%"},
            {"skillDescriptionAgro", "允许角色吸引所有附近的敌人。"},
            {"skillDescriptionSpike", "反射部分伤害给予攻击者。\n伤害反射 : 20%"},
            {"skillDescriptionAbsorption", "吸收其他玩家在一定范围内受到的伤害。\n范围 : 5m"},
            {"skillDescriptionInvincibility", "提供短暂的无敌状态。\n无敌时间 : 5s"},
            {"skillDescriptionEscalibur", "赋予角色传说剑Excalibur的力量，有1到5之间的凝固敌人的几率。\n凝固时间 : 5s"},
            {"skillDescriptionDashDegat", "在冲刺中对敌人造成伤害。\n伤害 : 30%"},
            {"skillDescriptionDague", "允许角色投掷匕首。"},
            {"skillDescriptionInvisibility", "允许角色在短时间内消失。\n隐形时间 : 10s"},
            {"skillDescriptionDoubleAttack", "允许角色进行双重攻击。"},
            {"skillDescriptionPoison", "使攻击带有毒液，在一段时间内造成伤害。\n毒液时间 : 5s"},
            {"skillChooseTitle", "选择你的新技能"},
            
            //Potion
            {"PotionBuy", "购买"},
            {"PotionPrice", "价格 : "},
            {"PotionTitle0", "生命药水"},
            {"PotionDescription0", "恢复角色40%的生命值。"},
            {"PotionTitle1", "抗性药水"},
            {"PotionDescription1", "恢复角色60%的抗性"},
            {"PotionTitle2", "速度药水"},
            {"PotionDescription2", "增加1.5倍角色的速度20秒 "},
            {"PotionTitle3", "抗性药水"},
            {"PotionDescription3", "减少50%角色的伤害"},
            {"PotionTitle4", "复活药水"},
            {"PotionDescription4", "复活最近的玩家（50%生命）"},
            {"PotionTitle5", "失忆药水"},
            {"PotionDescription5", "删除一个玩家的已获得技能"},
        },
        
        new Dictionary<string, string>
        {
            {"languageName", "Ελληνική"},
            
            //Connection Menu
            {"connectionMenuTitle", "Σύνδεση"},
            {"connectionMenuPseudoText", "Ψευδώνυμο"},
            {"connectionMenuPasswordText", "Κωδικός πρόσβασης"},
            {"connectionMenuConnectionButton", "Σύνδεση"},
            {"connectionMenuNoAccountButton", "Δεν έχετε λογαριασμό;"},
            {"connectionMenuFastConnectionButton", "Γρήγορη σύνδεση"},
            {"connectionMenuErrorUsernameOrPasswordText", "Λανθασμένο ψευδώνυμο ή κωδικός πρόσβασης"},
            {"connectionMenuErrorFastConnectionText", "Σφάλμα στη γρήγορη σύνδεση"},
            {"connectionMenuErrorNoFastConnectionText", "Δεν υπάρχει καταχωρημένη γρήγορη σύνδεση"},
            {"connectionMenuFastConnectionSaveButton", "Αποθήκευση του ψευδώνυμου και του κωδικού πρόσβασης τοπικά."},
            {"connectionMenuFastConnectionSaveWarningText", "/!\\ Προειδοποίηση :\nΗ αποθήκευση του ψευδώνυμου και του κωδικού πρόσβασης τοπικά διευκολύνει τη σύνδεση, αλλά μπορεί να συνεπάγεται κινδύνους ασφαλείας. Εάν το σύστημά σας έχει παραβιαστεί, οι πληροφορίες σας μπορεί να είναι προσβάσιμες."},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Εγγραφή"},
            {"inscriptionMenuPseudoText", "Ψευδώνυμο"},
            {"inscriptionMenuPasswordText", "Κωδικός πρόσβασης"},
            {"inscriptionMenuPasswordConfirmText", "Επιβεβαίωση κωδικού πρόσβασης"},
            {"inscriptionMenuInscriptionButton", "Εγγραφή"},
            {"inscriptionMenuAlreadyAccountButton", "Έχετε ήδη ένα λογαριασμό;"},
            {"inscriptionMenuErrorIncorrectConfirmText", "Λανθασμένη επιβεβαίωση κωδικού πρόσβασης"},
            {"inscriptionMenuErrorAlreadyExistText", "Το ψευδώνυμο υπάρχει ήδη"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Σαλόνι"},
            {"lobbyMenuCreateGame", "Δημιουργία παιχνιδιού"},
            {"lobbyMenuJoinGame", "Ενταχθείτε στο παιχνίδι"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Δημιουργία παιχνιδιού"},
            {"createGameMenuBackButton", "Πίσω =>"},
            {"createGameMenuStartGame", "Ξεκινήστε το παιχνίδι"},
            {"createGameMenuID", "Αναγνωριστικό παιχνιδιού : "},
            {"createGameMenuPlayer1", "Παίκτης 1 : "},
            {"createGameMenuPlayer2", "Παίκτης 2 : "},
            {"createGameMenuPlayer3", "Παίκτης 3 : "},
            {"createGameMenuPlayer4", "Παίκτης 4 : "},
            
            //Join Game Menu
            {"joinGameMenuTitle", "Ενταχθείτε στο παιχνίδι"},
            {"joinGameMenuBackButton", "<= Πίσω"},
            {"joinGameMenuGameID", "Αναγνωριστικό παιχνιδιού"},
            
            //Select Class Menu
            {"Archer", "Τοξότης"},
            {"Knight", "Ιππότης"},
            {"Scientist", "Επιστήμονας"},
            {"Assassin", "Δολοφόνος"},
            {"selectClassMenuReadyButton", "Δάνειο"},
            {"selectClassMenuWaitingText", "Περιμένοντας τους άλλους παίκτες..."},
            
            //Map loading
            {"gameLoadingMapText", "Φόρτωση χάρτη : "},
            {"gameLoadingMapWaitingText", "Περιμένοντας τους άλλους παίκτες"},
            
            //Pause Menu
            {"pauseMenuTitle", "Μενού παύσης"},
            {"pauseMenuResumeButton", "Συνέχισε"},
            {"pauseMenuSettingsButton", "Ρυθμίσεις"},
            {"pauseMenuLeaveButton", "Αφήνω"},
            {"pauseMenuConfirmLeaveTitle", "Είστε σίγουροι ότι θέλετε να αφήσετε το παιχνίδι;"},
            {"pauseMenuConfirmLeaveButton", "Αφήνω"},
            {"pauseMenuCancelLeaveButton", "Ματαίωση"},
            
            //Settings Menu
            {"settingsMenuTitle", "Ρυθμίσεις"},
            {"settingsMenuBackButton", "<= Πίσω"},
            {"settingsMenuResetButton", "Επαναφορά"},
            {"settingsMenuSaveButton", "Αποθήκευση"},
            
            //Settings Menu - Game
            {"settingsMenuGameButton", "Παιχνίδι"},
            {"settingsMenuGameLanguage", "Γλώσσα :"},
            {"settingsMenuGameMouseSensibility", "Ευαισθησία ποντικιού :"},
            {"settingsMenuGameFullScreen", "Πλήρης οθόνη :"},
            {"settingsMenuGameEnableChat", "Ενεργοποίηση συνομιλίας :"},
            {"settingsMenuGameChatSize", "Μέγεθος συνομιλίας :"},
            {"settingsMenuGameChatSizeSmall", "Μικρό"},
            {"settingsMenuGameChatSizeMedium", "Μεσαίο"},
            {"settingsMenuGameChatSizeLarge", "Μεγάλο"},
            
            //Settings Menu - Audio
            {"settingsMenuAudioButton", "Ήχος"},
            
            //Settings Menu - Video
            {"settingsMenuVideoButton", "Βίντεο"},
            
            //Settings Menu - Controls
            {"settingsMenuControlsButton", "Έλεγχοι"},
            {"settingsMenuControlsMoveForward", "Προς τα εμπρός"},
            {"settingsMenuControlsMoveBackward", "Προς τα πίσω"},
            {"settingsMenuControlsMoveLeft", "Αριστερά"},
            {"settingsMenuControlsMoveRight", "Δεξιά"},
            {"settingsMenuControlsSprint", "Τρέξιμο"},
            {"settingsMenuControlsDash", "Dash"},
            {"settingsMenuControlsCapacity1", "Χωρητικότητα 1"},
            {"settingsMenuControlsCapacity2", "Χωρητικότητα 2"},
            {"settingsMenuControlsCapacity3", "Χωρητικότητα 3"},
            {"settingsMenuControlsItem1", "Αντικείμενο 1"},
            {"settingsMenuControlsItem2", "Αντικείμενο 2"},
            {"settingsMenuControlsItem3", "Αντικείμενο 3"},
            {"settingsMenuControlsInventory", "Αποθήκη"},
            {"settingsMenuControlsReload", "Επαναφόρτωση"},
            {"settingsMenuControlsChat", "Συνομιλία"},
            {"settingsMenuControlsEnableChat", "Ενεργοποίηση συνομιλίας"},
            {"settingsMenuControlsPause", "Μενού παύσης"},
            
            //Skill Description
            {"skillDescriptionSpeed", "Αυξάνει την ταχύτητα κίνησης του χαρακτήρα.\nΤαχύτητα : "},
            {"skillDescriptionHealth", "Αυξάνει τη μέγιστη ζωτικότητα του χαρακτήρα.\nΖωτικότητα : "},
            {"skillDescriptionDamage", "Αυξάνει τη ζημιά που προκαλεί ο χαρακτήρας.\nΖημιά : "},
            {"skillDescriptionStamina", "Αυξάνει την αντοχή του χαρακτήρα σε συνεχείς ενέργειες.\nΑντοχή : "},
            {"skillDescriptionReload", "Μειώνει το χρόνο που απαιτείται για την επαναφόρτωση.\nΕπαναφόρτωση : "},
            {"skillDescriptionStaminaUse", "Μειώνει την κατανάλωση της αντοχής του χαρακτήρα σε συνεχείς ενέργειες.\nΚατανάλωση αντοχής : "},
            {"skillDescriptionArrow", "Προσθέτει βέλη για το χαρακτήρα να χρησιμοποιήσει (μέχρι 3 βέλη).\nΒέλη : +1"},
            {"skillDescriptionShootSpeed", "Αυξάνει την ταχύτητα των βελών.\nΤαχύτητα βέλους : "},
            {"skillDescriptionArrowPoison", "Τα βέλη είναι δηλητηριώδη και προκαλούν ζημιά για μια περίοδο.\nΔιάρκεια δηλητηρίας : 5s"},
            {"skillDescriptionArrowGel", "Τα βέλη είναι εντυπωσιακά με ζελέ, προκαλώντας αργότερα αποτελέσματα και ανακωχή των εχθρών.\nΔιάρκεια ζελέ : 5s"},
            {"skillDescriptionHealSpeed", "Αυξάνει την ταχύτητα της θεραπείας.\nΤαχύτητα θεραπείας ανά δευτερόλεπτο : "},
            {"skillDescriptionLaserMove", "Επιτρέπει στο χαρακτήρα να κινηθεί το λέιζερ κατά την εκτέλεση."},
            {"skillDescriptionRevive", "Επιτρέπει στο χαρακτήρα να αναστήσει πτώσεις συμπαίκτες."},
            {"skillDescriptionVampire", "Επιτρέπει στο χαρακτήρα να απορροφήσει τη ζωή των άλλων παικτών.\nΑπορρόφηση : 10% ζημιά"},
            {"skillDescriptionReviveAll", "Αυτή η δεξιότητα μπορεί να χρησιμοποιηθεί μόνο μία φορά σε όλο το παιχνίδι. Αναστήστε όλους τους πτώσεις συμπαίκτες και μεταφέρετέ τους στη θέση του χαρακτήρα."},
            {"skillDescriptionReloadProtection", "Προσφέρει προστασία κατά την επαναφόρτωση.\nΜείωση ζημιάς : 50%"},
            {"skillDescriptionCrit", "Αυξάνει το ποσοστό κριτικής επίθεσης.\nΚριτική επίθεση : "},
            {"skillDescriptionRange", "Αυξάνει την εμβέλεια επίθεσης του χαρακτήρα.\nΕμβέλεια : +20%"},
            {"skillDescriptionAgro", "Επιτρέπει στο χαρακτήρα να προσελκύσει όλους τους εχθρούς στην περιοχή."},
            {"skillDescriptionSpike", "Αντανακλά μέρος της ζημιάς στον επιτιθέμενο.\nΑντανάκλαση ζημιάς : 20%"},
            {"skillDescriptionAbsorption", "Απορροφά τη ζημιά που υπέστη άλλοι παίκτες σε συγκεκριμένη περιοχή.\nΠεριοχή : 5m"},
            {"skillDescriptionInvincibility", "Παρέχει μια προσωρινή κατάσταση αντιστάθμισης.\nΧρόνος αντιστάθμισης : 5s"},
            {"skillDescriptionEscalibur", "Προσφέρει στο χαρακτήρα τη δύναμη του θρυλικού σπαθιού Excalibur, με πιθανότητα από 1 έως 5 να παγώσει τους εχθρούς.\nΧρόνος παγώματος : 5s"},
            {"skillDescriptionDashDegat", "Προκαλεί ζημιά στους εχθρούς κατά τη διάρκεια της σύγκρουσης.\nΖημιά : 30%"},
            {"skillDescriptionDague", "Επιτρέπει στο χαρακτήρα να ρίξει μαχαίρια."},
            {"skillDescriptionInvisibility", "Επιτρέπει στο χαρακτήρα να εξαφανιστεί για ένα σύντομο χρονικό διάστημα.\nΧρόνος αφαίρεσης : 10s"},
            {"skillDescriptionDoubleAttack", "Επιτρέπει στο χαρακτήρα να κάνει διπλή επίθεση."},
            {"skillDescriptionPoison", "Επιτρέπει στην επίθεση να προκαλέσει ζημιά για μια περίοδο.\nΔιάρκεια δηλητηρίας : 5s"},
            {"skillChooseTitle", "Επιλέξτε τη νέα σας δεξιότητα"},
            
            //Potion
            {"PotionBuy", "Αγορά"},
            {"PotionPrice", "Τιμή : "},
            {"PotionTitle0", "Φίλτρο Ζωής"},
            {"PotionDescription0", "Ανακτά το 40% της ζωής του χαρακτήρα."},
            {"PotionTitle1", "Φίλτρο Αντοχής"},
            {"PotionDescription1", "Ανακτά το 60% της αντοχής του χαρακτήρα."},
            {"PotionTitle2", "Φίλτρο Ταχύτητας"},
            {"PotionDescription2", "Αυξάνει την ταχύτητα του χαρακτήρα κατά 1,5 φορά για 20 δευτερόλεπτα."},
            {"PotionTitle3", "Φίλτρο Αντοχής"},
            {"PotionDescription3", "Μειώνει τη ζημιά του χαρακτήρα κατά 50%"},
            {"PotionTitle4", "Φίλτρο Αναβίωσης"},
            {"PotionDescription4", "Αναβιώνει τον πιο πρόσφατο παίκτη (50% ζωή)"},
            {"PotionTitle5", "Φίλτρο Λήθης"},
            {"PotionDescription5", "Διαγράφει μια δεξιότητα που έχει αποκτήσει ένας παίκτης"},
        },
        
        new Dictionary<string, string>
        {
            {"languageName", "Русский"},
            
            //Connection Menu
            {"connectionMenuTitle", "Соединение"},
            {"connectionMenuPseudoText", "Псевдоним"},
            {"connectionMenuPasswordText", "Пароль"},
            {"connectionMenuConnectionButton", "Соединение"},
            {"connectionMenuNoAccountButton", "Нет учетной записи?"},
            {"connectionMenuFastConnectionButton", "Быстрое соединение"},
            {"connectionMenuErrorUsernameOrPasswordText", "Неверное имя пользователя или пароль"},
            {"connectionMenuErrorFastConnectionText", "Ошибка быстрого подключения"},
            {"connectionMenuErrorNoFastConnectionText", "Нет зарегистрированных быстрых подключений"},
            {"connectionMenuFastConnectionSaveButton", "Сохранить локально псевдоним и пароль."},
            {"connectionMenuFastConnectionSaveWarningText", "/!\\ Предупреждение :\nСохранение локально псевдонима и пароля упрощает подключение, но может представлять угрозу безопасности. Если ваше устройство скомпрометировано, ваши данные могут быть доступны."},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Регистрация"},
            {"inscriptionMenuPseudoText", "Псевдоним"},
            {"inscriptionMenuPasswordText", "Пароль"},
            {"inscriptionMenuPasswordConfirmText", "Подтвердите пароль"},
            {"inscriptionMenuInscriptionButton", "Регистрация"},
            {"inscriptionMenuAlreadyAccountButton", "Уже есть аккаунт?"},
            {"inscriptionMenuErrorIncorrectConfirmText", "Неверное подтверждение пароля"},
            {"inscriptionMenuErrorAlreadyExistText", "Имя пользователя уже существует"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Лобби"},
            {"lobbyMenuCreateGame", "Создать игру"},
            {"lobbyMenuJoinGame", "Присоединиться к игре"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Создать игру"},
            {"createGameMenuBackButton", "Назад =>"},
            {"createGameMenuStartGame", "Начать игру"},
            {"createGameMenuID", "Идентификатор игры : "},
            {"createGameMenuPlayer1", "Игрок 1 : "},
            {"createGameMenuPlayer2", "Игрок 2 : "},
            {"createGameMenuPlayer3", "Игрок 3 : "},
            {"createGameMenuPlayer4", "Игрок 4 : "},
            
            //Join Game Menu
            {"joinGameMenuTitle", "Присоединиться к игре"},
            {"joinGameMenuBackButton", "<= Назад"},
            {"joinGameMenuGameID", "Идентификатор игры"},
            
            //Select Class Menu
            {"Archer", "Лучник"},
            {"Knight", "Рыцарь"},
            {"Scientist", "Ученый"},
            {"Assassin", "Убийца"},
            {"selectClassMenuReadyButton", "Готов"},
            {"selectClassMenuWaitingText", "Ожидание других игроков..."},
            
            //Map loading
            {"gameLoadingMapText", "Загрузка карты : "},
            {"gameLoadingMapWaitingText", "Ожидание других игроков"},
            
            //Pause Menu
            {"pauseMenuTitle", "Меню паузы"},
            {"pauseMenuResumeButton", "Возобновить"},
            {"pauseMenuSettingsButton", "Настройки"},
            {"pauseMenuLeaveButton", "Покинуть"},
            {"pauseMenuConfirmLeaveTitle", "Вы уверены, что хотите покинуть игру?"},
            {"pauseMenuConfirmLeaveButton", "Покинуть"},
            {"pauseMenuCancelLeaveButton", "Отмена"},
            
            //Settings Menu
            {"settingsMenuTitle", "Настройки"},
            {"settingsMenuBackButton", "<= Назад"},
            {"settingsMenuResetButton", "Сброс"},
            {"settingsMenuSaveButton", "Сохранить"},
            
            //Settings Menu - Game
            {"settingsMenuGameButton", "Игра"},
            {"settingsMenuGameLanguage", "Язык :"},
            {"settingsMenuGameMouseSensibility", "Чувствительность мыши :"},
            {"settingsMenuGameFullScreen", "Полноэкранный :"},
            {"settingsMenuGameEnableChat", "Включить чат :"},
            {"settingsMenuGameChatSize", "Размер чата :"},
            {"settingsMenuGameChatSizeSmall", "Маленький"},
            {"settingsMenuGameChatSizeMedium", "Средний"},
            {"settingsMenuGameChatSizeLarge", "Большой"},
            
            //Settings Menu - Audio
            {"settingsMenuAudioButton", "Аудио"},
            
            //Settings Menu - Video
            {"settingsMenuVideoButton", "Видео"},
            
            //Settings Menu - Controls
            {"settingsMenuControlsButton", "Управление"},
            {"settingsMenuControlsMoveForward", "Вперед"},
            {"settingsMenuControlsMoveBackward", "Назад"},
            {"settingsMenuControlsMoveLeft", "Влево"},
            {"settingsMenuControlsMoveRight", "Вправо"},
            {"settingsMenuControlsSprint", "Спринт"},
            {"settingsMenuControlsDash", "Dash"},
            {"settingsMenuControlsCapacity1", "Вместимость 1"},
            {"settingsMenuControlsCapacity2", "Вместимость 2"},
            {"settingsMenuControlsCapacity3", "Вместимость 3"},
            {"settingsMenuControlsItem1", "Пункт 1"},
            {"settingsMenuControlsItem2", "Пункт 2"},
            {"settingsMenuControlsItem3", "Пункт 3"},
            {"settingsMenuControlsInventory", "Инвентарь"},
            {"settingsMenuControlsReload", "Перезагрузить"},
            {"settingsMenuControlsChat", "Чат"},
            {"settingsMenuControlsEnableChat", "Включить чат"},
            {"settingsMenuControlsPause", "Меню паузы"},
            
            //Skill Description
            {"skillDescriptionSpeed", "Увеличивает скорость движения персонажа.\nСкорость : "},
            {"skillDescriptionHealth", "Увеличивает максимальное здоровье персонажа.\nЗдоровье : "},
            {"skillDescriptionDamage", "Увеличивает урон, наносимый персонажем.\nУрон : "},
            {"skillDescriptionStamina", "Увеличивает выносливость персонажа при непрерывных действиях.\nВыносливость : "},
            {"skillDescriptionReload", "Уменьшает время, необходимое для перезагрузки.\nПерезагрузка : "},
            {"skillDescriptionStaminaUse", "Уменьшает расход выносливости персонажа при непрерывных действиях.\nРасход выносливости : "},
            {"skillDescriptionArrow", "Добавляет стрелы для использования персонажем (до 3 стрел).\nСтрелы : +1"},
            {"skillDescriptionShootSpeed", "Увеличивает скорость стрел.\nСкорость стрел : "},
            {"skillDescriptionArrowPoison", "Стрелы являются ядовитыми и наносят урон в течение определенного времени.\nДлительность яда : 5s"},
            {"skillDescriptionArrowGel", "Стрелы покрыты желе, вызывающее более поздние результаты и замедление врагов.\nДлительность желе : 5s"},
            {"skillDescriptionHealSpeed", "Увеличивает скорость лечения.\nСкорость лечения в секунду : "},
            {"skillDescriptionLaserMove", "Позволяет персонажу двигать лазер во время исполнения."},
            {"skillDescriptionRevive", "Позволяет персонажу воскрешать падения союзников."},
            {"skillDescriptionVampire", "Позволяет персонажу поглощать жизнь других игроков.\nПоглощение : 10% урона"},
            {"skillDescriptionReviveAll", "Это умение можно использовать только один раз во всей игре. Воскресить всех падших союзников и переместить их на позицию персонажа."},
            {"skillDescriptionReloadProtection", "Предоставляет защиту во время перезагрузки.\nУменьшение урона : 50%"},
            {"skillDescriptionCrit", "Увеличивает процент критической атаки.\nКритическая атака : "},
            {"skillDescriptionRange", "Увеличивает дальность атаки персонажа.\nДальность : +20%"},
            {"skillDescriptionAgro", "Позволяет персонажу привлекать всех врагов в область."},
            {"skillDescriptionSpike", "Отражает часть урона обратно на атакующего.\nОтражение урона : 20%"},
            {"skillDescriptionAbsorption", "Поглощает урон, полученный другими игроками в определенной области.\nОбласть : 5m"},
            {"skillDescriptionInvincibility", "Предоставляет временное состояние компенсации.\nВремя компенсации : 5s"},
            {"skillDescriptionEscalibur", "Предоставляет персонажу силу легендарного меча Excalibur, с вероятностью от 1 до 5 замораживать врагов.\nВремя замораживания : 5s"},
            {"skillDescriptionDashDegat", "Наносит урон врагам во время столкновения.\nУрон : 30%"},
            {"skillDescriptionDague", "Позволяет персонажу бросить ножи."},
            {"skillDescriptionInvisibility", "Позволяет персонажу исчезнуть на короткое время.\nВремя исчезновения : 10s"},
            {"skillDescriptionDoubleAttack", "Позволяет персонажу совершить двойную атаку."},
            {"skillDescriptionPoison", "Позволяет атаке нанести урон в течение определенного времени.\nДлительность яда : 5s"},
            {"skillChooseTitle", "Выберите ваш новый навык"},
            
            //Potion
            {"PotionBuy", "Купить"},
            {"PotionPrice", "Цена : "},
            {"PotionTitle0", "Зелье Жизни"},
            {"PotionDescription0", "Восстанавливает 40% здоровья персонажа."},
            {"PotionTitle1", "Зелье Выносливости"},
            {"PotionDescription1", "Восстанавливает 60% выносливости персонажа."},
            {"PotionTitle2", "Зелье Скорости"},
            {"PotionDescription2", "Увеличивает скорость персонажа в 1,5 раза на 20 секунд."},
            {"PotionTitle3", "Зелье Защиты"},
            {"PotionDescription3", "Уменьшает урон персонажа на 50%"},
            {"PotionTitle4", "Зелье Воскрешения"},
            {"PotionDescription4", "Воскрешает последнего игрока (50% здоровья)"},
            {"PotionTitle5", "Зелье Забвения"},
            {"PotionDescription5", "Удаляет навык, полученный игроком"},
        },
        
        new Dictionary<string, string>
        {
            {"languageName", "Hexadecimal"},
            
            //Connection Menu
            {"connectionMenuTitle", "436f6e6e656374696f6e"},
            {"connectionMenuPseudoText", "50736575646f"},
            {"connectionMenuPasswordText", "50617373776f7264"},
            {"connectionMenuConnectionButton", "436f6e6e656374696f6e"},
            {"connectionMenuNoAccountButton", "4e6f206163636f756e74203f"},
            {"connectionMenuFastConnectionButton", "4661737420636f6e6e656374696f6e"},
            {"connectionMenuErrorUsernameOrPasswordText", "556e636f727265637420757365726e616d65206f722070617373776f7264"},
            {"connectionMenuErrorFastConnectionText", "4661737420636f6e6e656374696f6e206572726f72"},
            {"connectionMenuErrorNoFastConnectionText", "4e6f206661737420636f6e6e656374696f6e207265636f726465642e"},
            {"connectionMenuFastConnectionSaveButton", "5361766520757365726e616d6520616e642070617373776f7264206c6f63616c6c792e"},
            {"connectionMenuFastConnectionSaveWarningText", "/!\\5761726e696e673a0a536176696e6720757365726e616d6520616e642070617373776f7264206c6f63616c6c792063616e206d616b652069742065617369657220746f20636f6e6e6563742c20627574206d61792070726573656e742072657370656374732073656375726974792e20496620796f757220646576696365206973206368726f6d652c20796f75722064617461206d61792062652061736365737369626c652e"},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "496e736372697074696f6e"},
            {"inscriptionMenuPseudoText", "50736575646f"},
            {"inscriptionMenuPasswordText", "50617373776f7264"},
            {"inscriptionMenuPasswordConfirmText", "436f6e6669726d65722070617373776f7264"},
            {"inscriptionMenuInscriptionButton", "496e736372697074696f6e"},
            {"inscriptionMenuAlreadyAccountButton", "416c7265616479206163636f756e74203f"},
            {"inscriptionMenuErrorIncorrectConfirmText", "496e636f727265637420636f6e6669726d6174696f6e"},
            {"inscriptionMenuErrorAlreadyExistText", "55736572206e616d65206c65737473206d756c7469706c65206578697374"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "4c6f626279"},
            {"lobbyMenuCreateGame", "4372656174652047616d65"},
            {"lobbyMenuJoinGame", "4a6f696e2047616d65"},
            
            //Create Game Menu
            {"createGameMenuTitle", "4372656174652047616d65"},
            {"createGameMenuBackButton", "3d3e204261636b>"},
            {"createGameMenuStartGame", "53746172742047616d65"},
            {"createGameMenuID", "4944206f66207468652047616d65"},
            {"createGameMenuPlayer1", "506c6179657220313a"},
            {"createGameMenuPlayer2", "506c6179657220323a"},
            {"createGameMenuPlayer3", "506c6179657220333a"},
            {"createGameMenuPlayer4", "506c6179657220343a"},
            
            //Join Game Menu
            {"joinGameMenuTitle", "4a6f696e2047616d65"},
            {"joinGameMenuBackButton", "<3d204261636b"},
            {"joinGameMenuGameID", "4944206f66207468652047616d65"},
            
            //Select Class Menu
            {"Archer", "417263686572"},
            {"Knight", "4b6e69676874"},
            {"Scientist", "536369656e74697374"},
            {"Assassin", "417373617373696e"},
            {"selectClassMenuReadyButton", "5265616479"},
            {"selectClassMenuWaitingText", "57616974696e6720666f7220746865206f7468657220706c6179657273202e2e2e"},
            
            //Map loading
            {"gameLoadingMapText", "4c6f6164696e6720746865206d6170203a"},
            {"gameLoadingMapWaitingText", "57616974696e6720666f7220746865206f7468657220706c6179657273"},
            
            //Pause Menu
            {"pauseMenuTitle", "5061757365204d656e75"},
            {"pauseMenuResumeButton", "526573756d65"},
            {"pauseMenuSettingsButton", "53657474696e6773"},
            {"pauseMenuLeaveButton", "4c65617665"},
            {"pauseMenuConfirmLeaveTitle", "41726520796f75207375726520796f752077616e7420746f206c65617665207468652067616d653f"},
            {"pauseMenuConfirmLeaveButton", "4c65617665"},
            {"pauseMenuCancelLeaveButton", "43616e63656c"},
            
            //Settings Menu
            {"settingsMenuTitle", "53657474696e6773"},
            {"settingsMenuBackButton", "<3d204261636b"},
            {"settingsMenuResetButton", "5265736574"},
            {"settingsMenuSaveButton", "53617665"},
            
            //Settings Menu - Game
            {"settingsMenuGameButton", "47616d65"},
            {"settingsMenuGameLanguage", "4c616e67756167653a"},
            {"settingsMenuGameMouseSensibility", "4d6f7573652053656e736962696c6974793a"},
            {"settingsMenuGameFullScreen", "46756c6c2053637265656e3a"},
            {"settingsMenuGameEnableChat", "456e61626c6520436861743a"},
            {"settingsMenuGameChatSize", "436861742053697a653a"},
            {"settingsMenuGameChatSizeSmall", "536d616c6c"},
            {"settingsMenuGameChatSizeMedium", "4d656469756d"},
            {"settingsMenuGameChatSizeLarge", "4c61726765"},
            
            //Settings Menu - Audio
            {"settingsMenuAudioButton", "417564696f"},
            
            //Settings Menu - Video
            {"settingsMenuVideoButton", "566964656f"},
            
            //Settings Menu - Controls
            {"settingsMenuControlsButton", "436f6e74726f6c73"},
            {"settingsMenuControlsMoveForward", "4d6f766520466f7277617264"},
            {"settingsMenuControlsMoveBackward", "4d6f7665204261636b77617264"},
            {"settingsMenuControlsMoveLeft", "4d6f7665204c656674"},
            {"settingsMenuControlsMoveRight", "4d6f7665205269676874"},
            {"settingsMenuControlsSprint", "53637265656e"},
            {"settingsMenuControlsDash", "4461736820"},
            {"settingsMenuControlsCapacity1", "43617061636974792031"},
            {"settingsMenuControlsCapacity2", "43617061636974792032"},
            {"settingsMenuControlsCapacity3", "43617061636974792033"},
            {"settingsMenuControlsItem1", "4974656d2031"},
            {"settingsMenuControlsItem2", "4974656d2032"},
            {"settingsMenuControlsItem3", "4974656d2033"},
            {"settingsMenuControlsInventory", "496e76656e746f7279"},
            {"settingsMenuControlsReload", "52656c6f6164"},
            {"settingsMenuControlsChat", "43686174"},
            {"settingsMenuControlsEnableChat", "456e61626c652043686174"},
            {"settingsMenuControlsPause", "4d656e75207061757365"},
            
            //Skill Description
            {"skillDescriptionSpeed", "556e63726561736573207370656564206f6620746865206d6f76656d656e742e0a53656564203a20"},
            {"skillDescriptionHealth", "556e6372656173657320746865206d6178696d756d206865616c7468206f6620746865206368617261637465722e0a4865616c74683a20"},
            {"skillDescriptionDamage", "556e63726561736573207468652064616d61676520746861742064616d6167657320746865206368617261637465722e0a44616d6167653a20"},
            {"skillDescriptionStamina", "556e6372656173657320746865207374616d696e61206f6620746865206368617261637465722073696e20636f6e74696e756f757320616374696f6e732e0a5374616d696e613a20"},
            {"skillDescriptionReload", "556e63726561736573207468652074696d652c206e656564656420746f207468652074696d652072657175697265642e0a52656c6f61643a20"},
            {"skillDescriptionStaminaUse", "556e6372656173657320746865207265736f75726365206f6620746865207374616d696e61206f662074686520636f6e74696e756f757320616374696f6e732e0a5374616d696e613a20"},
            {"skillDescriptionArrow", "41646473206172626f777320666f72207468652063686172616374657220746f2075736520746865206368617261637465722028646f2033206172726f77732e292e0a41646473203a202b31"},
            {"skillDescriptionShootSpeed", "556e6372656173657320746865207370656564206f6620746865206172726f77732e0a5368656f6f743a20"},
            {"skillDescriptionArrowPoison", "53747265616d732061726520706f69736f6e6f757320616e642063617573652064616d61676520666f722061206365727461696e2074696d652e0a44616d61676520706f69736f6e3a203573"},
            {"skillDescriptionArrowGel", "53747265616d732061726520656e636f6d70617373656420776974682067656c652c20726573756c74696e6720696e206c6174657220726573756c747320616e64206c6f7773696e672074686520656e656d6965732e0a44616d6167652067656c65:203573"},
            {"skillDescriptionHealSpeed", "556e6372656173657320746865207370656564206f6620746865206865616c696e672e0a5368656f6f64203a20"},
            {"skillDescriptionLaserMove", "5065726d697473207468652063686172616374657220746f206d6f766520746865206c617a657220646572696e672e"},
            {"skillDescriptionRevive", "5065726d6974732074686520726573706f6e736520746f207265766976652066616c6c656e2073756e646f776e73.0a526573706f6e7365203a20"},
            {"skillDescriptionVampire", "5065726d6974732074686520706c6179657220746f20616c736f726220746865206c696665206f66206f7468657220706c61796572732e0a506c617965723a20313025642064616d616765"},
            {"skillDescriptionReviveAll", "456c656d656e74206861732074686520636170616369747920746f20616e64206d6f7665206f6e6c79206f6e652074696d6520696e207468652067616d652e20456c656d656e7420616c6c2074686520706c61796572732066616c6c656e2073756e646f776e7320616e64206d6f7665207468656d20746f2074686520706f736974696f6e206f6620746865206368617261637465722e"},
            {"skillDescriptionReloadProtection", "506f726f7665732070726f74656374696f6e207572696e672072656c6f61642e0a556d656e7368697a652075726f6e613a20353025"},
            {"skillDescriptionCrit", "556e63726561736573207468652070657263656e7420637269746963616c2061747461636b2e0a4369746963616c2061747461636b3a20"},
            {"skillDescriptionRange", "556e63726561736573207468652072616e67652061747461636b206f6620746865206368617261637465722e0a52616e67653a202b323025"},
            {"skillDescriptionAgro", "5065726d6974732074686520706c6179657220746f2070726f766f6b6520616c6c2074686520656e656d69657320696e2074686520617265612e"},
            {"skillDescriptionSpike", "416e74616e61636c61206d657461646520746865207a616d656e206f626c6173742e0a416e74616e61636c6173683a20323025"},
            {"skillDescriptionAbsorption", "4162736f726273207468652064616d61676520746861742068617320737566666572206f7468657220706c617965727320696e20612073656372657420617265610a506572696f64613a20356d"},
            {"skillDescriptionInvincibility", "50726f766964657320612074656d706f7261727920636f6d70656e736174696f6e2e0a56656d706f207769746820636f6d70656e736174696f6e3a203573"},
            {"skillDescriptionEscalibur", "50726f7669646573207468652070617765722077697468207468652073696c652f6f6620746865206c6567656e646172792073776f726420457863616c696275722c206669676874207570206f6e6520746f20352e0a56656d706f206d6f76656d656e743a203573"},
            {"skillDescriptionDashDegat", "4e616e6f73697420647572616e74652020746865207572616e7320696e2074686520646972616c2e0a5562616e3a20333025"},
            {"skillDescriptionDague", "5065726d69747320746865207573657220746f207468726f77206b6e697665732e"},
            {"skillDescriptionInvisibility", "5065726d69747320746865207573657220746f20696e76697369626c6520666f72206120636f72746f6e742074696d652e0a56656d6520616661697265733a20313073"},
            {"skillDescriptionDoubleAttack", "5065726d69747320746865207573657220746f20636f6d6d697420646f75626c652061747461636b2e"},
            {"skillDescriptionPoison", "5065726d697473207468652061747461636b2063617573652064616d61676520666f722061206365727461696e2074696d652e0a44616d6167653a203573"},
            {"skillChooseTitle", "5669657720656e74657220796f7572206e657720736b696c6c"},
            
            //Potion
            {"PotionBuy", "4b756f666972"},
            {"PotionPrice", "43a0726963653a20"},
            {"PotionTitle0", "5a656c6965205a69686573"},
            {"PotionDescription0", "566f7374616e7469616c732034302525207a65646f7276e6f7374696120"},
            {"PotionTitle1", "5a656c69652056696e746f6c6576"},
            {"PotionDescription1", "566f7374616e7469616c7320363025252076696e746f6c657669646164206f6e"},
            {"PotionTitle2", "5a656c69652053636f727465"},
            {"PotionDescription2", "556e76656c6963686976657420736b6f72757320646520706172616d65747220696e20312c352072617a61206e61202073656b756e642e"},
            {"PotionTitle3", "5a656c6965205a617368697474652"},
            {"PotionDescription3", "556d656e73686165742075646f6e20706172616d657472206f6e20353025"},
            {"PotionTitle4", "5a656c696520566f736b7265736572"},
            {"PotionDescription4", "566f736b726573657220706f736c65646e206a657472205025207a646f726f766e657220706c617965722835302525206865616c7468"},
            {"PotionTitle5", "5a656c6965205a61626c657469"},
            {"PotionDescription5", "4469616772617669656d206d69656e6f206465206d69656e2064616e73206c6520736f757620656e7465722e"},
        }
    };
    
    public Dictionary<string, string> GetLanguage(int index)
    {
        if (index < 0 || index >= _listLanguage.Count)
        {
            return _listLanguage[0];
        }
        return _listLanguage[index];
    }
    
    public List<Dictionary<string, string>> GetAllLanguages()
    {
        return _listLanguage;
    }
}