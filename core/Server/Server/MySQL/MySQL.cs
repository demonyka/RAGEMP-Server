using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

public class MySQL
{
    private static readonly string connStr = "server=localhost;user=root;database=rageserver;password=;Pooling=true;";

    public static void Test()
    {
        using MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();
            conn.Close();
            NAPI.Util.ConsoleOutput("Успешное подключение к БД");
        }
        catch (Exception e)
        {
            NAPI.Util.ConsoleOutput(e.ToString());
        }
    }

    public static void Query(MySqlCommand command)
    {
        if(command == null || command.CommandText.Length < 1)
        {
            NAPI.Util.ConsoleOutput("Wrong command argument: null or empty.");
            return;
        }
        using MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();
            command.Connection = conn;
            command.ExecuteNonQuery();
            conn.Close();
        }
        catch (Exception e)
        {
            NAPI.Util.ConsoleOutput(e.ToString());
        }
    }

    public static DataTable QueryRead(MySqlCommand command)
    {
        if (command == null || command.CommandText.Length < 1)
        {
            NAPI.Util.ConsoleOutput("Wrong command argument: null or empty.");
            return null;
        }
        using MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();
            command.Connection = conn;
            using MySqlDataReader reader = command.ExecuteReader();
            using DataTable table = new DataTable();
            table.Load(reader);
            return table;
        }
        catch (Exception e)
        {
            NAPI.Util.ConsoleOutput(e.ToString());
            return null;
        }
    }

    public static async Task<DataTable> QueryReadAsync(MySqlCommand command)
    {
        if (command == null || command.CommandText.Length < 1)
        {
            NAPI.Util.ConsoleOutput("Wrong command argument: null or empty.");
            return null;
        }
        using MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            await conn.OpenAsync();
            command.Connection = conn;
            using DbDataReader reader = await command.ExecuteReaderAsync();
            using DataTable table = new DataTable();
            table.Load(reader);
            return table;
        }
        catch (Exception e)
        {
            NAPI.Util.ConsoleOutput(e.ToString());
            return null;
        }
    }
}