using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _doubleClickThreshold;
    [SerializeField] private LayerMask _cloudsLayer;

    [Header("Instances")]
    [SerializeField] private Node _cloudPrefab;
    [SerializeField] private Connection _connectionPrefab;

    private Connection _currentConnection;
    private int _cloudsCount;
    private float _lastTimeClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            LeftClickAction();

        if (Input.GetMouseButtonDown(1))
            RightClickAction();
    }

    private void LeftClickAction()
    {
        Node cloudUnderMouse = Helpers.GetCloudUnderMouse(_cloudsLayer);

        if (cloudUnderMouse == null)
        {
            if (IsDoubleClick && _currentConnection == null)
            {
                CreateCloud();
                return;
            }
            return;
        }

        if (_currentConnection == null)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                StartConnection(cloudUnderMouse);
        }
        else
            FinishConnection(cloudUnderMouse);
    }

    private void StartConnection(Node firstCloud)
    {
        _currentConnection = Instantiate(_connectionPrefab, new Vector2(100, 100), Quaternion.identity);
        _currentConnection.AddConnection(0, firstCloud);
    }

    private void FinishConnection(Node secondCloud)
    {
        _currentConnection.AddConnection(1, secondCloud);
        _currentConnection = null;
    }

    private bool IsDoubleClick {
        get
        {
            bool result = Time.time - _lastTimeClicked <= _doubleClickThreshold;
            _lastTimeClicked = Time.time;

            return result;
        }
    }

    private void CreateCloud()
    {
        Node newCloud = Instantiate(_cloudPrefab, InputManager.MouseWorldPos, Quaternion.identity);
        newCloud.name = $"Cloud{_cloudsCount}";
        _cloudsCount++;
    }

    private void RightClickAction()
    {
        CancelConnection();
    }

    private void CancelConnection()
    {
        if (_currentConnection == null)
            return;

        Destroy(_currentConnection.gameObject);
        _currentConnection = null;
    }
}
