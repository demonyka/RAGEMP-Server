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
    }
    [ServerEvent(Event.PlayerSpawn)]
    public void OnPlayerSpawn(Player player)
    {
        player.Position = new Vector3(-1851.4647, -1233.4248, 13.016661);
        player.Rotation = new Vector3(0, 0, 138.50723);
        player.Dimension = (uint)(player.Id + 1000);
    }
}