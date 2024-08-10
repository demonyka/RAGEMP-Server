using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System.Data;

public class RemoteEvents : Script
{
    [RemoteEvent("CLIENT:SERVER::CLIENT_CREATE_WAYPOINT")]
    public void OnClientCreateWaypoint(Player player, float posX, float posY, float posZ)
    {
        player.Position = new Vector3(posX, posY, posZ);
    }

    [RemoteEvent("CLIENT:SERVER::REGISTER_BUTTON_CLICKED")]
    public async void OnCefRegisterButtonClicked(Player player, string login, string password, string email) 
    {
        string selectQuery = "SELECT * FROM accounts WHERE login = @login OR email = @email";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        selectCommand.Parameters.AddWithValue("@login", login);
        selectCommand.Parameters.AddWithValue("@email", email);

        DataTable table = await MySQL.QueryReadAsync(selectCommand);
        if (table.Rows.Count > 0)
        {
            NAPI.Task.Run(() =>
            {
                NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::REGISTER_USER", false);
            }, 1000);
        }
        else
        {
            string insertQuery = "INSERT INTO accounts (login, password, email) VALUES (@login, @password, @email)";
            MySqlCommand insertCommand = new MySqlCommand(insertQuery);
            insertCommand.Parameters.AddWithValue("@login", login);
            insertCommand.Parameters.AddWithValue("@password", Crypto.Hash(password));
            insertCommand.Parameters.AddWithValue("@email", email);
            MySQL.Query(insertCommand);
            NAPI.Task.Run(() =>
            {
                NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::REGISTER_USER", true);
            });
        }
    }

    [RemoteEvent("CLIENT:SERVER::LOGIN_BUTTON_CLICKED")]
    public async void OnCefLoginButtonClicked(Player player, string login, string password)
    {
        string selectQuery = "SELECT * FROM accounts WHERE login = @login AND password = @password";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        selectCommand.Parameters.AddWithValue("@login", login);
        selectCommand.Parameters.AddWithValue("@password", Crypto.Hash(password));
        DataTable table = await MySQL.QueryReadAsync(selectCommand);
        if (table.Rows.Count > 0)
        {
            NAPI.Task.Run(() =>
            {
                NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::LOGIN_USER", true);
            });
        }
        else
        {
            NAPI.Task.Run(() =>
            {
                NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::LOGIN_USER", false);
            }, 1000);
        }
        
    }
}