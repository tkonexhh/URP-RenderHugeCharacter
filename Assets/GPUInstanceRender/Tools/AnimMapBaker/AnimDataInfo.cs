using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    [System.Serializable]
    public class AnimDataInfo
    {
        public int maxHeight;
        public List<AnimMapClip> animMapClips = new List<AnimMapClip>();
    }

    [System.Serializable]
    public struct AnimMapClip
    {
        //起始高度
        public int startHeight;
        public int height;
        public string name;
        // public float perFrameTime;
        public float animLen;
    }

}