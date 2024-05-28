using System;
using UnityEngine;
class MovingFishingBar : MonoBehaviour
{   //얘는 2D물리 적용되게 할거라 큰 신경 X.
    Rigidbody2D rb;
    float speed;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            this.rb.velocity += new Vector2(0,1) * speed * Time.unscaledDeltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {   //부딪힌 녀석이 물고기라면.
        if (collision.gameObject.GetComponent<MovingFish>() != null)
        {
            transform.GetComponentInParent<FishCatch>().catched += 10 * Time.unscaledDeltaTime;
        }
        else
        {
            transform.GetComponentInParent<FishCatch>().catched -= 10 * Time.unscaledDeltaTime;
        }
    }
}

