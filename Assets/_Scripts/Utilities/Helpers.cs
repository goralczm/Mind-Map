using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Set of functions accessible from any class
/// </summary>
public static class Helpers
{
    public static void DestroyChildren(this Transform t)
    {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }

    public static Node GetCloudUnderMouse(LayerMask cloudLayers)
    {
        RaycastHit2D hit = Physics2D.Raycast(InputManager.MouseWorldPos, Vector2.zero, cloudLayers);
        if (hit.collider == null)
            return null;

        return hit.transform.GetComponent<Node>();
    }

    public static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}