using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace SkeletalAnimationTest10
{
    class Skeleton2d
    {
        public List<Joint> Joints;
        public List<Texture2D> Textures;
        public List<Vector2> Positions;
        public List<float> Rotations;

        Vector2 Position = Vector2.Zero;

        public List<Translation> AbsoluteTrans;
        public List<Translation> Temp;
        public List<Translation> LocalTrans;

        public Skeleton2d()
        {
            Joints = new List<Joint>();

            Joint j = new Joint();
            j.Offset = new Vector2(0, 0);
            j.TextureOrigin = new Vector2(10, 43);
            j.ParentId = 0;
            j.TextureName = "machine_arm";
            Joints.Add(j);

            j = new Joint();
            j.Offset = new Vector2(53, -30);
            j.TextureOrigin = new Vector2(12, 27);
            j.ParentId = 0;
            j.TextureName = "machine_forearm";
            Joints.Add(j);

            Positions = new List<Vector2>();
            Rotations = new List<float>();
            for (int i = 0; i < Joints.Count; i++)
            {
                Positions.Add(Vector2.Zero);
                Rotations.Add(0f);
            }
            Rotations[0] = MathHelper.PiOver2 / 2;


        }

        string path = "RotationTest.txt";
        public void CreateTxtFile()
        {
            
            FileStream fs;
            using (fs = new FileStream("RotationTest.txt", FileMode.CreateNew,
                FileAccess.Write))
            {
                
            }


        }
        public void WriteTxtFile(float R)
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (File.Exists(path))
            {
                
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        sw.WriteLine(R.ToString());
                    }
                
            }

            //Stream my = File.Create("C:\\XNA 4 stuff\\test.txt");
            //TextWriterTraceListener t = new TextWriterTraceListener(my);
            //Trace.Listeners.Add(t);
            //Trace.Write(R.ToString());
            //Trace.Flush();

        }

        public void LoadContent(ContentManager content)
        {
            //CreateTxtFile();
            AbsoluteTrans = new List<Translation>(Joints.Count);
            LocalTrans = new List<Translation>();
            Translation v;
            v.Position = new Vector2(0, 0);
            v.Rotation = 0f;
            AbsoluteTrans.Add(v);
            AbsoluteTrans.Add(Translation.Identity);

            Translation t;
            t.Position = new Vector2(0, 0);
            t.Rotation = MathHelper.PiOver4;
            LocalTrans.Add(t);
            LocalTrans.Add(Translation.Identity);
            //LocalTrans.Add(Translation.Identity);
            //Console.WriteLine(LocalTrans[0].Rotation);
            Textures = new List<Texture2D>();

            for (int i = 0; i < Joints.Count; i++)
            {
                Textures.Add(content.Load<Texture2D>(Joints[i].TextureName));   
            }
        }

        public void AddPosition(Vector2 posAdd)
        {
            Positions[0] = Vector2.Add(Positions[0], posAdd);
            AbsoluteTrans[0]=AbsoluteTrans[0].TransformPosition(posAdd);
        }

        public void Calculations()
        {
            for (int i = 0; i < Joints.Count; i++)
            {
                AbsoluteTrans[i] = Translation.CalculateTransformation(AbsoluteTrans[Joints[i].ParentId],
                    LocalTrans[i], Joints[i].Offset);
                //if (i == 0)
                //{
                //    Positions[i] = Vector2.Add(Positions[i], Joints[i].Offset);
                //}
                //else
                //{
                    //Vector2 t = Vector2.Add(Positions[Joints[i].ParentId], Joints[i].Offset);
                    ////Vector2 t = Vector2.Add(Positions[i - 1], Joints[i].Offset);
                    //Positions[i] = t;
                    ////Positions[i] = RotatePoint(t, Positions[Joints[i].ParentId], Rotations[Joints[i].ParentId]);
                    ////Positions[i] = RotatePoint(t, Positions[i-1], Rotations[i - 1]);

                    //if (Rotations[i] != Rotations[i] + Rotations[i - 1])
                    //    Rotations[i] = Rotations[i] + Rotations[i - 1];
                //}
            }
            Temp = new List<Translation>(AbsoluteTrans);
        }

        public Vector2 RotatePoint(Vector2 point, Vector2 origin, float rotation)
        {
            Vector2 result = new Vector2(point.X - origin.X, point.Y - origin.Y);

            //float a = (float)Math.Atan2(result.Y, result.X);
            //a += rotation;            
            //result = result.Length() * new Vector2((float)Math.Cos(a), (float)Math.Sin(a));

            result = Vector2.Transform(result, Matrix.CreateRotationZ(rotation));
            return result + origin;
        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < Joints.Count; i++)
            {
                //sb.Draw(Textures[i],
                //    Positions[i],
                //    null,
                //    Color.White,
                //    0f,//Rotations[i],
                //    Joints[i].TextureOrigin,
                //    1f,
                //    SpriteEffects.None,
                //    1f);
                sb.Draw(Textures[i],
                    Temp[i].Position,
                    null,
                    Color.White,
                    Temp[i].Rotation,//0f,
                    Joints[i].TextureOrigin,
                    1f,
                    SpriteEffects.None,
                    1f);
                WriteTxtFile(AbsoluteTrans[i].Rotation);
                //Console.WriteLine(AbsoluteTrans[i].Rotation);
            }

            
        }

        public void ResetAbsoluteTrans()
        {
            for (int c = 0; c < Joints.Count; c++)
            {
                Translation t;
                t.Position = AbsoluteTrans[c].Position;
                t.Rotation = 0f;
                AbsoluteTrans[c] = t;// Translation.Identity;
                //Rotations[c] = 0f;
                //Positions[c] = Vector2.Zero;
                //Console.WriteLine(AbsoluteTrans[c].Rotation);
            }
        }
    }
}
