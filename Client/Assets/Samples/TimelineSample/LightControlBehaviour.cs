using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class LightControlBehaviour : PlayableBehaviour
{
    //public Light light = null;
    public Color color = Color.white;
    public float intensity = 1f;
    
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        //如果Mixer处理了，这里就不用处理了
        //Light light = playerData as Light;
        //if (light != null)
        //{
        //    light.color = color;
        //    light.intensity = intensity;
        //}
    }
}
