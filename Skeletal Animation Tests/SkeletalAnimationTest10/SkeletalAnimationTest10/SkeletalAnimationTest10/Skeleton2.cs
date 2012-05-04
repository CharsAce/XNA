using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SkeletalAnimationTest10
{
    public class Skeleton2
    {
        Bone2[] bones = new Bone2[2];
        BasicEffect be;
        DebugLine[] l;
        Texture2D[] textures;

        public Skeleton2(BasicEffect be)
        {
            bones[0] = new Bone2();
            bones[1] = new Bone2();

            //bones[0].BoneStart = new Vector2(400, 400);
            //bones[0].ParentId = -1;
            //bones[0].Length = 60.90156f;
            //bones[0].TextureName = "machine_arm";
            //bones[0].Rotation = 0f;
            //bones[0].TextureOrigin = new Vector2(10, 43);

            //bones[1].BoneStart = new Vector2(0, -32);
            //bones[1].ParentId = 0;
            //bones[1].Length = 48;
            //bones[1].TextureName = "machine_forearm2";
            //bones[1].Rotation = 0f;
            //bones[1].TextureOrigin = new Vector2(25, 24);
            
            bones[0].BoneStart = new Vector2(400, 400);
            bones[0].ParentId = -1;
            bones[0].Length = 60.90156f;
            bones[0].TextureName = "1";
            bones[0].Rotation = 0f;
            bones[0].TextureOrigin = new Vector2(10, 43);
            bones[0].Flag = BoneFlag.BoneAbsolute;

            bones[1].BoneStart = new Vector2(0, -32);
            bones[1].ParentId = 0;
            bones[1].Length = 48;
            bones[1].TextureName = "1";
            bones[1].Rotation = 0f;
            bones[1].TextureOrigin = new Vector2(25, 24);
            bones[0].Flag = BoneFlag.Child;

            this.be = be;

            l = new DebugLine[bones.Length];
            textures = new Texture2D[bones.Length];
            //for (int i = 0; i < l.Length; i++)
            //{
            //    l[i] = new DebugLine(be, bones[i].BoneStart.X, 
            //        bones[i].BoneStart.Y, bones[i].Length);
            //}
        }

        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < bones.Length; i++)
            {
                
                textures[i] = content.Load<Texture2D>(bones[i].TextureName);
            }
        }

        public void TransformBone()
        {
            
            bones[0].TransformedPoint = bones[0].BoneStart;
            //bones[0].TransformedPoint.Normalize();
            for (int i = 0; i < bones.Length; i++)
            {
                if (bones[i].ParentId>-1)
                {
                    //Vector2 origin = new Vector2(bones[bones[i].ParentId].BoneStart.X + bones[bones[i].ParentId].Length,
                    //    bones[bones[i].ParentId].BoneStart.Y);
                    //Vector2 origin = new Vector2(bones[bones[i].ParentId].BoneStart.X,
                    //    bones[bones[i].ParentId].BoneStart.Y);

                    Vector2 origin = Vector2.Transform(bones[bones[i].ParentId].BoneStart,
                        Matrix.CreateTranslation(new Vector3(bones[bones[i].ParentId].Length, 0, 0)));


                    //bones[i].TransformedPoint = new Vector2(bones[bones[i].ParentId].BoneStart.X + bones[i].BoneStart.X
                    //     , bones[bones[i].ParentId].BoneStart.Y + bones[i].BoneStart.Y);

                    bones[i].TransformedPoint += bones[i].BoneStart;
                    //bones[i].TransformedPoint = Vector2.Transform(bones[i].TransformedPoint,
                    //    Matrix.CreateRotationZ(MathHelper.PiOver2));

                    bones[i].TransformedPoint = Vector2.Transform(bones[i].TransformedPoint,
                        Matrix.CreateRotationZ(bones[bones[i].ParentId].Rotation));

                    //bones[i].TransformedPoint = Vector2.Add(bones[i].BoneStart, origin);

                    bones[i].TransformedPoint = Vector2.Add(bones[i].TransformedPoint, origin);
                    bones[i].Rotation = bones[bones[i].ParentId].Rotation + bones[i].Rotation;

                    //bones[i].TransformedPoint = Vector2.Transform(bones[i].BoneStart, Matrix.CreateTranslation(new Vector3(
                    //    origin.X, origin.Y, 0)));

                    //bones[i].TransformedPoint = Vector2.Transform(bones[i].TransformedPoint,
                    //    Matrix.CreateTranslation(new Vector3(
                    //    -origin.X, -origin.Y, 0)));

                    //bones[i].TransformedPoint = Vector2.Transform(bones[i].BoneStart, (Matrix.CreateTranslation(new Vector3(
                    //    origin.X, origin.Y, 0)) * Matrix.CreateRotationZ(MathHelper.PiOver2)));

                    //bones[i].TransformedPoint.Normalize();
                    //bones[i].TransformedPoint=new Vector2(bones[i].TransformedPoint.X+bones[bones[i].ParentId].Length,
                    //bones[i].TransformedPoint.Y+ bones[bones[i].ParentId].Length);

                    Vector2 vv = bones[i].TransformedPoint-origin;

                    //bones[i].TransformedPoint = Vector2.Transform(bones[i].TransformedPoint,
                    //    Matrix.CreateTranslation(new Vector3(bones[bones[i].ParentId].Length, 0, 0)));

                    //bones[i].TransformedPoint = Vector2.Transform(vv,
                    //    Matrix.CreateRotationZ(MathHelper.PiOver2));
                    //bones[i].TransformedPoint += origin;

                    //bones[i].TransformedPoint = Vector2.Transform(bones[i].TransformedPoint,
                    //    Matrix.CreateRotationZ(MathHelper.PiOver2));

                    //bones[i].TransformedPoint = Vector2.Transform(bones[i].TransformedPoint,
                    //    Matrix.CreateTranslation(new Vector3(
                    //    origin.X, origin.Y, 0)));

                    //bones[i].TransformedPoint = Vector2.Transform(bones[i].TransformedPoint,
                    //    Matrix.CreateTranslation(new Vector3(bones[bones[i].ParentId].Length, 0, 0)));



                    //bones[i].TransformedPoint += origin;
                    //+ bones[bones[i].ParentId].Length
                }
            }

            

            for (int i = 0; i < l.Length; i++)
            {
                //l[i] = new DebugLine(be, bones[i].BoneStart.X,
                //    bones[i].BoneStart.Y, bones[i].Length);
                l[i] = new DebugLine(be, bones[i].TransformedPoint.X,
                    bones[i].TransformedPoint.Y, bones[i].Length);
            }
        }

        public void DrawDebug(GraphicsDevice gd)
        {
            foreach (DebugLine it in l)
            {
                it.DebugDraw(gd);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < bones.Length; i++)
            {
                //if (i > 0)
                //{
                //    sb.Draw(textures[i], bones[i].TransformedPoint, null, Color.White,
                //        MathHelper.PiOver2, bones[i].TextureOrigin, 1f, SpriteEffects.None, 0f);
                //}
                //else
                {
                    sb.Draw(textures[i], bones[i].TransformedPoint, null, Color.White,
                        0, bones[i].TextureOrigin, 1f, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
