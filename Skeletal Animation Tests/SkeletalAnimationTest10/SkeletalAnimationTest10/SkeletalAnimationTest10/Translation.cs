using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletalAnimationTest10
{
    public struct Translation
    {
        public Vector2 Position;
        public float Rotation;
        public static Translation Identity;

        static Translation()
        {
            //Identity.Position = Vector2.Zero;
            Identity.Rotation = 0f;
        }

        public static Translation CalculateTransformation(Translation parent, Translation child, Vector2 childOff)
        {
            Translation result;
            result.Rotation = parent.Rotation + child.Rotation;
            Vector2 t = child.Position + childOff;
            Vector2 temp = RotatePoint(parent.Position, t, parent.Rotation);
            result.Position = temp;            

            return result;
        }

        public static Vector2 RotatePoint(Vector2 origin, Vector2 point, float rotation)
        {
            Vector2 result;
            result = Vector2.Transform(point, Matrix.CreateRotationZ(rotation));
            return result+origin;
        }

        public Translation TransformPosition(Vector2 offset)
        {
            Translation result = this;
            result.Position += offset;
            return result;
        }
    }
}
