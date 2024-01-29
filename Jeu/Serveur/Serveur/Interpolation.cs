using System.Net.Sockets;

namespace Serveur;

public class Interpolation : Serveur
{
    public static void Inter(string s, int id,Socket soc)
    {
        string s2 = info[id];
        
        string[] s3 = s.Split(";");
        string[] s4 = s2.Split(";");
        
        float x = float.Parse(s3[0]);
        float y = float.Parse(s3[1]);
        float z = float.Parse(s3[2]);
        
        float x2 = float.Parse(s4[0]);
        float y2 = float.Parse(s4[1]);
        float z2 = float.Parse(s4[2]);
        
        float x3 = (x - x2) / 10;
        float y3 = (y - y2) / 10;
        float z3 = (z - z2) / 10;
        
        for (int i = 0; i < 10; i++)
        {
            x2 += x3;
            y2 += y3;
            z2 += z3;
            
            info[id] = $"{x2};{y2};{z2}";
            
            SendAll(soc, GetInfo());
            
            Thread.Sleep(50);
        }
    }
}