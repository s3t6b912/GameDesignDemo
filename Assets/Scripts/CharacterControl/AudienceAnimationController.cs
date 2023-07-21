using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceAnimationController : MonoBehaviour
{
    private Animator anim;
    public GameObject playerDetector;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        AudienceCollisionReporter acr = playerDetector.GetComponent<AudienceCollisionReporter>();
        if (acr.colliding == true)
        {
            Animate("applause");
        }
        else
        {
            Animate("idle");
        }
    }

    private void Animate(string name)
    {
        DisableAnimations(anim, name);
        anim.SetBool(name, true);
    }

    public void AnimateIdle()
    {
        Animate("idle");
    }

    public void AnimateApplause()
    {
        Animate("applause");
    }

    private void DisableAnimations(Animator animator, string animation)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name != animation)
            {
                animator.SetBool(param.name, false);
            }
        }
    }
}
