using UnityEngine;

[System.Serializable]
public class NodeData
{
    public int id;
    public string name;
    public float[] rgb;
    public float[] position;

    public NodeData(Node node)
    {
        id = node.id;
        name = node.GetText();

        Color color = node.GetColor();
        rgb = new float[3];
        rgb[0] = color.r;
        rgb[1] = color.g;
        rgb[2] = color.b;

        position = new float[2];
        position[0] = node.transform.position.x;
        position[1] = node.transform.position.y;
    }
}
