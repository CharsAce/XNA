using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;

namespace SkeletalAnimationTest10
{    
    public class SkelJoint
    {
        [ContentSerializerIgnore]
        Vector2 skeletonPosition;

        public List<Joint> joints;
        [ContentSerializerIgnore]
        BasicEffect be;
        [ContentSerializerIgnore]
        List<DebugLine> l;
        [ContentSerializerIgnore]
        List<Texture2D> textures;

        [ContentSerializerIgnore]
        Vector2[] positions = new Vector2[3];
        [ContentSerializerIgnore]
        float[] rotations = new float[3];

        [ContentSerializerIgnore]
        List<Transformation> AbsoluteTransforms;
        [ContentSerializerIgnore]
        List<Transformation> RelativeTransforms;
        [ContentSerializerIgnore]
        List<Transformation> AbsoluteTransformsCopy;
        
        [ContentSerializerIgnore]
        XmlWriterSettings xmlWrite;

        [ContentSerializerIgnore]
        bool drawed = true;

        [ContentSerializerIgnore]
        bool flipped = true;

        [ContentSerializerIgnore]
        public bool Flipped
        {
            get { return flipped; }
        }

        public SkelJoint(BasicEffect be, int jointAmount)
        {
           
        }

        public SkelJoint(BasicEffect be)
        {
            joints = new List<Joint>();
            l = new List<DebugLine>();

            Joint A;
            A.Offset = new Vector2(0, 0);
            A.ParentId = -1;
            A.Flag = BoneFlag.BoneAbsolute;
            A.TextureName = "0";
            A.TextureOrigin = new Vector2(24, 24);
            joints.Add(A);
            
            Joint B;
            B.Offset = new Vector2(0, 30);
            B.ParentId = 0;
            B.Flag = BoneFlag.Child;
            B.TextureName = "1";
            B.TextureOrigin = new Vector2(12, 24);
            joints.Add(B);

            Joint C;
            C.Offset = new Vector2(84, 0);
            C.ParentId = 1;
            C.Flag = BoneFlag.Child;
            C.TextureName = "1";
            C.TextureOrigin = new Vector2(12, 24);
            joints.Add(C);

            this.be = be;

            AbsoluteTransforms = new List<Transformation>();
            AbsoluteTransformsCopy = new List<Transformation>();
            //RelativeTransforms = new List<Transformation>();
            //AbsoluteTransforms[0].Position = new Vector2(300, 300);
            //AbsoluteTransforms[0].Rotation = 0;
            //AbsoluteTransforms[0].Scale = new Vector2(1, 1);

            //AbsoluteTransforms.Add(Transformation.Default);

            for (int i = 0; i < joints.Count; i++)
            {
                AbsoluteTransforms.Add(Transformation.Default);
            }
        }

        public void LoadContent(ContentManager content)
        {
            textures = new List<Texture2D>();
            for (int i = 0; i < joints.Count; i++)
            {
                textures.Add(content.Load<Texture2D>(joints[i].TextureName));
                //textures[i] = content.Load<Texture2D>(joints[i].TextureName);
            }
        }

        public void TransBone()
        {
            for (int i = 0; i < joints.Count; i++)
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
            if (RelativeTransforms == null)
            {                
                RelativeTransforms = new List<Transformation>();
            }
            //Array.Copy(t, RelativeTransforms, t.Length);

            for (int i = 0; i < t.Length; i++)
            {
                if (RelativeTransforms.Count<t.Length)
                    RelativeTransforms.Add(t[i]);
                else
                    RelativeTransforms[i] = t[i];
            }
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
            AbsoluteTransformsCopy = AbsoluteTransforms;
            Transformation temp;
            temp = Transformation.Default;
            AbsoluteTransforms[0] = Transformation.Copy(temp);
                drawed = false;
            
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
            for (int i = 0; i < joints.Count; i++)
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
