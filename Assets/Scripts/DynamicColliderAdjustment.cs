using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator),typeof(CapsuleCollider2D))]
public class DynamicColliderAdjustment : MonoBehaviour
{
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;
    public Vector2 initialOffset = new Vector2(0f, 0.03f);
    public Vector2 animationEndOffset = new Vector2(0f, -0.85f);
    private bool isExploding = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        if (capsuleCollider != null)
        {
            capsuleCollider.offset = initialOffset;
        }
        else
        {
            Debug.LogError("CapsuleCollider2D component not found on the GameObject.");
        }
    }

    void Update()
    {
        if (!isExploding && animator.GetCurrentAnimatorStateInfo(0).IsName("animMeteorTrail"))
        {
            float progress = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
            if (capsuleCollider != null)
            {
                capsuleCollider.offset = Vector2.Lerp(initialOffset, animationEndOffset, progress);
            }
        }
        else if (isExploding && capsuleCollider != null)
        {
            capsuleCollider.offset = animationEndOffset;
        }
    }

    public void StopAdjustments()
    {
        isExploding = true;
    }

    public void ResetAdjustments()
    {
        isExploding = false;
        if (capsuleCollider != null)
        {
            capsuleCollider.offset = initialOffset;
        }
    }
}
