[System.Serializable]
public class ConnectionData
{
    public int firstNodeId;
    public int secondNodeId;

    public ConnectionData(Connection connection)
    {
        firstNodeId = connection.connectedNodes[0].id;
        secondNodeId = connection.connectedNodes[1].id;
    }
}
