using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventroy : MonoBehaviour // 플레이어에게 부착된다
{
    Inventory[] pInventory = new Inventory[36]; // 36칸의 인벤토리
    public int[] pInventoryCount = new int[36];
    ItemDB currentItemDB; // 아이템 정보 호출용
    [SerializeField] public int currentInventory; // 현재인벤
    [SerializeField] public int currentInventoryItem; // 현재인벤의 아이템ID
    [SerializeField] public int itemCount; // 현재 인벤의 아이템 수

    PlayerLeftClick PLClick;

    public int changeCount = 0;

    bool firstbag = false;
    bool secondbag = false;

    void MakePlayerInventory() // 시작할때 주는 도구 = 1회성
    {
        for (int i = 8; i < 36; i++)
        {
            pInventory[i] = new Inventory(0);
        }

        pInventory[0] = new Inventory(4); // 아이템ID 4 = 도끼
        pInventory[1] = new Inventory(5); // 아이템ID 5 = 괭이
        pInventory[2] = new Inventory(6); // 아이템ID 6 = 물뿌리개
        pInventory[3] = new Inventory(7); // 아이템ID 7 = 곡괭이
        pInventory[4] = new Inventory(8); // 아이템ID 8 = 낫
        pInventory[5] = new Inventory(9); // 아이템ID 9 = 강화도끼
        pInventory[6] = new Inventory(10); // 아이템ID 10 = 강화곡괭이
        pInventory[7] = new Inventory(20); // 아이템ID 20 = 낚싯대, 툴 ID=9
        pInventory[8] = new Inventory(14); // 봄작물1 씨앗 ,10개.
        for (int i = 0; i < 8; i++) { pInventory[i].itemCount = 1; }
        pInventory[8].itemCount = 10;
    }

    private void Awake()
    {
        PLClick = this.GetComponent<PlayerLeftClick>();
        MakePlayerInventory(); // 기본아이템을 생성한다
    }
    void Start()
    {
        currentInventory = 0;
        //현재 아이템 = 현재 인벤토리의 아이템ID
        currentInventoryItem = pInventory[currentInventory].itemID;
    }
    void Update()
    {
        ChangeInventory(); // 이것을 바탕으로 플레이어와 오브젝트가 상호작용
        InventoryItemData();

        if (changeCount != 0) // 갯수변화가 0이 아니라면
        {
            pInventory[currentInventory].itemCount += changeCount;
            changeCount = 0;
        }
    }
    void ChangeInventory() // 인벤토리를 바꾸고 아이템을 선택, 도구 사용중일때는 예외
    {
        if (!PLClick.toolUsed)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) { currentInventory = 0; } // 만약 1번키를 누른다면 플레이어인벤[0]의 아이템ID가 현재 아이템이다. 그걸 1234567890-=로 반복한다.
            if (Input.GetKeyDown(KeyCode.Alpha2)) { currentInventory = 1; }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { currentInventory = 2; }
            if (Input.GetKeyDown(KeyCode.Alpha4)) { currentInventory = 3; }
            if (Input.GetKeyDown(KeyCode.Alpha5)) { currentInventory = 4; }
            if (Input.GetKeyDown(KeyCode.Alpha6)) { currentInventory = 5; }
            if (Input.GetKeyDown(KeyCode.Alpha7)) { currentInventory = 6; }
            if (Input.GetKeyDown(KeyCode.Alpha8)) { currentInventory = 7; }
            if (Input.GetKeyDown(KeyCode.Alpha9)) { currentInventory = 8; }
            if (Input.GetKeyDown(KeyCode.Alpha0)) { currentInventory = 9; }
            if (Input.GetKeyDown(KeyCode.Minus)) { currentInventory = 10; }
            if (Input.GetKeyDown(KeyCode.Equals)) { currentInventory = 11; }

            float wheelscroll = Input.GetAxis("Mouse ScrollWheel");
            if (wheelscroll > 0)
            {
                currentInventory = currentInventory - 1;
                if (currentInventory == -1) { currentInventory = 11; }
            } //밀때
            else if (wheelscroll < 0)
            {
                currentInventory = currentInventory + 1;
                if (currentInventory == 12) { currentInventory = 0; }
            } //당길때

            currentInventoryItem = pInventory[currentInventory].itemID; // 아이템ID 호출
            currentItemDB = new ItemDB(currentInventoryItem); // ID를 기반으로 정보 호출
            itemCount = pInventory[currentInventory].itemCount;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FieldItem") // 충돌체가 아이템 이라면
        {
            for (int i = 0; i < 36; i++) // 인벤토리를 훑어서
            {
                if (collision.gameObject.GetComponent<FieldItem>().itemID == pInventory[i].itemID && collision.gameObject.GetComponent<FieldItem>().grade == pInventory[i].grade) // 같은 ID를 보유중인 인벤토리에
                {
                    pInventory[i].itemCount += 1; // 카운트를 올리고
                    Destroy(collision.gameObject); // 그놈을 제거하고
                    return; // 메서드 종료
                }
            }
            for (int i = 0; i < 36; i++) // 일치가 하나도 안된다면
            {
                if (pInventory[i].itemID == 0) // 인벤토리의 아이템 아이디가 비어있는곳을 찾아 (개인적으론 null쓰고싶긴한데)
                {
                    if (firstbag == false && i >= 12)
                    {
                        return;
                    }
                    else if (secondbag == false && i >= 24)
                    {
                        return;
                    }
                    pInventory[i].itemID = collision.gameObject.GetComponent<FieldItem>().itemID; // 그 인벤토리의 아이템ID를 충돌체의 ID로 바꾸고
                    pInventory[i].grade = collision.gameObject.GetComponent<FieldItem>().grade; // 그 인벤토리의 아이템 등급을 충돌체의 등급으로 바꾸고
                    pInventory[i].itemCount += 1; // 카운트를 올린다 (0이었으니까)
                    Destroy(collision.gameObject);
                    return; // 메서드 종료   
                }
            }
            return; // 그냥 종료
        }
    }

    void InventoryItemData()
    {
        for (int i = 0; i < 36; i++)
        {
            if (pInventory[i].itemCount <= 0)
            {
                pInventory[i].itemID = 0;
            }

            pInventoryCount[i] = pInventory[i].itemID;
        }
    }
}