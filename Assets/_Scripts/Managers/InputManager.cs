using UnityEngine;

public static class InputManager
{
    public static float ScrollWheel => Input.GetAxis("Mouse ScrollWheel");
    public static Vector2 MouseWorldPos
    {
        get
        {
            if (_cam == null)
                _cam = Camera.main;

            return _cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private static Camera _cam;
}
