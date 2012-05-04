using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletalAnimationTest10
{
    public class AnimationUpdater
    {
        int frameIndex;
        int currentFrame;
        BoneAnimation BoneAnimation;

        public AnimationUpdater(BoneAnimation data)
        {
            BoneAnimation = data;
            frameIndex = 0;
            currentFrame = 0;
        }

        public Transformation UpdateFrame(int index, Transformation setTrans)
        {
            Transformation temp;
            
            int te = (frameIndex + 1) % BoneAnimation.Keyframes.Count;
            float am;
            
            if (currentFrame == 0)
            {


                if (BoneAnimation.Keyframes.Count > 1)
                {
                    float t = (BoneAnimation.Keyframes[te].FrameNumber *
                    BoneAnimation.FPS) -
                        (BoneAnimation.Keyframes[frameIndex].FrameNumber *
                        BoneAnimation.FPS);

                    am = t / 1000;

                    temp = Transformation.Lerp(setTrans,
                        BoneAnimation.Keyframes[te].trans, am);
                    setTrans = temp;

                    return setTrans;
                }
                else
                {
                    setTrans = 
                    BoneAnimation.Keyframes[frameIndex].trans;

                    return setTrans;
                }
            }
            else
            {

                if (te > frameIndex)
                {
                    float t = (BoneAnimation.Keyframes[te].FrameNumber *
                    BoneAnimation.FPS) -
                        (BoneAnimation.Keyframes[frameIndex].FrameNumber *
                        BoneAnimation.FPS);

                    am = t / 1000;

                    temp = Transformation.Lerp(setTrans,
                    BoneAnimation.Keyframes[te].trans, am);
                    setTrans = temp;

                    return setTrans;
                }
                else
                {
                    float t = (BoneAnimation.Keyframes[frameIndex].FrameNumber *
                        BoneAnimation.FPS) - (BoneAnimation.Keyframes[te].FrameNumber *
                    BoneAnimation.FPS);

                    am = t / 1000;

                    temp = Transformation.Lerp(BoneAnimation.Keyframes[te].trans,
                        setTrans,
                        am);
                    setTrans = temp;

                    return setTrans;
                }
            }


        }

        public void Update(float elapsed, ref Transformation setTrans)
        {
            setTrans=UpdateFrame(0, setTrans);
            
            currentFrame++;

            int temp = (frameIndex + 1) % BoneAnimation.Keyframes.Count;

            if (currentFrame > BoneAnimation.Keyframes[temp].FrameNumber)
                frameIndex = (frameIndex + 1) % BoneAnimation.Keyframes.Count;

            if (currentFrame > BoneAnimation.FrameLength)
            {
                currentFrame = 0;
            }
        }
    }
}
