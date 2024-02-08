using Lib;

public partial class State4 : GameManager
{
	public int LoadBar = 0;
	public static void State()
	{
		if (ClassSelectUI.ClassChose != "")
		{
			InfoJoueur["class"] = ClassSelectUI.ClassChose;
			ClassSelectUI.ClassChose = "";
			UDP.Send(soc2,$"{InfoJoueur["id"]}/{InfoJoueur["class"]}",iep2);
		}
		else if (Map.MapIsReady() && !StartMap)
		{
			Map.Visible = false;
			UDP.Send(soc2,"load",iep2);
			MapOnLoad = false;
		}
		else if (StartMap)
		{
			GameManager.ProgressBar.QueueFree();
			Map.Visible = true;
			state = 5;
		}
	}
}
