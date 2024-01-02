using UnityEngine;

public static class UnityEngineCameraExtension
{
    private static void Example()
    {
        Camera camera = new Camera();
        Texture2D tx2D = camera.CaptureCamera(new Rect(0, 0, 100, 100));
    }

    public static Texture2D CaptureCamera(this Camera camera, Rect rect)
    {
        var renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
        camera.targetTexture = renderTexture;
        camera.Render();

        RenderTexture.active = renderTexture;

        var screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();

        camera.targetTexture = null;
        RenderTexture.active = null;
        UnityEngine.Object.Destroy(renderTexture);

        return screenShot;
    }
}