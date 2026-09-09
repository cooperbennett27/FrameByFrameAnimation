using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameByFrameAnimation
{
    public class AnimController : MonoBehaviour
    {
        [SerializeField] private AnimClip[] clips;
        [SerializeField] private string startAnim = "None";
        private SpriteRenderer spr;
        private Coroutine curAnim;

        private Dictionary<string, AnimClip> clipDict = new();

        private void Awake()
        {
            spr = GetComponent<SpriteRenderer>();

            foreach (AnimClip clip in clips)
            {
                if (!clipDict.TryAdd(clip.Name(), clip))
                {
                    Debug.LogError("Duplicate clip name " + clip.Name());
                }
            }
        }

        private void Start()
        {
            if (startAnim != "None")
            {
                PlayAnim(startAnim);
            }
        }

        public void PlayAnim(string name)
        {
            if (curAnim != null) { StopCoroutine(curAnim); }
            if (!clipDict.TryGetValue(name, out AnimClip clip))
            {
                Debug.LogError("No clip found called " + name);
                return;
            }

            curAnim = StartCoroutine(Animate(clip));
        }

        public void PlayAnim(string name, Action callback)
        {
            if (curAnim != null) { StopCoroutine(curAnim); }
            if (!clipDict.TryGetValue(name, out AnimClip clip))
            {
                Debug.LogError("No clip found called " + name);
                return;
            }

            if (clip.Looping()) { Debug.LogWarning("Animation " + name + " is played with a callback function but is also looping. The callback function will never be called."); }

            curAnim = StartCoroutine(Animate(clip, callback));
        }

        private IEnumerator Animate(AnimClip clip)
        {
            spr.sprite = clip.StartAnim();
            while (true)
            {
                yield return new WaitForSeconds(clip.FrameLength());
                spr.sprite = clip.NextSprite();
                if (clip.IsFinished())
                {
                    break;
                }
            }
        }

        private IEnumerator Animate(AnimClip clip, Action callback)
        {
            spr.sprite = clip.StartAnim();
            while (true)
            {
                yield return new WaitForSeconds(clip.FrameLength());
                spr.sprite = clip.NextSprite();
                if (clip.IsFinished())
                {
                    callback();
                    break;
                }
            }
        }
    }
}
