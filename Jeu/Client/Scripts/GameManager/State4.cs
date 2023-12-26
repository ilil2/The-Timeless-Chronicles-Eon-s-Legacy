public partial class State4 : GameManager
{
    public static void State()
    {
        if (ClassSelectUI.ClassChose != "")
        {
            InfoJoueur["class"] = ClassSelectUI.ClassChose;
            ClassSelectUI.ClassChose = "";
            tw2.WriteLine(InfoJoueur["class"]);
            tw2.Flush();
        }
        else if (_loadMap)
        {
            Map.Visible = true;
					
            _loadMap = false;
        }
        else if (((MapLvl1Script)Map).MapIsReady())
        {
            state = 5;
        }
    }
}