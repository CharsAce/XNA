using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletalAnimationTest10
{
    public class AnimationPlayer
    {
        SkeletonAnimation Animation;
        Transformation[] SetTrans;
        AnimationUpdater[] AnimationUpdates;
        
        int boneIndex;
        int currentFrame;
        int frameIndex;

        public int CurrentFrame
        {
            get { return currentFrame; }
        }

        public int FrameIndex
        {
            get { return frameIndex; }
        }

        public AnimationPlayer(SkeletonAnimation animation)
        {
            Animation = animation;
            AnimationUpdates = new AnimationUpdater[Animation.BoneAnimations.Count];
            SetTrans = new Transformation[Animation.BoneAnimations.Count];

            currentFrame = 0;
            frameIndex = 0;

            int length = Animation.BoneAnimations.Count;
            for (int i = 0; i < length; i++)
            {
                AnimationUpdates[i] = new AnimationUpdater(Animation.BoneAnimations[i]);
            }
            
        }

        public void UpdateFrame(int index, Transformation[] setTrans)
        {
            //Animation.BoneAnimations[index].Keyframes[index]; 

            //if (Animation.BoneAnimations[index].Keyframes.Count < 1)
            //{
            //    setTrans[index] = Animation.
            //        BoneAnimations[index].Keyframes[Animation.BoneAnimations[index].Keyframes.Count - 1].trans;
            //}
            //else if (Animation.BoneAnimations[index].Keyframes.Count <= frameIndex)
            //{
            //    setTrans[index] = Animation.
            //        BoneAnimations[index].Keyframes[frameIndex].trans;
            //}

            //if (setTrans[index].Rotation == null)
            if (currentFrame == 0)
                setTrans[index] = Animation.
                    BoneAnimations[index].Keyframes[frameIndex].trans;
            else
            {
                Transformation temp;
                int te = (frameIndex + 1) % 3;

                float am;

                if (te > frameIndex)
                {
                    //am=(Animation.BoneAnimations[index].Keyframes[te]-
                    //    Animation.BoneAnimations[index].Keyframes[frameIndex])
                    float t = (Animation.BoneAnimations[index].Keyframes[te].FrameNumber *
                    Animation.BoneAnimations[index].FPS) -
                        (Animation.BoneAnimations[index].Keyframes[frameIndex].FrameNumber *
                        Animation.BoneAnimations[index].FPS);

                    am = t / 1000;
                }
                else
                {
                    float t = (Animation.BoneAnimations[index].Keyframes[frameIndex].FrameNumber *
                        Animation.BoneAnimations[index].FPS) - (Animation.BoneAnimations[index].Keyframes[te].FrameNumber *
                    Animation.BoneAnimations[index].FPS);

                    am = t / 1000;
                }
                temp = Transformation.Lerp(setTrans[index],
                    Animation.BoneAnimations[index].Keyframes[te].trans, am);
                setTrans[index] = temp;
            }

            
        }

        public void UpdateFrame2(int index, Transformation[] setTrans)
        {
            Transformation temp;
            //int te = (frameIndex + 1) % 3;
            int te = (frameIndex + 1) % Animation.BoneAnimations[index].Keyframes.Count;
            float am;
            //Animation.BoneAnimations[index].Keyframes[index]; 

            //if (Animation.BoneAnimations[index].Keyframes.Count < 1)
            //{
            //    setTrans[index] = Animation.
            //        BoneAnimations[index].Keyframes[Animation.BoneAnimations[index].Keyframes.Count - 1].trans;
            //}
            //else if (Animation.BoneAnimations[index].Keyframes.Count <= frameIndex)
            //{
            //    setTrans[index] = Animation.
            //        BoneAnimations[index].Keyframes[frameIndex].trans;
            //}

            //if (setTrans[index].Rotation == null)
            if (currentFrame == 0)
            {
                

                if (Animation.BoneAnimations[index].Keyframes.Count > 1)
                {
                    float t = (Animation.BoneAnimations[index].Keyframes[te].FrameNumber *
                    Animation.BoneAnimations[index].FPS) -
                        (Animation.BoneAnimations[index].Keyframes[frameIndex].FrameNumber *
                        Animation.BoneAnimations[index].FPS);

                    am = t / 1000;

                    temp = Transformation.Lerp(setTrans[index],
                        Animation.BoneAnimations[index].Keyframes[te].trans, am);
                    setTrans[index] = temp;
                }
                else
                {
                    setTrans[index] = Animation.
                    BoneAnimations[index].Keyframes[frameIndex].trans;
                }
            }
            else
            {

                if (te > frameIndex)
                {
                    //am=(Animation.BoneAnimations[index].Keyframes[te]-
                    //    Animation.BoneAnimations[index].Keyframes[frameIndex])
                    float t = (Animation.BoneAnimations[index].Keyframes[te].FrameNumber *
                    Animation.BoneAnimations[index].FPS) -
                        (Animation.BoneAnimations[index].Keyframes[frameIndex].FrameNumber *
                        Animation.BoneAnimations[index].FPS);

                    am = t / 1000;

                    temp = Transformation.Lerp(setTrans[index],
                    Animation.BoneAnimations[index].Keyframes[te].trans, am);
                    setTrans[index] = temp;
                }
                else
                {
                    float t = (Animation.BoneAnimations[index].Keyframes[frameIndex].FrameNumber *
                        Animation.BoneAnimations[index].FPS) - (Animation.BoneAnimations[index].Keyframes[te].FrameNumber *
                    Animation.BoneAnimations[index].FPS);

                    am = t / 1000;

                    temp = Transformation.Lerp(Animation.BoneAnimations[index].Keyframes[te].trans, 
                        setTrans[index],
                        am);
                    setTrans[index] = temp;
                }
                //temp = Transformation.Lerp(setTrans[index],
                //    Animation.BoneAnimations[index].Keyframes[te].trans, am);
                //setTrans[index] = temp;
            }


        }
        
        public void Update(float elapsed, Transformation[] setTrans, bool b)
        {
            //for (int i = 0; i < 3; i++)
            //{
            //    if(b)
            //    UpdateFrame2(i, setTrans);   
            //    else
            //        UpdateFrame(i, setTrans);
            //}

            //currentFrame++;

            //int te = (frameIndex + 1) % 3;
            //if (currentFrame > Animation.BoneAnimations[0].Keyframes[te].FrameNumber)
            //    frameIndex = (frameIndex + 1) % 3;

            //if (currentFrame > Animation.BoneAnimations[0].FrameLength)
            //{
            //    currentFrame = 0;
            //}
            int length = Animation.BoneAnimations.Count;
            for (int i = 0; i < length; i++)
            {
                AnimationUpdates[i].Update(elapsed, ref setTrans[i]);

                //if (AnimationUpdates[i].BoneAnimationGetter.Keyframes.Count>=frameIndex)
                //{
                //    frameIndex = AnimationUpdates[i].FrameIndex;
                //}
                frameIndex = AnimationUpdates[i].FrameIndex;
            }
        }
    }
}
