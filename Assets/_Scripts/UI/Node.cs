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

    private void Awake()
    {
        _editor = NodeEditor.Instance;
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
        _rend.color = Color.grey;
    }

    public void Deselect()
    {
        _rend.color = Color.white;
    }

    public void DestroyObject()
    {
        for (int i = connections.Count - 1; i >= 0; i--)
            connections[i].DestroyObject();

        Destroy(gameObject);
    }
}
