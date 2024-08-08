using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Data;

public class Events : Script
{
    [ServerEvent(Event.ResourceStart)]
    public void OnResourceStart()
    {
        MySQL.Test();

        //string insertQuery = "INSERT INTO users (name) VALUES (@name)";
        //using MySqlCommand insertCommand = new MySqlCommand(insertQuery);
        //insertCommand.Parameters.AddWithValue("@name", "Zalupa_Petrovich");

        //MySQL.Query(insertCommand);

        //string query = "SELECT * FROM users";
        //using MySqlCommand command = new MySqlCommand(query);
        //DataTable data = MySQL.QueryRead(command);
        //foreach (DataRow row in data.Rows)
        //{
        //    var cells = row.ItemArray;
        //    foreach (var cell in cells)
        //    {
        //        NAPI.Util.ConsoleOutput(cell.ToString());
        //    }
        //}
    }
    [ServerEvent(Event.PlayerSpawn)]
    public void OnPlayerSpawn(Player player)
    {
        player.Position = new Vector3(45.231644, 7463.833, 6.065506);
        player.Dimension = (uint)(player.Id + 1000);
    }
}