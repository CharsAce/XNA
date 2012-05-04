using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletalAnimationTest10
{
    public struct Joint
    {        
        public Vector2 Offset;
        public Vector2 TextureOrigin;
        public float Length;
        public float Scale;
        public string TextureName;
        public int ParentId;
        public BoneFlag Flag;
        //public Joint Parent
        //{
        //    public get;
        //    public set;
        //}
    }
}
