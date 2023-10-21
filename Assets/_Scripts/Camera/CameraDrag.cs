using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    private Vector2 _startDragPos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            _startDragPos = InputManager.MouseWorldPos;
            return;
        }

        if (!Input.GetMouseButton(2))
            return;

        Vector2 offset = InputManager.MouseWorldPos - (Vector2)transform.position;
        Vector2 dir = _startDragPos - offset;
        Vector3 newPos = new Vector3(dir.x, dir.y, -10);

        transform.position = newPos;
    }
}
