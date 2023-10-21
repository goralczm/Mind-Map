using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Node : MonoBehaviour, IDestroyeable, ISelectable
{
    [Header("Connections")]
    public List<Connection> connections = new List<Connection>();

    [Header("Instances")]
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private SpriteRenderer _rend;

    private NodeEditor _editor;
    private Vector3 _startScale;

    private void Awake()
    {
        _editor = NodeEditor.Instance;
        _startScale = transform.localScale;
    }

    public void SetColor(float red, float green, float blue)
    {
        Color newColor = new Color(red, green, blue);
        _rend.color = newColor;
    }

    public Color GetColor()
    {
        return _rend.color;
    }

    public void SetText(string newText)
    {
        _text.SetText(newText);
    }

    public string GetText()
    {
        return _text.text;
    }

    private void OnMouseOver()
    {
        if (!Input.GetMouseButtonDown(1))
            return;

        _editor.EditNode(this);
    }

    public void Select()
    {
        transform.localScale = _startScale * 1.1f;
    }

    public void Deselect()
    {
        transform.localScale = _startScale;

    }

    public void DestroyObject()
    {
        for (int i = connections.Count - 1; i >= 0; i--)
            connections[i].DestroyObject();

        Destroy(gameObject);
    }
}
