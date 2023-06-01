using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class PlayableSample : MonoBehaviour
{
    public AnimationClip clip;
    
    PlayableGraph playableGraph;
    
    void Start()
    {
        playableGraph = PlayableGraph.Create();
        playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
        
        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());
        
        // 将剪辑包裹在可播放项中
        var clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);

        // 将可播放项连接到输出
        playableOutput.SetSourcePlayable(clipPlayable);

        // 播放该图。
        playableGraph.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
