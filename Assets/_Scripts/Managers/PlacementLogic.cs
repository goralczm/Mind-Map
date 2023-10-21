using UnityEngine;

public class PlacementLogic : MonoBehaviour
{
    [Header("Instances")]
    [SerializeField] private Node _nodePrefab;

    private int _nodesCount;

    private void Update()
    {
        if (Helpers.IsMouseOverUI())
            return;

        if (Input.GetMouseButtonDown(0))
            LeftClickAction();
    }

    private void LeftClickAction()
    {
        CreateNode();
    }

    public void CreateNode()
    {
        Node newNode = Instantiate(_nodePrefab, InputManager.MouseWorldPos, Quaternion.identity);
        newNode.name = $"Node{_nodesCount}";
        _nodesCount++;
    }
}
