using UnityEngine;

public class CanvasFinder : MonoBehaviour
{
    void Start()
    {
        GameObject cameraCanvas = GameObject.FindGameObjectWithTag("cameraCanvas");
        
        if (cameraCanvas != null)
        {
            transform.SetParent(cameraCanvas.transform, false);
        }
        else
        {
            Debug.LogError("Canvas with tag 'cameraCanvas' not found!");
        }
    }
}
