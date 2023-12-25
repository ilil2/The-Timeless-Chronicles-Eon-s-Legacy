public class State4 : GameManager
{
    public State4()
    {
        State();
    }

    public void State()
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
            AddChild(Map);
					
            _loadMap = false;
        }
        else if (((MapLvl1Script)Map).MapIsReady())
        {
            state = 5;
        }
    }
}