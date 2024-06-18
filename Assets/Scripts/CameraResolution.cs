using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    public int targetWidth = 1920;
    public int targetHeight = 1080;

    void Start()
    {
        Debug.Log("CameraResolution Start called.");
        SetCameraResolution();
    }

    void SetCameraResolution()
    {
        Camera camera = GetComponent<Camera>();
        if (camera == null)
        {
            Debug.LogError("Camera component is missing from the GameObject. Please attach a Camera component.");
            return;
        }

        float targetAspect = (float)targetWidth / targetHeight;
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            camera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = camera.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            camera.rect = rect;
        }

        Debug.Log("Camera resolution set to: " + Screen.width + "x" + Screen.height);
    }
}
