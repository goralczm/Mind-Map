using UnityEngine;

public class Connection : MonoBehaviour, IDestroyeable, ISelectable
{
    [Header("Connections")]
    public Node[] connectedNodes;

    [Header("Instances")]
    [SerializeField] private LineRenderer _line;

    private void Update()
    {
        if (connectedNodes[0] != null)
            _line.SetPosition(0, connectedNodes[0].transform.position);
        if (connectedNodes[1] != null)
            _line.SetPosition(1, connectedNodes[1].transform.position);
        else
            _line.SetPosition(1, InputManager.MouseWorldPos);
    }

    public void AddConnection(int index, Node node)
    {
        connectedNodes[index] = node;
        node.connections.Add(this);
    }

    public void Select()
    {

    }

    public void Deselect()
    {

    }

    public void DestroyObject()
    {
        connectedNodes[0]?.connections.Remove(this);
        connectedNodes[1]?.connections.Remove(this);

        ConnectionLogic.Instance.connections.Remove(this);
        Destroy(gameObject);
    }
}
