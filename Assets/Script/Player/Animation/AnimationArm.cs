using System;
using UnityEngine;
class AnimationArm : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            transform.localScale = new Vector2(1, 1);
            animator.Play("Player_Arm_Walk_Down");
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.localScale = new Vector2(1, 1);
            animator.Play("Player_Arm_Walk_Up");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector2(1, 1);
            animator.Play("Player_Arm_Walk_Right");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector2(1, 1);
            animator.Play("Player_Arm_Walk_Right");
        }
    }
}
