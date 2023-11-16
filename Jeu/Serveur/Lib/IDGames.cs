namespace Lib;

public class IDGames
{
    public static string LetterID()
    {
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        Random rand = new Random();
        string res = "";
        for (int i = 0; i < 6; i++)
        {
            res += letters[rand.Next(0, 26)];
        }

        return res;
    }
}