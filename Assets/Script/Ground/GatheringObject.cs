using UnityEngine;
using Random = UnityEngine.Random;
class GatheringObject : MonoBehaviour
{
    //1. 곡괭이 등으로 좌클릭시 파괴.
    //2. 폭탄 등 모종의 이유로 체력이 0이 될시 파괴.
    //3. 우클릭시 채집.
    //이외의 기능 없음.

    private int tItemID;
    public int itemID {
        get { return tItemID; } 
        set { 
            tItemID = value;
            GetComponent<SpriteRenderer>().sprite = SpriteManager.Instance.GetSprite(new ItemDB(value).name);
        } 
    }

    private int tCurrentHP;
    public int currentHP
    {
        get { return tCurrentHP; }
        set
        {
            tCurrentHP = value;
            if (tCurrentHP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MakeItem(collision);
        PlayerDestroy(collision);
    }

    private void MakeItem(Collider2D collision)
    {
        if (collision.tag == "RightClick" && collision.GetComponentInParent<PlayerController>() != null)
        {
            PlayerController pCon = collision.GetComponentInParent<PlayerController>();
            PlayerInventroy pInven = collision.GetComponentInParent<PlayerInventroy>();
            pCon.Motion(true);


            //1. pCon.gatherLevel에 따라 normal silver gold iriduim 의 값이 변경.
            //2. random값이 각 숫자보다 큰지 확인하고 등급결정.
            //3. normal ~ iriduim 값은 pCon에서 들고 있을 것.

            int judgeGrade = Random.Range(0, 1000);
            int grade;

            if (judgeGrade >= pCon.gatherGold)//gatherGold = 800. 20%
            {
                grade = 2;
            }
            else if (judgeGrade >= pCon.gatherSilver)//gatherSilver = 500. 30%
            {
                grade = 1;
            }
            else //gatherNone = 0. 50%
            {
                grade = 0;
            }
            pInven.AddDirectItem(itemID, grade, 1); //grade는 pcon의 레벨에 따라 변동확률

            Destroy(this.gameObject);
        }
    }

    private void PlayerDestroy(Collider2D collision)
    {
        if (collision.tag == "LeftClick" && collision.GetComponentInParent<PlayerInventroy>() != null)
        {
            PlayerController pCon = collision.GetComponentInParent<PlayerController>();
            PlayerInventroy pInven = collision.GetComponentInParent<PlayerInventroy>();

            if(pInven.currentItemToolType() == 1 || pInven.currentItemToolType() == 2 || pInven.currentItemToolType() == 4)
            {
                Destroy(this.gameObject);
            }
        }
    }
}