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
        string selectQuery = "SELECT * FROM users WHERE login = @login";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        selectCommand.Parameters.AddWithValue("@login", login);

        DataTable table = await MySQL.QueryReadAsync(selectCommand);
        if (table.Rows.Count > 0)
        {
            NAPI.Util.ConsoleOutput("Логин занят");
        }
        else
        {
            string insertQuery = "INSERT INTO users (login, password, email) VALUES (@login, @password, @email)";
            MySqlCommand insertCommand = new MySqlCommand(insertQuery);
            insertCommand.Parameters.AddWithValue("@login", login);
            insertCommand.Parameters.AddWithValue("@password", password);
            insertCommand.Parameters.AddWithValue("@email", email);
            MySQL.Query(insertCommand);
        }
    }
}