using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationManager : MonoBehaviour
{
    public enum AnimationType
    {
        IDLE,
        RUN,
        DEATH
    }

    public Animator animator;
    public List<AnimatorSetup> animatorSetup;

    private void OnValidate()
    {
        animator = GetComponent<Animator>();
    }

    public void Play(AnimationType type, float currentSpeedFactor = 1f)
    {
        foreach (var animation in animatorSetup)
        {
            if (animation.type == type)
            {
                animator.SetTrigger(animation.triggerName);
                animator.speed = animation.animationSpeed * currentSpeedFactor;
                break;
            }
        }
    }
}

[System.Serializable]
public class AnimatorSetup
{
    public AnimationManager.AnimationType type;
    public string triggerName;
    public float animationSpeed = 1f;
}