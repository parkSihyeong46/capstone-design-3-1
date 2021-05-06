using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransitionSystem_UI : MonoBehaviour
{
    Animator animator;
    public AreaTransitionSystem areaTransition;
    public bool fadeState;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TransitionFadeOut()
    {
        animator.SetTrigger("AreaFadeOut");
    }
    public void TransitionFadeIn()
    {
        animator.SetTrigger("AreaFadeIn");
    }

    public void TransitionArea()
    {
        areaTransition.ChangeArea();
    }
}
