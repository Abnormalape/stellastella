using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventroy : MonoBehaviour
{
    Inventory[] pInventory;
    public int currentInventoryItem; // 현재인벤에 들어있는 아이템ID
    int currentInventory; // 현재인벤 번호
    int itemCount;
    ItemDB currentItemDB;
    void MakePlayerInventory()
    {
        pInventory = new Inventory[12];// 인벤토리는 12칸
        pInventory[0] = new Inventory(4); // 아이템ID 4 = 도끼
        pInventory[1] = new Inventory(5); // 아이템ID 5 = 괭이
        pInventory[2] = new Inventory(6); // 아이템ID 6 = 물뿌리개
        pInventory[3] = new Inventory(7); // 아이템ID 7 = 곡괭이
        pInventory[4] = new Inventory(8); // 아이템ID 8 = 낫
        pInventory[5] = new Inventory(9); // 아이템ID 9 = 강화도끼
        pInventory[6] = new Inventory(10); // 아이템ID 10 = 강화곡괭이
        pInventory[7] = new Inventory(20); // 아이템ID 20 = 낚싯대, 툴 ID=9
    }
    
    void Start()
    {
        MakePlayerInventory(); // 플레이어 인벤토리를 생성한다
        currentInventory = 0;
        currentInventoryItem = pInventory[currentInventory].itemID; // 현재인벤토리가 선택한 물건은 currentInventory번인벤토리가 가진 아이템ID이다.
    }
    void Update()
    {
        currentInventoryItem = pInventory[currentInventory].itemID;
        currentItemDB = new ItemDB(currentInventoryItem); // 현재고른 아이템의 ID를 이용해 정보를 뽑아옴

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
    }

    void addItem(int itemID)  // 필드에서 접촉한 아이템을 인벤토리에 넣는법
                    // 아이템은 자신이 접촉한 오브젝트(player gathering)의 부모오브젝트(player) 를 향해 다가가며, 그 거리가 일정 수준이 되었을때 소멸하며 Playerinventory에 addItem을 작동시킨다.
    {
        // for문을 돌려서 pInventory[]에 있는 itemID와 비교를 한다
        // 그래서 pInventory[]가 가지고 있는 itemID와 일치는 한다면, 그 pInventory의 itemCount를 1증가 시킨다.
        // 그리고 탈출한다

        // 만약 for문을 실행하는 동안 어디에서도 찾아지지 않는다면
        // for문을 새로 돌려 null인 인벤토리에 itemID를 넣고 itemCount를 1증가시킨다.
    }
    void addItem(int itemID, int itemGrade)  // 아이템을 인벤토리에 넣는법
                              // 아이템은 자신이 접촉한 오브젝트(player gathering)의 부모오브젝트(player) 를 향해 다가가며, 그 거리가 일정 수준이 되었을때 소멸하며 Playerinventory에 addItem을 작동시킨다.
    {
        // 위와 같으나, itemID 와 itemGrade가 같은 애들을 찾는다.
    }
}