using RAGE;
using RAGE.Game;
using RAGE.Ui;

public class Main : Events.Script
{
    public Main()
    {
        Events.OnPlayerReady += OnPlayerReady;
        Events.OnPlayerSpawn += OnPlayerSpawn;
        Events.OnPlayerCreateWaypoint += OnPlayerCreateWaypoint;
    }

    public void OnPlayerReady()
    {
        Chat.Output("Добро пожаловать");
    }

    public void OnPlayerSpawn(Events.CancelEventArgs cancel)
    {
        HtmlWindow htmlWindow = new HtmlWindow("package://cef/auth/register.html");
        htmlWindow.Active = true;
        Cursor.ShowCursor(true, true);
    }

    public void OnPlayerCreateWaypoint(Vector3 position)
    {
        Events.CallRemote("CLIENT:SERVER::CLIENT_CREATE_WAYPOINT", position.X, position.Y, position.Z);
    }
}