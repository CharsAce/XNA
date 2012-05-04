using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletalAnimationTest10
{
    public class BoneAnimation
    {
        public List<Keyframe> Keyframes;

        public int FrameLength;
        public int FPS;

        public BoneAnimation()
        {
            Keyframes = new List<Keyframe>();
        }
    }
}
