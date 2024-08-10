﻿using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
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
        NAPI.Util.ConsoleOutput($"{table.Rows.Count}");
        if (table.Rows.Count > 0)
        {
            NAPI.Task.Run(() =>
            {
                NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::REGISTER_USER", false);
            }, 500);
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
            int accountId = Convert.ToInt32(table.Rows[0]["id"]);
            string usersQuery = "SELECT * FROM users WHERE account_id = @id";
            MySqlCommand usersCommand = new MySqlCommand(usersQuery);
            usersCommand.Parameters.AddWithValue("@id", accountId);
            DataTable usersTable = await MySQL.QueryReadAsync(usersCommand);
            if (usersTable.Rows.Count > 0)
            {
                NAPI.Task.Run(() =>
                {
                    NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::CHOOSE_PERSON");
                    player.Position = new Vector3(-1851.2072, -1233.2225, 13.017269);
                    player.Rotation = new Vector3(0, 0, -37.09287);
                    player.Dimension = (uint)(player.Id + 1000);
                });
            }
            else
            {
                NAPI.Task.Run(() =>
                {
                    NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::CREATE_PERSON");
                    player.Position = new Vector3(-1851.2072, -1233.2225, 13.017269);
                    player.Rotation = new Vector3(0, 0, -37.09287);
                    player.Dimension = (uint)(player.Id + 1000);
                });
            }
        }
        else
        {
            NAPI.Task.Run(() =>
            {
                NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::FAILED_USER_LOGIN");
            }, 1000);
        }
    }
}