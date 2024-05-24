using UnityEngine;

class FieldTreeLandStick : MonoBehaviour
{   //�ʵ� ��������
    [SerializeField] int fieldStickItemID;
    int hp;
    string stickName;
    GameObject dropItemPrefab;

    [SerializeField] PlayerInventroy playerInventroy;
    FieldTreeObjectDb FieldTreeObjectDb;
    ItemDB itemDB;
    ItemDB onHandItem;
    void FieldStickSetting() // ������Ʈ�� ��ҵ��� �����Ѵ�
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
    private void Update() //�÷��̾ Ư�������� ������������ �����ؾ��Ѵ�.
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LeftClick" && collision.transform.parent.tag == "Player")
        {
            playerInventroy = collision.GetComponentInParent<PlayerInventroy>();
            onHandItem = new ItemDB(playerInventroy.currentInventoryItem);
            onHandItem.itemSetting();
            //���� �϶�
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