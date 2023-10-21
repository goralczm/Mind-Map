using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    [SerializeField] private float _minZoom, _maxZoom;
    
    private Camera _camera;
    
    void Awake()
    {
        _camera = Camera.main;
        _maxZoom = 10f;
    }

    void Update()
    {
        float newCameraSize = _camera.orthographicSize - InputManager.ScrollWheel * 10f;
        newCameraSize = Mathf.Clamp(newCameraSize, _minZoom, _maxZoom);
        _camera.orthographicSize = newCameraSize;
    }
}
