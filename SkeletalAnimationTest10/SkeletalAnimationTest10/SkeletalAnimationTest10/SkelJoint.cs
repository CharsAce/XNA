using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SkeletalAnimationTest10
{    
    class SkelJoint
    {
        Vector2 skeletonPosition;
        Joint[] joints = new Joint[3];
        BasicEffect be;
        DebugLine[] l = new DebugLine[3];
        Texture2D[] textures = new Texture2D[3];
        Vector2[] positions = new Vector2[3];
        float[] rotations = new float[3];

        Transformation[] AbsoluteTransforms = new Transformation[3];
        Transformation[] RelativeTransforms = new Transformation[3];

        Transformation[] AbsoluteTransformsCopy = new Transformation[3];

        bool drawed = true;

        bool flipped = true;

        public bool Flipped
        {
            get { return flipped; }
        }

        public SkelJoint(BasicEffect be)
        {
            //for (int i = 0; i < joints.Length; i++)
            //{
            //    joints[i] = new Joint();                
            //}

            joints[0].Offset = new Vector2(0, 0);
            joints[0].ParentId = -1;
            joints[0].Flag = BoneFlag.BoneAbsolute;
            joints[0].TextureName = "0";
            joints[0].TextureOrigin = new Vector2(24, 24);
            //joints[0].Scale = 1f;
            //joints[0].Length = 0;

            joints[1].Offset = new Vector2(0, 30);
            joints[1].ParentId = 0;
            joints[1].Flag = BoneFlag.Child;
            joints[1].TextureName = "1";
            joints[1].TextureOrigin = new Vector2(12, 24);
            //joints[1].Scale = 1f;
            //joints[1].Length = 36;

            joints[2].Offset = new Vector2(84, 0);
            joints[2].ParentId = 1;
            joints[2].Flag = BoneFlag.Child;
            joints[2].TextureName = "1";
            joints[2].TextureOrigin = new Vector2(12, 24);
            //joints[2].Scale = 1f;
            //joints[2].Length = 36;

            rotations[0] = MathHelper.PiOver4;
            //rotations[1]=MathHelper.PiOver2;
            rotations[1] = 0;

            this.be = be;


            //RelativeTransforms[0].Position = new Vector2(0,0);
            //RelativeTransforms[0].Rotation = 0f;// MathHelper.PiOver4;
            //RelativeTransforms[0].Scale = 1f;
            //RelativeTransforms[1].Position = new Vector2(0,0);
            //RelativeTransforms[1].Rotation = MathHelper.PiOver4/3f;
            //RelativeTransforms[1].Scale = 1f;
            //RelativeTransforms[2].Position = new Vector2(0, 0);
            //RelativeTransforms[2].Rotation = MathHelper.PiOver2;
            //RelativeTransforms[2].Scale = 1f;

            AbsoluteTransforms[0].Position = new Vector2(300, 300);
            AbsoluteTransforms[0].Rotation = 0;
            AbsoluteTransforms[0].Scale = new Vector2(1, 1);
        }

        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < joints.Length; i++)
            {

                textures[i] = content.Load<Texture2D>(joints[i].TextureName);
            }
        }

        public void TransBone()
        {
            for (int i = 0; i < joints.Length; i++)
            {
                if (joints[i].Flag == BoneFlag.Child)
                {
                    //positions[i]=joints[i].Offset;
                    positions[i] = Vector2.Transform(positions[i], 
                        Matrix.CreateTranslation(new Vector3(joints[i].Offset,0)));

                    positions[i] = Vector2.Transform(positions[i], Matrix.CreateRotationZ(rotations[joints[i].ParentId]));
                    //positions[i] = positions[joints[i].ParentId] + joints[i].Offset;
                    positions[i] += positions[joints[i].ParentId];

                    rotations[i] = rotations[i] + rotations[joints[i].ParentId];

                    Vector2 offset = joints[i].Offset;
                    //offset.X *= -1;

                    AbsoluteTransforms[i] =
                        Transformation.Compose(AbsoluteTransforms[joints[i].ParentId],
                        RelativeTransforms[i].Translate(offset));
                }
                else
                {
                    positions[i] = joints[i].Offset;

                    Vector2 offset = joints[i].Offset;
                    //offset.X *= -1;

                    AbsoluteTransforms[i] =
                        Transformation.Compose(AbsoluteTransforms[i], RelativeTransforms[i].Translate(offset));
                }
            }
        }

        public void SetLocalTransform(Transformation[] t)
        {
            Array.Copy(t, RelativeTransforms, t.Length);
        }

        public void MoveSkeleton(Vector2 movement)
        {
            skeletonPosition += movement;
        }

        public void SetSkeletonPosition(Vector2 position)
        {
            skeletonPosition = position;
        }

        public void FlipSkeleton()
        {
            flipped = !flipped;            
        }
        public void CopyTransform()
        {
            //if (drawed)
            {
                Array.Copy(AbsoluteTransforms, AbsoluteTransformsCopy, AbsoluteTransforms.Length);
                //AbsoluteTransformsCopy = AbsoluteTransforms;

                //for (int i = 0; i < AbsoluteTransforms.Length; i++)
                //{
                //    AbsoluteTransformsCopy[i] = Transformation.Copy(AbsoluteTransforms[i]);
                //}
                Transformation temp;
                temp = Transformation.Default;
                ////temp.Position = AbsoluteTransformsCopy[0].Position;
                ////temp.Rotation = 0f;
                AbsoluteTransforms[0] = Transformation.Copy(temp);
                drawed = false;
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
            for (int i = 0; i < joints.Length; i++)
            {
                Vector2 pos = AbsoluteTransformsCopy[i].Position + skeletonPosition;
                Vector2 origin = joints[i].TextureOrigin;
                float rotation = AbsoluteTransformsCopy[i].Rotation;
                Vector2 scale = AbsoluteTransformsCopy[i].Scale;
                //if (i > 0)
                //{
                //    sb.Draw(textures[i], joints[i].TransformedPoint, null, Color.White,
                //        MathHelper.PiOver2, joints[i].TextureOrigin, 1f, SpriteEffects.None, 0f);
                //}
                //else
                if (flipped)
                {
                    //sb.Draw(textures[i], positions[i], null, Color.White,
                    //    0, joints[i].TextureOrigin, 1f, SpriteEffects.None, 0f);
                    //sb.Draw(textures[i], positions[i], null, Color.White,
                    //    rotations[i], joints[i].TextureOrigin, 1f, SpriteEffects.None, 0f);
                    //pos.X *= -1;
                    pos.X -= (AbsoluteTransformsCopy[i].Position.X * 2);
                    //if(i>0)
                    origin.X = textures[i].Width - origin.X;
                    rotation = -rotation;
                    //scale.X *= -1;
                    //sb.Draw(textures[i], AbsoluteTransformsCopy[i].Position, null, Color.White,
                    //AbsoluteTransformsCopy[i].Rotation, joints[i].TextureOrigin, 1f, SpriteEffects.None, 0f);

                    sb.Draw(textures[i], pos, null, Color.White,
                        rotation, origin, scale, SpriteEffects.FlipHorizontally, 0f);
                }
                else
                {
                    sb.Draw(textures[i], pos, null, Color.White,
                        rotation, origin, scale, SpriteEffects.None, 0f);
                }
            }
            drawed = true;
        }
    }
}
