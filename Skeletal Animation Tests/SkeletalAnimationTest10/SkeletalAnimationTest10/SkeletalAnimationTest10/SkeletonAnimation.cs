using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletalAnimationTest10
{
    public class SkeletonAnimation
    {
        public List<BoneAnimation> BoneAnimations;

        public SkeletonAnimation()
        {
            BoneAnimations = new List<BoneAnimation>();
        }
    }
}
