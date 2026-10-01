using UnityEngine;

public class CameraRatioSetter : MonoBehaviour
{
    private readonly float targetAspect = 16f / 9f;

    void Update()
    {
        SetAspectRatio();
    }

    private void SetAspectRatio()
    {
        Camera.main.aspect = targetAspect;

        float currentAspect = (float)Screen.width / Screen.height;

        if (currentAspect > targetAspect)
        {
            float scaledWidth = targetAspect / currentAspect;
            float x = (1f - scaledWidth) / 2f;
            Camera.main.rect = new Rect(x, 0, scaledWidth, 1f);
        }
        else
        {
            float scaledHeight = currentAspect / targetAspect;
            float y = (1f - scaledHeight) / 2f;
            Camera.main.rect = new Rect(0, y, 1f, scaledHeight);
        }
    }
}
