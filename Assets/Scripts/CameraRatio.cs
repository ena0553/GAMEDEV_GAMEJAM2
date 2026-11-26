using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Camera))]
public class LetterboxCamera : MonoBehaviour
{
    public float targetAspect = 16f / 9f;
    int lastW, lastH;

    void Update()
    {
        if (Screen.width == lastW && Screen.height == lastH) return;
        lastW = Screen.width; lastH = Screen.height;

        float windowAspect = (float)Screen.width / Screen.height;
        float scale = windowAspect / targetAspect;
        Camera cam = GetComponent<Camera>();

        if (scale < 1f)
            cam.rect = new Rect(0, (1f - scale) / 2f, 1f, scale);
        else
        {
            float w = 1f / scale;
            cam.rect = new Rect((1f - w) / 2f, 0, w, 1f);
        }
    }
}
