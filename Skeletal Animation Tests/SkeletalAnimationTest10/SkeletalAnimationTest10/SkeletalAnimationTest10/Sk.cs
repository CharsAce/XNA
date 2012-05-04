using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletalAnimationTest10
{
    class Sk
    {
        public Bone root, tmp, tmp2;
        public int Count=0;
        DebugLine[] Lines;
        public Sk()
        {
            //root;
            tmp = new Bone();
            tmp2 = new Bone();

            root = BoneAddChild2(root, new Vector2(400, 400),
                MathHelper.PiOver2, 10, BoneFlag.BoneAbsolute, "Head");

            tmp = BoneAddChild2(root, Vector2.Zero, -MathHelper.PiOver2, 30,
                BoneFlag.Off, "Back");
            tmp2 = BoneAddChild2(tmp, Vector2.Zero, -MathHelper.PiOver4, 30,
                BoneFlag.Off, "LLeg");
            BoneAddChild2(tmp2, Vector2.Zero, 0, 30,
                BoneFlag.Off, "LLeg2");
            tmp2 = BoneAddChild2(tmp, Vector2.Zero, -2 * MathHelper.PiOver4, 30,
                BoneFlag.Off, "RLeg");
            BoneAddChild2(tmp2, Vector2.Zero, 0, 30,
                BoneFlag.Off, "RLeg2");
            tmp = BoneAddChild2(root, Vector2.Zero, 0, 20,
                BoneFlag.Off, "LArm");
            BoneAddChild2(tmp, Vector2.Zero, 0, 20,
                BoneFlag.Off, "LArm2");
            tmp = BoneAddChild2(root, Vector2.Zero, MathHelper.Pi, 20,
                BoneFlag.Off, "RArm");
            BoneAddChild2(tmp, Vector2.Zero, MathHelper.Pi, 20,
                BoneFlag.Off, "RArm2");

            
            BoneDumpTree(root, 0);

            //Translations(root);

            //Console.WriteLine("Count " + Count);
            Lines = new DebugLine[20];
        }

        public Bone BoneAddChild2(Bone root, float x, float y,
            float a, int l, BoneFlag flag, string name)
        {
            Bone t = null;
            int i;

            if (root == null)// || root.Parent == null)
            {
                root = new Bone();
                root.Parent = null;
            }
            else if (root.ChildBones.Length < 11)
            {
                t = new Bone();
                t.Parent = root;
                
                root.ChildBones[root.ChildCount] = t;
                root.ChildCount++;
                root = t;
                //return root;
            }
            else
                return null;
            
            root.X = x;
            root.Y = y;
            root.Angle = a;
            root.Length = l;
            root.Flag = flag;
            root.Name = string.Copy(name);

            return root;

        }

        public Bone BoneAddChild2(Bone root, Vector2 pos,
            float a, int l, BoneFlag flag, string name)
        {
            Bone t = null;
            int i;

            if (root == null)// || root.Parent == null)
            {
                root = new Bone();
                root.Parent = null;
            }
            else if (root.ChildBones.Length < 11)
            {
                t = new Bone();
                t.Parent = root;

                root.ChildBones[root.ChildCount] = t;
                root.ChildCount++;
                root = t;
                //return root;
            }
            else
                return null;

            root.boneStart = pos;
            root.Angle = a;
            root.Length = l;
            root.Flag = flag;
            root.Name = string.Copy(name);

            return root;

        }

        public Bone BoneAddChild(Bone root, float x, float y,
            float a, int l, BoneFlag flag, string name)
        {
            Bone t = null;
            int i;

            if (root == null)// || root.Parent == null)
            {
                root = new Bone();
                root.Parent = null;
                root.X = x;
                root.Y = y;
                root.Angle = a;
                root.Length = l;
                root.Flag = flag;
                root.Name = string.Copy(name);

                return root;
            }
            else if (root.ChildBones.Length < 11)
            {
                t = new Bone();
                t.Parent = root;                
                t.X = x;
                t.Y = y;
                t.Angle = a;
                t.Length = l;
                t.Flag = flag;
                t.Name = string.Copy(name);
                root.ChildBones[root.ChildCount] = t;
                root.ChildCount++;
                return root;
            }
            else
                return null;


        }
        
        public void BoneDumpTree(Bone root, int level)
        {
            Count++;

            for (int i = 0; i <level+1; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine("\t"+ root.ChildCount + "\t" + root.Name);

            for (int i = 0; i < root.ChildCount; i++)
            {
                BoneDumpTree(root.ChildBones[i], level + 1);
            }
        }

        int c = 0;

        public void Translations(Bone root, BasicEffect be, GraphicsDevice gd)
        {
            Matrix l;
            Vector3 t = new Vector3(root.boneStart,0);
            DebugLine v;

            t = Vector3.Transform(t, Matrix.CreateRotationZ(root.Angle));
            //l = Matrix.CreateTranslation(new Vector3(root.boneStart, 0f)) *
            //    Matrix.CreateRotationZ(root.Angle); 
                
            
            //Console.WriteLine(l);
                       
                //Lines[c] = new DebugLine(be, t.X, t.Y, root.Length);
                //Lines[c].DebugDraw(gd);
                //v = new DebugLine(be, t.X, t.Y, root.Length);
                //v.DebugDraw(gd);
                c++;
            

            //Matrix.CreateTranslation(new Vector3(root.Length, 0f, 0f))
            for (int i = 0; i < root.ChildCount; i++)
            {
                Translations(root.ChildBones[i], be, gd);
            }
        }

        
    }
}
