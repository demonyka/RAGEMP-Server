using GTANetworkAPI;

public class Commands : Script
{
    [Command("getpos")]
    public void CMD_GetPos(Player player)
    {
        Vector3 playerPosition = player.Position;
        Vector3 playerRotation = player.Rotation;
        NAPI.Util.ConsoleOutput($"Position - {playerPosition.X}, {playerPosition.Y}, {playerPosition.Z}");
        NAPI.Util.ConsoleOutput($"Rotation - {playerRotation.X}, {playerRotation.Y}, {playerRotation.Z}");
    }
}