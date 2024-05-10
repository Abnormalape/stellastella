using System;
using Unity;
using UnityEditor;
using UnityEngine;
using Unity.VisualScripting;
using Unity.Mathematics;
using Unity.Properties;

class FieldStoneObject : MonoBehaviour
{ //필드에 존재하는 나무의 속성을 만든다.
    [SerializeField]
    int fieldStoneObjectID; // 외부에서 입력 받는다 <= 프리팹의 기초가 된다.
    int toolType; // 플레이어의 툴 타입과 같아야 데미지를 입는다.
    int toolLevel; // 플레이어의 툴 레벨보다 높으면 데미지가 없다.
    int hp; //체력
    string stoneName; // 이름 ex)돌, 단단한 돌
    GameObject[] dropItemPrefab; //드랍아이템의 프리팹

    [SerializeField]
    PlayerInventroy playerInventroy;
    FieldStoneObjectDB fieldStoneObjectDB; // 돌 오브젝트
    ItemDB[] itemDB; // 아이템 종류를 받는다. 몇개를 받을지는 모른다.
    ItemDB onHandItem; // 플레이어가 들고 있는 아이템
    SpriteRenderer stoneSprite;
    void FieldStoneSetting() // 오브젝트의 요소들을 생성한다
    {
        fieldStoneObjectDB = new FieldStoneObjectDB(fieldStoneObjectID); // ID에 따라서 툴타입, 툴레벨, hp, 이름 이 정해진다.
        fieldStoneObjectDB.FieldStoneObjectSetting();
        this.toolType = fieldStoneObjectDB.toolType;
        this.toolLevel = fieldStoneObjectDB.toolLevel;
        this.hp = fieldStoneObjectDB.hp;
        this.stoneName = fieldStoneObjectDB.stoneName;
        this.itemDB = new ItemDB[fieldStoneObjectDB .items]; // 아이템DB클래스에 아이템ID를 넘겨준다. 아이템DB는 받은 ID에 맞게 아이템 데이터를 저장한다.
        for (int i = 0; i < fieldStoneObjectDB.items; i++)
        {
            itemDB[i] = new ItemDB(fieldStoneObjectDB.itemID[i]); // 첫번째 아이템은 itemDB에 트리오브젝트의 첫번째 아이템ID를 받은 값
            itemDB[i].itemSetting();
        }
    }
    private void Awake()
    {
        FieldStoneSetting();
        stoneSprite = this.GetComponent<SpriteRenderer>();
        dropItemPrefab = new GameObject[fieldStoneObjectDB.items]; //DB에서 가짓수를 불러와 GameObject를 생성한다
        for (int i = 0; i < fieldStoneObjectDB.items; i++)
        {
            dropItemPrefab[i] = Resources.Load($"Prefabs/{itemDB[i].name}") as GameObject; //
        }
    }
    private void Start()
    {
        
    }
    private void Update() //플레이어가 특정조건을 만족시켰을때 반응해야한다.
    {
        dropItem();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInventroy = collision.GetComponentInParent<PlayerInventroy>();

        onHandItem = new ItemDB(playerInventroy.currentInventoryItem);
        onHandItem.itemSetting();
        if (onHandItem.toolType == this.toolType)
        {
            if (onHandItem.grade >= this.toolLevel)
            {
                this.hp -= onHandItem.hpRestore;
            }
            else if (onHandItem.grade < this.toolLevel)
            {
                Debug.Log("도구가 충분히 강하지 않은 것 같다.");
            }
        }
    }

    
    private void dropItem()
    {
        if (hp <= 0)
        {
            for (int i = 0; i < fieldStoneObjectDB.items; i++)  // prefab[0] [1]을 생성한다.
            {
                for (int j = 0; j < fieldStoneObjectDB.dropnumber[i]; j++)
                { // prefab[0]을 dropnumber[0]개 만큼 생성한다.
                    Instantiate(dropItemPrefab[i], this.transform.position, quaternion.identity);
                }
            }
            Destroy(this.gameObject);
        }
    }

}