using RAGE;
using RAGE.Elements;
using RAGE.Game;
using RAGE.Ui;

public class AuthEvents : Events.Script
{
    public AuthEvents()
    {
        Events.Add("SERVER:CLIENT::REGISTER_USER", OnServerRegisterUser);
        Events.Add("SERVER:CLIENT::FAILED_USER_LOGIN", OnServerFailedUserLogin);
        Events.Add("SERVER:CLIENT::CHOOSE_PERSON", OnServerChoosePerson);
    }

    public void OnServerFailedUserLogin(object[] args)
    {
        Main.openedWindow.ExecuteJs("document.dispatchEvent(new Event('loginUserFailed'))");
    }

    public void OnServerRegisterUser(object[] args)
    {
        bool isExist = (bool)args[0];
        if (!isExist)
        {
            Main.openedWindow.ExecuteJs("document.dispatchEvent(new Event('registerUserExists'))");
        }
        else
        {
            Main.openedWindow.ExecuteJs("document.dispatchEvent(new Event('registerUserSuccess'))");
        }
    }

    public void OnServerChoosePerson(object[] args)
    {
        Main.openedWindow.Destroy();
        Main.currentCamera = new Camera((ushort)Cam.CreateCameraWithParams(Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"), -1849.8408f, -1231.6427f, 13.7f, 0, 0, 140.14017f, 20.0f, true, 2), 0);
        Cam.PointCamAtCoord(Main.currentCamera.Id, -1851.2072f, -1233.2225f, 13.7f);
        Cam.SetCamActive(Main.currentCamera.Id, true);
        Cam.RenderScriptCams(true, false, 0, true, false, 0);

        //openedWindow = new HtmlWindow("package://cef/create_character/index.html");
        //openedWindow.Active = true;
        Cursor.ShowCursor(true, true);
    }
}