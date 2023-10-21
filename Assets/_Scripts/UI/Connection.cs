using UnityEngine;

public class Connection : MonoBehaviour, IDestroyeable, ISelectable
{
    [Header("Connections")]
    public Node[] connectedClouds;

    [Header("Instances")]
    [SerializeField] private LineRenderer _line;

    private void Update()
    {
        if (connectedClouds[0] != null)
            _line.SetPosition(0, connectedClouds[0].transform.position);
        if (connectedClouds[1] != null)
            _line.SetPosition(1, connectedClouds[1].transform.position);
        else
            _line.SetPosition(1, InputManager.MouseWorldPos);
    }

    public void AddConnection(int index, Node cloud)
    {
        connectedClouds[index] = cloud;
        cloud.connections.Add(this);
    }

    public void Select()
    {

    }

    public void Deselect()
    {

    }

    public void DestroyObject()
    {
        connectedClouds[0]?.connections.Remove(this);
        connectedClouds[1]?.connections.Remove(this);

        Destroy(gameObject);
    }
}
