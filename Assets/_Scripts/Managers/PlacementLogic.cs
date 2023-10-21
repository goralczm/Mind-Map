using System.Collections.Generic;
using UnityEngine;

public class PlacementLogic : Singleton<PlacementLogic>
{
    public List<Node> nodes = new List<Node>();
    public int nextId;

    [Header("Instances")]
    [SerializeField] private Node _nodePrefab;

    private void Update()
    {
        if (Helpers.IsMouseOverUI())
            return;

        if (Input.GetMouseButtonDown(0))
            LeftClickAction();
    }

    private void LeftClickAction()
    {
        Node newNode = CreateNode();
        newNode.id = nextId;
        nextId++;
    }

    public Node CreateNode()
    {
        Node newNode = Instantiate(_nodePrefab, InputManager.MouseWorldPos, Quaternion.identity);
        newNode.name = $"New Node";
        nodes.Add(newNode);

        return newNode;
    }
}
