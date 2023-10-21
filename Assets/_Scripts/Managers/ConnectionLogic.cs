using UnityEngine;

public class ConnectionLogic : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask _nodesLayer;

    [Header("Instances")]
    [SerializeField] private Connection _connectionPrefab;

    private Connection _currentConnection;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Helpers.IsMouseOverUI())
            LeftClickAction();

        if (Input.GetMouseButtonDown(1))
            CancelConnection();
    }

    private void LeftClickAction()
    {
        Node nodeUnderMouse = Helpers.GetCloudUnderMouse(_nodesLayer);

        if (nodeUnderMouse == null)
            return;

        if (_currentConnection == null)
            StartConnection(nodeUnderMouse);
        else
            FinishConnection(nodeUnderMouse);
    }

    private void StartConnection(Node firstNode)
    {
        _currentConnection = Instantiate(_connectionPrefab, new Vector2(100, 100), Quaternion.identity);
        _currentConnection.AddConnection(0, firstNode);
    }

    private void FinishConnection(Node secondNode)
    {
        _currentConnection.AddConnection(1, secondNode);
        _currentConnection = null;
    }

    private void CancelConnection()
    {
        if (_currentConnection == null)
            return;

        Destroy(_currentConnection.gameObject);
        _currentConnection = null;
    }

    private void OnDisable()
    {
        CancelConnection();
    }
}
