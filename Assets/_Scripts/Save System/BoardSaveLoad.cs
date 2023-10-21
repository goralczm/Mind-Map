using UnityEngine;

public class BoardSaveLoad : MonoBehaviour
{
    [SerializeField] private ConnectionLogic _connectionLogic;
    [SerializeField] private PlacementLogic _placementLogic;

    public void Save()
    {
        BoardData data = new BoardData();
        data.nextId = _placementLogic.nextId;
        data.connections = new ConnectionData[_connectionLogic.connections.Count];
        for (int i = 0; i < _connectionLogic.connections.Count; i++)
            data.connections[i] = new ConnectionData(_connectionLogic.connections[i]);
        data.nodes = new NodeData[_placementLogic.nodes.Count];
        for (int i = 0; i < _placementLogic.nodes.Count; i++)
            data.nodes[i] = new NodeData(_placementLogic.nodes[i]);

        SaveSystem.SaveData(data, "Board");
    }

    public void Load()
    {
        BoardData data = SaveSystem.LoadData("Board") as BoardData;
        _placementLogic.nextId = data.nextId;
        foreach (NodeData nodeData in data.nodes)
        {
            Node newNode = _placementLogic.CreateNode();
            newNode.id = nodeData.id;
            newNode.SetText(nodeData.name);
            newNode.SetColor(nodeData.rgb[0], nodeData.rgb[1], nodeData.rgb[2]);
            Vector2 pos = new Vector2(nodeData.position[0], nodeData.position[1]);
            newNode.transform.position = pos;
        }
        foreach (ConnectionData connectionData in data.connections)
        {
            Node firstNode = null;
            Node secondNode = null;

            foreach (Node node in _placementLogic.nodes)
            {
                if (node.id == connectionData.firstNodeId)
                    firstNode = node;

                if (node.id == connectionData.secondNodeId)
                    secondNode = node;
            }

            _connectionLogic.StartConnection(firstNode);
            _connectionLogic.FinishConnection(secondNode);
        }
    }

    public void ResetLevel()
    {
        Helpers.RestartLevel();
    }
}
