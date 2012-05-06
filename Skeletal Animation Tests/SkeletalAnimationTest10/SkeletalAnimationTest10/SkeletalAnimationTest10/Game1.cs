using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using System.Xml;

namespace SkeletalAnimationTest10
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        FrameRateCounter ff;
        SpriteFont DebugFont;
        Sk bb;
        //Skeleton2d skel;
        Skeleton2 sk;
        SkelJoint joi;

        SkeletonAnimation skeletonAnimation;
        BasicEffect be;

        MouseState currentMouseState;
        MouseState previousMouseState;

        XmlWriterSettings xmlSettings;

        Vector2 MousePosition;

        Transformation[] setTrans = new Transformation[3];
        AnimationPlayer animationPlayer;
        List<List<Transformation>> transList = new List<List<Transformation>>();
        int currentFrame = 0;
        int currentTr = 0;
        int findex = 0;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.SynchronizeWithVerticalRetrace = false;
            graphics.ApplyChanges();

            //IsFixedTimeStep = false;
            ff = new FrameRateCounter(this);
            Content.RootDirectory = "Content";
            Components.Add(ff);
            //IsFixedTimeStep = false;
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //skel = new Skeleton2d();
            bb = new Sk();
            LoadAnimation();
            
            base.Initialize();
            joi.SetSkeletonPosition(new Vector2(300, 300));
            //skel.AddPosition(new Vector2(100,100));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 
        public void LoadAnimation()
        {
            skeletonAnimation = new SkeletonAnimation();
            
            Keyframe ak= new Keyframe();
            ak.trans.Position = new Vector2(0, 0);
            ak.trans.Rotation = 0f;
            ak.trans.Scale=new Vector2(1, 1);
            ak.FrameNumber=0;

            Keyframe ak2 = new Keyframe();
            ak2.trans.Position = new Vector2(0, 0);
            ak2.trans.Rotation = 0f;
            ak2.trans.Scale = new Vector2(1, 1);
            ak2.FrameNumber = 19;

            Keyframe ak3 = new Keyframe();
            ak3.trans.Position = new Vector2(0, 0);
            ak3.trans.Rotation = 0f;
            ak3.trans.Scale = new Vector2(1, 1);
            ak3.FrameNumber = 39;
            
            BoneAnimation ab = new BoneAnimation();
            ab.Keyframes.Add(ak);
            //ab.Keyframes.Add(ak2);
            //ab.Keyframes.Add(ak3);
            ab.FrameLength = 40;
            ab.FPS = 12;

            skeletonAnimation.BoneAnimations.Add(ab);

            Keyframe bk = new Keyframe();
            bk.trans.Position = new Vector2(0, 0);
            bk.trans.Rotation = MathHelper.PiOver4;
            bk.trans.Scale = new Vector2(1, 1);
            bk.FrameNumber = 0;

            Keyframe bk2 = new Keyframe();
            bk2.trans.Position = new Vector2(0, 0);
            bk2.trans.Rotation = MathHelper.PiOver4/3;
            bk2.trans.Scale = new Vector2(1, 1);
            bk2.FrameNumber = 19;

            Keyframe bk3 = new Keyframe();
            bk3.trans.Position = new Vector2(0, 0);
            bk3.trans.Rotation = MathHelper.PiOver4;
            bk3.trans.Scale = new Vector2(1, 1);
            bk3.FrameNumber = 39;

            BoneAnimation bb = new BoneAnimation();
            bb.Keyframes.Add(bk);
            bb.Keyframes.Add(bk2);
            bb.Keyframes.Add(bk3);
            bb.FrameLength = 40;
            bb.FPS = 12;

            skeletonAnimation.BoneAnimations.Add(bb);

            Keyframe ck = new Keyframe();
            ck.trans.Position = new Vector2(0, 0);
            ck.trans.Rotation = MathHelper.PiOver2 / 2;
            ck.trans.Scale = new Vector2(1, 1);
            ck.FrameNumber = 0;

            Keyframe ck2 = new Keyframe();
            ck2.trans.Position = new Vector2(0, 0);
            ck2.trans.Rotation = MathHelper.PiOver2;
            ck2.trans.Scale = new Vector2(1, 1);
            ck2.FrameNumber = 19;

            Keyframe ck3 = new Keyframe();
            ck3.trans.Position = new Vector2(0, 0);
            ck3.trans.Rotation = MathHelper.PiOver2 / 2f;
            ck3.trans.Scale = new Vector2(1, 1);
            ck3.FrameNumber = 39;

            Keyframe ck4 = new Keyframe();
            ck4.trans.Position = new Vector2(0, 0);
            ck4.trans.Rotation = MathHelper.Pi;
            ck4.trans.Scale = new Vector2(1, 1);
            ck4.FrameNumber = 39;

            BoneAnimation cb = new BoneAnimation();
            cb.Keyframes.Add(ck);
            cb.Keyframes.Add(ck2);
            cb.Keyframes.Add(ck3);
            //cb.Keyframes.Add(ck4);
            cb.FrameLength = 40;
            cb.FPS = 10;

            skeletonAnimation.BoneAnimations.Add(cb);
            animationPlayer = new AnimationPlayer(skeletonAnimation);
        }

  
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //skel.LoadContent(Content);
            DebugFont = Content.Load<SpriteFont>("debugFont");
            LoadDebugs();
            sk = new Skeleton2(be);
            sk.TransformBone();
            sk.LoadContent(Content);

            joi = new SkelJoint(be);
            joi.LoadContent(Content);
            xmlSettings = new XmlWriterSettings();
            //joi.TransBone();
            //joi.CopyTransform();
            // TODO: use this.Content to load your game content here
        }

        private void LoadDebugs()
        {
            be = new BasicEffect(graphics.GraphicsDevice);
            be.VertexColorEnabled = true;
            Matrix projection = Matrix.CreateOrthographicOffCenter(0, GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height, 0,
                0, 1);
            be.Projection = projection;            
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        /// 
        public void WriteSkeleton()
        {
            xmlSettings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create("testSkl.xml", xmlSettings))
            {
                IntermediateSerializer.Serialize(writer, joi, null);
            }
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        Vector2 motion = Vector2.Zero;
        KeyboardState c;
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        float v = 0;
        bool b = true;
        protected override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            currentMouseState = Mouse.GetState();
            c=Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);
            if (c.IsKeyDown(Keys.Left))
            {
                motion.X--;
                if (!joi.Flipped)
                    joi.FlipSkeleton();
            }

            if (c.IsKeyDown(Keys.Down))
            {
                motion.Y++;                
            }
            if (c.IsKeyDown(Keys.Right))
            {
                motion.X++;
                if (joi.Flipped)
                    joi.FlipSkeleton();
            }

            if (c.IsKeyDown(Keys.Up))
            {
                motion.Y--;
            }
            if (c.IsKeyDown(Keys.A))
            {
                b = !b;
            }
            if (c.IsKeyDown(Keys.S))
            {
                WriteSkeleton();
            }

            if (motion!=Vector2.Zero)
            {
                //motion *= 800;
                if (b)
                {
                    motion.Normalize();
                    motion *= 5;
                }
                else
                    motion.Normalize();
                //skel.AddPosition(motion);
                joi.MoveSkeleton(motion);
                motion = Vector2.Zero;
            }
            
            //v += elapsed;
            //if (v >= 0f)
            //{
            //    for (int i = 0; i < 3; i++)
            //    {
            //        UpdateFrame(i);
            //    }
            //    currentTr++;
            //    //for (int i = 0; i < transList.Count; i++)
            //    //{
            //    //    UpdateTr(i);
            //    //}
            //    v = 0f;
            //}
            //int te = (currentFrame + 1) % 3;
            //if(currentTr>skeletonAnimation.BoneAnimations[0].Keyframes[te].FrameNumber)
            //currentFrame = (currentFrame + 1) % 3;

            //if (currentTr>skeletonAnimation.BoneAnimations[0].FrameLength)
            //{
            //    currentTr = 0;
            //}
            animationPlayer.Update(elapsed, setTrans, b);
            joi.SetLocalTransform(setTrans);
            joi.TransBone();
            joi.CopyTransform();
            
            //skel.Calculations();
            // TODO: Add your update logic here
            //skel.ResetAbsoluteTrans();
            base.Update(gameTime);
        }

        public void UpdateFrame(int index)
        {
            Transformation temp;
            int te = (currentFrame + 1) % 3;
            float am;
            //skeletonAnimation.BoneAnimations[index].Keyframes[index]; 

            //if (skeletonAnimation.BoneAnimations[index].Keyframes.Count < 1)
            //{
            //    setTrans[index] = skeletonAnimation.
            //        BoneAnimations[index].Keyframes[skeletonAnimation.BoneAnimations[index].Keyframes.Count - 1].trans;
            //}
            //else if (skeletonAnimation.BoneAnimations[index].Keyframes.Count <= currentFrame)
            //{
            //    setTrans[index] = skeletonAnimation.
            //        BoneAnimations[index].Keyframes[currentFrame].trans;
            //}

            //if (setTrans[index].Rotation == null)
            if (currentTr == 0)
            {
                setTrans[index] = skeletonAnimation.
                    BoneAnimations[index].Keyframes[currentFrame].trans;
                float t = (skeletonAnimation.BoneAnimations[index].Keyframes[te].FrameNumber *
                    skeletonAnimation.BoneAnimations[index].FPS) -
                        (skeletonAnimation.BoneAnimations[index].Keyframes[currentFrame].FrameNumber *
                        skeletonAnimation.BoneAnimations[index].FPS);

                am = t / 1000;
            }
            else
            {



                if (te > currentFrame)
                {
                    //am=(skeletonAnimation.BoneAnimations[index].Keyframes[te]-
                    //    skeletonAnimation.BoneAnimations[index].Keyframes[currentFrame])
                    float t = (skeletonAnimation.BoneAnimations[index].Keyframes[te].FrameNumber *
                    skeletonAnimation.BoneAnimations[index].FPS) -
                        (skeletonAnimation.BoneAnimations[index].Keyframes[currentFrame].FrameNumber *
                        skeletonAnimation.BoneAnimations[index].FPS);

                    am = t / 1000;
                }
                else
                {
                    float t = (skeletonAnimation.BoneAnimations[index].Keyframes[currentFrame].FrameNumber *
                        skeletonAnimation.BoneAnimations[index].FPS) - (skeletonAnimation.BoneAnimations[index].Keyframes[te].FrameNumber *
                    skeletonAnimation.BoneAnimations[index].FPS);

                    am = t / 1000;
                }

            }
            temp = Lerp(setTrans[index],
                    skeletonAnimation.BoneAnimations[index].Keyframes[te].trans, am);
            setTrans[index] = temp;
            //findex = (findex + 1) % skeletonAnimation.BoneAnimations.Count;
            //currentFrame = (currentFrame + 1) % 3;
        }

        public Transformation Lerp(Transformation trans1, Transformation trans2, float amount)
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


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //sk.DrawDebug(GraphicsDevice);
            //Console.WriteLine(MousePosition);
            //bb.Translations(bb.root, be, GraphicsDevice);
            spriteBatch.Begin();
            spriteBatch.DrawString(DebugFont, animationPlayer.FrameIndex.ToString(), Vector2.Zero, Color.White);
            //skel.Draw(spriteBatch);
            //sk.Draw(spriteBatch);
            joi.Draw(spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here
            //skel.ResetAbsoluteTrans();
            base.Draw(gameTime);
        }
    }
}
