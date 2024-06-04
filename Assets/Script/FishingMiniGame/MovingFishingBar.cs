using UnityEngine;
class MovingFishingBar : MonoBehaviour
{   //얘는 2D물리 적용되게 할거라 큰 신경 X.
    Rigidbody2D rb;
    [SerializeField] float speed = 10f;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            this.rb.velocity += Vector2.up * speed * Time.unscaledDeltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {   //접촉중이라면.
        if (collision.gameObject.GetComponent<MovingFish>() != null)
        {
            transform.parent.GetComponentInChildren<FishCatch>().IsCatching();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MovingFish>() != null)
        {
            transform.parent.GetComponentInChildren<FishCatch>().NotCatching();
        }
    }
}

