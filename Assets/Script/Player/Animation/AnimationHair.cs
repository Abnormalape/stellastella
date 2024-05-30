using System;
using UnityEngine;
class AnimationHair : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition = new Vector2(0f, -0.124f);
            transform.localScale = new Vector2(1, 1);
            animator.Play("Player_Hair_Down");
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition = new Vector2(0f, -0.124f);
            transform.localScale = new Vector2(1, 1);
            animator.Play("Player_Hair_Up");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition = new Vector2(0f, -0.124f);
            transform.localScale = new Vector2(1, 1);
            animator.Play("Player_Hair_Right");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition = new Vector2(0f, -0.124f);
            transform.localScale = new Vector2(1, 1);
            animator.Play("Player_Hair_Right");
        }
    }
}