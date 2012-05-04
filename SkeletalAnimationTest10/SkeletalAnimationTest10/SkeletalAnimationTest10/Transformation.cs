using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletalAnimationTest10
{
    public struct Transformation
    {
        public Vector2 Position;
        public float Rotation;
        public Vector2 Scale;

        public static Transformation Default;

        static Transformation()
        {
            Default = new Transformation();
            Default.Scale = Vector2.One;
        }

        public static Transformation Copy(Transformation trans)
        {
            Transformation result = trans;
            return result;
        }

        public static Transformation Compose(Transformation trans1, Transformation trans2)
        {
            Transformation result;
            //Vector2 tempPos = Vector2.Transform(trans2.Position, Matrix.CreateRotationZ(trans1.Rotation));
            //tempPos = Vector2.Add(tempPos, trans1.Position);
            Vector2 tempPos = trans1.RotatePosition(trans2.Position);
            //tempPos = Vector2.Add(tempPos, trans1.Position);
            //result.Position = Vector2.Transform(trans2.Position, Matrix.CreateRotationZ(trans1.Rotation));
            //result.Position += trans1.Position;
            result.Position = tempPos;
            result.Rotation = trans1.Rotation + trans2.Rotation;
            result.Scale = trans1.Scale * trans2.Scale;
            return result;
        }

        public Vector2 RotatePosition(Vector2 position)
        {
            Vector2 result;

            result = Vector2.Transform(position, Matrix.CreateScale(new Vector3(Scale, 0)));
            result = Vector2.Transform(result, 
                Matrix.CreateRotationZ(this.Rotation));
            
            result = Vector2.Transform(result, Matrix.CreateTranslation(new Vector3(Position,0)));
            //result *= Scale;
            return result;
        }

        public Transformation Translate(Vector2 point)
        {
            Transformation result;
            result.Rotation = this.Rotation;
            result.Scale = this.Scale;
            result.Position=Vector2.Transform(this.Position,
                Matrix.CreateTranslation(new Vector3(point,0)));            
            return result;
        }

        public static Transformation Lerp(Transformation trans1, Transformation trans2, float amount)
        {
            Transformation result;

            //result.Position = Vector2.Lerp(trans1.Position, trans2.Position, 0.5f);
            //result.Scale = MathHelper.Lerp(trans1.Scale, trans2.Scale, 0.5f);
            //result.Rotation = MathHelper.Lerp(trans1.Rotation, trans2.Rotation, 0.5f);

            result.Position = Vector2.Lerp(trans1.Position, trans2.Position, amount);
            result.Scale = Vector2.Lerp(trans1.Scale, trans2.Scale, amount);
            result.Rotation = MathHelper.Lerp(trans1.Rotation, trans2.Rotation, amount);

            return result;
        }
    }
}
