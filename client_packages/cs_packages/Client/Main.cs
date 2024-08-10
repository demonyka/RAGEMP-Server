using RAGE;
using RAGE.Elements;
using RAGE.Game;
using RAGE.Ui;

public class Main : Events.Script
{
    HtmlWindow openedWindow;
    Camera currentCamera;

    public Main()
    {
        Events.OnPlayerReady += OnPlayerReady;
        Events.OnPlayerSpawn += OnPlayerSpawn;
        Events.OnPlayerCreateWaypoint += OnPlayerCreateWaypoint;
        Events.Add("closeBrowser", onCloseBrowserMessage);
        Events.Add("CEF:CLIENT::REGISTER_BUTTON_CLICKED", OnCefRegisterButtonClicked);
        Events.Add("CEF:CLIENT::LOGIN_BUTTON_CLICKED", OnCefLoginButtonClicked);
        Events.Add("SERVER:CLIENT::REGISTER_BUTTON_CLICKER", OnCefRegisterButtonClicked);
        Events.Add("SERVER:CLIENT::REGISTER_USER", OnServerRegisterUser);
        Events.Add("SERVER:CLIENT::FAILED_USER_LOGIN", OnServerFailedUserLogin);
        Events.Add("SERVER:CLIENT::CREATE_PERSON", OnServerCreatePerson);
        Events.Add("SERVER:CLIENT::CHOOSE_PERSON", OnServerChoosePerson);
    }
    public void OnServerChoosePerson(object[] args)
    {
        openedWindow.Destroy();
        currentCamera = new Camera((ushort)Cam.CreateCameraWithParams(Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"), -1849.8408f, -1231.6427f, 13.7f, 0, 0, 140.14017f, 20.0f, true, 2), 0);
        Cam.PointCamAtCoord(currentCamera.Id, -1851.2072f, -1233.2225f, 13.7f);
        Cam.SetCamActive(currentCamera.Id, true);
        Cam.RenderScriptCams(true, false, 0, true, false, 0);

        //openedWindow = new HtmlWindow("package://cef/create_character/index.html");
        //openedWindow.Active = true;
        Cursor.ShowCursor(true, true);
    }

    public void OnServerCreatePerson(object[] args)
    {
        openedWindow.Destroy();
        currentCamera = new Camera((ushort)Cam.CreateCameraWithParams(Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"), -1849.8408f, -1231.6427f, 13.7f, 0, 0, 140.14017f, 20.0f, true, 2), 0);
        Cam.PointCamAtCoord(currentCamera.Id, -1851.2072f, -1233.2225f, 13.7f);
        Cam.SetCamActive(currentCamera.Id, true);
        Cam.RenderScriptCams(true, false, 0, true, false, 0);

        openedWindow = new HtmlWindow("package://cef/create_character/index.html");
        openedWindow.Active = true;
        Cursor.ShowCursor(true, true);
    }

    public void OnServerFailedUserLogin(object[] args)
    {
        openedWindow.ExecuteJs("document.dispatchEvent(new Event('loginUserFailed'))"); 
    }

    public void OnServerRegisterUser(object[] args)
    {
        bool isExist = (bool)args[0];
        if (!isExist)
        {
            openedWindow.ExecuteJs("document.dispatchEvent(new Event('registerUserExists'))");
        }
        else
        {
            openedWindow.ExecuteJs("document.dispatchEvent(new Event('registerUserSuccess'))");
        }
    }

    public void OnCefLoginButtonClicked(object[] args)
    {
        string login = args[0].ToString();
        string password = args[1].ToString();
        Events.CallRemote("CLIENT:SERVER::LOGIN_BUTTON_CLICKED", login, password);
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