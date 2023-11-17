namespace Lib;

public class ListManupulation
{
    public static bool ListofListContain(List<List<string>> list,int ID, string s)
    {
        foreach (var el in list)
        {
            if (el[ID] == s)
            {
                return true;
            }
        }

        return false;
    }
    
    public static int ListofListIndexOf(List<List<string>> list,int ID, string s)
    {
        for (int i = 0; i < list.Count; i++ )
        {
            if (list[i][ID] == s)
            {
                return i;
            }
        }
        return -2;
    }
    
    public static int ListofListNumberOf(List<List<string>> list,int ID, string s)
    {
        int res = 0;
        for (int i = 0; i < list.Count; i++ )
        {
            if (list[i][ID] == s)
            {
                res++;
            }
        }
        return res;
    }
    
    public static bool ListofListExist(List<List<string>> list,int ID, int ID2)
    {
        try
        {
            string l = list[ID][ID2];
            return true;
        }
        catch
        {
            return false;
        }
    }
}