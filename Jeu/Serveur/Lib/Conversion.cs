namespace Lib;

public class Conversion
{
    public static int AtoI(string? s)
    {
        int res = 0;

        foreach (var c in s)
        {
            if (char.IsNumber(c))
            {
                res += (int)c - 48;
                res *= 10;
            }
            else
            {
                throw new InvalidCastException("veuillez rentrer un nombre en base 10");
            }
        }

        res /= 10;
        return res;
    }
}