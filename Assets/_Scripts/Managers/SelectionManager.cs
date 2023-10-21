using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public List<Transform> selections = new List<Transform>();

    [Header("Settings")]
    [SerializeField] private LayerMask _selectableMask;

    [Header("Instances")]
    [SerializeField] private Transform _selection;

    private Vector2 _startSelectionPos;
    private Vector2 _endSelectionPos;
    private Vector2 _lastMousePos;
    private bool _isDragging;
    private bool _isSelecting;

    private void Update()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Node cloudUnderMouse = Helpers.GetCloudUnderMouse(_selectableMask);
                if (cloudUnderMouse == null)
                    StartSelection();
                else
                {
                    if (!selections.Contains(cloudUnderMouse.transform))
                    {
                        ResetSelections();
                        AddSelection(cloudUnderMouse.transform);
                    }

                    StartDrag();
                }
            }
        }
        else
            _isDragging = false;

        if (Input.GetMouseButtonUp(0))
            _isDragging = false;

        if (Input.GetMouseButtonDown(1))
            ResetSelections();

        if (_isDragging)
            HandleMovement();

        if (_isSelecting)
            HandleSelection();
    }

    private void HandleSelection()
    {
        if (Input.GetMouseButton(0))
        {
            _endSelectionPos = InputManager.MouseWorldPos;
            _selection.position = GetRectangleOrigin();
            _selection.localScale = GetRectangleSize();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Collider2D[] hits = GetSelectionFromRectangle();
            ResetSelections();
            foreach (Collider2D hit in hits)
                AddSelection(hit.transform);
            _selection.gameObject.SetActive(false);
            _isSelecting = false;
        }
    }

    private void StartSelection()
    {
        _startSelectionPos = InputManager.MouseWorldPos;
        _selection.gameObject.SetActive(true);
        _isSelecting = true;
    }

    private void StartDrag()
    {
        _lastMousePos = InputManager.MouseWorldPos;
        _isDragging = true;
    }

    private void HandleMovement()
    {
        foreach (Transform selectable in selections)
        {
            Vector2 diff = InputManager.MouseWorldPos - _lastMousePos;
            selectable.position = (Vector2)selectable.position + diff;
        }
        _lastMousePos = InputManager.MouseWorldPos;
    }

    private void ResetSelections()
    {
        foreach (Transform selection in selections)
        {
            selection.GetComponent<ISelectable>().Deselect();
        }
        selections.Clear();
        _isDragging = false;
    }

    private void AddSelection(Transform selection)
    {
        ISelectable newSelection = selection.GetComponent<ISelectable>();
        selections.Add(selection);
        newSelection.Select();
    }

    private Collider2D[] GetSelectionFromRectangle()
    {
        Vector2 origin = GetRectangleOrigin();
        Vector2 size = GetRectangleSize();

        return Physics2D.OverlapBoxAll(origin, size, _selectableMask);
    }

    private Vector2 GetRectangleOrigin()
    {
        return (_startSelectionPos + _endSelectionPos) / 2f;
    }

    private Vector2 GetRectangleSize()
    {
        return Vector2Abs(_endSelectionPos - _startSelectionPos);
    }

    private Vector2 Vector2Abs(Vector2 v)
    {
        return new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));
    }
}
