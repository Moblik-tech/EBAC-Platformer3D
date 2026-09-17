using System.Collections.Generic;
using UnityEngine;
using Moblik.Utils;

namespace Moblik.Animation
{
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
}