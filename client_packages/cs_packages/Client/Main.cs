using RAGE;
using RAGE.Game;
using RAGE.Ui;

public class Main : Events.Script
{
    HtmlWindow openedWindow;

    public Main()
    {
        Events.OnPlayerReady += OnPlayerReady;
        Events.OnPlayerSpawn += OnPlayerSpawn;
        Events.OnPlayerCreateWaypoint += OnPlayerCreateWaypoint;
        Events.Add("closeBrowser", onCloseBrowserMessage);
        Events.Add("CEF:CLIENT::REGISTER_BUTTON_CLICKED", OnCefRegisterButtonClicked);
    }

    public void OnCefRegisterButtonClicked(object[] args)
    {
        string login = args[0].ToString();
        string password = args[1].ToString();
        string email = args[2].ToString();
        
        Events.CallRemote("CLIENT:SERVER::REGISTER_BUTTON_CLICKED", login, password, email);
    }

    public void onCloseBrowserMessage(object[] args)
    {
        openedWindow.Destroy();
        Cursor.ShowCursor(false, false);
    }

    public void OnPlayerReady()
    {
        Chat.Output("Добро пожаловать");
    }

    public void OnPlayerSpawn(Events.CancelEventArgs cancel)
    {
        openedWindow = new HtmlWindow("package://cef/auth/index.html");
        openedWindow.Active = true;
        Cursor.ShowCursor(true, true);
    }

    public void OnPlayerCreateWaypoint(Vector3 position)
    {
        Events.CallRemote("CLIENT:SERVER::CLIENT_CREATE_WAYPOINT", position.X, position.Y, position.Z);
    }
}