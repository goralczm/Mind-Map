using UnityEngine;

public class InputOrder : MonoBehaviour
{
    [Header("Instances")]
    [SerializeField] private GameObject _placementLogic;
    [SerializeField] private GameObject _connectionLogic;
    [SerializeField] private GameObject _selectionLogic;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _placementLogic.SetActive(true);
            _connectionLogic.SetActive(false);
            _selectionLogic.SetActive(false);
            return;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            _placementLogic.SetActive(false);
            _connectionLogic.SetActive(true);
            _selectionLogic.SetActive(false);
            return;
        }

        _placementLogic.SetActive(false);
        _connectionLogic.SetActive(false);
        _selectionLogic.SetActive(true);
        return;
    }
}
