using UnityEngine;

class FieldTreeLandStick : MonoBehaviour
{   //필드 나무도막
    [SerializeField] int fieldStickItemID;
    int hp;
    string stickName;
    GameObject dropItemPrefab;

    [SerializeField] PlayerInventroy playerInventroy;
    FieldTreeObjectDb FieldTreeObjectDb;
    ItemDB itemDB;
    ItemDB onHandItem;
    void FieldStickSetting() // 오브젝트의 요소들을 생성한다
    {
        itemDB = new ItemDB(fieldStickItemID);
        itemDB.itemSetting();
        stickName = itemDB.name;
        hp = 1;
    }
    private void Awake()
    {
        FieldStickSetting();
        dropItemPrefab = Resources.Load($"Prefabs/FieldItems/{stickName}") as GameObject;
    }
    private void Start()
    {

    }
    private void Update() //플레이어가 특정조건을 만족시켰을때 반응해야한다.
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
            if (onHandItem.toolType == 1)
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