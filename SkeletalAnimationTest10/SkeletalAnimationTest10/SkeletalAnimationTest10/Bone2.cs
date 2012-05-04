using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletalAnimationTest10
{
    //public enum BoneFlag
    //{
    //    BoneAbsolute,
    //    Child,
    //    Off
    //}

    public class Bone2
    {
        public Vector2 BoneStart;
        public Vector2 BoneEnd;
        public Vector2 TransformedPoint;
        public float Length;
        public float Rotation;
        public int ParentId;
        public string TextureName;
        public Vector2 TextureOrigin;

        public BoneFlag Flag;
    }
}
