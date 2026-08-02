using System.Collections.Generic;
using UnityEngine;

namespace Moblik.Animation
{
    public enum AnimationType
    {
        NONE, IDLE, RUN, ATTACK, DEATH
    }

    public class AnimationBase : MonoBehaviour
    {
        public Animator animator;
        public List<AnimationSetup> animationSetup;

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            var setup = animationSetup.Find(i => i.animationType == animationType);
            
            if (setup != null)
            {
                animator.SetTrigger(setup.triggerName);
            }
        }
    }

    [System.Serializable]
    public class AnimationSetup
    {
        public AnimationType animationType;
        public string triggerName;
    }
}