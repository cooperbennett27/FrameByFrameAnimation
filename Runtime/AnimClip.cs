using System.Linq;
using UnityEngine;

namespace FrameByFrameAnimation
{
    [System.Serializable]
    public class AnimClip
    {
        [SerializeField] private string name;
        [SerializeField] private Sprite[] frames;
        [SerializeField] private float frameLength;
        [SerializeField] private bool loop;
        [SerializeField] private bool useFrameLengthArray;
        [SerializeField] private float[] frameLengthArray;
        private bool isFinished;
        private int curFrame;

        public string Name() { return name; }

        public float FrameLength()
        {
            if (useFrameLengthArray)
            {
                return frameLengthArray[curFrame];
            }
            else
            {
                return frameLength;
            }
        }

        public bool IsFinished() { return isFinished; }

        public bool Looping() { return loop; }

        public Sprite StartAnim()
        {
            if (useFrameLengthArray && frames.Count() != frameLengthArray.Count())
            {
                Debug.LogError("Frame length array is different size than frame array. Either disable 'Use Frame Length Array' or correct the frame length array.");
            }

            curFrame = 0;
            isFinished = false;
            return frames[curFrame];
        }

        public Sprite NextSprite()
        {
            if (curFrame < frames.Length - 1)
            {
                curFrame++;
            }
            else if (loop)
            {
                curFrame = 0;
            }
            else
            {
                isFinished = true;
            }
            return frames[curFrame];
        }
    }
}
