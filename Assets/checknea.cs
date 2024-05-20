using UnityEngine;

public class checknea : MonoBehaviour
{
    Collider2D[] asdf;
    private void Update()
    {
        
        asdf = Physics2D.OverlapCircleAll(transform.position, 0.6f);
        for (int i = 0; i < asdf.Length; i++)
        {
            if (asdf[i] != this.gameObject.GetComponent<Collider2D>())
            {
                asdf[i].GetComponent<SpriteRenderer>().color = Color.black;
            }
        }
    }
}
