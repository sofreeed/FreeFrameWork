using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class PlayableSample : MonoBehaviour
{
    public AnimationClip clip;
    
    /*
     * Playable.Create()：创建对应类型的对象Playable，需要传入PlayableGraph，这样graph才能持有这个新创建的Playable

       PlayableOutput.Create()：创建对应类型的Playable Output
       PlayableOutput.SetSourcePlayable()：Playable Output还需要链接Playable，如果没有链接，那这个Playable Output就没有效果
       
       PlayableGraph.Connect(): 将两个Playables连接到一起，注意一些Playables可能没有输入
       PlayableGraph.Create(): 创建一个PlayableGraph
       PlayableGraph.Play(): 播放PlayableGraph
       PlayableGraph.Evaluate() : 计算出在指定时间的状态
       PlayableGraph.Destroy(): 将PlayableGraph创建出来的所有Playables和PlayableOutputs销毁掉
     */
    PlayableGraph playableGraph;
    
    void Start()
    {
        //创建PlayableGraph
        playableGraph = PlayableGraph.Create();

        //AnimationMixerPlayable mixerPlayable = AnimationMixerPlayable.Create(playableGraph, 2);
        
        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());
        var clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);
        playableOutput.SetSourcePlayable(clipPlayable);

        
        var customPlayable = ScriptPlayable<CustomPlayableSample>.Create(playableGraph);
        customPlayable.GetBehaviour().Init();
        playableOutput.SetSourcePlayable(customPlayable);
        

        // 播放该图。
        playableGraph.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
