using UnityEngine;


class PlayerFishingRodSearchShoreCollider : MonoBehaviour
{   //낚시용 프리팹: 생성되자마자 점진적으로 범위를 넓혀 주변 물가를 탐색하고 물가와의 거리를 상위개체에 전달하고 자신을 파괴
    Collider2D[] hit;
    int j;
    bool stopit = false;
    private void OnEnable()
    {
        for (int i = 1; i < 10; i++) // 1거리 부터 5거리까지 탐색
        {
            if (!stopit)
            {
                j = i;
                float newsize;
                newsize = Mathf.Sqrt(2) * i - 0.1f;

                hit = Physics2D.OverlapBoxAll(transform.position, new Vector2(newsize, newsize), 45f);
                CheckShore(i);
            }
        }
    }

    
    
    
    private void Update()
    {   
        Destroy(gameObject);
    }



    private void CheckShore(int b)
    {
        for (int i = 0; i<hit.Length; i++)
        {
            if (hit[i].gameObject.layer == LayerMask.NameToLayer("Shore"))
            {
                j = b; // 물가로부터의 거리: 물고기의 등급결정.
                stopit = true;
            }
        }
    }


}
