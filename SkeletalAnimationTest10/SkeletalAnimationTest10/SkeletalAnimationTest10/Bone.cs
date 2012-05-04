using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletalAnimationTest10
{
    public enum BoneFlag
    {
        BoneAbsolute,
        Child,
        Off
    }
    public class Bone
    {
        public string Name;
        public Vector2 boneStart;
        //public Vector2 boneEnd;
        //public Vector2 Offset;
        public float X;
        public float Y;
        public float Angle;
        public int Length;
        public BoneFlag Flag;
        public int ChildCount;
        public Bone Parent;
        public Bone[] ChildBones= new Bone[10];
        public bool Init;
    }
}
