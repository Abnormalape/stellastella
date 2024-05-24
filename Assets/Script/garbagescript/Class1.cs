using System;
using Unity;
using UnityEngine;

class Class1 : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "LeftClick")
        {
            spriteRenderer.color = Color.black;
        }
    }
}

