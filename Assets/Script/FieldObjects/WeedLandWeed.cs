using UnityEngine;

class WeedLandWeed : MonoBehaviour
{   //필드 나무도막
    [SerializeField] int WeedItemID;
    int hp;
    string weedName;
    GameObject dropItemPrefab;

    [SerializeField] PlayerInventroy playerInventroy;
    ItemDB itemDB;
    ItemDB onHandItem;
    void FieldStickSetting() // 오브젝트의 요소들을 생성한다
    {
        itemDB = new ItemDB(WeedItemID);
        itemDB.itemSetting();
        weedName = itemDB.name;
        hp = 1;
    }
    private void Awake()
    {
        FieldStickSetting();
        dropItemPrefab = Resources.Load($"Prefabs/FieldItems/{weedName}") as GameObject;
    }
    private void Start()
    {

    }
    private void Update() 
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LeftClick" && collision.transform.parent.tag == "Player")
        {
            playerInventroy = collision.GetComponentInParent<PlayerInventroy>();
            onHandItem = new ItemDB(playerInventroy.currentInventoryItem);
            onHandItem.itemSetting();
            //도끼 일때
            if (onHandItem.toolType == 1 && onHandItem.toolType == 2 && onHandItem.toolType == 4 && onHandItem.toolType == 5)
            {
                dropItem();
            }
        }
    }


    private void dropItem()
    {
        Instantiate(dropItemPrefab, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}