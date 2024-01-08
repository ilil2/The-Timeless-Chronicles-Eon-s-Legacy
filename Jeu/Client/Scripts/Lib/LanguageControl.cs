﻿using System.Collections.Generic;

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
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Inscription"},
            {"inscriptionMenuPseudoText", "Pseudo"},
            {"inscriptionMenuPasswordText", "Password"},
            {"inscriptionMenuPasswordConfirmText", "Confirm password"},
            {"inscriptionMenuInscriptionButton", "Inscription"},
            {"inscriptionMenuAlreadyAccountButton", "Already have an account ?"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Lobby"},
            {"lobbyMenuCreateGame", "Create Game"},
            {"lobbyMenuJoinGame", "Join Game"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Create Game"},
            {"createGameMenuBackButton", "<= Back"},
            {"createGameMenuStartGame", "Password"},
            
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
            {"languageName", "English (Hexadecimal)"},
            
            //Connection Menu
            {"connectionMenuTitle", "436f6e6e656374696f6e"},
            {"connectionMenuPseudoText", "50736575646f"},
            {"connectionMenuPasswordText", "50617373776f7264"},
            {"connectionMenuConnectionButton", "436f6e6e656374696f6e"},
            {"connectionMenuNoAccountButton", "4e6f206163636f756e74203f"},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "496e736372697074696f6e"},
            {"inscriptionMenuPseudoText", "50736575646f"},
            {"inscriptionMenuPasswordText", "50617373776f7264"},
            {"inscriptionMenuPasswordConfirmText", "436f6e6669726d65722070617373776f7264"},
            {"inscriptionMenuInscriptionButton", "496e736372697074696f6e"},
            {"inscriptionMenuAlreadyAccountButton", "416c7265616479206163636f756e74203f"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "4c6f626279"},
            {"lobbyMenuCreateGame", "4372656174652047616d65"},
            {"lobbyMenuJoinGame", "4a6f696e2047616d65"},
            
            //Create Game Menu
            {"createGameMenuTitle", "4372656174652047616d65"},
            {"createGameMenuBackButton", "<3d204261636b"},
            {"createGameMenuStartGame", "50617373776f7264"},
            
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
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Inscription"},
            {"inscriptionMenuPseudoText", "Pseudo"},
            {"inscriptionMenuPasswordText", "Mot de passe"},
            {"inscriptionMenuPasswordConfirmText", "Confirmer le mot de passe"},
            {"inscriptionMenuInscriptionButton", "Inscription"},
            {"inscriptionMenuAlreadyAccountButton", "Déjà un compte ?"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Lobby"},
            {"lobbyMenuCreateGame", "Créer une partie"},
            {"lobbyMenuJoinGame", "Rejoindre une partie"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Créer une partie"},
            {"createGameMenuBackButton", "<= Retour"},
            {"createGameMenuStartGame", "Démarrer la partie"},
            
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
            {"languageName", "Français (Hexadécimal)"},
            
            //Connection Menu
            {"connectionMenuTitle", "436f6e6e6578696f6e"},
            {"connectionMenuPseudoText", "50736575646f"},
            {"connectionMenuPasswordText", "4d6f7420646520706173736520"},
            {"connectionMenuConnectionButton", "436f6e6e6578696f6e"},
            {"connectionMenuNoAccountButton", "50617320646520636f6d707465203f"},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "496e736372697074696f6e"},
            {"inscriptionMenuPseudoText", "50736575646f"},
            {"inscriptionMenuPasswordText", "4d6f7420646520706173736520"},
            {"inscriptionMenuPasswordConfirmText", "436f6e6669726d6572206c6520706173736520"},
            {"inscriptionMenuInscriptionButton", "496e736372697074696f6e"},
            {"inscriptionMenuAlreadyAccountButton", "446576657a20616e20636f6d707465203f"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "4c6f626279"},
            {"lobbyMenuCreateGame", "43726565652047616d65"},
            {"lobbyMenuJoinGame", "52656a6f696e6472652047616d65"},
            
            //Create Game Menu
            {"createGameMenuTitle", "43726565652047616d65"},
            {"createGameMenuBackButton", "<3d204261636b"},
            {"createGameMenuStartGame", "506173736572"},
            
            //Join Game Menu
            {"joinGameMenuTitle", "52656a6f696e6472652047616d65"},
            {"joinGameMenuBackButton", "<3d204261636b"},
            {"joinGameMenuGameID", "4944206465206c612047616d65"},
            
            //Select Class Menu
            {"selectClassMenuArcherClass", "417263686572"},
            {"selectClassMenuKnightClass", "43686576616c696572"},
            {"selectClassMenuScientistClass", "536369656e7469737465"},
            {"selectClassMenuAssassinClass", "417373617373696e"},
            {"selectClassMenuReadyButton", "507265747"},
            {"selectClassMenuWaitingText", "457320617474656e7465206465206c657320617574726573206a6f7565757273202e2e2e"},
            
            //Pause Menu
            {"pauseMenuTitle", "4d656e75205061757365"},
            {"pauseMenuResumeButton", "526573756d6572"},
            {"pauseMenuSettingsButton", "506172616d6574726573"},
            {"pauseMenuLeaveButton", "5169756974746572"},
            {"pauseMenuConfirmLeaveTitle", "43eates-vous s3r de vouloir quitter le jeu ?"},
            {"pauseMenuConfirmLeaveButton", "5169756974746572"},
            {"pauseMenuCancelLeaveButton", "416e6e756c6572"},
            
            //Settings Menu
            {"settingsMenuTitle", "506172616d6574726573"},
            {"settingsMenuBackButton", "<3d204261636b"},
            {"settingsMenuResetButton", "5265736574"},
            {"settingsMenuSaveButton", "53617665"},
            
            //Settings Menu - Game
            {"settingsMenuGameButton", "4a6575"},
            {"settingsMenuGameLanguage", "4c616e67756167653a"},
            {"settingsMenuGameMouseSensibility", "53656e736962696c697465206465206c6120736f75726973"},
            {"settingsMenuGameFullScreen", "46756c6c2053637265656e3a"},
            {"settingsMenuGameEnableChat", "4163746976657220636861743a"},
            {"settingsMenuGameChatSize", "5461696c6c6520647520636861743a"},
            {"settingsMenuGameChatSizeSmall", "5065746974"},
            {"settingsMenuGameChatSizeMedium", "4d6f79656e"},
            {"settingsMenuGameChatSizeLarge", "47616e6420656e636f7265"},
            
            //Settings Menu - Audio
            {"settingsMenuAudioButton", "417564696f"},
            
            //Settings Menu - Video
            {"settingsMenuVideoButton", "566964656f"},
            
            //Settings Menu - Controls
            {"settingsMenuControlsButton", "436f6e74726f6c6573"},
            {"settingsMenuControlsMoveForward", "416176616e636572"},
            {"settingsMenuControlsMoveBackward", "526563756c6572"},
            {"settingsMenuControlsMoveLeft", "476163686572"},
            {"settingsMenuControlsMoveRight", "44656d696e"},
            {"settingsMenuControlsSprint", "53637265656e"},
            {"settingsMenuControlsDash", "4461736820"},
            {"settingsMenuControlsCapacity1", "43617061636974652031"},
            {"settingsMenuControlsCapacity2", "43617061636974652032"},
            {"settingsMenuControlsCapacity3", "43617061636974652033"},
            {"settingsMenuControlsItem1", "4f626a65742031"},
            {"settingsMenuControlsItem2", "4f626a65742032"},
            {"settingsMenuControlsItem3", "4f626a65742033"},
            {"settingsMenuControlsInventory", "496e76656e746f697265"},
            {"settingsMenuControlsReload", "52656c6f6164"},
            {"settingsMenuControlsChat", "5463686174"},
            {"settingsMenuControlsPause", "5061757365"},
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
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Einschreibung"},
            {"inscriptionMenuPseudoText", "Pseudo"},
            {"inscriptionMenuPasswordText", "Passwort"},
            {"inscriptionMenuPasswordConfirmText", "Passwort bestätigen"},
            {"inscriptionMenuInscriptionButton", "Einschreibung"},
            {"inscriptionMenuAlreadyAccountButton", "Bereits ein Konto?"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Lobby"},
            {"lobbyMenuCreateGame", "Spiel erstellen"},
            {"lobbyMenuJoinGame", "Spiel beitreten"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Spiel erstellen"},
            {"createGameMenuBackButton", "<= Zurück"},
            {"createGameMenuStartGame", "Spiel starten"},
            
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
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Inscripción"},
            {"inscriptionMenuPseudoText", "Pseudo"},
            {"inscriptionMenuPasswordText", "Contraseña"},
            {"inscriptionMenuPasswordConfirmText", "Confirmar contraseña"},
            {"inscriptionMenuInscriptionButton", "Inscripción"},
            {"inscriptionMenuAlreadyAccountButton", "¿Ya tienes una cuenta?"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Lobby"},
            {"lobbyMenuCreateGame", "Crear juego"},
            {"lobbyMenuJoinGame", "Unirse al juego"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Crear juego"},
            {"createGameMenuBackButton", "<= Regreso"},
            {"createGameMenuStartGame", "Comenzar el juego"},
            
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
            {"languageName", "Latinus"},
            
            //Connection Menu
            {"connectionMenuTitle", "Connexio"},
            {"connectionMenuPseudoText", "Pseudo"},
            {"connectionMenuPasswordText", "Password"},
            {"connectionMenuConnectionButton", "Connexio"},
            {"connectionMenuNoAccountButton", "Non habes rationem?"},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Inscriptio"},
            {"inscriptionMenuPseudoText", "Pseudo"},
            {"inscriptionMenuPasswordText", "Password"},
            {"inscriptionMenuPasswordConfirmText", "Password confirmare"},
            {"inscriptionMenuInscriptionButton", "Inscriptio"},
            {"inscriptionMenuAlreadyAccountButton", "Habes rationem?"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Lobby"},
            {"lobbyMenuCreateGame", "Ludum creare"},
            {"lobbyMenuJoinGame", "Ludum iungere"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Ludum creare"},
            {"createGameMenuBackButton", "<= Retro"},
            {"createGameMenuStartGame", "Ludum incipere"},
            
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
            {"languageName", "中文"},
            
            //Connection Menu
            {"connectionMenuTitle", "连接"},
            {"connectionMenuPseudoText", "伪"},
            {"connectionMenuPasswordText", "密码"},
            {"connectionMenuConnectionButton", "连接"},
            {"connectionMenuNoAccountButton", "没有帐户？"},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "注册"},
            {"inscriptionMenuPseudoText", "伪"},
            {"inscriptionMenuPasswordText", "密码"},
            {"inscriptionMenuPasswordConfirmText", "确认密码"},
            {"inscriptionMenuInscriptionButton", "注册"},
            {"inscriptionMenuAlreadyAccountButton", "已经有一个帐户？"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "大厅"},
            {"lobbyMenuCreateGame", "创建游戏"},
            {"lobbyMenuJoinGame", "加入游戏"},
            
            //Create Game Menu
            {"createGameMenuTitle", "创建游戏"},
            {"createGameMenuBackButton", "<= 返回"},
            {"createGameMenuStartGame", "开始游戏"},
            
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
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Εγγραφή"},
            {"inscriptionMenuPseudoText", "Ψευδώνυμο"},
            {"inscriptionMenuPasswordText", "Κωδικός πρόσβασης"},
            {"inscriptionMenuPasswordConfirmText", "Επιβεβαίωση κωδικού πρόσβασης"},
            {"inscriptionMenuInscriptionButton", "Εγγραφή"},
            {"inscriptionMenuAlreadyAccountButton", "Έχετε ήδη ένα λογαριασμό;"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Σαλόνι"},
            {"lobbyMenuCreateGame", "Δημιουργία παιχνιδιού"},
            {"lobbyMenuJoinGame", "Ενταχθείτε στο παιχνίδι"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Δημιουργία παιχνιδιού"},
            {"createGameMenuBackButton", "<= Πίσω"},
            {"createGameMenuStartGame", "Ξεκινήστε το παιχνίδι"},
            
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
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Регистрация"},
            {"inscriptionMenuPseudoText", "Псевдоним"},
            {"inscriptionMenuPasswordText", "Пароль"},
            {"inscriptionMenuPasswordConfirmText", "Подтвердите пароль"},
            {"inscriptionMenuInscriptionButton", "Регистрация"},
            {"inscriptionMenuAlreadyAccountButton", "Уже есть аккаунт?"},
            
            //Lobby Menu
            {"lobbyMenuTitle", "Лобби"},
            {"lobbyMenuCreateGame", "Создать игру"},
            {"lobbyMenuJoinGame", "Присоединиться к игре"},
            
            //Create Game Menu
            {"createGameMenuTitle", "Создать игру"},
            {"createGameMenuBackButton", "<= Назад"},
            {"createGameMenuStartGame", "Начать игру"},
            
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
    };
    
    public Dictionary<string, string> GetLanguage(int index)
    {
        return _listLanguage[index];
    }
}