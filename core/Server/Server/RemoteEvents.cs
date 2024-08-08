using GTANetworkAPI;

public class RemoteEvents : Script
{
    [RemoteEvent("CLIENT:SERVER::CLIENT_CREATE_WAYPOINT")]
    public void OnClientCreateWaypoint(Player player, float posX, float posY, float posZ)
    {
        player.Position = new Vector3(posX, posY, posZ);
    }
}