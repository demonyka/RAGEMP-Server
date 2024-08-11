using RAGE;
using RAGE.Elements;
using RAGE.Game;
using RAGE.Ui;

public class Main : Events.Script
{
    public static HtmlWindow openedWindow;
    public static Camera currentCamera;

    public Main()
    {
        Events.OnPlayerReady += OnPlayerReady;
        Events.OnPlayerSpawn += OnPlayerSpawn;
        Events.OnPlayerCreateWaypoint += OnPlayerCreateWaypoint;
    }

    public void OnPlayerReady()
    {
        
    }

    public void OnPlayerSpawn(Events.CancelEventArgs cancel)
    {
        openedWindow = new HtmlWindow("package://cef/auth/index.html");
        openedWindow.Active = true;
        Cursor.ShowCursor(true, true);
        Ui.DisplayRadar(false);
        Chat.Show(false);
    }

    public void OnPlayerCreateWaypoint(Vector3 position)
    {
        Events.CallRemote("CLIENT:SERVER::CLIENT_CREATE_WAYPOINT", position.X, position.Y, position.Z);
    }
}