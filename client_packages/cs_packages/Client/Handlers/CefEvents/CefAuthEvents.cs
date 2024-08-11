using RAGE;

public class CefAuthEvents : Events.Script
{
    public CefAuthEvents()
    {
        Events.Add("CEF:CLIENT::REGISTER_BUTTON_CLICKED", OnCefRegisterButtonClicked);
        Events.Add("CEF:CLIENT::LOGIN_BUTTON_CLICKED", OnCefLoginButtonClicked);
        Events.Add("CEF:CLIENT::PERSON_CREATE_BUTTON_CLICKED", OnCefPersonCreateButtonClicked);
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

    public void OnCefPersonCreateButtonClicked(object[] args) 
    {
        string name = args[0].ToString();
        string age = args[1].ToString();
        string sex = args[2].ToString();

        Events.CallRemote("CLIENT:SERVER:PERSON_CREATE_BUTTON_CLICKED", name, age, sex);
    }
}