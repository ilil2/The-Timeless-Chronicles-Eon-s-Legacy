using Godot;
using Lib;

public partial class State0 : GameManager
{
    public static void State()
    {
        if (tentative_connection)
        {
	        if (ConnectionUI.ConnectionButton.ButtonPressed)
	        {
		        if (ConnectionUI._pseudo.Length >= 4 && ConnectionUI._pseudo.Length <= 16 &&
		            ConnectionUI._password.Length >= 8 && ConnectionUI._password.Length <= 32)
		        {
			        tw.WriteLine($"conn:{ConnectionUI._pseudo};{Hashing.ToSHA256(ConnectionUI._password)}");
			        tw.Flush();
        							
			        string? line = tr.ReadLine();
			        if (line == "connection success")
			        {
				        tentative_connection = false;
				        InfoJoueur["pseudo"] = ConnectionUI._pseudo;
				        FastConnectionManager.SetConnection(ConnectionUI._pseudo, Hashing.ToSHA256(ConnectionUI._password));
				        FastConnectionManager.SaveConnection();
			        }
			        else
			        {
				        ConnectionError = "Pseudo ou mot de passe incorrect";
			        }
        
		        }
		        else
		        {
			        ConnectionError = "Pseudo ou mot de passe incorrect";
		        }
	        }
        
	        else if (ConnectionUI.InscriptionButton.ButtonPressed)
	        {
		        if (ConnectionUI._pseudo.Length >= 4 && ConnectionUI._pseudo.Length <= 16 &&
		            ConnectionUI._password.Length >= 8 && ConnectionUI._password.Length <= 32)
		        {
			        tw.WriteLine($"insc:{ConnectionUI._pseudo};{Hashing.ToSHA256(ConnectionUI._password)}");
			        tw.Flush();
        							
			        string line = tr.ReadLine();
			        if (line == "creation success")
			        {
				        tentative_connection = false;
				        InfoJoueur["pseudo"] = ConnectionUI._pseudo;
			        }
			        else
			        {
				        ConnectionError = "Pseudo deja existant";
			        }
        							
		        }
		        else
		        {
			        ConnectionError = "Pseudo ou mot de passe incorrect";
		        }
	        }
	        
	        else if (ConnectionUI.FastConnectionButton.ButtonPressed)
	        {
		        tw.WriteLine($"conn:{ConnectionUI._pseudo};{ConnectionUI._password}");
		        tw.Flush();
		        
		        string? line = tr.ReadLine();
		        if (line == "connection success")
		        {
			        tentative_connection = false;
			        InfoJoueur["pseudo"] = ConnectionUI._pseudo;
		        }
		        else
		        {
			        ConnectionError = "Erreur dans la connexion rapide";
		        }
	        }
        }
        				
        else
        {
	        ConnectionUI.in_connection = false;
	        state = 1;
        }
    }
}