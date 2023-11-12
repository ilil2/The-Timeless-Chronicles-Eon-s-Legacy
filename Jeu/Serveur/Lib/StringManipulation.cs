namespace Lib;

public class StringManipulation
{
    public static bool Contain(string s, string ns)
    {
        foreach (var nc in ns)
        {
            foreach (var c in s)
            {
                if (c == nc)
                {
                    return true;
                }
            }
        }

        return false;
    }
}