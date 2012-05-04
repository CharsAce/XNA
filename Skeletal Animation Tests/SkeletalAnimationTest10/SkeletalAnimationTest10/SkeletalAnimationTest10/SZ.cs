using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SkeletalAnimationTest10
{
    public class SZ
    {
        Bone2[] bones = new Bone2[2];
        BasicEffect be;
        DebugLine[] l;
        Texture2D[] textures;
        Vector2[] positions = new Vector2[2];

        public SZ(BasicEffect be)
        {
            bones[0] = new Bone2();
            bones[1] = new Bone2();

            bones[0].BoneStart = new Vector2(24, 0);
            bones[0].BoneEnd = new Vector2(24, 48);
            bones[0].ParentId = -1;
            bones[0].TextureName = "0";
            bones[0].TextureOrigin = new Vector2(24, 0);
            
            bones[1].BoneStart = new Vector2(0, 24);
            bones[1].BoneEnd = new Vector2(48, 24);
            bones[1].ParentId = 0;
            bones[0].TextureName = "1";
            bones[1].TextureOrigin = new Vector2(12,24);


            this.be = be;

            l = new DebugLine[bones.Length];
            textures = new Texture2D[bones.Length];

            positions[0] = new Vector2(200, 200);
            positions[1] = new Vector2(0, 0);
        }

        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < bones.Length; i++)
            {

                textures[i] = content.Load<Texture2D>(bones[i].TextureName);
            }
        }

        public void TransBone()
        {
            for (int i = 0; i < bones.Length; i++)
            {
                if (bones[i].Flag==BoneFlag.Child)
                {
                    
                }
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
