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
            {"createGameMenuBackButton", "<= Back"},
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
            {"selectClassMenuArcherClass", "Archer"},
            {"selectClassMenuKnightClass", "Knight"},
            {"selectClassMenuScientistClass", "Scientist"},
            {"selectClassMenuAssassinClass", "Assassin"},
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
            {"settingsMenuControlsPause", "Pause"},
        },
        
        new Dictionary<string, string>
        {
            {"languageName", "Français"},
            
            //Connection Menu
            {"connectionMenuTitle", "Connexion"},
            {"connectionMenuPseudoText", "Pseudo"},
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
            {"inscriptionMenuPseudoText", "Pseudo"},
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
            {"createGameMenuBackButton", "<= Retour"},
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
            {"selectClassMenuArcherClass", "Archer"},
            {"selectClassMenuKnightClass", "Chevalier"},
            {"selectClassMenuScientistClass", "Scientifique"},
            {"selectClassMenuAssassinClass", "Assassin"},
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
            {"settingsMenuControlsPause", "Pause"},
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
            {"createGameMenuBackButton", "<= Distreiñ"},
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
            {"selectClassMenuArcherClass", "Sagittaire"},
            {"selectClassMenuKnightClass", "Kavaler"},
            {"selectClassMenuScientistClass", "Skiantour"},
            {"selectClassMenuAssassinClass", "Lec'helour"},
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
            {"settingsMenuControlsPause", "Meneger"},
        },
        
        new Dictionary<string, string>
        {
            {"languageName", "Duetsch"},
            
            //Connection Menu
            {"connectionMenuTitle", "Verbindung"},
            {"connectionMenuPseudoText", "Pseudo"},
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
            {"inscriptionMenuPseudoText", "Pseudo"},
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
            {"createGameMenuBackButton", "<= Zurück"},
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
            {"selectClassMenuArcherClass", "Bogenschütze"},
            {"selectClassMenuKnightClass", "Ritter"},
            {"selectClassMenuScientistClass", "Wissenschaftler"},
            {"selectClassMenuAssassinClass", "Attentäter"},
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
            {"settingsMenuControlsPause", "Pause"},
        },
        
        new Dictionary<string, string>
        {
            {"languageName", "Español"},
            
            //Connection Menu
            {"connectionMenuTitle", "Conexión"},
            {"connectionMenuPseudoText", "Pseudo"},
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
            {"inscriptionMenuPseudoText", "Pseudo"},
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
            {"createGameMenuBackButton", "<= Regreso"},
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
            {"selectClassMenuArcherClass", "Arquero"},
            {"selectClassMenuKnightClass", "Caballero"},
            {"selectClassMenuScientistClass", "Científico"},
            {"selectClassMenuAssassinClass", "Asesino"},
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
            {"settingsMenuControlsPause", "Pausa"},
        },
        
        new Dictionary<string, string>
        {
            {"languageName", "Esperanto"}, 
            
            //Connection Menu
            {"connectionMenuTitle", "Konekto"},
            {"connectionMenuPseudoText", "Pseudo"},
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
            {"inscriptionMenuPseudoText", "Pseudo"},
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
            {"createGameMenuBackButton", "<= Reen"},
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
            {"selectClassMenuArcherClass", "Arĉisto"},
            {"selectClassMenuKnightClass", "Kavaliro"},
            {"selectClassMenuScientistClass", "Sciencisto"},
            {"selectClassMenuAssassinClass", "Murdisto"},
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
            {"settingsMenuControlsPause", "Paŭzo"},
        },
        
        new Dictionary<string, string>
        {
            {"languageName", "Latinus"},
            
            //Connection Menu
            {"connectionMenuTitle", "Connexio"},
            {"connectionMenuPseudoText", "Pseudo"},
            {"connectionMenuPasswordText", "Password"},
            {"connectionMenuConnectionButton", "Connexio"},
            {"connectionMenuNoAccountButton", "Non habes rationem?"},
            {"connectionMenuFastConnectionButton", "Connexio celeriter"},
            {"connectionMenuErrorUsernameOrPasswordText", "Pseudo vel password falsum"},
            {"connectionMenuErrorFastConnectionText", "Error in connexione celeriter"},
            {"connectionMenuErrorNoFastConnectionText", "Nulla connexio celeriter registrata"},
            {"connectionMenuFastConnectionSaveButton", "Serva pseudo et password localiter."},
            {"connectionMenuFastConnectionSaveWarningText", "/!\\ Cave :\nServare pseudo et password localiter facilius est in ingressu, sed potest periculum securitatis habere. Si instrumentum tuum compromissum est, tuae informationes possunt esse accessibiles."},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Inscriptio"},
            {"inscriptionMenuPseudoText", "Pseudo"},
            {"inscriptionMenuPasswordText", "Password"},
            {"inscriptionMenuPasswordConfirmText", "Password confirmare"},
            {"inscriptionMenuInscriptionButton", "Inscriptio"},
            {"inscriptionMenuAlreadyAccountButton", "Habes rationem?"},
            {"inscriptionMenuErrorIncorrectConfirmText", "Password confirmare falsum"},
            {"inscriptionMenuErrorAlreadyExistText", "Pseudo iam existit"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Lobby"},
            {"lobbyMenuCreateGame", "Ludum creare"},
            {"lobbyMenuJoinGame", "Ludum iungere"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Ludum creare"},
            {"createGameMenuBackButton", "<= Retro"},
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
            {"selectClassMenuArcherClass", "Sagittarius"},
            {"selectClassMenuKnightClass", "Eques"},
            {"selectClassMenuScientistClass", "Scientist"},
            {"selectClassMenuAssassinClass", "Occisor"},
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
            {"settingsMenuControlsPause", "Pause"},
        },
        
        new Dictionary<string, string>
        {
            {"languageName", "Zulu"},
            
            //Connection Menu
            {"connectionMenuTitle", "Ukuxhumana"},
            {"connectionMenuPseudoText", "Pseudo"},
            {"connectionMenuPasswordText", "Iphasiwedi"},
            {"connectionMenuConnectionButton", "Ukuxhumana"},
            {"connectionMenuNoAccountButton", "Awunayo i-akhawunti?"},
            {"connectionMenuFastConnectionButton", "Ukuxhumana okukhulu"},
            {"connectionMenuErrorUsernameOrPasswordText", "Pseudo noma iphasiwedi elingenayo"},
            {"connectionMenuErrorFastConnectionText", "Iphutha ekuxhumaneni okukhulu"},
            {"connectionMenuErrorNoFastConnectionText", "Awukho ukuxhumana okukhulu okuqashisayo"},
            {"connectionMenuFastConnectionSaveButton", "Londoloza i-pseudo noma iphasiwedi ekhaya."},
            {"connectionMenuFastConnectionSaveWarningText", "/!\\ Iseluleko :\nUkuphathwa kwe-pseudo noma iphasiwedi ekhaya kuyafaka kahle ukuxhumana, kodwa kungaba nezimvo zokuphepha. Uma isayithi sakho sibuyekezwe, izifundo zakho zingakwazi ukufinyelela."},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Ukubhalisa"},
            {"inscriptionMenuPseudoText", "Pseudo"},
            {"inscriptionMenuPasswordText", "Iphasiwedi"},
            {"inscriptionMenuPasswordConfirmText", "Yaqinisa iphasiwedi"},
            {"inscriptionMenuInscriptionButton", "Ukubhalisa"},
            {"inscriptionMenuAlreadyAccountButton", "Unayo i-akhawunti?"},
            {"inscriptionMenuErrorIncorrectConfirmText", "Yaqinisa iphasiwedi elingenayo"},
            {"inscriptionMenuErrorAlreadyExistText", "Pseudo elingenayo"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Lobby"},
            {"lobbyMenuCreateGame", "Yakha umdlalo"},
            {"lobbyMenuJoinGame", "Ukungena umdlalo"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Yakha umdlalo"},
            {"createGameMenuBackButton", "<= Emuva"},
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
            {"selectClassMenuArcherClass", "Umshayeli"},
            {"selectClassMenuKnightClass", "Umkhonto"},
            {"selectClassMenuScientistClass", "Umfundi"},
            {"selectClassMenuAssassinClass", "Ukubulala"},
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
            {"settingsMenuControlsPause", "I-Pause"},
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
            {"createGameMenuBackButton", "<= 返回"},
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
            {"selectClassMenuArcherClass", "弓箭手"},
            {"selectClassMenuKnightClass", "骑士"},
            {"selectClassMenuScientistClass", "科学家"},
            {"selectClassMenuAssassinClass", "刺客"},
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
            {"settingsMenuControlsPause", "暂停"},
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
            {"createGameMenuBackButton", "<= Πίσω"},
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
            {"selectClassMenuArcherClass", "Τοξότης"},
            {"selectClassMenuKnightClass", "Ιππότης"},
            {"selectClassMenuScientistClass", "Επιστήμονας"},
            {"selectClassMenuAssassinClass", "Δολοφόνος"},
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
            {"settingsMenuControlsPause", "Παύση"},
            
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
            {"createGameMenuBackButton", "<= Назад"},
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
            {"selectClassMenuArcherClass", "Лучник"},
            {"selectClassMenuKnightClass", "Рыцарь"},
            {"selectClassMenuScientistClass", "Ученый"},
            {"selectClassMenuAssassinClass", "Убийца"},
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
            {"settingsMenuControlsPause", "Пауза"},
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
            {"createGameMenuBackButton", "<3d204261636b"},
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
            {"selectClassMenuArcherClass", "417263686572"},
            {"selectClassMenuKnightClass", "4b6e69676874"},
            {"selectClassMenuScientistClass", "536369656e74697374"},
            {"selectClassMenuAssassinClass", "417373617373696e"},
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
            {"settingsMenuControlsPause", "5061757365"},
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