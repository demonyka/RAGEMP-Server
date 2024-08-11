using RAGE;
using RAGE.Ui;

public class ServerEvents : Events.Script
{
    public ServerEvents()
    {
        Events.Add("closeBrowser", onPlayerCloseBrowserMessage);
    }

    public void onPlayerCloseBrowserMessage(object[] args)
    {
        Main.openedWindow.Destroy();
        Cursor.ShowCursor(false, false);
    }
}