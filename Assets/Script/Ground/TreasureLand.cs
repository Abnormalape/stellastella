using UnityEngine;
using Random = UnityEngine.Random;
class TreasureLand : MonoBehaviour
{   // 보물이 있는가 없는가만 판정.
    SpriteRenderer spriteRenderer;
    ItemDB itemDB;

    bool treasure;

    int currentDate;
    [SerializeField]GameManager gameManager;

    [SerializeField]
    Sprite[] treasureSprite = new Sprite[2]; // 보물 스프라이트 2개
    GameObject treasureObject; // 팠을때 나오는 보물 목록



    private void OnEnable()
    {
        currentDate = gameManager.currentDay;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }
    private void Update() 
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        itemDB = new ItemDB(collision.gameObject.GetComponentInParent<PlayerInventroy>().currentInventoryItem);
        if (itemDB.toolType == 2 && collision.tag == "LeftClick" && treasure) // 부딪힌 놈의 부모는 괭이이면서, 부딪힌 놈은 툴이라면
        {   // 보물이 있는 상태에서 괭이질을 받으면 자신의 스프라이트(보물)을 삭제한다.
            this.spriteRenderer.sprite = null;
        }
    }
    private void MakeTreasure()// 보물 스프라이트 생성 및 보물 상태 확인
    { 
        // 몇 %의 확률로 보물이 생성되고.
        // 그 스프라이트는 2개중 1개이고.
        // 얘가 가질 보물의 종류와 갯수는 때에따라 다르고.
        // 그렇게 생성이 되었다면 - 보물 상태는 참이다.
        treasure = true;
    }

    public void OutSideFarm() // 농장외부에선 보물이 발견된다.
    {
        if (currentDate != gameManager.currentDay && transform.childCount == 0) // 날이 바뀔때, 자식 오브젝트(보물, 경작지)가 없다면
        {
            int judge = Random.Range(0, 100);
            if (judge >= 90) //10% 확률
            {
                Instantiate(gameObject); //TreasureControl을 가진 보물 프리팹을 만든다
            }
            currentDate = gameManager.currentDay;
        }
    }
}