using UnityEngine;
using UnityEngine.UI;

public static class UnityEngineUIGraphicExtension
{
    private static void Example()
    {
        var gameObject = new GameObject();
        var image = gameObject.AddComponent<Image>();
        var rawImage = gameObject.AddComponent<RawImage>();
        
        image.ColorAlpha(1.0f);
        rawImage.ColorAlpha(1.0f);
        
        image.FillAmount(0.0f); 
    }
    
    public static T ColorAlpha<T>(this T selfGraphic, float alpha) where T : Graphic
    {
        var color = selfGraphic.color;
        color.a = alpha;
        selfGraphic.color = color;
        return selfGraphic;
    }

    public static Image FillAmount(this Image selfImage, float fillAmount)
    {
        selfImage.fillAmount = fillAmount;
        return selfImage;
    }
}