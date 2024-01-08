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
            {"languageName", "Français"},
            
            //Connection Menu
            {"connectionMenuTitle", "Connection"},
            {"connectionMenuPseudoText", "Pseudo"},
            {"connectionMenuPasswordText", "Mot de passe"},
            {"connectionMenuConnectionButton", "Connection"},
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
            {"languageName", "Ελληνική"},
            
            //Connection Menu
            {"connectionMenuTitle", "Σύνδεση"},
            {"connectionMenuPseudoText", "Pseudo"},
            {"connectionMenuPasswordText", "Κωδικός πρόσβασης"},
            {"connectionMenuConnectionButton", "Σύνδεση"},
            {"connectionMenuNoAccountButton", "Δεν έχετε λογαριασμό;"},
            
            //Inscription Menu
            {"inscriptionMenuTitle", "Εγγραφή"},
            {"inscriptionMenuPseudoText", "Pseudo"},
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
        }
    };
    
    public Dictionary<string, string> GetLanguage(int index)
    {
        return _listLanguage[index];
    }
}