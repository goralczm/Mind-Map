using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NodeEditor : Singleton<NodeEditor>
{
    [Header("Instances")]
    [SerializeField] private UITweener _editorWindow;
    [SerializeField] private TMP_InputField _editorText;
    [SerializeField] private Slider _red;
    [SerializeField] private Slider _green;
    [SerializeField] private Slider _blue;

    private Node _currentlyEditingNode;

    private void Update()
    {
        if (!_editorWindow.gameObject.activeSelf)
            return;

        if (_editorWindow.isBusy)
            return;

        if (_currentlyEditingNode == null)
            return;

        _currentlyEditingNode.SetText(_editorText.text);
        _currentlyEditingNode.SetColor(_red.value, _green.value, _blue.value);
    }

    public void EditNode(Node node)
    {
        if (_currentlyEditingNode == node)
        {
            _editorWindow.Hide();
            _currentlyEditingNode = null;
            return;
        }

        _currentlyEditingNode = node;
        _editorText.SetTextWithoutNotify(_currentlyEditingNode.GetText());
        Color nodeColor = _currentlyEditingNode.GetColor();
        _red.value = nodeColor.r;
        _green.value = nodeColor.g;
        _blue.value = nodeColor.b;
        _editorWindow.Show();
    }
}
