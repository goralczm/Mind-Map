using UnityEngine;

public class CloudsManager : Singleton<CloudsManager>
{
    public Node CurrentlyFocusedCloud { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
            DeleteFocusedCloud();
    }

    public void FocusOnCloud(Node cloud)
    {
        if (CurrentlyFocusedCloud == cloud)
        {
            CurrentlyFocusedCloud.Deselect();
            CurrentlyFocusedCloud = null;
            return;
        }

        if (CurrentlyFocusedCloud != null)
            CurrentlyFocusedCloud.Deselect();

        CurrentlyFocusedCloud = cloud;
        CurrentlyFocusedCloud.Select();
    }

    private void DeleteFocusedCloud()
    {
        if (CurrentlyFocusedCloud == null)
            return;

        CurrentlyFocusedCloud.DestroyObject();
        CurrentlyFocusedCloud = null;
    }
}
